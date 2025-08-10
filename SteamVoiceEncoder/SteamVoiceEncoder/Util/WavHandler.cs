using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SteamVoiceEncoder.Util
{
    class WavHandler
    {
        List<short> lDataList = null;
        List<short> rDataList = null;
        uint sampleRate = 0;

        public List<short> GetChannel(int N)
        {
            if (N == 0)
                return lDataList;

            if (N == 1)
                return rDataList;

            return null;

        }

        public List<short> GetClip(int Channel, int start, int length)
        {
            var channelSamples = GetChannel(Channel);

            // No data in requested channel
            if (channelSamples == null) return null;

            // start beyond end of available data, this is an error
            if (channelSamples.Count < start) return null;

            // Insufficient data, return lease available
            if (channelSamples.Count < (start + length)) return channelSamples.GetRange(start, channelSamples.Count - start);

            // Return a chunk from inside
            var snippetSamples = channelSamples.GetRange(start, length);

            return snippetSamples;
        }

        public double GetSecondsDuration() => (double)lDataList.Count / sampleRate;


        public byte[] GetClipBytes(int Channel, int start, int length)
        {
            // FIXME> when length is beyond end of array
            List<short> samples = GetClip(Channel, start, length);

            // Zero data, or other error
            if (samples == null) return null;

            // No data left
            if (samples.Count == 0 )
                return null;

            // Less than requested, fill with zero
            if (samples.Count < length ) samples.AddRange(Enumerable.Repeat((short)0, length-samples.Count));

            var vByteArray = new List<byte>();

            foreach (short v in samples)
            {
                byte msb = (byte)((v >> 0) & 0xff);
                byte lsb = (byte)((v >> 8) & 0xff);

                vByteArray.Add(msb);
                vByteArray.Add(lsb);
            }

            // //char cArray = System.Text.Encoding.ASCII.GetString(buffer0).ToCharArray();
            byte[] data = vByteArray.ToArray();

            return data;
        }


        public void OpenAndRead(string WavFilepath)
        {
            lDataList = new List<short>();
            rDataList = new List<short>();

            ushort channels = 0;
            ushort blockSize = 0;
            ushort bitsPerSample = 0;
            uint dataSize = 0;
        
            bool foundFmt = false;
            bool foundData = false;
        
            using (FileStream fs = new FileStream(WavFilepath, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                // Читаем RIFF Header
                byte[] riffID = br.ReadBytes(4);
                uint riffSize = br.ReadUInt32();
                byte[] wavID = br.ReadBytes(4);
        
                if (Encoding.ASCII.GetString(riffID) != "RIFF" || Encoding.ASCII.GetString(wavID) != "WAVE")
                {
                    throw new Exception("Invalid WAV file");
                }
        
        
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    string chunkId = Encoding.ASCII.GetString(br.ReadBytes(4));
                    uint chunkSize = br.ReadUInt32();
        
                    if (chunkId == "fmt ")
                    {
                        foundFmt = true;
                        ushort audioFormat = br.ReadUInt16();
                        channels = br.ReadUInt16();
                        sampleRate = br.ReadUInt32();
                        uint byteRate = br.ReadUInt32();
                        blockSize = br.ReadUInt16();
                        bitsPerSample = br.ReadUInt16();
        
                        if (chunkSize > 16)
                            br.ReadBytes((int)(chunkSize - 16));
                    }
                    else if (chunkId == "data")
                    {
                        foundData = true;
                        dataSize = chunkSize;
        
                        int sampleCount = (int)(dataSize / (bitsPerSample / 8));
        
                        for (int i = 0; i < sampleCount / channels; i++)
                        {
                            if (channels >= 1)
                                lDataList.Add(br.ReadInt16());
        
                            if (channels >= 2)
                                rDataList.Add(br.ReadInt16());
                        }
                        break;
                    }
                    else
                    {
                        br.ReadBytes((int)chunkSize);
                    }
                }
        
                if (!foundFmt || !foundData)
                    throw new Exception("Invalid WAV file, missing fmt or data chunk");
        
                Console.WriteLine($"Channels: {channels}");
                Console.WriteLine($"SampleRate: {sampleRate}");
                Console.WriteLine($"BitsPerSample: {bitsPerSample}");
                Console.WriteLine($"DataSize: {dataSize}");
            }
        }
    }
}
