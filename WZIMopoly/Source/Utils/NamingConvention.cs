#region Using Statements
using System.Text.RegularExpressions;
#endregion

namespace WZIMopoly.Utils
{
    internal static class NamingConvention
    {
        /// <summary>
        /// Converts a string in snake_case format to PascalCase format.
        /// </summary>
        /// <param name="text">The text to convert.</param>
        /// <returns>The converted text.</returns>
        internal static string ConvertSnakeCaseToPascalCase(string text)
        {
            string pattern = @"(^|_)(\w)";
            var regex = new Regex(pattern);
            var result = regex.Replace(text, match => match.Groups[2].Value.ToUpper());
            return result;
        }
    }
}
