using System.Collections;
using NHibernate.Validator.Constraints;
using System.Collections.Generic;
using System;

namespace nhprovider.model
{
    /// <summary>
    /// Plain Old CLR Object (POCO) representing the persistent attributes of a <see cref="Role"/> object.
    /// </summary>
    public partial class AspnetRole
    {
        #region Fields
        private System.Guid _RoleId;

        private string _RoleName;

        private string _LoweredRoleName;

        private string _Description;

        private AspnetApplication _AspnetApplication;

        private IList<AspnetUser> _AspnetUsers;

        #endregion Fields

        #region Extensibility Method Definitions

        partial void OnCreated();

        public override bool Equals(object obj)
        {
            AspnetRole toCompare = obj as AspnetRole;
            if (toCompare == null)
            {
                return false;
            }

            if (!Object.Equals(this.RoleId, toCompare.RoleId))
                return false;
            if (!Object.Equals(this.RoleName, toCompare.RoleName))
                return false;
            if (!Object.Equals(this.Description, toCompare.Description))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 13;
            hashCode = (hashCode * 7) + RoleId.GetHashCode();
            hashCode = (hashCode * 7) + RoleName.GetHashCode();
            return hashCode;
        }

        #endregion

        public AspnetRole()
        {
            this._AspnetUsers = new List<AspnetUser>();
            OnCreated();
        }


        /// <summary>
        /// There are no comments for RoleId in the schema.
        /// </summary>
        public virtual System.Guid RoleId
        {
            get
            {
                return this._RoleId;
            }
            set
            {
                this._RoleId = value;
            }
        }


        /// <summary>
        /// There are no comments for RoleName in the schema.
        /// </summary>
        public virtual string RoleName
        {
            get
            {
                return this._RoleName;
            }
            set
            {
                this._RoleName = value;
            }
        }


        /// <summary>
        /// There are no comments for LoweredRoleName in the schema.
        /// </summary>
        public virtual string LoweredRoleName
        {
            get
            {
                return this._LoweredRoleName;
            }
            set
            {
                this._LoweredRoleName = value;
            }
        }


        /// <summary>
        /// There are no comments for Description in the schema.
        /// </summary>
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


        /// <summary>
        /// There are no comments for AspnetApplication in the schema.
        /// </summary>
        public virtual AspnetApplication AspnetApplication
        {
            get
            {
                return this._AspnetApplication;
            }
            set
            {
                this._AspnetApplication = value;
            }
        }

        /// <summary>
        /// There are no comments for AspnetApplication in the schema.
        /// </summary>
        public virtual IList<AspnetUser> AspnetUsers
        {
            get
            {
                return this._AspnetUsers;
            }
            set
            {
                this._AspnetUsers = value;
            }
        }
    }
}
