using System;

namespace nhprovider.model
{
    public partial class AspnetPersonalizationPerUser
    {
        #region Fields
        private System.Guid _Id;

        private byte[] _PageSettings;

        private System.DateTime _LastUpdatedDate;

        private AspnetPath _AspnetPath;

        private AspnetUser _AspnetUser;
        #endregion Fields

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion

        public AspnetPersonalizationPerUser()
        {
            OnCreated();
        }


        /// <summary>
        /// There are no comments for Id in the schema.
        /// </summary>
        public virtual System.Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }


        /// <summary>
        /// There are no comments for PageSettings in the schema.
        /// </summary>
        public virtual byte[] PageSettings
        {
            get
            {
                return this._PageSettings;
            }
            set
            {
                this._PageSettings = value;
            }
        }


        /// <summary>
        /// There are no comments for LastUpdatedDate in the schema.
        /// </summary>
        public virtual System.DateTime LastUpdatedDate
        {
            get
            {
                if (_LastUpdatedDate == DateTime.MinValue)
                    _LastUpdatedDate = new DateTime(1753, 1, 1);
                return _LastUpdatedDate;
            }
            set
            {
                this._LastUpdatedDate = value;
            }
        }


        /// <summary>
        /// There are no comments for AspnetPath in the schema.
        /// </summary>
        public virtual AspnetPath AspnetPath
        {
            get
            {
                return this._AspnetPath;
            }
            set
            {
                this._AspnetPath = value;
            }
        }


        /// <summary>
        /// There are no comments for AspnetUser in the schema.
        /// </summary>
        public virtual AspnetUser AspnetUser
        {
            get
            {
                return this._AspnetUser;
            }
            set
            {
                this._AspnetUser = value;
            }
        }
    }
}
