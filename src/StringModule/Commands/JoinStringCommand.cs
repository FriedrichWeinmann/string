// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Internal;
using System.Management.Automation.Language;
using System.Text;

namespace StringModule.Commands
{
    /// <summary>
    /// Join-Object implementation.
    /// </summary>
    [Cmdlet(VerbsCommon.Join, "String", RemotingCapability = RemotingCapability.None, DefaultParameterSetName = "default")]
    [OutputType(typeof(string))]
    public sealed class JoinStringCommand : PSCmdlet
    {
        /// <summary>A bigger default to not get re-allocations in common use cases.</summary>
        private const int DefaultOutputStringCapacity = 256;
        private readonly StringBuilder _OutputBuilder = new StringBuilder(DefaultOutputStringCapacity);
        private bool _FirstInputObject = true;
        private int _CurrentCount;

        /// <summary>
        /// Gets or sets the property name or script block to use as the value to join.
        /// </summary>
        [Parameter(Position = 1)]
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the delimiter to join the output with.
        /// </summary>
        [Parameter(Position = 0)]
        [ArgumentCompleter(typeof(JoinItemCompleter))]
        [AllowEmptyString]
        public string Separator = "";

        /// <summary>
        /// How many items to combine before starting a new string
        /// </summary>
        [Parameter()]
        public int Count;

        /// <summary>
        /// Gets or sets the input object to join into text.
        /// </summary>
        [Parameter(ValueFromPipeline = true)]
        public PSObject[] InputObject { get; set; }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (InputObject != null)
            {
                foreach (PSObject inputObject in InputObject)
                {
                    if (inputObject == null)
                        continue;
                    if (inputObject == AutomationNull.Value)
                        continue;
                    
                    var inputValue = Property == null
                                        ? inputObject
                                        : inputObject.Properties[Property] != null
                                            ? inputObject.Properties[Property].Value
                                            : "";

                    // conversion to string always succeeds.
                    if (!LanguagePrimitives.TryConvertTo<string>(inputValue, CultureInfo.InvariantCulture, out var stringValue))
                    {
                        throw new PSInvalidCastException("InvalidCastFromAnyTypeToString");
                    }

                    if (_FirstInputObject)
                    {
                        _FirstInputObject = false;
                    }
                    else
                    {
                        _OutputBuilder.Append(Separator);
                    }
                    _OutputBuilder.Append(stringValue);

                    if (Count <= 0)
                        continue;

                    _CurrentCount++;
                    if (_CurrentCount >= Count)
                    {
                        WriteObject(_OutputBuilder.ToString());
                        _OutputBuilder.Clear();
                        _FirstInputObject = true;
                        _CurrentCount = 0;
                    }
                }
            }
        }

        /// <inheritdoc />
        protected override void EndProcessing()
        {
            if (!_FirstInputObject)
                WriteObject(_OutputBuilder.ToString());
        }
    }

    internal class JoinItemCompleter : IArgumentCompleter
    {
        public IEnumerable<CompletionResult> CompleteArgument(
            string commandName,
            string parameterName,
            string wordToComplete,
            CommandAst commandAst,
            IDictionary fakeBoundParameters)
        {
            switch (parameterName)
            {
                case "Separator": return CompleteSeparator(wordToComplete);
                case "FormatString": return CompleteFormatString(wordToComplete);
            }

            return null;
        }

        private IEnumerable<CompletionResult> CompleteFormatString(string wordToComplete)
        {
            var res = new List<CompletionResult>();
            void AddMatching(string completionText)
            {
                if (completionText.StartsWith(wordToComplete, StringComparison.OrdinalIgnoreCase))
                {
                    res.Add(new CompletionResult(completionText));
                }
            }

            AddMatching("'[{0}]'");
            AddMatching("'{0:N2}'");
            AddMatching("\"`r`n    `${0}\"");
            AddMatching("\"`r`n    [string] `${0}\"");

            return res;
        }

        private IEnumerable<CompletionResult> CompleteSeparator(string wordToComplete)
        {
            var res = new List<CompletionResult>(10);

            void AddMatching(string completionText, string listText, string toolTip)
            {
                if (completionText.StartsWith(wordToComplete, StringComparison.OrdinalIgnoreCase))
                {
                    res.Add(new CompletionResult(completionText, listText, CompletionResultType.ParameterValue, toolTip));
                }
            }

            AddMatching("', '", "Comma-Space", "', ' - Comma-Space");
            AddMatching("';'", "Semi-Colon", "';'  - Semi-Colon ");
            AddMatching("'; '", "Semi-Colon-Space", "'; ' - Semi-Colon-Space");
            AddMatching($"\"{NewLineText}\"", "Newline", $"{NewLineText} - Newline");
            AddMatching("','", "Comma", "','  - Comma");
            AddMatching("'-'", "Dash", "'-'  - Dash");
            AddMatching("' '", "Space", "' '  - Space");
            return res;
        }

        public string NewLineText
        {
            get
            {
#if UNIX
                return "`n";
#else
                return "`r`n";
#endif
            }
        }
    }
}