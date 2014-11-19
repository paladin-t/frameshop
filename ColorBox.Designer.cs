namespace Frameshop
{
    partial class ColorBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.trackA = new System.Windows.Forms.TrackBar();
            this.numA = new System.Windows.Forms.NumericUpDown();
            this.numR = new System.Windows.Forms.NumericUpDown();
            this.trackR = new System.Windows.Forms.TrackBar();
            this.labelBar = new System.Windows.Forms.Label();
            this.numG = new System.Windows.Forms.NumericUpDown();
            this.trackG = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.numB = new System.Windows.Forms.NumericUpDown();
            this.trackB = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlPreview = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.trackA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackB)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "lableFoo";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "A";
            // 
            // trackA
            // 
            this.trackA.AutoSize = false;
            this.trackA.Location = new System.Drawing.Point(21, 0);
            this.trackA.Maximum = 255;
            this.trackA.Name = "trackA";
            this.trackA.Size = new System.Drawing.Size(104, 20);
            this.trackA.TabIndex = 0;
            this.trackA.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackA.Scroll += new System.EventHandler(this.trackA_Scroll);
            // 
            // numA
            // 
            this.numA.Location = new System.Drawing.Point(131, 0);
            this.numA.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numA.Name = "numA";
            this.numA.Size = new System.Drawing.Size(61, 20);
            this.numA.TabIndex = 4;
            this.numA.ValueChanged += new System.EventHandler(this.numA_ValueChanged);
            // 
            // numR
            // 
            this.numR.Location = new System.Drawing.Point(131, 26);
            this.numR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numR.Name = "numR";
            this.numR.Size = new System.Drawing.Size(61, 20);
            this.numR.TabIndex = 5;
            this.numR.ValueChanged += new System.EventHandler(this.numR_ValueChanged);
            // 
            // trackR
            // 
            this.trackR.AutoSize = false;
            this.trackR.Location = new System.Drawing.Point(21, 26);
            this.trackR.Maximum = 255;
            this.trackR.Name = "trackR";
            this.trackR.Size = new System.Drawing.Size(104, 20);
            this.trackR.TabIndex = 1;
            this.trackR.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackR.Scroll += new System.EventHandler(this.trackR_Scroll);
            // 
            // labelBar
            // 
            this.labelBar.AutoSize = true;
            this.labelBar.Location = new System.Drawing.Point(0, 26);
            this.labelBar.Name = "labelBar";
            this.labelBar.Size = new System.Drawing.Size(15, 13);
            this.labelBar.TabIndex = 9;
            this.labelBar.Text = "R";
            // 
            // numG
            // 
            this.numG.Location = new System.Drawing.Point(131, 52);
            this.numG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numG.Name = "numG";
            this.numG.Size = new System.Drawing.Size(61, 20);
            this.numG.TabIndex = 6;
            this.numG.ValueChanged += new System.EventHandler(this.numG_ValueChanged);
            // 
            // trackG
            // 
            this.trackG.AutoSize = false;
            this.trackG.Location = new System.Drawing.Point(21, 52);
            this.trackG.Maximum = 255;
            this.trackG.Name = "trackG";
            this.trackG.Size = new System.Drawing.Size(104, 20);
            this.trackG.TabIndex = 2;
            this.trackG.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackG.Scroll += new System.EventHandler(this.trackG_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "G";
            // 
            // numB
            // 
            this.numB.Location = new System.Drawing.Point(131, 78);
            this.numB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numB.Name = "numB";
            this.numB.Size = new System.Drawing.Size(61, 20);
            this.numB.TabIndex = 7;
            this.numB.ValueChanged += new System.EventHandler(this.numB_ValueChanged);
            // 
            // trackB
            // 
            this.trackB.AutoSize = false;
            this.trackB.Location = new System.Drawing.Point(21, 78);
            this.trackB.Maximum = 255;
            this.trackB.Name = "trackB";
            this.trackB.Size = new System.Drawing.Size(104, 20);
            this.trackB.TabIndex = 3;
            this.trackB.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackB.Scroll += new System.EventHandler(this.trackB_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "B";
            // 
            // pnlPreview
            // 
            this.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreview.Location = new System.Drawing.Point(3, 104);
            this.pnlPreview.Name = "pnlPreview";
            this.pnlPreview.Size = new System.Drawing.Size(189, 32);
            this.pnlPreview.TabIndex = 12;
            // 
            // ColorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPreview);
            this.Controls.Add(this.numB);
            this.Controls.Add(this.trackB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numG);
            this.Controls.Add(this.trackG);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numR);
            this.Controls.Add(this.trackR);
            this.Controls.Add(this.labelBar);
            this.Controls.Add(this.numA);
            this.Controls.Add(this.trackA);
            this.Controls.Add(this.label1);
            this.Name = "ColorBox";
            this.Size = new System.Drawing.Size(195, 139);
            ((System.ComponentModel.ISupportInitialize)(this.trackA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackA;
        private System.Windows.Forms.NumericUpDown numA;
        private System.Windows.Forms.NumericUpDown numR;
        private System.Windows.Forms.TrackBar trackR;
        private System.Windows.Forms.Label labelBar;
        private System.Windows.Forms.NumericUpDown numG;
        private System.Windows.Forms.TrackBar trackG;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numB;
        private System.Windows.Forms.TrackBar trackB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlPreview;
    }
}
