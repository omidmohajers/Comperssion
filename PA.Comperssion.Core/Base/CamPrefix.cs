using System;

namespace PA.Comperssion.Core
{
    public class CamPrefix
    {
        public CamPrefix(byte[] input)
        {  
            //yek byte etelaate header xxxxxxxx
            byte header = input[0];

            byte info = (byte)(header & 192);
            switch (info)
            {
                case 64:
                    PreType = PrefixType.Audio;
                    GetAudioType(header);
                    break;
                case 128:
                    PreType = PrefixType.Video;
                    GetVideoType(header);
                    break;
                default:
                    PreType = PrefixType.InitInfo;
                    break;
            }
            ByteHeader = header;
            byte[] len = new byte[4];
            Array.Copy(input, 1, len, 0, len.Length);
            PacketSize = BitConverter.ToInt32(len, 0);
        }

        public CamPrefix(PrefixType mt, VideoType vt, AudioType at)
        {
            byte header = 192;
            header = (mt == PrefixType.Audio) ? (byte)64 : (mt == PrefixType.Video ? (byte)128 : (byte)192);

            if (mt == PrefixType.Audio)
                header = (byte)(header | (byte)at);
            else
                header = (byte)(header | (byte)vt);
            PreType = mt;
            VideoType = vt;
            AudioType = at;
            ByteHeader = header;
            PacketSize = 0;
        }

        public CamPrefix(PrefixType mt, VideoType vt, AudioType at,int packetSize)
        {
            byte header = 192;
            header = (mt == PrefixType.Audio) ? (byte)64 : (mt == PrefixType.Video ? (byte)128 : (byte)192);

            if (mt == PrefixType.Audio)
                header = (byte)(header | (byte)at);
            else
                header = (byte)(header | (byte)vt);
            PreType = mt;
            VideoType = vt;
            AudioType = at;
            ByteHeader = header;
            PacketSize = packetSize;
        }
        public byte[] GetPrefixArray()
        {
            byte[] result = new byte[5];
            result[0] = ByteHeader;
            byte[] buff = BitConverter.GetBytes(PacketSize);
            Array.Copy(buff, 0, result, 1, buff.Length);
            return result;
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
        public VideoType VideoType { get; private set; }
        public AudioType AudioType { get; private set; }
        public PrefixType PreType { get; private set; }
        public byte ByteHeader { get; }
        public int PacketSize { get; private set; }
    }
    public enum PrefixType
    {
        Audio = 64,
        Video = 128,
        InitInfo = 192,
    }
}