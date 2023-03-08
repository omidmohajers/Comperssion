using System.Collections;

namespace PA.Comperssion.Core
{
    public class MediaHeader
    {

        public MediaHeader(byte[] input)
        {
            //yek byte etelaate header xxxxxxxx
            byte header = input[0];
            System.Collections.BitArray ba = new BitArray(new byte[] { header });
            // 8 = Media Type
            MediaType = ba.Get(7) ? MediaType.Audio : MediaType.Video;
            switch (MediaType)
            {
                case MediaType.Video:
                    GetVideoType(header);
                    break;
                case MediaType.Audio:
                    GetAudioType(header);
                    break;
            }
            ByteHeader = header;

        }

        public MediaHeader(MediaType mt, VideoType vt,AudioType at)
        {
            byte header = 128;
            header = (mt == MediaType.Audio) ? (byte)128 : (byte)0;

            if (mt == MediaType.Audio)
                header = (byte)(header | (byte)at);
            else
                header = (byte)(header | (byte)vt);
            MediaType = mt;
            VideoType = vt;
            AudioType = at;
            ByteHeader = header;

        }

        private void GetAudioType(byte header)
        {
            // xxxxXXXXX = Audio Type = 00001111 = 15 
            AudioType = (AudioType)(header & 15);
        }

        private void GetVideoType(byte header)
        {
            // xxxxXXXXX = Video Type = 00001111 = 15 
            VideoType = (VideoType)(header & 15);
        }

        public MediaType MediaType { get; private set; }
        public VideoType VideoType { get; private set; }
        public AudioType AudioType { get; private set; }
        public byte ByteHeader { get; }
    }
}
