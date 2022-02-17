namespace WebpToImage
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dirPathBox = new System.Windows.Forms.TextBox();
            this.dirPathRefButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.startButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "フォルダパス";
            // 
            // dirPathBox
            // 
            this.dirPathBox.Location = new System.Drawing.Point(156, 12);
            this.dirPathBox.Name = "dirPathBox";
            this.dirPathBox.Size = new System.Drawing.Size(669, 39);
            this.dirPathBox.TabIndex = 1;
            // 
            // dirPathRefButton
            // 
            this.dirPathRefButton.Location = new System.Drawing.Point(831, 12);
            this.dirPathRefButton.Name = "dirPathRefButton";
            this.dirPathRefButton.Size = new System.Drawing.Size(92, 39);
            this.dirPathRefButton.TabIndex = 2;
            this.dirPathRefButton.Text = "...";
            this.dirPathRefButton.UseVisualStyleBackColor = true;
            this.dirPathRefButton.Click += new System.EventHandler(this.dirPathRefButton_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 69);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(182, 46);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "処理開始";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 132);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(911, 46);
            this.progressBar.TabIndex = 4;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 187);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(119, 32);
            this.statusLabel.TabIndex = 5;
            this.statusLabel.Text = "開始待ち...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 228);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.dirPathRefButton);
            this.Controls.Add(this.dirPathBox);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Webp⇒PNG一括変換";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox dirPathBox;
        private Button dirPathRefButton;
        private FolderBrowserDialog folderBrowserDialog;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private Button startButton;
        private ProgressBar progressBar;
        private Label statusLabel;
    }
}