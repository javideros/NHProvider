using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Validator.Constraints;

namespace nhprovider.model
{
    public partial class AspnetWebEventEvent
    {
        #region Fields
        private string _EventId;

        private System.DateTime _EventTimeUtc;

        private System.DateTime _EventTime;

        private string _EventType;

        private decimal _EventSequence;

        private decimal _EventOccurrence;

        private int _EventCode;

        private int _EventDetailCode;

        private string _Message;

        private string _ApplicationPath;

        private string _ApplicationVirtualPath;

        private string _MachineName;

        private string _RequestUrl;

        private string _ExceptionType;

        private string _Details;

        #endregion Fields

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion

        public AspnetWebEventEvent()
        {
            OnCreated();
        }


        /// <summary>
        /// There are no comments for EventId in the schema.
        /// </summary>
        public virtual string EventId
        {
            get
            {
                return this._EventId;
            }
            set
            {
                this._EventId = value;
            }
        }


        /// <summary>
        /// There are no comments for EventTimeUtc in the schema.
        /// </summary>
        public virtual System.DateTime EventTimeUtc
        {
            get
            {
                return this._EventTimeUtc;
            }
            set
            {
                this._EventTimeUtc = value;
            }
        }


        /// <summary>
        /// There are no comments for EventTime in the schema.
        /// </summary>
        public virtual System.DateTime EventTime
        {
            get
            {
                return this._EventTime;
            }
            set
            {
                this._EventTime = value;
            }
        }


        /// <summary>
        /// There are no comments for EventType in the schema.
        /// </summary>
        public virtual string EventType
        {
            get
            {
                return this._EventType;
            }
            set
            {
                this._EventType = value;
            }
        }


        /// <summary>
        /// There are no comments for EventSequence in the schema.
        /// </summary>
        public virtual decimal EventSequence
        {
            get
            {
                return this._EventSequence;
            }
            set
            {
                this._EventSequence = value;
            }
        }


        /// <summary>
        /// There are no comments for EventOccurrence in the schema.
        /// </summary>
        public virtual decimal EventOccurrence
        {
            get
            {
                return this._EventOccurrence;
            }
            set
            {
                this._EventOccurrence = value;
            }
        }


        /// <summary>
        /// There are no comments for EventCode in the schema.
        /// </summary>
        public virtual int EventCode
        {
            get
            {
                return this._EventCode;
            }
            set
            {
                this._EventCode = value;
            }
        }


        /// <summary>
        /// There are no comments for EventDetailCode in the schema.
        /// </summary>
        public virtual int EventDetailCode
        {
            get
            {
                return this._EventDetailCode;
            }
            set
            {
                this._EventDetailCode = value;
            }
        }


        /// <summary>
        /// There are no comments for Message in the schema.
        /// </summary>
        public virtual string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                this._Message = value;
            }
        }


        /// <summary>
        /// There are no comments for ApplicationPath in the schema.
        /// </summary>
        public virtual string ApplicationPath
        {
            get
            {
                return this._ApplicationPath;
            }
            set
            {
                this._ApplicationPath = value;
            }
        }


        /// <summary>
        /// There are no comments for ApplicationVirtualPath in the schema.
        /// </summary>
        public virtual string ApplicationVirtualPath
        {
            get
            {
                return this._ApplicationVirtualPath;
            }
            set
            {
                this._ApplicationVirtualPath = value;
            }
        }


        /// <summary>
        /// There are no comments for MachineName in the schema.
        /// </summary>
        public virtual string MachineName
        {
            get
            {
                return this._MachineName;
            }
            set
            {
                this._MachineName = value;
            }
        }


        /// <summary>
        /// There are no comments for RequestUrl in the schema.
        /// </summary>
        public virtual string RequestUrl
        {
            get
            {
                return this._RequestUrl;
            }
            set
            {
                this._RequestUrl = value;
            }
        }


        /// <summary>
        /// There are no comments for ExceptionType in the schema.
        /// </summary>
        public virtual string ExceptionType
        {
            get
            {
                return this._ExceptionType;
            }
            set
            {
                this._ExceptionType = value;
            }
        }


        /// <summary>
        /// There are no comments for Details in the schema.
        /// </summary>
        public virtual string Details
        {
            get
            {
                return this._Details;
            }
            set
            {
                this._Details = value;
            }
        }
    }
}
