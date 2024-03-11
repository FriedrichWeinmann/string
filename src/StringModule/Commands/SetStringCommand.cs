using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
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
        /// What case should be applied
        /// </summary>
        [Parameter()] 
        public Case Case = Case.Unspecified;

        /// <summary>
        /// The files to replace in
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public FileSystemInfo[] InputFile;

        /// <summary>
        /// The strings to update
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        [AllowEmptyString()]
        [AllowNull()]
        public string[] InputString;

        /// <summary>
        /// Ignore file extension blacklist
        /// </summary>
        [Parameter()]
        public SwitchParameter Force;
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
            // Avoid Doublebinding files from pipeline
            if (InputFile != null && InputFile[0]?.FullName == InputString[0])
                InputString = new string[0];

            foreach (string item in InputString)
            {
                WriteObject(Transform(item));
            }

            if (InputFile == null)
                return;

            string content = "";

            foreach (FileSystemInfo info in InputFile)
            {
                if (null == info)
                    continue;
                if (typeof(DirectoryInfo) == info.GetType())
                    continue;
                if (!Force.ToBool() && IsUnsupported(info.Extension))
                    continue;

                try { content = File.ReadAllText(info.FullName); }
                catch (Exception e)
                {
                    ErrorRecord record = new ErrorRecord(e, "ReadFileError", ErrorCategory.ReadError, info);
                    WriteError(record);
                    continue;
                }

                content = Transform(content);

                try { File.WriteAllText(info.FullName, content, GetFileEncoding(info.FullName)); }
                catch (Exception e)
                {
                    ErrorRecord record = new ErrorRecord(e, "WriteFileError", ErrorCategory.WriteError, info);
                    WriteError(record);
                    continue;
                }
            }
        }
        #endregion Methods

        #region Utility
        /// <summary>
        /// Centralizes the input conversion, respecting the parameters specified
        /// </summary>
        /// <param name="Value">The value to convert</param>
        /// <returns>The converted value</returns>
        private string Transform(string Value)
        {
            string transformed = Value;
            if (DoNotUseRegex.ToBool())
                transformed = Value.Replace(OldValue, StringValue);
            else if (ScriptBlockValue != null)
                transformed = Regex.Replace(transformed, OldValue, ScriptBlockEvaluator, Options);
            else
                transformed = Regex.Replace(transformed, OldValue, StringValue, Options);

            if (Case == Case.Unspecified)
                return transformed;

            if (Case == Case.Lower)
                return transformed.ToLower();
            if (Case == Case.Upper)
                return transformed.ToUpper();
            if (Case == Case.Title)
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(transformed);

            return transformed;
        }

        /// <summary>
        /// Returns the assumeed encoding of the file specified, defaults to UTF8.
        /// </summary>
        /// <param name="Path">Path to the file to inspect</param>
        /// <returns>The encoding (UTF8 if not otherwise identified)</returns>
        private Encoding GetFileEncoding(string Path)
        {
            using (var reader = new StreamReader(Path, Encoding.UTF8, true))
            {
                reader.Peek(); // Only after actually performing a read action will we figure out the encoding
                return reader.CurrentEncoding;
            }
        }

        /// <summary>
        /// Checks whether the extension is blacklisted from string-replacement
        /// </summary>
        /// <param name="Extension">The extension to verify</param>
        /// <returns>Blacklisted or not</returns>
        private bool IsUnsupported(string Extension)
        {
            string extLower = Extension.ToLower();
            string[] blacklist = new string[]
            {
                ".exe",
                ".bin",
                ".dll",

                ".jpg",
                ".jpeg",
                ".png",
                ".gif",
                ".mp3",
                ".mp4",
                ".avi",

                ".pfx",
                ".cer",

                ".xls",
                ".xlsx",
                ".doc",
                ".docx",
                ".ppt",
                ".pptx",
                ".pdf"
            };

            return blacklist.Contains(extLower);
        }
        #endregion Utility
    }
}
