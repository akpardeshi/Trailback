using System.Text;

namespace ModularForge.Trailback.Editor
{
    /// <summary>
    /// Generates a formatted Trailback validation report containing setup diagnostics, warnings, errors,
    /// and informational messages.
    /// </summary>
    public static class ValidationReportFormatter
    {
        #region Variables
        
        /// <summary>
        /// Header displayed at the beginning of a formatted validation report.
        /// </summary>
        private const string Header = "========== TRAILBACK VALIDATION ==========";
        
        /// <summary>
        /// Footer displayed at the end of a formatted validation report.
        /// </summary>
        private const string Footer = "==========================================";
        
        #endregion
        
        
        #region Formatter Methods
        
        /// <summary>
        /// Creates a formatted validation report from the supplied validation result.
        /// </summary>
        /// <param name="result">
        /// Validation result to format.
        /// </param>
        /// <returns>
        /// A formatted report containing validation summary information and all generated validation messages.
        /// </returns>
        public static string Format(ValidationResult result)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(Header);
            builder.AppendLine($"Errors: {result.ErrorCount}");
            builder.AppendLine($"Warnings: {result.WarningCount}");
            builder.AppendLine($"Info: {result.InfoCount}");
            builder.AppendLine();

            foreach (var message in result.Messages)
            {
                string prefix = GetPrefix(message.Severity);
                builder.AppendLine($"{prefix} {message.Message}");
            }

            builder.AppendLine(Footer);
            return builder.ToString();
        }
        
        #endregion

        
        #region Helper Methods
        
        /// <summary>
        /// Returns the display prefix associated with a validation severity level.
        /// </summary>
        /// <param name="severity">
        /// Severity level to convert.
        /// </param>
        /// <returns>
        /// A visual indicator representing the specified severity level.
        /// </returns>
        private static string GetPrefix(ValidationSeverity severity)
        {
            return severity switch
            {
                ValidationSeverity.Info => "✓",
                ValidationSeverity.Warning => "⚠",
                ValidationSeverity.Error => "✖",

                _ => "-"
            };
        }
        
        #endregion
    }
}