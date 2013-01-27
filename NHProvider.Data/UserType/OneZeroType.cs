using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace nhprovider.usertype
{
    /// <summary>
    /// NHibernate data type to handle Oracle representation of a boolean, which is mainly <c>CHAR(1)</c>.
    /// </summary>
    /// <remarks>
    /// <p>This implementation is neceassary to avoid an exception thrown by <see cref="bool.Parse"/> since it does not know
    /// how to handle <c>1</c> or <c>0</c> and boolean values. The implementation is only required for the <see cref="NullSafeGet"/>
    /// method. Therefore, the remainder of the class delegates to the <see cref="NHibernateUtil.Boolean"/> instance.</p>
    /// <p>An alternative to using a user type to circumvent this problem is to simply use <c>NUMBER(1,0)</c> instead of <c>CHAR(1)</c>
    /// when defining a column in Oracle to represent a boolean.</p>
    /// </remarks>
    [Serializable]
    public class OneZeroType : IUserType
    {
        #region IUserType Implementation
        /// <summary>
        /// The SQL types for the columns mapped by this type. 
        /// </summary>
        public SqlType[] SqlTypes
        {
            get { return new SqlType[] { NHibernateUtil.Boolean.SqlType }; }
        }
        /// <summary>
        /// The type returned by <see cref="NullSafeGet"/>.
        /// </summary>
        public Type ReturnedType
        {
            get { return typeof(bool); }
        }
        /// <summary>
        /// Compare two instances of the class mapped by this type for persistent "equality"
        /// ie. equality of persistent state.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public new bool Equals(object x, object y)
        {
            return (x == y);
        }
        /// <summary>
        /// Get a hashcode for the instance, consistent with persistence "equality"
        /// </summary>
        /// <remarks>Required for NHibernate 1.2 Beta 2.</remarks>
        public int GetHashCode(object obj)
        {
            return ((null == obj) ? GetHashCode() : obj.GetHashCode());
        }
        /// <summary>
        /// Retrieve an instance of the mapped class from a result set.
        /// </summary>
        /// <remarks>Implementors should handle possibility of null values.</remarks>
        /// <param name="rs">result set containing the data to evaluate.</param>
        /// <param name="names">column names.</param>
        /// <param name="owner">the containing entity.</param>
        /// <returns></returns>
        /// <exception cref="HibernateException">HibernateException</exception>
        /// <exception cref="System.Data.SqlClient.SqlException">SqlException</exception>
        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            object val = null;

            int index = rs.GetOrdinal(names[0]);
            if (!rs.IsDBNull(index))
            {
                try
                {
                    val = rs[index];
                    val = ("1".Equals(val) ? true : ("0".Equals(val) ? false : NHibernateUtil.Boolean.Get(rs, index)));
                }
                catch (InvalidCastException ex)
                {
                    throw new ADOException(
                        "Could not cast the value in field " + names[0] + " of type " + rs[index].GetType().Name + " to the Type " + GetType().Name +
                            ".  Please check to make sure that the mapping is correct and that your DataProvider supports this Data Type.", ex);
                }
            }

            return val;
        }
        /// <summary>
        /// Write an instance of the mapped class to a prepared statement.
        /// </summary>
        /// <remarks>
        /// Implementors should handle possibility of <c>null</c> values. A multi-column type should be written
        /// to parameters starting from <paramref name="index"/>.
        /// </remarks>
        /// <param name="cmd">a IDbCommand</param>
        /// <param name="value">the object to write</param>
        /// <param name="index">command parameter index</param>
        /// <exception cref="HibernateException">HibernateException</exception>
        /// <exception cref="System.Data.SqlClient.SqlException">SqlException</exception>
        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            NHibernateUtil.Boolean.NullSafeSet(cmd, value, index);
        }
        /// <summary>
        /// Return a deep copy of the persistent state, stopping at entities and at collections.
        /// </summary>
        /// <param name="value">generally a collection element or entity field.</param>
        /// <returns>a copy.</returns>
        public object DeepCopy(object value)
        {
            //return NHibernateUtil.Boolean.DeepCopy(value, EntityMode.Map,
            //    NHibernateHelper.GetCurrentSession().GetSessionImplementation().Factory);
            return value;
        }
        /// <summary>
        /// Are objects of this type mutable?
        /// </summary>
        public bool IsMutable
        {
            get { return NHibernateUtil.Boolean.IsMutable; }
        }
        /// <summary>
        /// During merge, replace the existing (<paramref name="target" />) value in the entity
        /// we are merging to with a new (<paramref name="original" />) value from the detached
        /// entity we are merging. For immutable objects, or null values, it is safe to simply
        /// return the first parameter. For mutable objects, it is safe to return a copy of the
        /// first parameter. For objects with component values, it might make sense to
        /// recursively replace component values.
        /// </summary>
        /// <param name="original">the value from the detached entity being merged</param>
        /// <param name="target">the value in the managed entity</param>
        /// <param name="owner">the managed entity</param>
        /// <returns>the value to be merged</returns>
        public object Replace(object original, object target, object owner)
        {
            return DeepCopy(original);
        }
        /// <summary>
        /// Reconstruct an object from the cacheable representation. At the very least this
        /// method should perform a deep copy if the type is mutable. (optional operation)
        /// </summary>
        /// <param name="cached">the object to be cached</param>
        /// <param name="owner">the owner of the cached object</param>
        /// <returns>a reconstructed object from the cachable representation</returns>
        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }
        /// <summary>
        /// Transform the object into its cacheable representation. At the very least this
        /// method should perform a deep copy if the type is mutable. That may not be enough
        /// for some implementations, however; for example, associations must be cached as
        /// identifier values. (optional operation)
        /// </summary>
        /// <param name="value">the object to be cached</param>
        /// <returns>a cacheable representation of the object</returns>
        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }
        #endregion IUserType Implementation
    }
}
