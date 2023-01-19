using System;
using System.Management.Automation;

namespace Microsoft.PowerShell.Commands.Utility.commands.utility
{
    /// <summary>
    /// Implements Get-SubString command
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SubString", DefaultParameterSetName = "trim")]
    [OutputType(typeof(string))]
    public class GetSubStringCommand : PSCmdlet
    {
        #region Parameters
        /// <summary>
        /// What string to trim with
        /// </summary>
        [Parameter(Position = 0, ParameterSetName = "trim")]
        public string Trim = " ";

        /// <summary>
        /// What string to trim the start with
        /// </summary>
        [Parameter(ParameterSetName = "trimpartial")]
        public string TrimStart;

        /// <summary>
        /// What string to trim the end with
        /// </summary>
        [Parameter(ParameterSetName = "trimpartial")]
        public string TrimEnd;

        /// <summary>
        /// Where to start taking a substring
        /// </summary>
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "substring")]
        public int Start;

        /// <summary>
        /// How long the picked substring should be
        /// </summary>
        [Parameter(Position = 1, ParameterSetName = "substring")]
        public int Length;

        /// <summary>
        /// The strings to process
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [AllowEmptyString()]
        [AllowNull()]
        public string[] InputString;
        #endregion Parameters

        #region Methods
        /// <summary>
        /// Processes the individual items that were input
        /// </summary>
        protected override void ProcessRecord()
        {
            foreach (string item in InputString)
            {
                switch (ParameterSetName)
                {
                    case "substring":
                        if (Start + Length > item.Length)
                        {
                            WriteError(new ErrorRecord(new ArgumentException("Input too short! { item } is shorter than the minimal required length { Start + Length } for this substring operation.", "InputString"), "InputTooShort", ErrorCategory.InvalidArgument, item));
                            break;
                        }
                        if (Length > 0)
                            WriteObject(item.Substring(Start, Length));
                        else
                            WriteObject(item.Substring(Start));
                        break;
                    case "trim":
                        WriteObject(item.Trim(Trim.ToCharArray()));
                        break;
                    case "trimpartial":
                        if (!String.IsNullOrEmpty(TrimStart) && !String.IsNullOrEmpty(TrimEnd))
                            WriteObject(item.TrimStart(TrimStart.ToCharArray()).TrimEnd(TrimEnd.ToCharArray()));
                        else if (!String.IsNullOrEmpty(TrimStart))
                            WriteObject(item.TrimStart(TrimStart.ToCharArray()));
                        else
                            WriteObject(item.TrimEnd(TrimEnd.ToCharArray()));
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion Methods
    }
}
