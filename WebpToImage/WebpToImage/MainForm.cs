using System.Drawing.Imaging;
using WebPWrapper;

namespace WebpToImage
{
    public partial class MainForm : Form
    {
        private class WorkerParam
        {
            public string DirPath { get; set; } = "";
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Log.Info("�A�v���N��");
        }

        private void dirPathRefButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                dirPathBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (bgWorker.IsBusy)
            {
                return;
            }

            var dirPath = dirPathBox.Text;
            if (!Directory.Exists(dirPath))
            {
                MessageBox.Show("�t�H���_�p�X���s���ł�");
                return;
            }

            startButton.Enabled = false;
            bgWorker.RunWorkerAsync(new WorkerParam()
            {
                DirPath = dirPath,
            });
        }

        private void bgWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (e.Argument is not WorkerParam param)
            {
                e.Result = "�p�����[�^�[�G���[";
                return;
            }

            Log.Info($"�����J�n path={param.DirPath}");
            var files = Directory.GetFiles(param.DirPath, "*.webp", SearchOption.AllDirectories);
            bgWorker.ReportProgress(files.Length, "MAX");
            var success = 0;
            var index = 0;
            foreach (var file in files)
            {
                bgWorker.ReportProgress(index, file);
                if (convertWebpToPng(file))
                {
                    success++;
                }
                index++;
            }

            e.Result = $"������={index}, ������={success}";
            return;
        }

        private bool convertWebpToPng(string webpFilePath)
        {
            try
            {
                using var webp = new WebP();
                var image = webp.Load(webpFilePath);
                var dirPath = Path.GetDirectoryName(webpFilePath);
                if (dirPath == null)
                {
                    dirPath = "";
                }
                var newFilePath = Path.Combine(dirPath, Path.GetFileNameWithoutExtension(webpFilePath) + ".png");

                image.Save(newFilePath, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "�摜�t�@�C���ϊ���O���� path=" + webpFilePath);
                return false;
            }

            return true;
        }

        private void bgWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (e.UserState == null)
            {
                return;
            }
            if (e.UserState.ToString() == "MAX")
            {
                progressBar.Maximum = e.ProgressPercentage;
                statusLabel.Text = "������...";
            }
            else
            {
                progressBar.Value = e.ProgressPercentage;
                statusLabel.Text = e.UserState.ToString();
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                MessageBox.Show("�������ʖ��擾");
                statusLabel.Text = "�������ʖ��擾";
            }
            else
            {
                MessageBox.Show(e.Result.ToString());
                statusLabel.Text = e.Result.ToString();
            }
            startButton.Enabled = true;
        }
    }
}