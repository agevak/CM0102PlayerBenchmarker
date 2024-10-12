namespace CM
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbBenchmarking = new System.Windows.Forms.GroupBox();
            this.pgbBenchmark = new System.Windows.Forms.ProgressBar();
            this.txtClubName = new System.Windows.Forms.TextBox();
            this.lblClubName = new System.Windows.Forms.Label();
            this.txtConcurrentRunsCount = new System.Windows.Forms.TextBox();
            this.txtRunsCount = new System.Windows.Forms.TextBox();
            this.lblConcurrentRunsCount = new System.Windows.Forms.Label();
            this.lblTotalRunCounts = new System.Windows.Forms.Label();
            this.txtTestName = new System.Windows.Forms.TextBox();
            this.btnRunBenchmark = new System.Windows.Forms.Button();
            this.btnConvertExternalFile = new System.Windows.Forms.Button();
            this.lblTestName = new System.Windows.Forms.Label();
            this.btnLoadSaveFile = new System.Windows.Forms.Button();
            this.btnSaveSaveFile = new System.Windows.Forms.Button();
            this.grbBenchmarking.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbBenchmarking
            // 
            this.grbBenchmarking.Controls.Add(this.pgbBenchmark);
            this.grbBenchmarking.Controls.Add(this.txtClubName);
            this.grbBenchmarking.Controls.Add(this.lblClubName);
            this.grbBenchmarking.Controls.Add(this.txtConcurrentRunsCount);
            this.grbBenchmarking.Controls.Add(this.txtRunsCount);
            this.grbBenchmarking.Controls.Add(this.lblConcurrentRunsCount);
            this.grbBenchmarking.Controls.Add(this.lblTotalRunCounts);
            this.grbBenchmarking.Controls.Add(this.txtTestName);
            this.grbBenchmarking.Controls.Add(this.btnRunBenchmark);
            this.grbBenchmarking.Controls.Add(this.btnConvertExternalFile);
            this.grbBenchmarking.Controls.Add(this.lblTestName);
            this.grbBenchmarking.Location = new System.Drawing.Point(439, 116);
            this.grbBenchmarking.Name = "grbBenchmarking";
            this.grbBenchmarking.Size = new System.Drawing.Size(250, 205);
            this.grbBenchmarking.TabIndex = 0;
            this.grbBenchmarking.TabStop = false;
            this.grbBenchmarking.Text = "Benchmarking";
            // 
            // pgbBenchmark
            // 
            this.pgbBenchmark.Location = new System.Drawing.Point(6, 171);
            this.pgbBenchmark.Name = "pgbBenchmark";
            this.pgbBenchmark.Size = new System.Drawing.Size(236, 23);
            this.pgbBenchmark.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbBenchmark.TabIndex = 10;
            // 
            // txtClubName
            // 
            this.txtClubName.Location = new System.Drawing.Point(72, 105);
            this.txtClubName.Name = "txtClubName";
            this.txtClubName.Size = new System.Drawing.Size(170, 20);
            this.txtClubName.TabIndex = 9;
            this.txtClubName.Text = "PAS Giannina";
            this.txtClubName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblClubName
            // 
            this.lblClubName.AutoSize = true;
            this.lblClubName.Location = new System.Drawing.Point(6, 108);
            this.lblClubName.Name = "lblClubName";
            this.lblClubName.Size = new System.Drawing.Size(60, 13);
            this.lblClubName.TabIndex = 8;
            this.lblClubName.Text = "Club name:";
            // 
            // txtConcurrentRunsCount
            // 
            this.txtConcurrentRunsCount.Location = new System.Drawing.Point(127, 77);
            this.txtConcurrentRunsCount.Name = "txtConcurrentRunsCount";
            this.txtConcurrentRunsCount.Size = new System.Drawing.Size(115, 20);
            this.txtConcurrentRunsCount.TabIndex = 7;
            this.txtConcurrentRunsCount.Text = "20";
            this.txtConcurrentRunsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRunsCount
            // 
            this.txtRunsCount.Location = new System.Drawing.Point(99, 50);
            this.txtRunsCount.Name = "txtRunsCount";
            this.txtRunsCount.Size = new System.Drawing.Size(143, 20);
            this.txtRunsCount.TabIndex = 6;
            this.txtRunsCount.Text = "40";
            this.txtRunsCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblConcurrentRunsCount
            // 
            this.lblConcurrentRunsCount.AutoSize = true;
            this.lblConcurrentRunsCount.Location = new System.Drawing.Point(6, 80);
            this.lblConcurrentRunsCount.Name = "lblConcurrentRunsCount";
            this.lblConcurrentRunsCount.Size = new System.Drawing.Size(115, 13);
            this.lblConcurrentRunsCount.TabIndex = 5;
            this.lblConcurrentRunsCount.Text = "Concurrent runs count:";
            // 
            // lblTotalRunCounts
            // 
            this.lblTotalRunCounts.AutoSize = true;
            this.lblTotalRunCounts.Location = new System.Drawing.Point(6, 53);
            this.lblTotalRunCounts.Name = "lblTotalRunCounts";
            this.lblTotalRunCounts.Size = new System.Drawing.Size(87, 13);
            this.lblTotalRunCounts.TabIndex = 4;
            this.lblTotalRunCounts.Text = "Total runs count:";
            // 
            // txtTestName
            // 
            this.txtTestName.Location = new System.Drawing.Point(73, 23);
            this.txtTestName.Name = "txtTestName";
            this.txtTestName.Size = new System.Drawing.Size(169, 20);
            this.txtTestName.TabIndex = 3;
            this.txtTestName.Text = "test";
            this.txtTestName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnRunBenchmark
            // 
            this.btnRunBenchmark.Location = new System.Drawing.Point(167, 142);
            this.btnRunBenchmark.Name = "btnRunBenchmark";
            this.btnRunBenchmark.Size = new System.Drawing.Size(75, 23);
            this.btnRunBenchmark.TabIndex = 2;
            this.btnRunBenchmark.Text = "Run";
            this.btnRunBenchmark.UseVisualStyleBackColor = true;
            this.btnRunBenchmark.Click += new System.EventHandler(this.btnRunBenchmark_Click);
            // 
            // btnConvertExternalFile
            // 
            this.btnConvertExternalFile.Location = new System.Drawing.Point(6, 142);
            this.btnConvertExternalFile.Name = "btnConvertExternalFile";
            this.btnConvertExternalFile.Size = new System.Drawing.Size(127, 23);
            this.btnConvertExternalFile.TabIndex = 1;
            this.btnConvertExternalFile.Text = "Convert external file...";
            this.btnConvertExternalFile.UseVisualStyleBackColor = true;
            this.btnConvertExternalFile.Visible = false;
            this.btnConvertExternalFile.Click += new System.EventHandler(this.btnConvertExternalFile_Click_1);
            // 
            // lblTestName
            // 
            this.lblTestName.AutoSize = true;
            this.lblTestName.Location = new System.Drawing.Point(6, 26);
            this.lblTestName.Name = "lblTestName";
            this.lblTestName.Size = new System.Drawing.Size(60, 13);
            this.lblTestName.TabIndex = 0;
            this.lblTestName.Text = "Test name:";
            // 
            // btnLoadSaveFile
            // 
            this.btnLoadSaveFile.Location = new System.Drawing.Point(448, 39);
            this.btnLoadSaveFile.Name = "btnLoadSaveFile";
            this.btnLoadSaveFile.Size = new System.Drawing.Size(127, 23);
            this.btnLoadSaveFile.TabIndex = 2;
            this.btnLoadSaveFile.Text = "Load CM save file";
            this.btnLoadSaveFile.UseVisualStyleBackColor = true;
            this.btnLoadSaveFile.Click += new System.EventHandler(this.btnLoadSaveFile_Click);
            // 
            // btnSaveSaveFile
            // 
            this.btnSaveSaveFile.Location = new System.Drawing.Point(448, 68);
            this.btnSaveSaveFile.Name = "btnSaveSaveFile";
            this.btnSaveSaveFile.Size = new System.Drawing.Size(127, 23);
            this.btnSaveSaveFile.TabIndex = 3;
            this.btnSaveSaveFile.Text = "Save CM save file";
            this.btnSaveSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveSaveFile.Click += new System.EventHandler(this.btnSaveSaveFile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(729, 1061);
            this.Controls.Add(this.btnSaveSaveFile);
            this.Controls.Add(this.btnLoadSaveFile);
            this.Controls.Add(this.grbBenchmarking);
            this.Name = "MainForm";
            this.Text = "PlayerBenchmarker";
            this.grbBenchmarking.ResumeLayout(false);
            this.grbBenchmarking.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grbBenchmarking;
        private System.Windows.Forms.TextBox txtClubName;
        private System.Windows.Forms.Label lblClubName;
        private System.Windows.Forms.TextBox txtConcurrentRunsCount;
        private System.Windows.Forms.TextBox txtRunsCount;
        private System.Windows.Forms.Label lblConcurrentRunsCount;
        private System.Windows.Forms.Label lblTotalRunCounts;
        private System.Windows.Forms.TextBox txtTestName;
        private System.Windows.Forms.Button btnRunBenchmark;
        private System.Windows.Forms.Button btnConvertExternalFile;
        private System.Windows.Forms.Label lblTestName;
        private System.Windows.Forms.Button btnLoadSaveFile;
        private System.Windows.Forms.Button btnSaveSaveFile;
        private System.Windows.Forms.ProgressBar pgbBenchmark;
    }
}

