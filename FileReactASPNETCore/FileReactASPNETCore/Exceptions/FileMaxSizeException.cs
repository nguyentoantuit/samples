using System;

namespace FileReactASPNETCore.Exceptions
{
    public class FileMaxSizeException : Exception
    {
        private static readonly int Megabyte = 1048576;
        private readonly long _maxSize;
        private readonly long _actualSizeInByte;

        public FileMaxSizeException(long maxSizeInByte, long actualSizeInByte)
        {
            _maxSize = maxSizeInByte;
            _actualSizeInByte = actualSizeInByte;
        }

        public override string Message => $"{base.Message}. Details: maximum file size supported is {_maxSize} bytes (~{ToMB(_maxSize)}MB), actual processing file(s) size is {_actualSizeInByte} bytes (~{ToMB(_actualSizeInByte)}MB)";

        private static double ToMB(long size)
        {
            return Math.Round((float)(size / Megabyte), 2);
        }
    }
}
