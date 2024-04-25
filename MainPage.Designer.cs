namespace Winietowanie
{
    partial class MainPage
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.outputImage = new System.Windows.Forms.PictureBox();
            this.inputImage = new System.Windows.Forms.PictureBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.IntensityTrackBar = new System.Windows.Forms.TrackBar();
            this.IntensityLabel = new System.Windows.Forms.Label();
            this.RadiusLabel = new System.Windows.Forms.Label();
            this.RadiusTrackBar = new System.Windows.Forms.TrackBar();
            this.ThreadTrackBar = new System.Windows.Forms.TrackBar();
            this.ThreadLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ASMTimeLabel = new System.Windows.Forms.Label();
            this.CTimeLabel = new System.Windows.Forms.Label();
            this.CLibrary = new System.Windows.Forms.RadioButton();
            this.ASMLibrary = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.RedTrackBar = new System.Windows.Forms.TrackBar();
            this.GreenTrackBar = new System.Windows.Forms.TrackBar();
            this.BlueTrackBar = new System.Windows.Forms.TrackBar();
            this.RedLabel = new System.Windows.Forms.Label();
            this.GreenLabel = new System.Windows.Forms.Label();
            this.BlueLabel = new System.Windows.Forms.Label();
            this.SaveToFileCheckBox = new System.Windows.Forms.CheckBox();
            this.RaportButton = new System.Windows.Forms.Button();
            this.ProbesUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.outputImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntensityTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProbesUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // outputImage
            // 
            this.outputImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputImage.Location = new System.Drawing.Point(449, 54);
            this.outputImage.Name = "outputImage";
            this.outputImage.Size = new System.Drawing.Size(300, 300);
            this.outputImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.outputImage.TabIndex = 2;
            this.outputImage.TabStop = false;
            // 
            // inputImage
            // 
            this.inputImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputImage.Location = new System.Drawing.Point(18, 54);
            this.inputImage.Name = "inputImage";
            this.inputImage.Size = new System.Drawing.Size(300, 300);
            this.inputImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.inputImage.TabIndex = 3;
            this.inputImage.TabStop = false;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Location = new System.Drawing.Point(18, 360);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BrowseButton.TabIndex = 6;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click_1);
            // 
            // ConvertButton
            // 
            this.ConvertButton.Location = new System.Drawing.Point(24, 61);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(75, 23);
            this.ConvertButton.TabIndex = 7;
            this.ConvertButton.Text = "Convert";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click_1);
            // 
            // IntensityTrackBar
            // 
            this.IntensityTrackBar.Location = new System.Drawing.Point(18, 424);
            this.IntensityTrackBar.Maximum = 20;
            this.IntensityTrackBar.Name = "IntensityTrackBar";
            this.IntensityTrackBar.Size = new System.Drawing.Size(300, 45);
            this.IntensityTrackBar.TabIndex = 8;
            this.IntensityTrackBar.Scroll += new System.EventHandler(this.IntensityTrackBar_Scroll);
            // 
            // IntensityLabel
            // 
            this.IntensityLabel.AutoSize = true;
            this.IntensityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.IntensityLabel.Location = new System.Drawing.Point(14, 401);
            this.IntensityLabel.Name = "IntensityLabel";
            this.IntensityLabel.Size = new System.Drawing.Size(107, 16);
            this.IntensityLabel.TabIndex = 9;
            this.IntensityLabel.Text = "Vignette Intensity";
            // 
            // RadiusLabel
            // 
            this.RadiusLabel.AutoSize = true;
            this.RadiusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RadiusLabel.Location = new System.Drawing.Point(14, 486);
            this.RadiusLabel.Name = "RadiusLabel";
            this.RadiusLabel.Size = new System.Drawing.Size(102, 16);
            this.RadiusLabel.TabIndex = 10;
            this.RadiusLabel.Text = "Vignette Radius";
            // 
            // RadiusTrackBar
            // 
            this.RadiusTrackBar.Location = new System.Drawing.Point(18, 509);
            this.RadiusTrackBar.Maximum = 20;
            this.RadiusTrackBar.Name = "RadiusTrackBar";
            this.RadiusTrackBar.Size = new System.Drawing.Size(300, 45);
            this.RadiusTrackBar.TabIndex = 11;
            this.RadiusTrackBar.Scroll += new System.EventHandler(this.RadiusTrackBar_Scroll);
            // 
            // ThreadTrackBar
            // 
            this.ThreadTrackBar.Location = new System.Drawing.Point(449, 509);
            this.ThreadTrackBar.Maximum = 20;
            this.ThreadTrackBar.Name = "ThreadTrackBar";
            this.ThreadTrackBar.Size = new System.Drawing.Size(300, 45);
            this.ThreadTrackBar.TabIndex = 12;
            this.ThreadTrackBar.Scroll += new System.EventHandler(this.ThreadTrackBar_Scroll);
            // 
            // ThreadLabel
            // 
            this.ThreadLabel.AutoSize = true;
            this.ThreadLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ThreadLabel.Location = new System.Drawing.Point(445, 486);
            this.ThreadLabel.Name = "ThreadLabel";
            this.ThreadLabel.Size = new System.Drawing.Size(97, 16);
            this.ThreadLabel.TabIndex = 13;
            this.ThreadLabel.Text = "Threads used: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ASMTimeLabel);
            this.groupBox1.Controls.Add(this.CTimeLabel);
            this.groupBox1.Controls.Add(this.CLibrary);
            this.groupBox1.Controls.Add(this.ASMLibrary);
            this.groupBox1.Location = new System.Drawing.Point(449, 372);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 97);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose DLL Library";
            // 
            // ASMTimeLabel
            // 
            this.ASMTimeLabel.AutoSize = true;
            this.ASMTimeLabel.Location = new System.Drawing.Point(136, 66);
            this.ASMTimeLabel.Name = "ASMTimeLabel";
            this.ASMTimeLabel.Size = new System.Drawing.Size(130, 13);
            this.ASMTimeLabel.TabIndex = 4;
            this.ASMTimeLabel.Text = "ASM Generator time: -- ms";
            // 
            // CTimeLabel
            // 
            this.CTimeLabel.AutoSize = true;
            this.CTimeLabel.Location = new System.Drawing.Point(136, 33);
            this.CTimeLabel.Name = "CTimeLabel";
            this.CTimeLabel.Size = new System.Drawing.Size(121, 13);
            this.CTimeLabel.TabIndex = 3;
            this.CTimeLabel.Text = "C# Generator time: -- ms";
            // 
            // CLibrary
            // 
            this.CLibrary.AutoSize = true;
            this.CLibrary.Checked = true;
            this.CLibrary.Location = new System.Drawing.Point(6, 29);
            this.CLibrary.Name = "CLibrary";
            this.CLibrary.Size = new System.Drawing.Size(73, 17);
            this.CLibrary.TabIndex = 2;
            this.CLibrary.TabStop = true;
            this.CLibrary.Text = "C# Library";
            this.CLibrary.UseVisualStyleBackColor = true;
            // 
            // ASMLibrary
            // 
            this.ASMLibrary.AutoSize = true;
            this.ASMLibrary.Location = new System.Drawing.Point(6, 64);
            this.ASMLibrary.Name = "ASMLibrary";
            this.ASMLibrary.Size = new System.Drawing.Size(82, 17);
            this.ASMLibrary.TabIndex = 1;
            this.ASMLibrary.Text = "ASM Library";
            this.ASMLibrary.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "Vignette Generator";
            // 
            // RedTrackBar
            // 
            this.RedTrackBar.BackColor = System.Drawing.SystemColors.Control;
            this.RedTrackBar.Location = new System.Drawing.Point(18, 589);
            this.RedTrackBar.Maximum = 20;
            this.RedTrackBar.Name = "RedTrackBar";
            this.RedTrackBar.Size = new System.Drawing.Size(733, 45);
            this.RedTrackBar.TabIndex = 16;
            this.RedTrackBar.Scroll += new System.EventHandler(this.RedTrackBar_Scroll);
            // 
            // GreenTrackBar
            // 
            this.GreenTrackBar.Location = new System.Drawing.Point(18, 663);
            this.GreenTrackBar.Maximum = 20;
            this.GreenTrackBar.Name = "GreenTrackBar";
            this.GreenTrackBar.Size = new System.Drawing.Size(733, 45);
            this.GreenTrackBar.TabIndex = 17;
            this.GreenTrackBar.Scroll += new System.EventHandler(this.GreenTrackBar_Scroll);
            // 
            // BlueTrackBar
            // 
            this.BlueTrackBar.Location = new System.Drawing.Point(18, 739);
            this.BlueTrackBar.Maximum = 20;
            this.BlueTrackBar.Name = "BlueTrackBar";
            this.BlueTrackBar.Size = new System.Drawing.Size(733, 45);
            this.BlueTrackBar.TabIndex = 18;
            this.BlueTrackBar.Scroll += new System.EventHandler(this.BlueTrackBar_Scroll);
            // 
            // RedLabel
            // 
            this.RedLabel.AutoSize = true;
            this.RedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RedLabel.Location = new System.Drawing.Point(14, 566);
            this.RedLabel.Name = "RedLabel";
            this.RedLabel.Size = new System.Drawing.Size(85, 16);
            this.RedLabel.TabIndex = 19;
            this.RedLabel.Text = "Vignette Red";
            // 
            // GreenLabel
            // 
            this.GreenLabel.AutoSize = true;
            this.GreenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GreenLabel.Location = new System.Drawing.Point(14, 640);
            this.GreenLabel.Name = "GreenLabel";
            this.GreenLabel.Size = new System.Drawing.Size(96, 16);
            this.GreenLabel.TabIndex = 20;
            this.GreenLabel.Text = "Vignette Green";
            // 
            // BlueLabel
            // 
            this.BlueLabel.AutoSize = true;
            this.BlueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BlueLabel.Location = new System.Drawing.Point(14, 716);
            this.BlueLabel.Name = "BlueLabel";
            this.BlueLabel.Size = new System.Drawing.Size(86, 16);
            this.BlueLabel.TabIndex = 21;
            this.BlueLabel.Text = "Vignette Blue";
            // 
            // SaveToFileCheckBox
            // 
            this.SaveToFileCheckBox.AutoSize = true;
            this.SaveToFileCheckBox.Location = new System.Drawing.Point(24, 29);
            this.SaveToFileCheckBox.Name = "SaveToFileCheckBox";
            this.SaveToFileCheckBox.Size = new System.Drawing.Size(79, 17);
            this.SaveToFileCheckBox.TabIndex = 23;
            this.SaveToFileCheckBox.Text = "Save to file";
            this.SaveToFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // RaportButton
            // 
            this.RaportButton.Location = new System.Drawing.Point(24, 61);
            this.RaportButton.Name = "RaportButton";
            this.RaportButton.Size = new System.Drawing.Size(75, 23);
            this.RaportButton.TabIndex = 24;
            this.RaportButton.Text = "Generate";
            this.RaportButton.UseVisualStyleBackColor = true;
            this.RaportButton.Click += new System.EventHandler(this.RaportButton_Click);
            // 
            // ProbesUpDown
            // 
            this.ProbesUpDown.Location = new System.Drawing.Point(67, 26);
            this.ProbesUpDown.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.ProbesUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ProbesUpDown.Name = "ProbesUpDown";
            this.ProbesUpDown.Size = new System.Drawing.Size(39, 20);
            this.ProbesUpDown.TabIndex = 25;
            this.ProbesUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.Location = new System.Drawing.Point(21, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Probes";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RaportButton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ProbesUpDown);
            this.groupBox2.Location = new System.Drawing.Point(324, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(119, 90);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generate Report";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ConvertButton);
            this.groupBox3.Controls.Add(this.SaveToFileCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(324, 264);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(119, 90);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Make Vignette";
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 789);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BlueLabel);
            this.Controls.Add(this.GreenLabel);
            this.Controls.Add(this.RedLabel);
            this.Controls.Add(this.BlueTrackBar);
            this.Controls.Add(this.GreenTrackBar);
            this.Controls.Add(this.RedTrackBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ThreadLabel);
            this.Controls.Add(this.ThreadTrackBar);
            this.Controls.Add(this.RadiusTrackBar);
            this.Controls.Add(this.RadiusLabel);
            this.Controls.Add(this.IntensityLabel);
            this.Controls.Add(this.IntensityTrackBar);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.inputImage);
            this.Controls.Add(this.outputImage);
            this.Name = "MainPage";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.outputImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntensityTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadiusTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RedTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProbesUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox outputImage;
        private System.Windows.Forms.PictureBox inputImage;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.TrackBar IntensityTrackBar;
        private System.Windows.Forms.Label IntensityLabel;
        private System.Windows.Forms.Label RadiusLabel;
        private System.Windows.Forms.TrackBar RadiusTrackBar;
        private System.Windows.Forms.TrackBar ThreadTrackBar;
        private System.Windows.Forms.Label ThreadLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton ASMLibrary;
        private System.Windows.Forms.RadioButton CLibrary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ASMTimeLabel;
        private System.Windows.Forms.Label CTimeLabel;
        private System.Windows.Forms.TrackBar RedTrackBar;
        private System.Windows.Forms.TrackBar GreenTrackBar;
        private System.Windows.Forms.TrackBar BlueTrackBar;
        private System.Windows.Forms.Label RedLabel;
        private System.Windows.Forms.Label GreenLabel;
        private System.Windows.Forms.Label BlueLabel;
        private System.Windows.Forms.CheckBox SaveToFileCheckBox;
        private System.Windows.Forms.Button RaportButton;
        private System.Windows.Forms.NumericUpDown ProbesUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

