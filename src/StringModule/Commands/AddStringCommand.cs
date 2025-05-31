using System;
using System.Management.Automation;

namespace StringModule.Commands
{
    /// <summary>
    /// Implementation of the Add-String command
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "String", DefaultParameterSetName = "wrap")]
    [OutputType(typeof(string))]
    public sealed class AddStringCommand : PSCmdlet
    {
        #region Parameters
        /// <summary>
        /// The character to pad the input string with on the left side
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "padLeft")]
        public char PadLeft;

        /// <summary>
        /// The character to pad the input string with on the right side
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "padRight")]
        public char PadRight;

        /// <summary>
        /// To what total string width to pad
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "padLeft")]
        [Parameter(Mandatory = true, ParameterSetName = "padRight")]
        public int PadWidth;

        /// <summary>
        /// The string to add before the input
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "wrap")]
        public string Before;

        /// <summary>
        /// The string to add behind the input
        /// </summary>
        [Parameter(Position = 1, ParameterSetName = "wrap")]
        public string Behind;

        /// <summary>
        /// The property of the input to add things to. By default, the entire object is used instead.
        /// </summary>
        [Alias("p", "Property")]
        [Parameter()]
        public string PropertyName;

        /// <summary>
        /// The string to add to
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [AllowEmptyString()]
        [AllowNull()]
        public PSObject[] InputString;
        #endregion Parameters

        private bool _byProperty = false;

        #region Methods
        /// <summary>
        /// Prepares some simple vlidation
        /// </summary>
        protected override void BeginProcessing()
        {
            _byProperty = !String.IsNullOrEmpty(PropertyName);
        }

        /// <summary>
        /// Process each string as it is passed through.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (InputString == null)
                return;

            string line;
            
            foreach (PSObject item in InputString)
            {
                if (!_byProperty)
                    line = LanguagePrimitives.ConvertTo<string>(item);
                else
                {
                    if (null == item.Properties[PropertyName])
                    {
                        WriteError(new ErrorRecord(
                            new ArgumentException($"Property {PropertyName} not found on {LanguagePrimitives.ConvertTo<string>(item)} of type {item.BaseObject?.GetType().FullName}"),
                            "PropertyExistsNot",
                            ErrorCategory.InvalidArgument,
                            item
                        ));
                        continue;
                    }
                    line = LanguagePrimitives.ConvertTo<string>(item.Properties[PropertyName].Value);
                }
                if (ParameterSetName == "wrap")
                    WriteObject(String.Format("{0}{1}{2}", Before, line, Behind));
                else if (ParameterSetName == "padRight")
                    WriteObject(line.PadRight(PadWidth, PadRight));
                else
                    WriteObject(line.PadLeft(PadWidth, PadLeft));
            }
        }
        #endregion Methods
    }
}
