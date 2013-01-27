using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using System;

namespace nhprovider.model
{
    public partial class AspnetUser
    {

        #region Fields
        private System.Guid _UserId;

        private string _UserName;

        private string _LoweredUserName;

        private string _MobileAlias;

        private bool _IsAnonymous;

        private System.DateTime _LastActivityDate;

        private AspnetApplication _AspnetApplication;

        private IList<AspnetPersonalizationPerUser> _AspnetPersonalizationPerUsers;

        private IList<AspnetRole> _AspnetRoles;
        #endregion Fields

        #region Extensibility Method Definitions

        partial void OnCreated();


        public override bool Equals(object obj)
        {
            AspnetUser toCompare = obj as AspnetUser;
            if (toCompare == null)
            {
                return false;
            }

            if (!Object.Equals(this.UserId, toCompare.UserId))
                return false;
            if (!Object.Equals(this.UserName, toCompare.UserName))
                return false;
            if (!Object.Equals(this.LastActivityDate, toCompare.LastActivityDate))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 13;
            hashCode = (hashCode * 7) + UserId.GetHashCode();
            hashCode = (hashCode * 7) + UserName.GetHashCode();
            return hashCode;
        }

        #endregion

        public AspnetUser()
        {
            //this._AspnetPersonalizationPerUsers = new List<AspnetPersonalizationPerUser>();
            //this._AspnetRoles = new List<AspnetRole>();
            OnCreated();
        }


        /// <summary>
        /// There are no comments for UserId in the schema.
        /// </summary>
        public virtual System.Guid UserId
        {
            get
            {
                return this._UserId;
            }
            set
            {
                this._UserId = value;
            }
        }


        /// <summary>
        /// There are no comments for UserName in the schema.
        /// </summary>
        public virtual string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }


        /// <summary>
        /// There are no comments for LoweredUserName in the schema.
        /// </summary>
        public virtual string LoweredUserName
        {
            get
            {
                return this._LoweredUserName;
            }
            set
            {
                this._LoweredUserName = value;
            }
        }


        /// <summary>
        /// There are no comments for MobileAlias in the schema.
        /// </summary>
        public virtual string MobileAlias
        {
            get
            {
                return this._MobileAlias;
            }
            set
            {
                this._MobileAlias = value;
            }
        }


        /// <summary>
        /// There are no comments for IsAnonymous in the schema.
        /// </summary>
        public virtual bool IsAnonymous
        {
            get
            {
                return this._IsAnonymous;
            }
            set
            {
                this._IsAnonymous = value;
            }
        }


        /// <summary>
        /// There are no comments for LastActivityDate in the schema.
        /// </summary>
        public virtual System.DateTime LastActivityDate
        {
            get
            {
                if (_LastActivityDate == DateTime.MinValue)
                    _LastActivityDate = new DateTime(1753, 1, 1);
                return this._LastActivityDate;
            }
            set
            {
                this._LastActivityDate = value;
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
        /// There are no comments for AspnetPersonalizationPerUsers in the schema.
        /// </summary>
        public virtual IList<AspnetPersonalizationPerUser> AspnetPersonalizationPerUsers
        {
            get
            {
                return this._AspnetPersonalizationPerUsers;
            }
            set
            {
                this._AspnetPersonalizationPerUsers = value;
            }
        }


        /// <summary>
        /// There are no comments for AspnetRoles in the schema.
        /// </summary>
        public virtual IList<AspnetRole> AspnetRoles
        {
            get
            {
                return this._AspnetRoles;
            }
            set
            {
                this._AspnetRoles = value;
            }
        }

    }
}
