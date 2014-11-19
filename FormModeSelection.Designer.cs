namespace Frameshop
{
    partial class FormModeSelection
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
            this.rdoColor = new System.Windows.Forms.RadioButton();
            this.btnOk = new System.Windows.Forms.Button();
            this.rdoPlt16 = new System.Windows.Forms.RadioButton();
            this.rdoPlt256 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoColor
            // 
            this.rdoColor.AutoSize = true;
            this.rdoColor.Checked = true;
            this.rdoColor.Location = new System.Drawing.Point(6, 19);
            this.rdoColor.Name = "rdoColor";
            this.rdoColor.Size = new System.Drawing.Size(80, 17);
            this.rdoColor.TabIndex = 1;
            this.rdoColor.TabStop = true;
            this.rdoColor.Text = "&32bit Colors";
            this.rdoColor.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(35, 108);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // rdoPlt16
            // 
            this.rdoPlt16.AutoSize = true;
            this.rdoPlt16.Location = new System.Drawing.Point(6, 42);
            this.rdoPlt16.Name = "rdoPlt16";
            this.rdoPlt16.Size = new System.Drawing.Size(69, 17);
            this.rdoPlt16.TabIndex = 3;
            this.rdoPlt16.Text = "&16 Colors";
            this.rdoPlt16.UseVisualStyleBackColor = true;
            // 
            // rdoPlt256
            // 
            this.rdoPlt256.AutoSize = true;
            this.rdoPlt256.Location = new System.Drawing.Point(6, 65);
            this.rdoPlt256.Name = "rdoPlt256";
            this.rdoPlt256.Size = new System.Drawing.Size(75, 17);
            this.rdoPlt256.TabIndex = 4;
            this.rdoPlt256.Text = "&256 Colors";
            this.rdoPlt256.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoColor);
            this.groupBox1.Controls.Add(this.rdoPlt256);
            this.groupBox1.Controls.Add(this.rdoPlt16);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(98, 90);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // FormModeSelection
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(123, 143);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormModeSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mode Selection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormModeSelection_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdoColor;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RadioButton rdoPlt16;
        private System.Windows.Forms.RadioButton rdoPlt256;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}