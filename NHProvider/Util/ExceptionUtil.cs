using System;
using System.Configuration.Provider;
using System.Text;

namespace NHProvider.util
{
    /// <summary>
    /// Helper class for constructing exception mesages.
    /// </summary>
    internal static class ExceptionUtil
    {
        /// <summary>
        /// Construct a new <see cref="ProviderException"/> instance with the given parameters.
        /// </summary>
        /// <param name="message">message describing the exception.</param>
        /// <returns>A new <see cref="ProviderException"/> instance</returns>
        public static ProviderException NewProviderException(string message)
        {
            return (new ProviderException(FormatExceptionMessage(message)));
        }
        /// <summary>
        /// Construct a new <see cref="ProviderException"/> instance with the given parameters.
        /// </summary>
        /// <param name="message">message describing the exception.</param>
        /// <param name="ex">inner exception representing the root of the problem.</param>
        /// <returns>A new <see cref="ProviderException"/> instance</returns>
        public static ProviderException NewProviderException(string message, Exception ex)
        {
            return (new ProviderException(FormatExceptionMessage(message, ex)));
        }
        /// <summary>
        /// Construct a new <see cref="ProviderException"/> instance with the given parameters.
        /// </summary>
        /// <param name="source">object instance causing the exception.</param>
        /// <param name="message">message describing the exception.</param>
        /// <returns>A new <see cref="ProviderException"/> instance</returns>
        public static ProviderException NewProviderException(object source, string message)
        {
            return (new ProviderException(FormatExceptionMessage(source, message)));
        }
        /// <summary>
        /// Construct a new <see cref="ProviderException"/> instance with the given parameters.
        /// </summary>
        /// <param name="source">object instance causing the exception.</param>
        /// <param name="message">message describing the exception.</param>
        /// <param name="ex">inner exception representing the root of the problem.</param>
        /// <returns>A new <see cref="ProviderException"/> instance</returns>
        public static ProviderException NewProviderException(object source, string message, Exception ex)
        {
            return (new ProviderException(FormatExceptionMessage(source, message, ex)));
        }
        /// <summary>
        /// Provides consistent formatting of the exception message to be thrown.
        /// </summary>
        /// <param name="message">message to be thrown</param>
        /// <returns>Formatted exception message.</returns>
        public static string FormatExceptionMessage(string message)
        {
            // Delegate processing to helper.
            return FormatExceptionMessage("NHibernateProvider", message);
        }
        /// <summary>
        /// Provides consistent formatting of the exception message to be thrown.
        /// </summary>
        /// <param name="message">message to be thrown</param>
        /// <param name="ex">actual cause of the exception.</param>
        /// <returns>Formatted exception message.</returns>
        public static string FormatExceptionMessage(string message, Exception ex)
        {
            // Delegate processing to helper.
            return FormatExceptionMessage("NHibernateProvider", message, ex);
        }
        /// <summary>
        /// Provides consistent formatting of the exception message to be thrown.
        /// </summary>
        /// <param name="source">object instance from where the formatting was requested.</param>
        /// <param name="message">message to be thrown</param>
        /// <returns>Formatted exception message.</returns>
        public static string FormatExceptionMessage(object source, string message)
        {
            // Delegate processing to helper.
            return FormatExceptionMessage(source.GetType(), message);
        }
        /// <summary>
        /// Provides consistent formatting of the exception message to be thrown.
        /// </summary>
        /// <param name="source">object instance from where the formatting was requested.</param>
        /// <param name="message">message to be thrown</param>
        /// <param name="ex">actual cause of the exception.</param>
        /// <returns>Formatted exception message.</returns>
        public static string FormatExceptionMessage(object source, string message, Exception ex)
        {
            // Delegate processing to helper.
            return FormatExceptionMessage(source.GetType(), message, ex);
        }
        /// <summary>
        /// Provides consistent formatting of the exception message to be thrown.
        /// </summary>
        /// <param name="type">object type from where the formatting was requested.</param>
        /// <param name="message">message to be thrown</param>
        /// <returns>Formatted exception message.</returns>
        public static string FormatExceptionMessage(Type type, string message)
        {
            // Delegate processing to helper.
            return FormatExceptionMessage(type.Name, message);
        }
        /// <summary>
        /// Provides consistent formatting of the exception message to be thrown.
        /// </summary>
        /// <param name="type">object type from where the formatting was requested.</param>
        /// <param name="message">message to be thrown</param>
        /// <param name="ex">actual cause of the exception.</param>
        /// <returns>Formatted exception message.</returns>
        public static string FormatExceptionMessage(Type type, string message, Exception ex)
        {
            // Delegate processing to helper.
            return FormatExceptionMessage(type.Name, message, ex);
        }
        /// <summary>
        /// Provides consistent formatting of the exception message to be thrown.
        /// </summary>
        /// <param name="className">name of the class where the exception occured.</param>
        /// <param name="message">message to be thrown</param>
        /// <returns>Formatted exception message.</returns>
        public static string FormatExceptionMessage(string className, string message)
        {
            // Call the overloaded method.
            return FormatExceptionMessage(className, message, null);
        }
        /// <summary>
        /// Provides consistent formatting of the exception message to be thrown.
        /// </summary>
        /// <param name="className">name of the class where the exception occured.</param>
        /// <param name="message">message to be thrown</param>
        /// <param name="ex">actual cause of the exception.</param>
        /// <returns>Formatted exception message.</returns>
        public static string FormatExceptionMessage(string className, string message, Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<br><br>{0}: ", className);
            sb.Append(message);
            if (null != ex)
            {
                sb.AppendFormat("; operation failed with error \"{0}\".<br><br>", ex.Message);
                sb.AppendFormat("<i>Base Exception Message</i>: \"{0}\"<br><br>", ex.GetBaseException().Message);
                sb.AppendFormat("<i>Base Exception Stack Trace</i>: {0}", ex.GetBaseException().StackTrace);
            }
            return sb.ToString();
        }
    }
}
