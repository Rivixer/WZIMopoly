using System;

namespace WZIMopoly.Enums
{
    /// <summary>
    /// Represents a subject grade.
    /// </summary>
    internal enum SubjectGrade
    {
        Two,
        Three,
        ThreeHalf,
        Four,
        FourHalf,
        Five,
        Exemption
    }

    /// <summary>
    /// Represents the subject grade extensions class.
    /// </summary>
    internal static class SubjectGradeExtensions
    {
        /// <summary>
        /// Converts the subject grade enum value to string in the X.X form or "Exem.".
        /// </summary>
        /// <param name="grade">
        /// The grade to convert.
        /// </param>
        /// <returns>
        /// The converted grade.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// The enum value or the language is invalid.
        /// </exception>
        internal static string ConvertToString(this SubjectGrade grade)
        {
            return grade switch
            {
                SubjectGrade.Two => "2.0",
                SubjectGrade.Three => "3.0",
                SubjectGrade.ThreeHalf => "3.5",
                SubjectGrade.Four => "4.0",
                SubjectGrade.FourHalf => "4.5",
                SubjectGrade.Five => "5.0",
                SubjectGrade.Exemption => WZIMopoly.Language switch
                {
                    Language.English => "Exem.",
                    Language.Polish => "Zwol.",
                    _ => throw new ArgumentException("Language not implemented.")
                },
                _ => throw new ArgumentException("Invalid grade.")
            };
        }
    }
}
