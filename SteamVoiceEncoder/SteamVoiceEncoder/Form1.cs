using Newtonsoft.Json;
using SteamVoiceEncoder.Logic;
using System.Diagnostics;

namespace SteamVoiceEncoder
{
    public partial class Form1 : Form
    {
        private string _filePath = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetLastInfo(null, false);
        }

        private void richInput_TextChanged(object sender, EventArgs e) { }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = downloadsPath;

                openFileDialog.Filter = "(*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SwitchFile(openFileDialog.FileName);
                }
                else
                {
                    MessageBox.Show("Вы не выбрали аудио!");
                }
            }
        }

        private void btnStartConvert_Click(object sender, EventArgs e)
        {
            if(File.Exists(_filePath) == false)
            {
                MessageBox.Show("Файл не найден!");
                return;
            }

            string directory = Path.GetDirectoryName(_filePath);
            string fileName = Path.GetFileNameWithoutExtension(_filePath);
            string fullPathJson = Path.Combine(directory, fileName + ".json");
            string fullPathWav = Path.Combine(directory, fileName + "_temp.wav");

            SetLastInfo("Конвертация...");

            StartFFMPEGConvert(_filePath, fullPathWav);

            if (!File.Exists(fullPathWav))
            {
                MessageBox.Show("Конвертированный файл не найден!");
                return;
            }

            SetLastInfo("Кодировщик...");

            var sound = WavToSteamVoiceConverter.ConvertFile(fullPathWav);
            if(sound == null) return;

            File.WriteAllText(fullPathJson, JsonConvert.SerializeObject(sound));

            File.Delete(fullPathWav);

            SetLastInfo($"Файл сохранен по пути:\n{fullPathJson}");

            if(openFolderAfterComplete.Checked)
                Process.Start("explorer.exe", $"/select,\"{fullPathJson}\"");
        }


        private void SwitchFile(string filePath) 
        {
            _filePath = filePath;

            richInput.Text = filePath; 
        }

        private void SetLastInfo(string text, bool enable = true)
        {
            if (!enable) lastInfo.Visible = false;
            lastInfo.Visible = true;

            lastInfo.Enabled = true;
            lastInfo.Text = text;
        }

        private void StartFFMPEGConvert(string inputFile, string outputFile)
        {
            string arguments = $"-i \"{inputFile}\" -ar 24000 -ac 1 \"{outputFile}\"";

            var processInfo = new ProcessStartInfo
            {
                FileName = @".\ffmpeg.exe",
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = new Process())
            {
                process.StartInfo = processInfo;

                process.Start();

                process.WaitForExit();
            }
        }
    }
}
