namespace WU_Downloader
{
    partial class Main
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.text_xmlPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_XMLPath = new System.Windows.Forms.Button();
            this.button_Start1 = new System.Windows.Forms.Button();
            this.bgDownload = new System.ComponentModel.BackgroundWorker();
            this.text_Log = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // text_xmlPath
            // 
            this.text_xmlPath.Location = new System.Drawing.Point(79, 6);
            this.text_xmlPath.Name = "text_xmlPath";
            this.text_xmlPath.Size = new System.Drawing.Size(412, 19);
            this.text_xmlPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "XMLファイル";
            // 
            // button_XMLPath
            // 
            this.button_XMLPath.Location = new System.Drawing.Point(497, 4);
            this.button_XMLPath.Name = "button_XMLPath";
            this.button_XMLPath.Size = new System.Drawing.Size(75, 23);
            this.button_XMLPath.TabIndex = 2;
            this.button_XMLPath.Text = "参照";
            this.button_XMLPath.UseVisualStyleBackColor = true;
            // 
            // button_Start1
            // 
            this.button_Start1.Location = new System.Drawing.Point(497, 33);
            this.button_Start1.Name = "button_Start1";
            this.button_Start1.Size = new System.Drawing.Size(75, 23);
            this.button_Start1.TabIndex = 3;
            this.button_Start1.Text = "処理開始";
            this.button_Start1.UseVisualStyleBackColor = true;
            this.button_Start1.Click += new System.EventHandler(this.button_Start1_Click);
            // 
            // bgDownload
            // 
            this.bgDownload.WorkerReportsProgress = true;
            this.bgDownload.WorkerSupportsCancellation = true;
            this.bgDownload.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgDownload_DoWork);
            this.bgDownload.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgDownload_ProgressChanged);
            this.bgDownload.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgDownload_RunWorkerCompleted);
            // 
            // text_Log
            // 
            this.text_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_Log.Location = new System.Drawing.Point(12, 78);
            this.text_Log.MaxLength = 0;
            this.text_Log.Multiline = true;
            this.text_Log.Name = "text_Log";
            this.text_Log.ReadOnly = true;
            this.text_Log.Size = new System.Drawing.Size(560, 272);
            this.text_Log.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "ログ";
            // 
            // button_Cancel
            // 
            this.button_Cancel.Enabled = false;
            this.button_Cancel.Location = new System.Drawing.Point(416, 33);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 6;
            this.button_Cancel.Text = "中止";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_Log);
            this.Controls.Add(this.button_Start1);
            this.Controls.Add(this.button_XMLPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_xmlPath);
            this.MaximumSize = new System.Drawing.Size(600, 2000);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "Main";
            this.Text = "Windows Update Downloader";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox text_xmlPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_XMLPath;
        private System.Windows.Forms.Button button_Start1;
        private System.ComponentModel.BackgroundWorker bgDownload;
        private System.Windows.Forms.TextBox text_Log;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Cancel;
    }
}

