using System;

namespace nhprovider.model
{
    public partial class AspnetProfile : AspnetUser
    {
        #region Fields
        private string _PropertyNames;

        private string _PropertyValuesString;

        private byte[] _PropertyValuesBinary;

        private System.DateTime _LastUpdatedDate;
        #endregion Fields

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion

        public AspnetProfile()
        {
            OnCreated();
        }


        /// <summary>
        /// There are no comments for PropertyNames in the schema.
        /// </summary>
        public virtual string PropertyNames
        {
            get
            {
                return this._PropertyNames;
            }
            set
            {
                this._PropertyNames = value;
            }
        }


        /// <summary>
        /// There are no comments for PropertyValuesString in the schema.
        /// </summary>
        public virtual string PropertyValuesString
        {
            get
            {
                return this._PropertyValuesString;
            }
            set
            {
                this._PropertyValuesString = value;
            }
        }


        /// <summary>
        /// There are no comments for PropertyValuesBinary in the schema.
        /// </summary>
        public virtual byte[] PropertyValuesBinary
        {
            get
            {
                return this._PropertyValuesBinary;
            }
            set
            {
                this._PropertyValuesBinary = value;
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
