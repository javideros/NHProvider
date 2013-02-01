using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Hosting;
using System.Web.Security;
using NHibernate;
using NHibernate.Type;
using System.Collections.Generic;
using nhprovider.model;
using nhprovider.service.contracts;
using NHProvider.util;
using NHProvider.Properties;
using System.Diagnostics;
using Spring.Context;
using Spring.Context.Support;
using System.Linq;

namespace NHProvider
{
    /// <summary>
    /// Provides role services using NHibernate as the persistence mechanism.
    /// </summary>
    public sealed class NHibernateRoleProvider : RoleProvider
    {
        #region Fields
        private string eventSource = "HNibernateMembershipProvider";
        private string eventLog = "Application";

        private bool pWriteExceptionsToEventLog = false;

        private AspnetApplication application;

        private IAspnetRoleService _aspnetRoleService;
        private IAspnetMembershipService _aspnetMembershipService;
        private IAspnetApplicationService _aspnetApplicationService;
        #endregion Fields

        //#region Properties
        /// <summary>
        /// The name of the application using the custom membership provider.
        /// </summary>
        /// <returns>
        /// The name of the application using the custom membership provider.
        /// </returns>
        public override string ApplicationName
        {
            get { return application.ApplicationName; }
            set { application.ApplicationName = value; }
        }

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }
        //public IApplicationService ApplicationService
        //{
        //    set { _applicationService = value; }
        //}

        public IAspnetRoleService AspnetRoleService
        {
            set { _aspnetRoleService = value; }
        }

        public IAspnetMembershipService AspnetMembershipService
        {
            set { _aspnetMembershipService = value; }
        }

        public IAspnetApplicationService AspnetApplicationService
        {
            set { _aspnetApplicationService = value; }
        }
       //#endregion Properties

