using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace StringModule.Commands
{
    /// <summary>
    /// Implementation of the Expand-String command
    /// </summary>
    [Cmdlet(VerbsData.Expand, "String", DefaultParameterSetName = "Base64")]
    [OutputType(typeof(string))]
    [OutputType(typeof(byte[]))]
    public class ExpandStringCommand : PSCmdlet
    {
        /// <summary>
        /// The encoding to use for converting the bytes to string after decompression.
        /// </summary>
        [Parameter()]
        public Encoding Encoding;

        /// <summary>
        /// The text to decompress.
        /// Expects base64-encoded bytes that are the result of previous string compression.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Base64")]
        [AllowEmptyString()]
        [AllowNull()]
        public string[] InputString;

        /// <summary>
        /// The bytes of the text to decompress.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "Bytes")]
        public byte[] Bytes;

        private Encoding _Encoding;

        /// <summary>
        /// Prepares the execution, resolving the encoding to use.
        /// </summary>
        protected override void BeginProcessing()
        {
            if (null == Encoding)
                _Encoding = Encoding.UTF8;
            else
                _Encoding = Encoding;
        }

        /// <summary>
        /// Expand the provided bytes / strings.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (Bytes != null)
            {
                WriteObject(Compression.ExpandString(Bytes, _Encoding), true);
                return;
            }

            foreach (string line in InputString)
                WriteObject(Compression.ExpandString(Convert.FromBase64String(line), _Encoding), true);
        }
    }
}
