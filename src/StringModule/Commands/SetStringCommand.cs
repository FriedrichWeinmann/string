using System.Management.Automation;
using System.Reflection;
using System.Text.RegularExpressions;

namespace StringModule.Commands
{
    /// <summary>
    /// Implements the Set-String command
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "String", DefaultParameterSetName = "regex")]
    [OutputType(typeof(string))]
    public class SetStringCommand : PSCmdlet
    {
        #region Parameters
        /// <summary>
        /// What to replace
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string OldValue;

        /// <summary>
        /// What to replace with
        /// </summary>
        [Parameter(Position = 1)]
        public object NewValue;

        /// <summary>
        /// Whether the simple string Replace() method should be used instead
        /// </summary>
        [Parameter(ParameterSetName = "simple")]
        [Alias("simple")]
        public SwitchParameter DoNotUseRegex;

        /// <summary>
        /// The regex options to apply while replacing
        /// </summary>
        [Parameter(ParameterSetName = "regex")]
        public RegexOptions Options = RegexOptions.IgnoreCase;

        /// <summary>
        /// The strings to update
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [AllowEmptyString()]
        public string[] InputString;
        #endregion Parameters

        #region Fields
        /// <summary>
        /// Stores the string value of the With parameter, if applicable.
        /// </summary>
        private string StringValue;

        /// <summary>
        /// Stores the scriptblock value of the With parameter, if applicable.
        /// </summary>
        private ScriptBlock ScriptBlockValue;

        /// <summary>
        /// Evaluates the replace match.
        /// </summary>
        private MatchEvaluator ScriptBlockEvaluator;

        /// <summary>
        /// The reflected method needed to invoke in a consistent manner
        /// </summary>
        private MethodInfo _DoInvokeReturnAsIs;
        #endregion Fields

        #region Methods
        /// <summary>
        /// Handles the input conversion on the What parameter
        /// </summary>
        protected override void BeginProcessing()
        {
            if (NewValue == null)
                StringValue = "";
            else if (!DoNotUseRegex.ToBool() && (NewValue is ScriptBlock))
            {
                ScriptBlockValue = (ScriptBlock)NewValue;
                _DoInvokeReturnAsIs = typeof(ScriptBlock).GetMethod("DoInvokeReturnAsIs", BindingFlags.Instance | BindingFlags.NonPublic);
                ScriptBlockEvaluator = match => {
                    object[] parameters = new object[] { false, 2, match, null, null, null };
                    var result = _DoInvokeReturnAsIs.Invoke(ScriptBlockValue, parameters);

                    return LanguagePrimitives.ConvertTo<string>(result);
                };
            }
            else
                StringValue = NewValue.ToString();
        }

        /// <summary>
        /// Processes each input item
        /// </summary>
        protected override void ProcessRecord()
        {
            foreach (string item in InputString)
            {
                if (DoNotUseRegex.ToBool())
                    WriteObject(item.Replace(OldValue, StringValue));
                else if (ScriptBlockValue != null)
                    WriteObject(Regex.Replace(item, OldValue, ScriptBlockEvaluator, Options));
                else
                    WriteObject(Regex.Replace(item, OldValue, StringValue, Options));
            }
        }
        #endregion Methods
    }
}