        #region Initialization
        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific
        /// attributes specified in the configuration for this provider.</param>
        /// <param name="name">The friendly name of the provider.</param>
        /// <exception cref="ArgumentNullException">The name of the provider is null.</exception>
        /// <exception cref="InvalidOperationException">An attempt is made to call <see cref="Initialize(System.String,System.Collections.Specialized.NameValueCollection)"></see> on a provider after the provider has already been initialized.</exception>
        /// <exception cref="ArgumentException">The name of the provider has a length of zero.</exception>
        public override void Initialize(string name, NameValueCollection config)
        {
            // Initialize values from Web.config.
            if (null == config)
            {
                throw (new ArgumentNullException("config"));
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "NHibernateRoleProvider";
            }
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "NHibernate Role Provider");
            }

            // Call the base class implementation.
            base.Initialize(name, config);


            if (_aspnetMembershipService == null)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                _aspnetMembershipService = (IAspnetMembershipService)context.GetObject("IAspnetMembershipService");
            }

            if (_aspnetApplicationService == null)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                _aspnetApplicationService = (IAspnetApplicationService)context.GetObject("IAspnetApplicationService");
            }

            if (_aspnetRoleService == null)
            {
                IApplicationContext context = ContextRegistry.GetContext();
                _aspnetRoleService = (IAspnetRoleService)context.GetObject("IAspnetRoleService");
            }

            string appName = ConfigurationUtil.GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            application =
             _aspnetApplicationService.CreateOrLoadApplication(appName);

            if (config["writeExceptionsToEventLog"] != null)
            {
                if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE")
                {
                    pWriteExceptionsToEventLog = true;
                }
            }


            // Load configuration data.
            //application = _applicationService.CreateOrLoadApplication(appName);
            
            
            //application =
            //    NHibernateProviderEntityHelper.CreateOrLoadApplication(
            //        ConfigurationUtil.GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath));

        }
        #endregion Initialization

        //#region Operations
        /// <summary>
        /// Adds a new role to the data source for the configured application name.
        /// </summary>
        /// <param name="roleName">the name of the role to create.</param>
        public override void CreateRole(string roleName)
        {
            if (roleName.Contains(","))
            {
                throw new ArgumentException("Role names cannot contain commas.");
            }

            // Make sure we are not attempting to insert an existing role.
            if (RoleExists(roleName))
            {
                throw ExceptionUtil.NewProviderException(this, Resources.Role_AlreadyExists);
            }

            try
            {
                AspnetRole role = new AspnetRole();
                role.RoleName = roleName;
                role.LoweredRoleName = roleName.ToLowerInvariant();
                role.AspnetApplication = application;
                _aspnetRoleService.SaveAspnetRole(role);
            }
            catch (Exception ex)
            {
                throw ExceptionUtil.NewProviderException(this, Resources.Role_UnableToCreate, ex);
            }
        }
        /// <summary>
        /// Removes a role from the data source for the configured application name.
        /// </summary>
        /// <param name="roleName">the name of the role to delete.</param>
        /// <param name="throwOnPopulatedRole">if <c>true</c>, throw an exception if <c>roleName</c> has one or more
        /// members and do not delete <c>roleName</c>.</param>
        /// <returns><c>true</c> if the role was successfully deleted; otherwise, <c>false</c>.</returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (!RoleExists(roleName))
            {
                throw ExceptionUtil.NewProviderException(this,("Role does not exist."));
            }
            // Assume we are unable to perform the operation.
            bool result = false;

            // Check to see if we need to throw an exception if roles have been linked to other objects.
            if (throwOnPopulatedRole && (0 < GetUsersInRole(roleName).Length))
            {
                // Indicate the role is not empty and cannot be removed.
                throw ExceptionUtil.NewProviderException(this, "role is not empty.");
            }

            // Remove role information from the data store.
            try
            {
                // Get the role information.
                AspnetRole role = _aspnetRoleService.GetAspnetRole(roleName);
                if (null != role)
                {
                    // Delete the references to applications/roles.
                    IEnumerable<AspnetRole> resultRoles = application.AspnetRoles.ToList().AsQueryable();
                    IList<AspnetRole> removeRole = new List<AspnetRole>();
                    removeRole.Add(role);
                    resultRoles = resultRoles.Except<AspnetRole>(removeRole);
                    application.AspnetRoles = resultRoles.ToList<AspnetRole>();
                    _aspnetApplicationService.UpdateApplication(application);
                                        
                    //_aspnetRoleService.RemoveAppRoleReferences(application.ApplicationId, role.RoleId);
                    // Delete the role record.
                    _aspnetRoleService.DeleteRole(role);
                    // Indicate no errors occured.
                    result = true;
                }
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "DeleteRole");

                    return false;
                }
                else
                {
                    throw ExceptionUtil.NewProviderException(this, Resources.Role_UnableToDelete, ex);
                }
                
            }

            // Return the result of the operation.
            return result;
        }
        /// <summary>
        /// Gets a value indicating whether the specified role name already exists in the data store
        /// for the configured application name.
        /// </summary>
        /// <param name="roleName">the name of the role to search for.</param>
        /// <returns>
        /// <c>true</c> if the role name already exists in the data store for the configured application name;
        /// otherwise, <c>false</c>.
        ///</returns>
        public override bool RoleExists(string roleName)
        {
            // Assume the role does not exist.
            bool exists = false;

            // Check against the data store if the role exists.
            try
            {
                IList<AspnetRole> roles = _aspnetRoleService.GetAspnetRoles(roleName.ToLower(), application.ApplicationId);
                exists = (0 < roles.Count);
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "RoleExists");
                }
                else
                {
                    throw ExceptionUtil.NewProviderException(this, Resources.Role_UnableToCheckIfExists, ex);
                }
                
            }

            // Return the result of the operation.
            return exists;
        }
        /// <summary>
        /// gets a value indicating whether the specified user is in the specified role for the configured application name.
        /// </summary>
        /// <param name="username">the name of the user to search for.</param>
        /// <param name="rolename">the name of the role to search in.</param>
        /// <returns>
        /// <c>true</c> if the specified user is in the specified role for the configured application name;
        /// otherwise, <c>false</c>.
        /// </returns>
        public override bool IsUserInRole(string username, string rolename)
        {
            // assume the given role is not associated to the given user.
            bool isinrole = false;

            //// check against the data store if the role has been assigned to the given user.
            try
            {
                if (_aspnetMembershipService == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    _aspnetMembershipService = (IAspnetMembershipService)context.GetObject("IAspnetMembershipService");
                }

                if (_aspnetRoleService == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    _aspnetRoleService = (IAspnetRoleService)context.GetObject("IAspnetRoleshipService");
                }

                AspnetMembership user = _aspnetMembershipService.GetMembership(username);
                AspnetRole role = _aspnetRoleService.GetAspnetRole(rolename);
                if ((null != user) && (null != role))
                {
                    //TODO wint linq :int appuserrole = _aspnetRoleService.IsUserInrole(application.ApplicationId, user.UserId, role.RoleId);
                    int appuserrole = user.AspnetRoles.Count(n => n.RoleId.Equals(role.RoleId));
                    isinrole = (0 < appuserrole);
                }
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "IsUserInRole");
                }
                else
                {
                    throw ExceptionUtil.NewProviderException(this, Resources.Role_UnableToFindUserInRole, ex);
                }
                
            }

            // return the result of the operation.
            return isinrole;
        }

        /// <summary>
        /// Gets a list of the roles that a specified user is in for the configured application name.
        /// </summary>
        /// <param name="username">the name of the user for whom to return a list of roles.</param>
        /// <returns>
        /// A string array containing the names of all the roles that the specified user is in for
        /// the configured application name.
        /// </returns>
        public override string[] GetRolesForUser(string username)
        {
            // Prepare a placeholder for the roles.
            string[] roleNames = new string[0];

            // Load the list from the data store.
            try
            {
                if (_aspnetMembershipService == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    _aspnetMembershipService = (IAspnetMembershipService)context.GetObject("IAspnetMembershipService");
                }

                AspnetMembership user = _aspnetMembershipService.GetMembership(username);
                if (user != null)
                {
                    IList<AspnetRole> userRoles = user.AspnetRoles;
                    if ((user.AspnetRoles != null) | (user.AspnetRoles.Count > 0))
                    {
                        IList<string> roles = (from n in user.AspnetRoles select n.RoleName).ToList<string>();
                        //TODO with Linq  = _aspnetUsersInRoleService.GetRolesNamesForUser(application.ApplicationId, user.UserId);
                        if (null != roles)
                        {
                            roleNames = new string[roles.Count];
                            for (int i = 0; i < roles.Count; i++)
                            {
                                roleNames[i] = roles[i].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog)
                {
                  WriteToEventLog(ex, "GetRolesForUser");
                }
                else
                {
                  throw ExceptionUtil.NewProviderException(this, Resources.Role_UnableToGetRolesForUser, ex);
                }
                
            }

            // Return the result of the operation.
            return roleNames;
        }
        ///// <summary>
        ///// Adds the specified user names to the specified roles for the configured application name.
        ///// </summary>
        ///// <param name="roleNames">A string array of the role names to add the specified user names to. </param>
        ///// <param name="userNames">A string array of user names to be added to the specified roles. </param>
        public override void AddUsersToRoles(string[] userNames, string[] roleNames)
        {
            foreach (string rolename in roleNames)
            {
                if (!RoleExists(rolename))
                {
                    throw ExceptionUtil.NewProviderException(this, "Role name not found.");
                }
            }

            foreach (string username in userNames)
            {
                if (username.Contains(","))
                {
                    throw new ArgumentException("User names cannot contain commas.");
                }

                foreach (string rolename in roleNames)
                {
                    if (IsUserInRole(username, rolename))
                    {
                        throw ExceptionUtil.NewProviderException(this, "User is already in role.");
                    }
                }
            }
            // Add the users to the given roles.
            try
            {
                // For every user in the given list attempt to add to the given roles.
                foreach (string userName in userNames)
                {
                    // Assume that the given user name will be found. If any is not found this call will fail.
                    AspnetMembership user = _aspnetMembershipService.GetAspnetMembership(userName, application);
                    // Each role must be added from the user being currently processed. The assumption is that
                    // the same list of roles will apply to all given users.
                    foreach (string roleName in roleNames)
                    {
                        // Assume that the given user name will be found. If any is not found this call will fail.
                    AspnetRole role = _aspnetRoleService.GetAspnetRole(roleName);
                    if (user.AspnetRoles == null)
                        user.AspnetRoles = new List<AspnetRole>();

                    user.AspnetRoles.Add(role);
                    _aspnetMembershipService.UpdateMembership(user);
                        //TODO with linq
                    //AspnetUsersInRole uir = new AspnetUsersInRole();
                    //uir.Role = role;
                    //uir.UserRoleIdentifier.RoleId = role.RoleId;
                    //uir.User = user;
                    //uir.UserRoleIdentifier.UserId = user.UserId;

                    //// NOTE: To ensure this relationship is stored we must use Save and not SaveOrUpdate.
                    //_aspnetUsersInRoleService.SaveAspnetUsersInRole(uir);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ExceptionUtil.NewProviderException(this, Resources.Role_UnableToAddUsersToRoles, ex);
            }
        }
        /// <summary>
        /// Removes the specified user names from the specified roles for the configured application name.
        /// </summary>
        /// <param name="userNames">string array of user names to be removed from the specified roles.</param>
        /// <param name="roleNames">string array of role names from which to remove the specified user names.</param>
        public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
        {
            foreach (string rolename in roleNames)
            {
                if (!RoleExists(rolename))
                {
                    throw ExceptionUtil.NewProviderException(this, "Role name not found.");
                }
            }

            foreach (string username in userNames)
            {
                foreach (string rolename in roleNames)
                {
                    if (!IsUserInRole(username, rolename))
                    {
                        throw ExceptionUtil.NewProviderException(this, "User is not in role.");
                    }
                }
            }

            // Remove the users from the given roles.
            try
            {
                // For every user in the given list attempt to remove the given roles.
                foreach (string userName in userNames)
                {
                    // Assume that the given user name will be found. If any is not found this call will fail.
                    AspnetMembership user = _aspnetMembershipService.GetMembership(userName);
                    // Each role must be attempted to be removed from the user being currently processed. If no
                    // association is found, ignore it.
                    foreach (string roleName in roleNames)
                    {
                        // Assume that the given user name will be found. If any is not found this call will fail.
                        AspnetRole role = _aspnetRoleService.GetAspnetRole(roleName);
                        // Execute the delete operation.

                       //TODO with Linq _aspnetUsersInRoleService.RemoveUserFromRole(application.ApplicationId, user.UserId, role.RoleId); //.DeleteByNamedQuery("ApplicationUserRole.RemoveUserFromRole", values, types);
                        IEnumerable<AspnetUser> resultUsers = role.AspnetUsers.ToList().AsQueryable();
                        IList<AspnetUser> removeUser = new List<AspnetUser>();
                        removeUser.Add(user);
                        resultUsers = resultUsers.Except<AspnetUser>(removeUser);
                        //application.AspnetRoles = resultRoles.ToList<AspnetRole>();
                        _aspnetRoleService.UpdateAspnetRole(role);

                        IList<AspnetRole> roles = user.AspnetRoles.ToList<AspnetRole>();
                        IList <AspnetRole> toDeleteRoles = new List<AspnetRole>();
                        toDeleteRoles.Add(role);
                        IList<AspnetRole> finalRoles = roles.Except<AspnetRole>(toDeleteRoles).ToList<AspnetRole>();
                        user.AspnetRoles = finalRoles;
                        _aspnetMembershipService.UpdateMembership(user);
                    }
                }
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "RemoveUsersFromRoles");
                }
                else
                {
                    throw ExceptionUtil.NewProviderException(this, Resources.Role_UnableToRemoveUsersFromRoles, ex);
                }
                
            }
        }
        /// <summary>
        /// Gets a list of users in the specified role for the configured application name.
        /// </summary>
        /// <param name="roleName">the name of the role for which to get the list of users.</param>
        /// <returns>
        /// A string array containing the names of all the users who are members of the specified role
        /// for the configured application name.
        /// </returns>
        public override string[] GetUsersInRole(string roleName)
        {
            // Prepare a placeholder for the roles.
            string[] userNames = new string[0];

            // Load the list from the data store.
            try
            {
                AspnetRole role = _aspnetRoleService.GetAspnetRole(roleName);

                IList<string> users = (from user in role.AspnetUsers select user.UserName).ToList<string>(); 
                // TODO with Linq = _aspnetUsersInRoleService.GetUserNamesInRole(application.ApplicationId, role.RoleId); //FindByNamedQuery<string>("ApplicationUserRole.GetUserNamesInRole", values, types);
                if (null != users)
                {
                    userNames = new string[users.Count];
                    for (int i = 0; i < users.Count; i++)
                    {
                        userNames[i] = users[i].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "GetUsersInRole");
                }
                else
                {
                    throw ExceptionUtil.NewProviderException(this, Resources.Role_UnableToGetUsersInRole, ex);
                }
            }

            // Return the result of the operation.
            return userNames;
        }
        /// <summary>
        /// Gets a list of all the roles for the configured application name.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles stored in the data store for the configured application name.
        /// </returns>
        public override string[] GetAllRoles()
        {
            // Prepare a placeholder for the roles.
            string[] roleNames = new string[0];

            // Load the list of roles for the configured application name.
            try
            {
                if (_aspnetRoleService == null)
                {
                    IApplicationContext context = ContextRegistry.GetContext();
                    _aspnetRoleService = (IAspnetRoleService)context.GetObject("IAspnetRoleService");
                }

                IList<String> list = _aspnetRoleService.GetRoleNames(application);
                if (0 < list.Count)
                {
                    roleNames = new string[list.Count];
                    int i = 0;
                    foreach (string roleName in list)
                    {
                        roleNames[i++] = roleName;
                    }
                }
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "GetAllRoles");
                }
                else
                {
                    throw ExceptionUtil.NewProviderException(this, Resources.Role_UnableToGetAllRoles, ex);
                }
                
            }

            // Return the result of the operation.
            return roleNames;
        }
        /// <summary>
        /// Gets an array of user names in a role where the user name contains the specified user name to match.
        /// </summary>
        /// <param name="roleName">the name of the role to search in.</param>
        /// <param name="usernameToMatch">the user name to search for.</param>
        /// <returns>
        /// A string array containing the names of all the users where the user name matches <c>usernameToMatch</c>
        /// and the user is a member of the specified role.
        /// </returns>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            // Prepare a placeholder for the users.
            string[] userNames = new string[0];

            // Load the list of users for the given role name.
            try
            {
                // Replace all * and ? wildcards for % and _, respectively.
                usernameToMatch = usernameToMatch.Replace('*', '%');
                usernameToMatch = usernameToMatch.Replace('?', '_');

                // Perform the search.
                AspnetRole role = _aspnetRoleService.GetAspnetRole(roleName);
                IList<String> users = (from user in role.AspnetUsers where System.Data.Linq.SqlClient.SqlMethods.Like(user.UserName, usernameToMatch) select user.UserName).ToList<string>(); //role.AspnetUsers.Select (n => n.UserName Where (n like usernameToMatch));
                //// TODO with Linq _aspnetUsersInRoleService.FindUserNamesInRoleByUserLoweredName(application.ApplicationId, role.RoleId, usernameToMatch.ToLower()); //_userService.FindByNamedQuery<String>("ApplicationUserRole.FindUserNamesInRoleByUserLoweredName", values, types);
                if (null != users)
                {
                    userNames = new string[users.Count];
                    for (int i = 0; i < users.Count; i++)
                    {
                        userNames[i] = users[i].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "FindUsersInRole");
                }
                else
                {
                    throw ExceptionUtil.NewProviderException(this, Resources.Role_UnableToFindUsersInRole, ex);
                }
            }

            // Return the result of the operation.
            return userNames;
        }
        //#endregion Operations

        // 
        // WriteToEventLog 
        //   A helper function that writes exception detail to the event log. Exceptions 
        // are written to the event log as a security measure to avoid private database 
        // details from being returned to the browser. If a method does not return a status 
        // or boolean indicating the action succeeded or failed, a generic exception is also  
        // thrown by the caller. 
        // 

        private void WriteToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = "An exception occurred communicating with the data source.\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }
    }
}