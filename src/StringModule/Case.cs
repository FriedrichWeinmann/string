using System;
using System.Collections.Generic;
using System.Text;

namespace StringModule
{
    /// <summary>
    /// The different string casings
    /// </summary>
    public enum Case
    {
        /// <summary>
        /// No case preference was specified
        /// </summary>
        Unspecified = 1,

        /// <summary>
        /// All letters should be converted to uppercase
        /// </summary>
        Upper = 2,

        /// <summary>
        /// All letters should be converted to lowercase
        /// </summary>
        Lower = 3,

        /// <summary>
        /// Only the first letter of a word should be uppercase
        /// </summary>
        Title = 4
    }
}
