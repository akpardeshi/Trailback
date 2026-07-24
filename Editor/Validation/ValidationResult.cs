using System.Collections.Generic;

namespace ModularForge.Trailback.Editor
{
    /// <summary>
    /// Stores the results produced by a validation pass, including all validation messages and severity counts.
    /// </summary>
    public sealed class ValidationResult
    {
        #region Readonly Lists
        
        private readonly List<ValidationMessage> _messages = new();

        /// <summary>
        /// Collection of validation messages generated during the validation process.
        /// </summary>
        public IReadOnlyList<ValidationMessage> Messages => _messages;
        
        #endregion


        #region Properties 
        
        /// <summary>
        /// Total number of validation errors.
        /// </summary>
        public int ErrorCount { get; private set; }


        /// <summary>
        /// Total number of validation warnings.
        /// </summary>
        public int WarningCount { get; private set; }


        /// <summary>
        /// Total number of informational messages.
        /// </summary>
        public int InfoCount { get; private set; }


        /// <summary>
        /// Returns true if one or more validation errors were detected.
        /// </summary>
        public bool HasErrors => ErrorCount > 0;


        /// <summary>
        /// Returns true if one or more validation warnings were detected.
        /// </summary>
        public bool HasWarnings => WarningCount > 0;

        #endregion
        

        #region Public Methods
        
        /// <summary>
        /// Adds a validation message to the result.
        /// </summary>
        /// <param name="severity">
        /// Severity level of the validation message.
        /// </param>
        /// <param name="message">
        /// Description of the validation result.
        /// </param>
        /// <param name="context">
        /// Optional Unity object associated with the validation result.
        /// </param>
        public void Add(ValidationSeverity severity, string message, UnityEngine.Object context = null)
        {
            _messages.Add(
                new ValidationMessage(severity, message, context));

            switch (severity)
            {
                case ValidationSeverity.Info:
                    InfoCount++;
                    break;

                case ValidationSeverity.Warning:
                    WarningCount++;
                    break;

                case ValidationSeverity.Error:
                    ErrorCount++;
                    break;
            }
        }
        
        #endregion
    }
}