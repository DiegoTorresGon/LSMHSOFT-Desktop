
using System;
using System.ComponentModel;

namespace LSMHSOFT___Desktop
{
    public struct Arguments
    {
        public string model;
        public string disturb;
        public string elev;
        public string density;
        public string m;
        public string p;
        public string c;
        public string v;
        public string r;
        public string i;
        public string dataOutputBuffer;
        public int nLinesBuffer;
        public bool outputStarting;
    }
    partial class LSMHSOFT
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label4;
            this.ModelFileButton = new System.Windows.Forms.Button();
            this.OpenModelFile = new System.Windows.Forms.OpenFileDialog();
            this.DataOutput = new System.Windows.Forms.TextBox();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenDensityButton = new System.Windows.Forms.Button();
            this.MTextBox = new System.Windows.Forms.TextBox();
            this.PTextBox = new System.Windows.Forms.TextBox();
            this.CTextBox = new System.Windows.Forms.TextBox();
            this.VTextBox = new System.Windows.Forms.TextBox();
            this.RTextBox = new System.Windows.Forms.TextBox();
            this.ITextBox = new System.Windows.Forms.TextBox();
            this.RunButton = new System.Windows.Forms.Button();
            this.ModelLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ModelFileLabel = new System.Windows.Forms.Label();
            this.OpenDisturbFile = new System.Windows.Forms.OpenFileDialog();
            this.DisturbFileLabel = new System.Windows.Forms.Label();
            this.ElevFileLabel = new System.Windows.Forms.Label();
            this.OpenElevFile = new System.Windows.Forms.OpenFileDialog();
            this.DensityFileLabel = new System.Windows.Forms.Label();
            this.OpenDensityFile = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.MLabel = new System.Windows.Forms.Label();
            this.PLabel = new System.Windows.Forms.Label();
            this.CLabel = new System.Windows.Forms.Label();
            this.VLabel = new System.Windows.Forms.Label();
            this.RLabel = new System.Windows.Forms.Label();
            this.ILabel = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SaveButton = new System.Windows.Forms.Button();
            this.SaveOutput = new System.Windows.Forms.SaveFileDialog();
            label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(20, 437);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(59, 17);
            label4.TabIndex = 20;
            label4.Text = "Density:";
            // 
            // ModelFileButton
            // 
            this.ModelFileButton.AutoSize = true;
            this.ModelFileButton.Location = new System.Drawing.Point(20, 97);
            this.ModelFileButton.Name = "ModelFileButton";
            this.ModelFileButton.Size = new System.Drawing.Size(103, 27);
            this.ModelFileButton.TabIndex = 0;
            this.ModelFileButton.Text = "Select File";
            this.toolTip1.SetToolTip(this.ModelFileButton, "Global geopotential model that will be used as a reference model.\r\nIt includes ha" +
        "rmonic coefficients in GFZ format (n, m, Cnm, Snm, sigmaC, sigmaS).\r\n");
            this.ModelFileButton.UseVisualStyleBackColor = true;
            this.ModelFileButton.Click += new System.EventHandler(this.ModelFileButton_Click);
            // 
            // OpenModelFile
            // 
            this.OpenModelFile.Filter = "GFC Files (*.gfc)|*.gfc|All files (*.*)|*.*";
            this.OpenModelFile.Title = "Open model file";
            // 
            // DataOutput
            // 
            this.DataOutput.AcceptsReturn = true;
            this.DataOutput.AcceptsTab = true;
            this.DataOutput.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.DataOutput.Location = new System.Drawing.Point(285, 89);
            this.DataOutput.MaxLength = 10000000;
            this.DataOutput.Multiline = true;
            this.DataOutput.Name = "DataOutput";
            this.DataOutput.ReadOnly = true;
            this.DataOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataOutput.Size = new System.Drawing.Size(376, 440);
            this.DataOutput.TabIndex = 0;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.TitleLabel.Location = new System.Drawing.Point(205, 25);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(286, 31);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "LSMHSOFT - Desktop";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(20, 208);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select File";
            this.toolTip1.SetToolTip(this.button1, "Mean gravity disturbances which cover the data area.\r\nIt includes grid based data" +
        " (geodetic latitude, longitude and mean disturbance).");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.Location = new System.Drawing.Point(20, 312);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 27);
            this.button2.TabIndex = 3;
            this.button2.Text = "Select File";
            this.toolTip1.SetToolTip(this.button2, "Mean topographic elevations which cover the data area.\r\nIt includes grid based DE" +
        "M data (geodetic latitude, longitude and mean elevation).");
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ElevFileButton);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(18, 402);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Optional arguments:";
            // 
            // OpenDensityButton
            // 
            this.OpenDensityButton.AutoSize = true;
            this.OpenDensityButton.Location = new System.Drawing.Point(23, 457);
            this.OpenDensityButton.Name = "OpenDensityButton";
            this.OpenDensityButton.Size = new System.Drawing.Size(103, 27);
            this.OpenDensityButton.TabIndex = 5;
            this.OpenDensityButton.Text = "Select File";
            this.toolTip1.SetToolTip(this.OpenDensityButton, "Mean topographic densities which cover the target area.\r\nIt includes grid based d" +
        "ata (geodetic latitude, longitude and mean density).");
            this.OpenDensityButton.UseVisualStyleBackColor = true;
            this.OpenDensityButton.Click += new System.EventHandler(this.DensityFileButton);
            // 
            // MTextBox
            // 
            this.MTextBox.Location = new System.Drawing.Point(61, 550);
            this.MTextBox.Name = "MTextBox";
            this.MTextBox.Size = new System.Drawing.Size(136, 22);
            this.MTextBox.TabIndex = 6;
            this.MTextBox.Text = "195";
            this.toolTip1.SetToolTip(this.MTextBox, "Maximum expansion of the GGM used in the computation.\r\ndefault: 195");
            this.MTextBox.TextChanged += new System.EventHandler(this.MTextBox_TextChanged);
            // 
            // PTextBox
            // 
            this.PTextBox.Location = new System.Drawing.Point(61, 595);
            this.PTextBox.Name = "PTextBox";
            this.PTextBox.Size = new System.Drawing.Size(136, 22);
            this.PTextBox.TabIndex = 7;
            this.PTextBox.Text = "0.5";
            this.toolTip1.SetToolTip(this.PTextBox, "Integration cap size (unit: degree).\r\ndefault: 0.5");
            this.PTextBox.TextChanged += new System.EventHandler(this.PTextBox_TextChanged);
            // 
            // CTextBox
            // 
            this.CTextBox.Location = new System.Drawing.Point(61, 637);
            this.CTextBox.Name = "CTextBox";
            this.CTextBox.Size = new System.Drawing.Size(136, 22);
            this.CTextBox.TabIndex = 8;
            this.CTextBox.Text = "1.0";
            this.toolTip1.SetToolTip(this.CTextBox, "Variance of terrestrial gravity data (unit: mGal^2).\r\ndefault: 1.0");
            this.CTextBox.TextChanged += new System.EventHandler(this.CTextBox_TextChanged);
            // 
            // VTextBox
            // 
            this.VTextBox.Location = new System.Drawing.Point(308, 550);
            this.VTextBox.Name = "VTextBox";
            this.VTextBox.Size = new System.Drawing.Size(136, 22);
            this.VTextBox.TabIndex = 9;
            this.VTextBox.Text = "1";
            this.toolTip1.SetToolTip(this.VTextBox, "Version of the LSMH method (biased=1, unbiased=2, optimum=3).\r\ndefault: 1");
            this.VTextBox.TextChanged += new System.EventHandler(this.VTextBox_TextChanged);
            // 
            // RTextBox
            // 
            this.RTextBox.Location = new System.Drawing.Point(308, 595);
            this.RTextBox.Name = "RTextBox";
            this.RTextBox.Size = new System.Drawing.Size(136, 22);
            this.RTextBox.TabIndex = 10;
            this.RTextBox.Text = "45.01/46.99/1.01/4.99";
            this.toolTip1.SetToolTip(this.RTextBox, "Limits of the target area (MinLat/MaxLat/MinLon/MaxLon).\r\ndefault: 45.01/46.99/1." +
        "01/4.99 (2x4 degrees)");
            this.RTextBox.TextChanged += new System.EventHandler(this.RTextBox_TextChanged);
            // 
            // ITextBox
            // 
            this.ITextBox.Location = new System.Drawing.Point(308, 637);
            this.ITextBox.Name = "ITextBox";
            this.ITextBox.Size = new System.Drawing.Size(136, 22);
            this.ITextBox.TabIndex = 11;
            this.ITextBox.Text = "0.02/0.02";
            this.toolTip1.SetToolTip(this.ITextBox, "Intervals of the grid (LatInterval/LonInterval).\r\ndefault: 0.02/0.02 (72x72 arc-s" +
        "econds)");
            this.ITextBox.TextChanged += new System.EventHandler(this.ITextBox_TextChanged);
            // 
            // RunButton
            // 
            this.RunButton.AutoSize = true;
            this.RunButton.Location = new System.Drawing.Point(508, 624);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(120, 49);
            this.RunButton.TabIndex = 12;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // ModelLabel
            // 
            this.ModelLabel.AutoSize = true;
            this.ModelLabel.Location = new System.Drawing.Point(17, 77);
            this.ModelLabel.Name = "ModelLabel";
            this.ModelLabel.Size = new System.Drawing.Size(50, 17);
            this.ModelLabel.TabIndex = 13;
            this.ModelLabel.Text = "Model:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Disturbances:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 292);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Elevation:";
            // 
            // ModelFileLabel
            // 
            this.ModelFileLabel.AutoEllipsis = true;
            this.ModelFileLabel.AutoSize = true;
            this.ModelFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.ModelFileLabel.ForeColor = System.Drawing.Color.Red;
            this.ModelFileLabel.Location = new System.Drawing.Point(17, 127);
            this.ModelFileLabel.MaximumSize = new System.Drawing.Size(250, 60);
            this.ModelFileLabel.Name = "ModelFileLabel";
            this.ModelFileLabel.Size = new System.Drawing.Size(10, 15);
            this.ModelFileLabel.TabIndex = 16;
            this.ModelFileLabel.Text = " ";
            // 
            // OpenDisturbFile
            // 
            this.OpenDisturbFile.Filter = "XYZ files|*.xyz|All files|*.*";
            this.OpenDisturbFile.Title = "Open Disturbances File";
            // 
            // DisturbFileLabel
            // 
            this.DisturbFileLabel.AutoEllipsis = true;
            this.DisturbFileLabel.AutoSize = true;
            this.DisturbFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.DisturbFileLabel.ForeColor = System.Drawing.Color.Red;
            this.DisturbFileLabel.Location = new System.Drawing.Point(20, 238);
            this.DisturbFileLabel.MaximumSize = new System.Drawing.Size(250, 55);
            this.DisturbFileLabel.Name = "DisturbFileLabel";
            this.DisturbFileLabel.Size = new System.Drawing.Size(10, 15);
            this.DisturbFileLabel.TabIndex = 17;
            this.DisturbFileLabel.Text = " ";
            // 
            // ElevFileLabel
            // 
            this.ElevFileLabel.AutoEllipsis = true;
            this.ElevFileLabel.AutoSize = true;
            this.ElevFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.ElevFileLabel.ForeColor = System.Drawing.Color.Red;
            this.ElevFileLabel.Location = new System.Drawing.Point(17, 342);
            this.ElevFileLabel.MaximumSize = new System.Drawing.Size(250, 60);
            this.ElevFileLabel.Name = "ElevFileLabel";
            this.ElevFileLabel.Size = new System.Drawing.Size(10, 15);
            this.ElevFileLabel.TabIndex = 18;
            this.ElevFileLabel.Text = " ";
            // 
            // OpenElevFile
            // 
            this.OpenElevFile.Filter = "XYZ files (*.xyz)|*.xyz|All files|*.*";
            this.OpenElevFile.Title = "Open Elevation File";
            // 
            // DensityFileLabel
            // 
            this.DensityFileLabel.AutoEllipsis = true;
            this.DensityFileLabel.AutoSize = true;
            this.DensityFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.DensityFileLabel.ForeColor = System.Drawing.Color.Red;
            this.DensityFileLabel.Location = new System.Drawing.Point(20, 487);
            this.DensityFileLabel.MaximumSize = new System.Drawing.Size(250, 60);
            this.DensityFileLabel.Name = "DensityFileLabel";
            this.DensityFileLabel.Size = new System.Drawing.Size(10, 15);
            this.DensityFileLabel.TabIndex = 19;
            this.DensityFileLabel.Text = " ";
            // 
            // OpenDensityFile
            // 
            this.OpenDensityFile.Filter = "XYZ files (*.xyz)|*.xyz|All files|*.*";
            this.OpenDensityFile.Title = "Open Density File";
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // MLabel
            // 
            this.MLabel.AutoSize = true;
            this.MLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.MLabel.Location = new System.Drawing.Point(16, 552);
            this.MLabel.Name = "MLabel";
            this.MLabel.Size = new System.Drawing.Size(39, 20);
            this.MLabel.TabIndex = 21;
            this.MLabel.Text = "-M :";
            // 
            // PLabel
            // 
            this.PLabel.AutoSize = true;
            this.PLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.PLabel.Location = new System.Drawing.Point(19, 599);
            this.PLabel.Name = "PLabel";
            this.PLabel.Size = new System.Drawing.Size(36, 20);
            this.PLabel.TabIndex = 22;
            this.PLabel.Text = "-P :";
            // 
            // CLabel
            // 
            this.CLabel.AutoSize = true;
            this.CLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.CLabel.Location = new System.Drawing.Point(18, 639);
            this.CLabel.Name = "CLabel";
            this.CLabel.Size = new System.Drawing.Size(37, 20);
            this.CLabel.TabIndex = 23;
            this.CLabel.Text = "-C :";
            // 
            // VLabel
            // 
            this.VLabel.AutoSize = true;
            this.VLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.VLabel.Location = new System.Drawing.Point(266, 550);
            this.VLabel.Name = "VLabel";
            this.VLabel.Size = new System.Drawing.Size(36, 20);
            this.VLabel.TabIndex = 24;
            this.VLabel.Text = "-V :";
            // 
            // RLabel
            // 
            this.RLabel.AutoSize = true;
            this.RLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.RLabel.Location = new System.Drawing.Point(265, 599);
            this.RLabel.Name = "RLabel";
            this.RLabel.Size = new System.Drawing.Size(37, 20);
            this.RLabel.TabIndex = 25;
            this.RLabel.Text = "-R :";
            // 
            // ILabel
            // 
            this.ILabel.AutoSize = true;
            this.ILabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ILabel.Location = new System.Drawing.Point(273, 639);
            this.ILabel.Name = "ILabel";
            this.ILabel.Size = new System.Drawing.Size(29, 20);
            this.ILabel.TabIndex = 26;
            this.ILabel.Text = "-I :";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_Completed);
            // 
            // SaveButton
            // 
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(508, 552);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(119, 49);
            this.SaveButton.TabIndex = 27;
            this.SaveButton.Text = "Save result";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // SaveOutput
            // 
            this.SaveOutput.Filter = "XYZ files (.xyz)|*.xyz|All files|*.*";
            this.SaveOutput.Title = "Save output file";
            // 
            // LSMHSOFT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(686, 688);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ILabel);
            this.Controls.Add(this.RLabel);
            this.Controls.Add(this.VLabel);
            this.Controls.Add(this.CLabel);
            this.Controls.Add(this.PLabel);
            this.Controls.Add(this.MLabel);
            this.Controls.Add(label4);
            this.Controls.Add(this.DensityFileLabel);
            this.Controls.Add(this.ElevFileLabel);
            this.Controls.Add(this.DisturbFileLabel);
            this.Controls.Add(this.ModelFileLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ModelLabel);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.ITextBox);
            this.Controls.Add(this.RTextBox);
            this.Controls.Add(this.VTextBox);
            this.Controls.Add(this.CTextBox);
            this.Controls.Add(this.PTextBox);
            this.Controls.Add(this.MTextBox);
            this.Controls.Add(this.OpenDensityButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.DataOutput);
            this.Controls.Add(this.ModelFileButton);
            this.Name = "LSMHSOFT";
            this.Text = "LSMHSOFT - Desktop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ModelFileButton;
        private System.Windows.Forms.OpenFileDialog OpenModelFile;
        private System.Windows.Forms.TextBox DataOutput;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MTextBox;
        private System.Windows.Forms.TextBox PTextBox;
        private System.Windows.Forms.TextBox CTextBox;
        private System.Windows.Forms.TextBox VTextBox;
        private System.Windows.Forms.TextBox RTextBox;
        private System.Windows.Forms.TextBox ITextBox;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Label ModelLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ModelFileLabel;
        private System.Windows.Forms.OpenFileDialog OpenDisturbFile;
        private System.Windows.Forms.Label DisturbFileLabel;
        private System.Windows.Forms.Label ElevFileLabel;
        private System.Windows.Forms.OpenFileDialog OpenElevFile;
        private System.Windows.Forms.Button OpenDensityButton;
        private System.Windows.Forms.Label DensityFileLabel;
        private System.Windows.Forms.OpenFileDialog OpenDensityFile;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label MLabel;
        private System.Windows.Forms.Label PLabel;
        private System.Windows.Forms.Label CLabel;
        private System.Windows.Forms.Label VLabel;
        private System.Windows.Forms.Label RLabel;
        private System.Windows.Forms.Label ILabel;
        Arguments args;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.SaveFileDialog SaveOutput;
    }
}

