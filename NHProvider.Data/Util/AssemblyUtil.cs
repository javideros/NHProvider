using System;
using System.Reflection;

namespace nhprovider.util
{
    /// <summary>
    /// Helper class for accessing assembly details.
    /// </summary>
    public static class AssemblyUtil
    {
        /// <summary>
        /// Returns the full assembly signature.
        /// </summary>
        /// <param name="assemblyName">short name of the assembly for which to return the details.</param>
        /// <returns>assembly details, if found; otherwise the string <c>[unknown]</c>.</returns>
        public static string GetAssemblyFullName(string assemblyName)
        {
            string fullName = "[unknown]";

            if (!string.IsNullOrEmpty(assemblyName))
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly assembly in assemblies)
                {
                    string[] parts = assembly.FullName.Split(',');
                    if (assemblyName.Equals(parts[0]))
                    {
                        fullName = assembly.FullName;
                        break;
                    }
                }
            }

            return fullName;
        }
    }
}