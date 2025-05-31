using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Text;

namespace StringModule
{
    /// <summary>
    /// String compression tooling
    /// </summary>
    public static class Compression
    {
        /// <summary>
        /// Compress string using default zip algorithms
        /// </summary>
        /// <param name="String">The string to compress</param>
        /// <param name="Encoding">The encoding to use to convert between string and bytes</param>
        /// <returns>Returns a compressed string as byte-array.</returns>
        public static byte[] CompressString(string String, Encoding Encoding = null)
        {
            if (Encoding == null)
                Encoding = Encoding.UTF8;
            byte[] bytes = Encoding.GetBytes(String);
            using (MemoryStream outputStream = new MemoryStream())
            using (GZipStream gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
            {
                gZipStream.Write(bytes, 0, bytes.Length);
                gZipStream.Close();
                outputStream.Close();
                return outputStream.ToArray();
            }
        }

        /// <summary>
        /// Expand a string using default zig algorithms
        /// </summary>
        /// <param name="CompressedString">The compressed string to expand</param>
        /// /// <param name="Encoding">The encoding to use to convert between string and bytes</param>
        /// <returns>Returns an expanded string.</returns>
        public static string ExpandString(byte[] CompressedString, Encoding Encoding = null)
        {
            if (Encoding == null)
                Encoding = Encoding.UTF8;

            using (MemoryStream inputStream = new MemoryStream(CompressedString))
            using (MemoryStream outputStream = new MemoryStream())
            using (GZipStream converter = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                converter.CopyTo(outputStream);
                converter.Close();
                inputStream.Close();
                string result = Encoding.GetString(outputStream.ToArray());
                outputStream.Close();
                return result;
            }
        }
    }
}
