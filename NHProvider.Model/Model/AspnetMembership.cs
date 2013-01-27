using System;

namespace nhprovider.model
{
    /// <summary>
    /// Plain Old CLR Object (POCO) representing the persistent attributes of a <see cref="User"/> object.
    /// </summary>
    public partial class AspnetMembership : AspnetUser
    {
        #region Fields
        private string _Password;

        private int _PasswordFormat;

        private string _PasswordSalt;

        private string _MobilePIN;

        private string _Email;

        private string _LoweredEmail;

        private string _PasswordQuestion;

        private string _PasswordAnswer;

        private bool _IsApproved;

        private bool _IsLockedOut;

        private System.DateTime _CreateDate;

        private System.DateTime _LastLoginDate;

        private System.DateTime _LastPasswordChangedDate;

        private System.DateTime _LastLockoutDate;

        private int _FailedPasswordAttemptCount;

        private System.DateTime _FailedPasswordAttemptWindowStart;

        private int _FailedPasswordAnswerAttemptCount;

        private System.DateTime _FailedPasswordAnswerAttemptWindowStart;

        private string _Comment;

        private AspnetApplication _AspnetApplication;
        #endregion Fields

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion

        #region Initialization

        public AspnetMembership()
        {
            OnCreated();
        }
        #endregion Initialization

        #region Properties
        /// <summary>
        /// There are no comments for Password in the schema.
        /// </summary>
        public virtual string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }


        /// <summary>
        /// There are no comments for PasswordFormat in the schema.
        /// </summary>
        public virtual int PasswordFormat
        {
            get
            {
                return this._PasswordFormat;
            }
            set
            {
                this._PasswordFormat = value;
            }
        }


        /// <summary>
        /// There are no comments for PasswordSalt in the schema.
        /// </summary>
        public virtual string PasswordSalt
        {
            get
            {
                return this._PasswordSalt;
            }
            set
            {
                this._PasswordSalt = value;
            }
        }


        /// <summary>
        /// There are no comments for MobilePIN in the schema.
        /// </summary>
        public virtual string MobilePIN
        {
            get
            {
                return this._MobilePIN;
            }
            set
            {
                this._MobilePIN = value;
            }
        }


        /// <summary>
        /// There are no comments for Email in the schema.
        /// </summary>
        public virtual string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this._Email = value;
            }
        }


        /// <summary>
        /// There are no comments for LoweredEmail in the schema.
        /// </summary>
        public virtual string LoweredEmail
        {
            get
            {
                return this._LoweredEmail;
            }
            set
            {
                this._LoweredEmail = value;
            }
        }


        /// <summary>
        /// There are no comments for PasswordQuestion in the schema.
        /// </summary>
        public virtual string PasswordQuestion
        {
            get
            {
                return this._PasswordQuestion;
            }
            set
            {
                this._PasswordQuestion = value;
            }
        }


        /// <summary>
        /// There are no comments for PasswordAnswer in the schema.
        /// </summary>
        public virtual string PasswordAnswer
        {
            get
            {
                return this._PasswordAnswer;
            }
            set
            {
                this._PasswordAnswer = value;
            }
        }


        /// <summary>
        /// There are no comments for IsApproved in the schema.
        /// </summary>
        public virtual bool IsApproved
        {
            get
            {
                return this._IsApproved;
            }
            set
            {
                this._IsApproved = value;
            }
        }


        /// <summary>
        /// There are no comments for IsLockedOut in the schema.
        /// </summary>
        public virtual bool IsLockedOut
        {
            get
            {
                return this._IsLockedOut;
            }
            set
            {
                this._IsLockedOut = value;
            }
        }


        /// <summary>
        /// There are no comments for CreateDate in the schema.
        /// </summary>
        public virtual System.DateTime CreateDate
        {
            get
            {
                if (_CreateDate == DateTime.MinValue)
                    _CreateDate = new DateTime(1753, 1, 1);
                return _CreateDate;
            }
            set
            {
                this._CreateDate = value;
            }
        }


        /// <summary>
        /// There are no comments for LastLoginDate in the schema.
        /// </summary>
        public virtual System.DateTime LastLoginDate
        {
            get
            {
                if (_LastLoginDate == DateTime.MinValue)
                    _LastLoginDate = new DateTime(1753, 1, 1);
                return _LastLoginDate;
            }
            set
            {
                this._LastLoginDate = value;
            }
        }


        /// <summary>
        /// There are no comments for LastPasswordChangedDate in the schema.
        /// </summary>
        public virtual System.DateTime LastPasswordChangedDate
        {
            get
            {
                if (_LastPasswordChangedDate == DateTime.MinValue)
                    _LastPasswordChangedDate = new DateTime(1753, 1, 1);
                return _LastPasswordChangedDate;
            }
            set
            {
                this._LastPasswordChangedDate = value;
            }
        }


        /// <summary>
        /// There are no comments for LastLockoutDate in the schema.
        /// </summary>
        public virtual System.DateTime LastLockoutDate
        {
            get
            {
                if (_LastLockoutDate == DateTime.MinValue)
                    _LastLockoutDate = new DateTime(1753, 1, 1);
                return _LastLockoutDate;
            }
            set
            {
                this._LastLockoutDate = value;
            }
        }


        /// <summary>
        /// There are no comments for FailedPasswordAttemptCount in the schema.
        /// </summary>
        public virtual int FailedPasswordAttemptCount
        {
            get
            {
                return this._FailedPasswordAttemptCount;
            }
            set
            {
                this._FailedPasswordAttemptCount = value;
            }
        }


        /// <summary>
        /// There are no comments for FailedPasswordAttemptWindowStart in the schema.
        /// </summary>
        public virtual System.DateTime FailedPasswordAttemptWindowStart
        {
            get
            {
                if (_FailedPasswordAttemptWindowStart == DateTime.MinValue)
                    _FailedPasswordAttemptWindowStart = new DateTime(1753, 1, 1);
                return _FailedPasswordAttemptWindowStart;
            }
            set
            {
                this._FailedPasswordAttemptWindowStart = value;
            }
        }


        /// <summary>
        /// There are no comments for FailedPasswordAnswerAttemptCount in the schema.
        /// </summary>
        public virtual int FailedPasswordAnswerAttemptCount
        {
            get
            {
                return this._FailedPasswordAnswerAttemptCount;
            }
            set
            {
                this._FailedPasswordAnswerAttemptCount = value;
            }
        }


        /// <summary>
        /// There are no comments for FailedPasswordAnswerAttemptWindowStart in the schema.
        /// </summary>
        public virtual System.DateTime FailedPasswordAnswerAttemptWindowStart
        {
            get
            {
                if (_FailedPasswordAnswerAttemptWindowStart == DateTime.MinValue)
                    _FailedPasswordAnswerAttemptWindowStart = new DateTime(1753, 1, 1);
                return _FailedPasswordAnswerAttemptWindowStart;
            }
            set
            {
                this._FailedPasswordAnswerAttemptWindowStart = value;
            }
        }


        /// <summary>
        /// There are no comments for Comment in the schema.
        /// </summary>
        public virtual string Comment
        {
            get
            {
                return this._Comment;
            }
            set
            {
                this._Comment = value;
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

        #endregion Properties

    }

}
