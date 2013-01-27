namespace NHProvider.util
{
    /// <summary>
    /// Helper class for fetching configuration data.
    /// </summary>
    internal static class ConfigurationUtil
    {
        /// <summary>
        /// Helper method to validate the given configuration value and assign the given default
        /// if the configuration value is not valid.
        /// </summary>
        /// <param name="configValue">value to test.</param>
        /// <param name="defaultValue">value to assign if <c>configValue</c> is not valid.</param>
        /// <returns>A valid configuration value.</returns>
        internal static string GetConfigValue(string configValue, string defaultValue)
        {
            return (string.IsNullOrEmpty(configValue) ? defaultValue : configValue);
        }
    }
}
