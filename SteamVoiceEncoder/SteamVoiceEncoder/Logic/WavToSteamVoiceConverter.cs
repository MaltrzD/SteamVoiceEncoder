using SteamVoiceEncoder.Models;
using SteamVoiceEncoder.Util;
using System.Text;

namespace SteamVoiceEncoder.Logic
{
    public class WavToSteamVoiceConverter
    {
        public static SteamVoiceSound ConvertFile(string path)
        {
            WavHandler wv = new WavHandler();
            try
            {
                wv.OpenAndRead(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR! Failed to open the sample WAV file.");
                return null;
            }

            SteamVoiceFrame encoder = new SteamVoiceFrame();
            StringBuilder output = new StringBuilder();

            int frameIndex = 0;
            int maxSamplesPerFrame = 960;

            List<byte[]> frames = new List<byte[]>();

            while (true)
            {
                byte[] sampleFrame = wv.GetClipBytes(0, frameIndex, maxSamplesPerFrame);
                if (sampleFrame == null || sampleFrame.Length == 0)
                    break;

                byte[] compressed;
                int compressedSize = encoder.EncodeSamplesToFrame(sampleFrame, out compressed);

                frames.Add(compressed);

                frameIndex += maxSamplesPerFrame;
            }

            return new SteamVoiceSound() { Data = frames, SecondsDuration = wv.GetSecondsDuration() };
        }
    }
}
