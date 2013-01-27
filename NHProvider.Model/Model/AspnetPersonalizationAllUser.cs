using System;

namespace nhprovider.model
{
    public partial class AspnetPersonalizationAllUser : AspnetPath
    {
        #region Fields
        private byte[] _PageSettings;

        private System.DateTime _LastUpdatedDate;
        #endregion Fields

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion

        public AspnetPersonalizationAllUser()
        {
            OnCreated();
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
    }
}
