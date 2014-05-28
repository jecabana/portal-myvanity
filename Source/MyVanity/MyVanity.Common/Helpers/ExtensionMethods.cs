using System;
using System.IO;

namespace MyVanity.Common.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// This method is the same as string.Contains, but case insensitive
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        public static bool IgnoreCase(this string source, string toCheck)
        {
            if (source == null) return false;
            return source.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static byte[] ToByteArray(this Stream stream, int length)
        {
            byte[] fileData;

            using (var binaryReader = new BinaryReader(stream))
            {
                fileData = binaryReader.ReadBytes(length);
            }

            return fileData;
        }

    }
}
