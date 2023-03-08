using System;
using System.Drawing;
using System.Threading;

namespace PA.Comperssion.Core
{
    public class VideoSource : IDisposable
    {
        public byte FrameRate { get; set; }
        public Size FrameSize { get; set; }
        public int BitRate { get; set; }
        public byte[] Frame { get; protected set; }
        private byte[] currentFrame = null;
        private ManualResetEvent fManualResetEvent = new ManualResetEvent(false);
        public event EventHandler<NewVideoFrameEventArgs> NewFrameReceived = null;


        public byte[] CurrentFrame
        {
            get
            {
                byte[] result = null;
                try
                {
                    result = currentFrame;
                }
                catch(Exception ex)
                {
                }
                finally
                {

                }
                return result;
            }
            set
            {
                try
                {
                    currentFrame = value;
                    RaiseNewFrameReceived(currentFrame);
                  //  fManualResetEvent.Set();
                }
                catch(Exception ex)
                {

                }
                finally
                {

                }
            }
        }

        public byte[] GetNext()
        {
            //if (WasDisposed)
            //    return null;

          //  fManualResetEvent.WaitOne();

            byte[] result = null;
            try
            {
                result = currentFrame;
            }
            catch(Exception ex)
            {

            }
            finally
            {
             //   fManualResetEvent.Reset();
            }
            return result;
        }
        protected void RaiseNewFrameReceived(byte[] frame)
        {
            Frame = frame;
            if (NewFrameReceived != null)
                NewFrameReceived(this, new NewVideoFrameEventArgs(Frame));
        }


        public virtual void Start()
        {

        }

        public virtual void Stop()
        {

        }

        public virtual void Dispose()
        {
        }
    }
}
