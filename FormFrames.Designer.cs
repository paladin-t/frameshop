namespace Frameshop
{
    partial class FormFrames
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
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panelFoo = new System.Windows.Forms.Panel();
            this.cmbFilter = new Frameshop.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.panelFoo.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowPanel
            // 
            this.flowPanel.AllowDrop = true;
            this.flowPanel.AutoScroll = true;
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Location = new System.Drawing.Point(0, 22);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(581, 298);
            this.flowPanel.TabIndex = 0;
            this.flowPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowPanel_DragDrop);
            this.flowPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowPanel_DragEnter);
            // 
            // panelFoo
            // 
            this.panelFoo.Controls.Add(this.cmbFilter);
            this.panelFoo.Controls.Add(this.label1);
            this.panelFoo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFoo.Location = new System.Drawing.Point(0, 0);
            this.panelFoo.Name = "panelFoo";
            this.panelFoo.Size = new System.Drawing.Size(581, 22);
            this.panelFoo.TabIndex = 1;
            // 
            // cmbFilter
            // 
            this.cmbFilter.BlankText = "Blank";
            this.cmbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilter.ForeColor = System.Drawing.Color.Black;
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Location = new System.Drawing.Point(32, 0);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(549, 21);
            this.cmbFilter.TabIndex = 1;
            this.cmbFilter.Text = "Blank";
            this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            this.cmbFilter.TextChanged += new System.EventHandler(this.cmbFilter_TextChanged);
            this.cmbFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(cmbFilter_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter:";
            // 
            // FormFrames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 320);
            this.Controls.Add(this.flowPanel);
            this.Controls.Add(this.panelFoo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormFrames";
            this.Text = "Frames";
            this.Load += new System.EventHandler(this.FormFrames_Load);
            this.panelFoo.ResumeLayout(false);
            this.panelFoo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.Panel panelFoo;
        private ComboBoxEx cmbFilter;
        private System.Windows.Forms.Label label1;
    }
}