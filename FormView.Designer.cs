namespace Frameshop
{
    partial class FormView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormView));
            this.panelFoo = new System.Windows.Forms.Panel();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.atlasBox = new Frameshop.AtlasBox();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuRemoveFrame = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.panelBar = new System.Windows.Forms.Panel();
            this.cmbMag = new System.Windows.Forms.ComboBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblPage = new System.Windows.Forms.Label();
            this.numPage = new System.Windows.Forms.NumericUpDown();
            this.btnRemovePage = new System.Windows.Forms.Button();
            this.btnAddPage = new System.Windows.Forms.Button();
            this.treePages = new System.Windows.Forms.TreeView();
            this.contextMenuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuRemoveFrame_Tree = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListPagesTree = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.panelFoo.SuspendLayout();
            this.panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.atlasBox)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.panelBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPage)).BeginInit();
            this.contextMenuTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFoo
            // 
            this.panelFoo.Controls.Add(this.panelCenter);
            this.panelFoo.Controls.Add(this.lblFilePath);
            this.panelFoo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFoo.Location = new System.Drawing.Point(0, 0);
            this.panelFoo.Name = "panelFoo";
            this.panelFoo.Size = new System.Drawing.Size(478, 403);
            this.panelFoo.TabIndex = 2;
            // 
            // panelCenter
            // 
            this.panelCenter.AutoScroll = true;
            this.panelCenter.Controls.Add(this.atlasBox);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 13);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(478, 390);
            this.panelCenter.TabIndex = 2;
            // 
            // atlasBox
            // 
            this.atlasBox.BackColor = System.Drawing.Color.White;
            this.atlasBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.atlasBox.ContextMenuStrip = this.contextMenu;
            this.atlasBox.Image = null;
            this.atlasBox.Location = new System.Drawing.Point(3, 3);
            this.atlasBox.MouseOver = -1;
            this.atlasBox.Name = "atlasBox";
            this.atlasBox.PageIndex = 0;
            this.atlasBox.Size = new System.Drawing.Size(200, 200);
            this.atlasBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.atlasBox.TabIndex = 0;
            this.atlasBox.TabStop = false;
            this.atlasBox.Zoom = 1F;
            this.atlasBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.atlasBox_MouseMove);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRemoveFrame});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(167, 26);
            // 
            // menuRemoveFrame
            // 
            this.menuRemoveFrame.Name = "menuRemoveFrame";
            this.menuRemoveFrame.Size = new System.Drawing.Size(166, 22);
            this.menuRemoveFrame.Text = "&Remove from Page";
            this.menuRemoveFrame.Click += new System.EventHandler(this.menuRemoveFrame_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFilePath.Location = new System.Drawing.Point(0, 0);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(44, 13);
            this.lblFilePath.TabIndex = 1;
            this.lblFilePath.Text = "Content";
            // 
            // panelBar
            // 
            this.panelBar.Controls.Add(this.cmbMag);
            this.panelBar.Controls.Add(this.lblTotal);
            this.panelBar.Controls.Add(this.lblPage);
            this.panelBar.Controls.Add(this.numPage);
            this.panelBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBar.Location = new System.Drawing.Point(0, 426);
            this.panelBar.Name = "panelBar";
            this.panelBar.Size = new System.Drawing.Size(721, 20);
            this.panelBar.TabIndex = 3;
            // 
            // cmbMag
            // 
            this.cmbMag.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbMag.FormattingEnabled = true;
            this.cmbMag.Location = new System.Drawing.Point(0, 0);
            this.cmbMag.Name = "cmbMag";
            this.cmbMag.Size = new System.Drawing.Size(85, 21);
            this.cmbMag.TabIndex = 1;
            this.cmbMag.SelectedIndexChanged += new System.EventHandler(this.cmbMag_SelectedIndexChanged);
            this.cmbMag.TextChanged += new System.EventHandler(this.cmbMag_TextChanged);
            this.cmbMag.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMag_KeyPress);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotal.Location = new System.Drawing.Point(521, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Padding = new System.Windows.Forms.Padding(0, 2, 5, 0);
            this.lblTotal.Size = new System.Drawing.Size(45, 15);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total:0";
            // 
            // lblPage
            // 
            this.lblPage.AutoSize = true;
            this.lblPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPage.Location = new System.Drawing.Point(566, 0);
            this.lblPage.Name = "lblPage";
            this.lblPage.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblPage.Size = new System.Drawing.Size(35, 15);
            this.lblPage.TabIndex = 2;
            this.lblPage.Text = "Page:";
            // 
            // numPage
            // 
            this.numPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.numPage.Location = new System.Drawing.Point(601, 0);
            this.numPage.Name = "numPage";
            this.numPage.Size = new System.Drawing.Size(120, 20);
            this.numPage.TabIndex = 3;
            this.numPage.ValueChanged += new System.EventHandler(this.numPage_ValueChanged);
            // 
            // btnRemovePage
            // 
            this.btnRemovePage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRemovePage.Location = new System.Drawing.Point(0, 403);
            this.btnRemovePage.Name = "btnRemovePage";
            this.btnRemovePage.Size = new System.Drawing.Size(478, 23);
            this.btnRemovePage.TabIndex = 1;
            this.btnRemovePage.TabStop = false;
            this.btnRemovePage.Text = "-";
            this.btnRemovePage.UseVisualStyleBackColor = true;
            this.btnRemovePage.Click += new System.EventHandler(this.btnRemovePage_Click);
            // 
            // btnAddPage
            // 
            this.btnAddPage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddPage.Location = new System.Drawing.Point(0, 403);
            this.btnAddPage.Name = "btnAddPage";
            this.btnAddPage.Size = new System.Drawing.Size(239, 23);
            this.btnAddPage.TabIndex = 0;
            this.btnAddPage.TabStop = false;
            this.btnAddPage.Text = "+";
            this.btnAddPage.UseVisualStyleBackColor = true;
            this.btnAddPage.Click += new System.EventHandler(this.btnAddPage_Click);
            // 
            // treePages
            // 
            this.treePages.ContextMenuStrip = this.contextMenuTree;
            this.treePages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePages.ImageIndex = 0;
            this.treePages.ImageList = this.imageListPagesTree;
            this.treePages.Location = new System.Drawing.Point(0, 13);
            this.treePages.Name = "treePages";
            this.treePages.SelectedImageIndex = 0;
            this.treePages.Size = new System.Drawing.Size(239, 390);
            this.treePages.TabIndex = 1;
            // 
            // contextMenuTree
            // 
            this.contextMenuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRemoveFrame_Tree});
            this.contextMenuTree.Name = "contextMenu";
            this.contextMenuTree.Size = new System.Drawing.Size(167, 26);
            // 
            // menuRemoveFrame_Tree
            // 
            this.menuRemoveFrame_Tree.Name = "menuRemoveFrame_Tree";
            this.menuRemoveFrame_Tree.Size = new System.Drawing.Size(166, 22);
            this.menuRemoveFrame_Tree.Text = "&Remove from Page";
            this.menuRemoveFrame_Tree.Click += new System.EventHandler(this.menuRemoveFrame_Tree_Click);
            // 
            // imageListPagesTree
            // 
            this.imageListPagesTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPagesTree.ImageStream")));
            this.imageListPagesTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPagesTree.Images.SetKeyName(0, "tree_page_node.png");
            this.imageListPagesTree.Images.SetKeyName(1, "tree_page_node_sel.png");
            this.imageListPagesTree.Images.SetKeyName(2, "tree_frame_node.png");
            this.imageListPagesTree.Images.SetKeyName(3, "tree_frame_node_sel.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treePages);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnAddPage);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelFoo);
            this.splitContainer1.Panel2.Controls.Add(this.btnRemovePage);
            this.splitContainer1.Size = new System.Drawing.Size(721, 426);
            this.splitContainer1.SplitterDistance = 239;
            this.splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pages";
            // 
            // FormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 446);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelBar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormView";
            this.Text = "View";
            this.Load += new System.EventHandler(this.FormView_Load);
            this.panelFoo.ResumeLayout(false);
            this.panelFoo.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.atlasBox)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.panelBar.ResumeLayout(false);
            this.panelBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPage)).EndInit();
            this.contextMenuTree.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AtlasBox atlasBox;
        private System.Windows.Forms.Panel panelFoo;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Panel panelBar;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.NumericUpDown numPage;
        private System.Windows.Forms.Button btnRemovePage;
        private System.Windows.Forms.Button btnAddPage;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TreeView treePages;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageListPagesTree;
        private System.Windows.Forms.ComboBox cmbMag;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuRemoveFrame;
        private System.Windows.Forms.ContextMenuStrip contextMenuTree;
        private System.Windows.Forms.ToolStripMenuItem menuRemoveFrame_Tree;

    }
}