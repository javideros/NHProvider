using NHibernate.Validator.Constraints;
using System.Collections.Generic;

namespace nhprovider.model
{
    public partial class AspnetApplication
    {
        #region Fields

        private string _ApplicationName;

        private string _LoweredApplicationName;

        private System.Guid _ApplicationId;

        private string _Description;

        private IList<AspnetMembership> _aspnetMemberships;

        private IList<AspnetPath> _aspnetPaths;

       private IList<AspnetRole> _aspnetRoles;

       private IList<AspnetUser> _aspnetUsers;

        #endregion Fields

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion

        #region Initialization

        public AspnetApplication()
        {
            //this._aspnetMemberships = new List<AspnetMembership>();
            //this._aspnetPaths = new List<AspnetPath>();
            //this._aspnetRoles = new List<AspnetRole>();
            //this._aspnetUsers = new List<AspnetUser>();
            OnCreated();
        }
        #endregion Initialization

        #region Properties

        /// <summary>
        /// There are no comments for ApplicationName in the schema.
        /// </summary>
        [NotNull]
        [Length(Max = 256)]
        public virtual string ApplicationName
        {
            get
            {
                return this._ApplicationName;
            }
            set
            {
                this._ApplicationName = value;
            }
        }


        /// <summary>
        /// There are no comments for LoweredApplicationName in the schema.
        /// </summary>
        [NotNull]
        [Length(Max = 256)]
        public virtual string LoweredApplicationName
        {
            get
            {
                return this._LoweredApplicationName;
            }
            set
            {
                this._LoweredApplicationName = value;
            }
        }


        /// <summary>
        /// There are no comments for ApplicationId in the schema.
        /// </summary>
        public virtual System.Guid ApplicationId
        {
            get
            {
                return this._ApplicationId;
            }
            set
            {
                this._ApplicationId = value;
            }
        }


        /// <summary>
        /// There are no comments for Description in the schema.
        /// </summary>
        [Length(Max = 256)]
        public virtual string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }

        

        public virtual IList<AspnetMembership> AspnetMemberships
        {
            get { return _aspnetMemberships; }
            set { _aspnetMemberships = value; }
        }

        public virtual IList<AspnetRole> AspnetRoles
        {
            get { return _aspnetRoles; }
            set { _aspnetRoles = value; }
        }

        public virtual IList<AspnetUser> AspnetUsers
        {
            get { return _aspnetUsers; }
            set { _aspnetUsers = value; }
        }

        public virtual IList<AspnetPath> AspnetPaths
        {
            get { return _aspnetPaths; }
            set { _aspnetPaths = value; }
        }
        #endregion Properties

    }
}
