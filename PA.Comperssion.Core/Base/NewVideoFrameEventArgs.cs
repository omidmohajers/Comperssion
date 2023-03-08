using System;

namespace PA.Comperssion.Core
{
    public class NewVideoFrameEventArgs : EventArgs
    {
        public NewVideoFrameEventArgs(byte[] frame)
        {
            Frame = frame;
        }

        //
        // Summary:
        //     New frame from video source.
        public byte[] Frame { get; }
    }
}
