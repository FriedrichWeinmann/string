using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

using StringModule;

namespace StringModule.Commands
{
    /// <summary>
    /// Implementation of the Compress-String command
    /// </summary>
    [Cmdlet(VerbsData.Compress, "String")]
    [OutputType(typeof(string))]
    [OutputType(typeof(byte[]))]
    public sealed class CompressStringCommand : PSCmdlet
    {
        /// <summary>
        /// The encoding to use for converting the string to bytes before compression.
        /// </summary>
        [Parameter()]
        public Encoding Encoding;

        /// <summary>
        /// Return the compressed string as byte-array.
        /// By default, the compressed string will be returned as Base64 string.
        /// </summary>
        [Parameter()]
        public SwitchParameter AsBytes;

        /// <summary>
        /// The text to compress.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [AllowEmptyString()]
        [AllowNull()]
        public string[] InputString;

        private Encoding _Encoding;

        protected override void BeginProcessing()
        {
            if (null == Encoding)
                _Encoding = Encoding.UTF8;
            else
                _Encoding = Encoding;
        }

        protected override void ProcessRecord()
        {
            if (null == InputString)
                return;

            foreach (string line in InputString)
            {
                byte[] result = Compression.CompressString(line, _Encoding);
                if (AsBytes.ToBool())
                    WriteObject(result, false);
                else
                    WriteObject(Convert.ToBase64String(result), true);
            }
        }
    }
}
