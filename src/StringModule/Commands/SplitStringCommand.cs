using System;
using System.Management.Automation;
using System.Text.RegularExpressions;

namespace StringModule.Commands
{
    /// <summary>
    /// Implements the Split-String command
    /// </summary>
    [Cmdlet(VerbsCommon.Split, "String", DefaultParameterSetName = "regex")]
    [OutputType(typeof(string))]
    public class SplitStringCommand : Cmdlet
    {
        #region Parameters
        /// <summary>
        /// What to split by
        /// </summary>
        [Parameter(Position = 1)]
        [Alias("with")]
        public string Separator = Environment.NewLine;

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
        /// The maximum number of items to split into.
        /// </summary>
        [Parameter()]
        public int Count;

        /// <summary>
        /// The strings to update
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [AllowEmptyString()]
        [AllowNull()]
        public string[] InputString;
        #endregion Parameters

        private char[] _Separator
        {
            get
            {
                if (separator == null)
                    separator = Separator.ToCharArray();
                return separator;
            }
        }
        private char[] separator;

        private Regex _Regex
        {
            get
            {
                if (regex != null)
                    return regex;

                regex = new Regex(Separator, Options);
                return regex;
            }
        }
        private Regex regex;

        #region Methods
        /// <summary>
        /// Processes the items to split
        /// </summary>
        protected override void ProcessRecord()
        {
            foreach (string item in InputString)
            {
                if (DoNotUseRegex.ToBool())
                    if (Count > 0)
                        WriteObject(item.Split(_Separator, Count), true);
                    else
                        WriteObject(item.Split(_Separator), true);
                else
                {
                    if (Count < 1)
                        WriteObject(_Regex.Split(item), true);
                    else
                        WriteObject(_Regex.Split(item, Count), true);
                }
            }
        }
        #endregion Methods
    }
}