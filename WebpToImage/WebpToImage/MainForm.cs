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
            Log.Info("アプリ起動");
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
                MessageBox.Show("フォルダパスが不正です");
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
                e.Result = "パラメーターエラー";
                return;
            }

            Log.Info($"処理開始 path={param.DirPath}");
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

            e.Result = $"処理数={index}, 成功数={success}";
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
                Log.Error(ex, "画像ファイル変換例外発生 path=" + webpFilePath);
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
                statusLabel.Text = "準備中...";
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
                MessageBox.Show("処理結果未取得");
                statusLabel.Text = "処理結果未取得";
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