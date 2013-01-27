using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace nhprovider.model
{
    public partial class AspnetPath
    {
        #region Fields
        private System.Guid _PathId;

        private string _Path;

        private string _LoweredPath;

        private AspnetApplication _AspnetApplication;

        private IList<AspnetPersonalizationPerUser> _AspnetPersonalizationPerUsers;
        
        #endregion Fields
        
        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion

        public AspnetPath()
        {
            this._AspnetPersonalizationPerUsers = new List<AspnetPersonalizationPerUser>();
            OnCreated();
        }

        public virtual System.Guid PathId
        {
            get
            {
                return this._PathId;
            }
            set
            {
                this._PathId = value;
            }
        }


        /// <summary>
        /// There are no comments for Path in the schema.
        /// </summary>
        public virtual string Path
        {
            get
            {
                return this._Path;
            }
            set
            {
                this._Path = value;
            }
        }


        /// <summary>
        /// There are no comments for LoweredPath in the schema.
        /// </summary>
        public virtual string LoweredPath
        {
            get
            {
                return this._LoweredPath;
            }
            set
            {
                this._LoweredPath = value;
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
    }
}
