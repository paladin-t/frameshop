namespace Frameshop
{
    partial class FormOutput
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
            this.toolStripOutput = new System.Windows.Forms.ToolStrip();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnNewline = new System.Windows.Forms.ToolStripButton();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.toolStripOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripOutput
            // 
            this.toolStripOutput.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripOutput.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClear,
            this.btnNewline});
            this.toolStripOutput.Location = new System.Drawing.Point(0, 0);
            this.toolStripOutput.Name = "toolStripOutput";
            this.toolStripOutput.Size = new System.Drawing.Size(292, 25);
            this.toolStripOutput.TabIndex = 0;
            this.toolStripOutput.Text = "toolStrip1";
            // 
            // btnClear
            // 
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClear.Image = global::Frameshop.Properties.Resources.cls;
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(23, 22);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnNewline
            // 
            this.btnNewline.Checked = true;
            this.btnNewline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnNewline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewline.Image = global::Frameshop.Properties.Resources.newline;
            this.btnNewline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewline.Name = "btnNewline";
            this.btnNewline.Size = new System.Drawing.Size(23, 22);
            this.btnNewline.Text = "Newline";
            this.btnNewline.Click += new System.EventHandler(this.btnNewline_Click);
            // 
            // txtOut
            // 
            this.txtOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOut.Location = new System.Drawing.Point(0, 25);
            this.txtOut.Multiline = true;
            this.txtOut.Name = "txtOut";
            this.txtOut.ReadOnly = true;
            this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOut.Size = new System.Drawing.Size(292, 241);
            this.txtOut.TabIndex = 1;
            this.txtOut.WordWrap = false;
            // 
            // FormOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.txtOut);
            this.Controls.Add(this.toolStripOutput);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormOutput";
            this.Text = "Output";
            this.toolStripOutput.ResumeLayout(false);
            this.toolStripOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripOutput;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.TextBox txtOut;
        private System.Windows.Forms.ToolStripButton btnNewline;
    }
}