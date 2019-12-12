using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace StringModule.Commands
{
    /// <summary>
    /// Implementation of the Format-String command
    /// </summary>
    [Cmdlet(VerbsCommon.Format, "String")]
    [OutputType(typeof(string))]
    public class FormatStringCommand : Cmdlet
    {
        #region Parameters
        /// <summary>
        /// The formatting string to use to format the input with
        /// </summary>
        [Parameter(Position = 0, Mandatory = true)]
        public string Format;

        /// <summary>
        /// The number of items to format in bulk
        /// </summary>
        [Parameter(Position = 1)]
        public int Count = 1;

        /// <summary>
        /// The property to select
        /// </summary>
        [Parameter()]
        public string[] Property;

        /// <summary>
        /// Object(s) to format
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public PSObject[] InputObject;
        #endregion Parameters

        #region Internal Properties
        /// <summary>
        /// The property where cached items for bulk formatting are stored
        /// </summary>
        private List<object> ItemCache { get; set; } = new List<object>();
        #endregion Internal Properties

        #region Methods
        /// <summary>
        /// Processes each item for formatting
        /// </summary>
        protected override void ProcessRecord()
        {
            foreach (PSObject item in InputObject)
            {
                if (Count == 1)
                    if (Property != null)
                        WriteObject(String.Format(Format, GetPSPropertyValue(item, Property)));
                    else
                        WriteObject(String.Format(Format, item));
                else
                {
                    if (Property == null)
                    {
                        ItemCache.Add(item);
                        if (ItemCache.Count == Count)
                        {
                            WriteObject(String.Format(Format, ItemCache.Select(x => x).ToArray()));
                            ItemCache.Clear();
                        }
                    }
                    else
                    {
                        foreach (string propertyName in Property)
                        {
                            ItemCache.Add(GetPSPropertyValue(item, propertyName));
                            if (ItemCache.Count == Count)
                            {
                                WriteObject(String.Format(Format, ItemCache.Select(x => x).ToArray()));
                                ItemCache.Clear();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Flushes the final objects if present
        /// </summary>
        protected override void EndProcessing()
        {
            if ((Count > 1) && (ItemCache.Count > 0))
            {
                while (ItemCache.Count < Count)
                    ItemCache.Add(null);

                WriteObject(String.Format(Format, ItemCache.Select(x => x).ToArray()));
            }
        }
        #endregion Methods

        private object[] GetPSPropertyValue(PSObject Object, string[] Property)
        {
            List<object> data = new List<object>();
            foreach (string propertyName in Property)
            {
                if (Object.Properties[propertyName] == null)
                    data.Add("");
                data.Add(Object.Properties[propertyName].Value);
            }
            return data.ToArray();
        }

        private object GetPSPropertyValue(PSObject Object, string Property)
        {
            if (Object.Properties[Property] == null)
                    return "";
            return Object.Properties[Property].Value;
        }
    }
}
