namespace Frameshop
{
    partial class FormAnimation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnimation));
            this.panelFoo = new System.Windows.Forms.Panel();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.picView = new Frameshop.AnimBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.treePages = new System.Windows.Forms.TreeView();
            this.menuPages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuAddToSeq = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListPagesTree = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInsertBlankFrame = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMoveMode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuadMode1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuadMode2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuadMode3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuadMode4 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuQuadMode5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuShowCenterPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportGif = new System.Windows.Forms.ToolStripMenuItem();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeSeq = new System.Windows.Forms.TreeView();
            this.menuAnims = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuRemoveFromSeq = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.propSeq = new System.Windows.Forms.PropertyGrid();
            this.panelFoo.SuspendLayout();
            this.panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picView)).BeginInit();
            this.menuPages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.menuAnims.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFoo
            // 
            this.panelFoo.Controls.Add(this.panelCenter);
            this.panelFoo.Controls.Add(this.lblFilePath);
            this.panelFoo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFoo.Location = new System.Drawing.Point(0, 0);
            this.panelFoo.Name = "panelFoo";
            this.panelFoo.Size = new System.Drawing.Size(307, 399);
            this.panelFoo.TabIndex = 2;
            // 
            // panelCenter
            // 
            this.panelCenter.AutoScroll = true;
            this.panelCenter.Controls.Add(this.picView);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 12);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(307, 387);
            this.panelCenter.TabIndex = 2;
            this.panelCenter.SizeChanged += new System.EventHandler(this.panelCenter_SizeChanged);
            // 
            // picView
            // 
            this.picView.BackColor = System.Drawing.Color.White;
            this.picView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picView.Frame = null;
            this.picView.Image = null;
            this.picView.IndexInSequence = -1;
            this.picView.Location = new System.Drawing.Point(17, 15);
            this.picView.Name = "picView";
            this.picView.Offset = new System.Drawing.Point(0, 0);
            this.picView.ShowCenterPoint = true;
            this.picView.Size = new System.Drawing.Size(66, 66);
            this.picView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picView.State = Frameshop.AnimBoxState.Move;
            this.picView.TabIndex = 0;
            this.picView.TabStop = false;
            this.picView.WorkingSequence = null;
            this.picView.AnimBoxContentSizeChanged += new System.EventHandler(this.picView_AnimBoxContentSizeChanged);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFilePath.Location = new System.Drawing.Point(0, 0);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(47, 12);
            this.lblFilePath.TabIndex = 1;
            this.lblFilePath.Text = "Preview";
            // 
            // treePages
            // 
            this.treePages.ContextMenuStrip = this.menuPages;
            this.treePages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePages.ImageIndex = 0;
            this.treePages.ImageList = this.imageListPagesTree;
            this.treePages.Location = new System.Drawing.Point(0, 12);
            this.treePages.Name = "treePages";
            this.treePages.SelectedImageIndex = 0;
            this.treePages.Size = new System.Drawing.Size(153, 387);
            this.treePages.TabIndex = 1;
            // 
            // menuPages
            // 
            this.menuPages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAddToSeq});
            this.menuPages.Name = "menuPages";
            this.menuPages.Size = new System.Drawing.Size(177, 26);
            // 
            // menuAddToSeq
            // 
            this.menuAddToSeq.Name = "menuAddToSeq";
            this.menuAddToSeq.Size = new System.Drawing.Size(176, 22);
            this.menuAddToSeq.Text = "&Add to Sequence";
            this.menuAddToSeq.Click += new System.EventHandler(this.menuAddToSeq_Click);
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelFoo);
            this.splitContainer1.Size = new System.Drawing.Size(464, 399);
            this.splitContainer1.SplitterDistance = 153;
            this.splitContainer1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pages";
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuPlay,
            this.menuExport});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(622, 25);
            this.menuMain.TabIndex = 5;
            this.menuMain.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew,
            this.menuOpen,
            this.toolStripSeparator1,
            this.menuSave,
            this.toolStripSeparator2,
            this.menuClose});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(39, 21);
            this.menuFile.Text = "&File";
            // 
            // menuNew
            // 
            this.menuNew.Name = "menuNew";
            this.menuNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuNew.Size = new System.Drawing.Size(155, 22);
            this.menuNew.Text = "&New";
            this.menuNew.Click += new System.EventHandler(this.menuNew_Click);
            // 
            // menuOpen
            // 
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOpen.Size = new System.Drawing.Size(155, 22);
            this.menuOpen.Text = "&Open";
            this.menuOpen.Click += new System.EventHandler(this.menuOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuSave.Size = new System.Drawing.Size(155, 22);
            this.menuSave.Text = "&Save";
            this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // menuClose
            // 
            this.menuClose.Name = "menuClose";
            this.menuClose.Size = new System.Drawing.Size(155, 22);
            this.menuClose.Text = "&Close";
            this.menuClose.Click += new System.EventHandler(this.menuClose_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuInsertBlankFrame,
            this.toolStripSeparator4,
            this.menuMoveMode,
            this.menuQuadMode1,
            this.menuQuadMode2,
            this.menuQuadMode3,
            this.menuQuadMode4,
            this.menuQuadMode5,
            this.toolStripSeparator3,
            this.menuShowCenterPoint});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(42, 21);
            this.menuEdit.Text = "&Edit";
            // 
            // menuInsertBlankFrame
            // 
            this.menuInsertBlankFrame.Name = "menuInsertBlankFrame";
            this.menuInsertBlankFrame.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.menuInsertBlankFrame.Size = new System.Drawing.Size(227, 22);
            this.menuInsertBlankFrame.Text = "&Insert Blank Frame";
            this.menuInsertBlankFrame.Click += new System.EventHandler(this.menuInsertBlankFrame_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(224, 6);
            // 
            // menuMoveMode
            // 
            this.menuMoveMode.Checked = true;
            this.menuMoveMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuMoveMode.Name = "menuMoveMode";
            this.menuMoveMode.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.menuMoveMode.Size = new System.Drawing.Size(227, 22);
            this.menuMoveMode.Text = "&Move Mode";
            this.menuMoveMode.Click += new System.EventHandler(this.menuMoveMode_Click);
            // 
            // menuQuadMode1
            // 
            this.menuQuadMode1.Name = "menuQuadMode1";
            this.menuQuadMode1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.menuQuadMode1.Size = new System.Drawing.Size(227, 22);
            this.menuQuadMode1.Text = "Quad Mode &1";
            this.menuQuadMode1.Click += new System.EventHandler(this.menuQuadMode1_Click);
            // 
            // menuQuadMode2
            // 
            this.menuQuadMode2.Name = "menuQuadMode2";
            this.menuQuadMode2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.menuQuadMode2.Size = new System.Drawing.Size(227, 22);
            this.menuQuadMode2.Text = "Quad Mode &2";
            this.menuQuadMode2.Click += new System.EventHandler(this.menuQuadMode2_Click);
            // 
            // menuQuadMode3
            // 
            this.menuQuadMode3.Name = "menuQuadMode3";
            this.menuQuadMode3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.menuQuadMode3.Size = new System.Drawing.Size(227, 22);
            this.menuQuadMode3.Text = "Quad Mode &3";
            this.menuQuadMode3.Click += new System.EventHandler(this.menuQuadMode3_Click);
            // 
            // menuQuadMode4
            // 
            this.menuQuadMode4.Name = "menuQuadMode4";
            this.menuQuadMode4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.menuQuadMode4.Size = new System.Drawing.Size(227, 22);
            this.menuQuadMode4.Text = "Quad Mode &4";
            this.menuQuadMode4.Click += new System.EventHandler(this.menuQuadMode4_Click);
            // 
            // menuQuadMode5
            // 
            this.menuQuadMode5.Name = "menuQuadMode5";
            this.menuQuadMode5.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.menuQuadMode5.Size = new System.Drawing.Size(227, 22);
            this.menuQuadMode5.Text = "Quad Mode &5";
            this.menuQuadMode5.Click += new System.EventHandler(this.menuQuadMode5_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(224, 6);
            // 
            // menuShowCenterPoint
            // 
            this.menuShowCenterPoint.Checked = true;
            this.menuShowCenterPoint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuShowCenterPoint.Name = "menuShowCenterPoint";
            this.menuShowCenterPoint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.menuShowCenterPoint.Size = new System.Drawing.Size(227, 22);
            this.menuShowCenterPoint.Text = "&Show Center Point";
            this.menuShowCenterPoint.Click += new System.EventHandler(this.menuShowCenterPoint_Click);
            // 
            // menuPlay
            // 
            this.menuPlay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuPreview});
            this.menuPlay.Name = "menuPlay";
            this.menuPlay.Size = new System.Drawing.Size(43, 21);
            this.menuPlay.Text = "&Play";
            // 
            // menuPreview
            // 
            this.menuPreview.Name = "menuPreview";
            this.menuPreview.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.menuPreview.Size = new System.Drawing.Size(164, 22);
            this.menuPreview.Text = "&Preview";
            this.menuPreview.Click += new System.EventHandler(this.menuPreview_Click);
            // 
            // menuExport
            // 
            this.menuExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExportGif});
            this.menuExport.Name = "menuExport";
            this.menuExport.Size = new System.Drawing.Size(58, 21);
            this.menuExport.Text = "E&xport";
            // 
            // menuExportGif
            // 
            this.menuExportGif.Name = "menuExportGif";
            this.menuExportGif.Size = new System.Drawing.Size(153, 22);
            this.menuExportGif.Text = "Export to &GIF";
            this.menuExportGif.Click += new System.EventHandler(this.menuExportGif_Click);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 25);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitContainer3);
            this.splitMain.Size = new System.Drawing.Size(622, 399);
            this.splitMain.SplitterDistance = 464;
            this.splitMain.TabIndex = 6;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeSeq);
            this.splitContainer3.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.propSeq);
            this.splitContainer3.Size = new System.Drawing.Size(154, 399);
            this.splitContainer3.SplitterDistance = 216;
            this.splitContainer3.TabIndex = 4;
            // 
            // treeSeq
            // 
            this.treeSeq.AllowDrop = true;
            this.treeSeq.ContextMenuStrip = this.menuAnims;
            this.treeSeq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeSeq.ImageIndex = 2;
            this.treeSeq.ImageList = this.imageListPagesTree;
            this.treeSeq.Location = new System.Drawing.Point(0, 12);
            this.treeSeq.Name = "treeSeq";
            this.treeSeq.SelectedImageIndex = 3;
            this.treeSeq.Size = new System.Drawing.Size(154, 204);
            this.treeSeq.TabIndex = 3;
            // 
            // menuAnims
            // 
            this.menuAnims.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRemoveFromSeq});
            this.menuAnims.Name = "menuPages";
            this.menuAnims.Size = new System.Drawing.Size(216, 26);
            // 
            // menuRemoveFromSeq
            // 
            this.menuRemoveFromSeq.Name = "menuRemoveFromSeq";
            this.menuRemoveFromSeq.Size = new System.Drawing.Size(215, 22);
            this.menuRemoveFromSeq.Text = "&Remove from Sequence";
            this.menuRemoveFromSeq.Click += new System.EventHandler(this.menuRemoveFromSeq_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sequence";
            // 
            // propSeq
            // 
            this.propSeq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propSeq.Location = new System.Drawing.Point(0, 0);
            this.propSeq.Name = "propSeq";
            this.propSeq.Size = new System.Drawing.Size(154, 179);
            this.propSeq.TabIndex = 0;
            this.propSeq.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propSeq_PropertyValueChanged);
            // 
            // FormAnimation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 424);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.menuMain);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormAnimation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Animation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAnimation_FormClosing);
            this.Load += new System.EventHandler(this.FormAnimation_Load);
            this.panelFoo.ResumeLayout(false);
            this.panelFoo.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picView)).EndInit();
            this.menuPages.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.menuAnims.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelFoo;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.TreeView treePages;
        private System.Windows.Forms.ImageList imageListPagesTree;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private Frameshop.AnimBox picView;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.TreeView treeSeq;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.PropertyGrid propSeq;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuClose;
        private System.Windows.Forms.ContextMenuStrip menuPages;
        private System.Windows.Forms.ToolStripMenuItem menuAddToSeq;
        private System.Windows.Forms.ContextMenuStrip menuAnims;
        private System.Windows.Forms.ToolStripMenuItem menuRemoveFromSeq;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuMoveMode;
        private System.Windows.Forms.ToolStripMenuItem menuQuadMode1;
        private System.Windows.Forms.ToolStripMenuItem menuQuadMode2;
        private System.Windows.Forms.ToolStripMenuItem menuQuadMode3;
        private System.Windows.Forms.ToolStripMenuItem menuQuadMode4;
        private System.Windows.Forms.ToolStripMenuItem menuQuadMode5;
        private System.Windows.Forms.ToolStripMenuItem menuPlay;
        private System.Windows.Forms.ToolStripMenuItem menuPreview;
        private System.Windows.Forms.ToolStripMenuItem menuExport;
        private System.Windows.Forms.ToolStripMenuItem menuExportGif;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuShowCenterPoint;
        private System.Windows.Forms.ToolStripMenuItem menuInsertBlankFrame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}