using System;
using System.IO;

namespace PA.Comperssion.Core
{
    public class MediaStream : Stream
    {
        public Stream BaseStream { get; private set; }
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }
        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }
        public override long Length
        {
            get
            {
                return BaseStream.Length;
            }
        }
        public override long Position
        {
            get
            {
                return BaseStream.Position;
            }
            set
            {
                throw new NotSupportedException();
            }
        }
        private readonly byte[] fSizeBytes = new byte[5];
        private int fReadPosition;
        private byte[] fReadBuffer;
        private byte fReadCRC;
        public MediaStream(Stream baseStream)
        {
            BaseStream = baseStream;
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            return BaseStream.Read(buffer, offset, count);
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            BaseStream.Write(buffer, offset, count);
        }

        
        private int fMaxLength;
        private int fCollectionNumber = GC.CollectionCount(GC.MaxGeneration);
        private byte fWriteCRC;
        public override void Flush()
        {
            BaseStream.Flush();
        }
    }
}
