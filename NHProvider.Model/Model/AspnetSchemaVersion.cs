using NHibernate.Validator.Constraints;
using System;

namespace nhprovider.model
{
    public partial class AspnetSchemaVersion
    {
        #region Fields
        private string _Feature;

        private string _CompatibleSchemaVersion;

        private bool _IsCurrentVersion;

        #endregion Fields

        #region Extensibility Method Definitions

        partial void OnCreated();

        public override bool Equals(object obj)
        {
            AspnetSchemaVersion toCompare = obj as AspnetSchemaVersion;
            if (toCompare == null)
            {
                return false;
            }

            if (!Object.Equals(this.Feature, toCompare.Feature))
                return false;
            if (!Object.Equals(this.CompatibleSchemaVersion, toCompare.CompatibleSchemaVersion))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 13;
            hashCode = (hashCode * 7) + Feature.GetHashCode();
            hashCode = (hashCode * 7) + CompatibleSchemaVersion.GetHashCode();
            return hashCode;
        }

        #endregion

        public AspnetSchemaVersion()
        {
            OnCreated();
        }


        /// <summary>
        /// There are no comments for Feature in the schema.
        /// </summary>
        public virtual string Feature
        {
            get
            {
                return this._Feature;
            }
            set
            {
                this._Feature = value;
            }
        }


        /// <summary>
        /// There are no comments for CompatibleSchemaVersion in the schema.
        /// </summary>
        public virtual string CompatibleSchemaVersion
        {
            get
            {
                return this._CompatibleSchemaVersion;
            }
            set
            {
                this._CompatibleSchemaVersion = value;
            }
        }


        /// <summary>
        /// There are no comments for IsCurrentVersion in the schema.
        /// </summary>
        public virtual bool IsCurrentVersion
        {
            get
            {
                return this._IsCurrentVersion;
            }
            set
            {
                this._IsCurrentVersion = value;
            }
        }
    }
}
