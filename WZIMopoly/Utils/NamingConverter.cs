using System.Text.RegularExpressions;

namespace WZIMopoly.Utils
{
    /// <summary>
    /// Provides utils methods for naming conventions.
    /// </summary>
    internal static class NamingConverter
    {
        /// <summary>
        /// Converts a string in snake_case format to PascalCase format.
        /// </summary>
        /// <param name="text">
        /// The text to convert.
        /// </param>
        /// <returns>
        /// The converted text.
        /// </returns>
        internal static string ConvertSnakeCaseToPascalCase(string text)
        {
            string pattern = @"(^|_)(\w)";
            var regex = new Regex(pattern);
            var result = regex.Replace(text.ToLower(), match => match.Groups[2].Value.ToUpper());
            return result;
        }

        internal static string ConvertShitToFileNames(string text)
        {
            string pattern = @" (\w)";
            var regex = new Regex(pattern);
            var result = regex.Replace(text, match => match.Groups[1].Value.ToUpper());
            return result;
        }
    }
}
