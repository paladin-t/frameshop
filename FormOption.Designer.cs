namespace Frameshop
{
    partial class FormOption
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
            this.tabOptions = new System.Windows.Forms.TabControl();
            this.tabPostEvent = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.ckbConvertAnim = new System.Windows.Forms.CheckBox();
            this.btnWhatConvertAnim = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cmbAnim = new System.Windows.Forms.ComboBox();
            this.btnEditAnim = new System.Windows.Forms.Button();
            this.btnWhatAnim = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbData = new System.Windows.Forms.ComboBox();
            this.btnEditData = new System.Windows.Forms.Button();
            this.btnWhatData = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbTexture = new System.Windows.Forms.ComboBox();
            this.btnEditTexture = new System.Windows.Forms.Button();
            this.btnWhatTex = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbFolder = new System.Windows.Forms.ComboBox();
            this.btnEditFolder = new System.Windows.Forms.Button();
            this.btnWhatFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabMisc = new System.Windows.Forms.TabPage();
            this.panel9 = new System.Windows.Forms.Panel();
            this.ckbAddFolderRecursively = new System.Windows.Forms.CheckBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.ckbAskBeforeRemoveFrame = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ckbFileRel = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabOptions.SuspendLayout();
            this.tabPostEvent.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabMisc.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.tabPostEvent);
            this.tabOptions.Controls.Add(this.tabMisc);
            this.tabOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOptions.Location = new System.Drawing.Point(0, 0);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.SelectedIndex = 0;
            this.tabOptions.Size = new System.Drawing.Size(418, 195);
            this.tabOptions.TabIndex = 0;
            // 
            // tabPostEvent
            // 
            this.tabPostEvent.Controls.Add(this.panel7);
            this.tabPostEvent.Controls.Add(this.panel6);
            this.tabPostEvent.Controls.Add(this.panel4);
            this.tabPostEvent.Controls.Add(this.panel3);
            this.tabPostEvent.Controls.Add(this.panel2);
            this.tabPostEvent.Location = new System.Drawing.Point(4, 22);
            this.tabPostEvent.Name = "tabPostEvent";
            this.tabPostEvent.Padding = new System.Windows.Forms.Padding(3);
            this.tabPostEvent.Size = new System.Drawing.Size(410, 169);
            this.tabPostEvent.TabIndex = 1;
            this.tabPostEvent.Text = "Post Event";
            this.tabPostEvent.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.ckbConvertAnim);
            this.panel7.Controls.Add(this.btnWhatConvertAnim);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(3, 131);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(404, 20);
            this.panel7.TabIndex = 8;
            // 
            // ckbConvertAnim
            // 
            this.ckbConvertAnim.AutoSize = true;
            this.ckbConvertAnim.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckbConvertAnim.Location = new System.Drawing.Point(0, 0);
            this.ckbConvertAnim.Name = "ckbConvertAnim";
            this.ckbConvertAnim.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.ckbConvertAnim.Size = new System.Drawing.Size(382, 19);
            this.ckbConvertAnim.TabIndex = 8;
            this.ckbConvertAnim.Text = "Call animation command when publishing";
            this.ckbConvertAnim.UseVisualStyleBackColor = true;
            this.ckbConvertAnim.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            // 
            // btnWhatConvertAnim
            // 
            this.btnWhatConvertAnim.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWhatConvertAnim.Location = new System.Drawing.Point(382, 0);
            this.btnWhatConvertAnim.Name = "btnWhatConvertAnim";
            this.btnWhatConvertAnim.Size = new System.Drawing.Size(22, 20);
            this.btnWhatConvertAnim.TabIndex = 2;
            this.btnWhatConvertAnim.Text = "?";
            this.btnWhatConvertAnim.UseVisualStyleBackColor = true;
            this.btnWhatConvertAnim.Click += new System.EventHandler(this.btnWhatConvertAnim_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cmbAnim);
            this.panel6.Controls.Add(this.btnEditAnim);
            this.panel6.Controls.Add(this.btnWhatAnim);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 99);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(404, 32);
            this.panel6.TabIndex = 6;
            // 
            // cmbAnim
            // 
            this.cmbAnim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbAnim.FormattingEnabled = true;
            this.cmbAnim.Location = new System.Drawing.Point(59, 0);
            this.cmbAnim.Name = "cmbAnim";
            this.cmbAnim.Size = new System.Drawing.Size(248, 20);
            this.cmbAnim.TabIndex = 0;
            this.cmbAnim.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // btnEditAnim
            // 
            this.btnEditAnim.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEditAnim.Location = new System.Drawing.Point(307, 0);
            this.btnEditAnim.Name = "btnEditAnim";
            this.btnEditAnim.Size = new System.Drawing.Size(75, 20);
            this.btnEditAnim.TabIndex = 1;
            this.btnEditAnim.Text = "Edit";
            this.btnEditAnim.UseVisualStyleBackColor = true;
            this.btnEditAnim.Click += new System.EventHandler(this.btnEditAnim_Click);
            // 
            // btnWhatAnim
            // 
            this.btnWhatAnim.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWhatAnim.Location = new System.Drawing.Point(382, 0);
            this.btnWhatAnim.Name = "btnWhatAnim";
            this.btnWhatAnim.Size = new System.Drawing.Size(22, 20);
            this.btnWhatAnim.TabIndex = 2;
            this.btnWhatAnim.Text = "?";
            this.btnWhatAnim.UseVisualStyleBackColor = true;
            this.btnWhatAnim.Click += new System.EventHandler(this.btnWhatAnim_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label7.Size = new System.Drawing.Size(59, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Animation";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(0, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(215, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "Command to process animation files.";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cmbData);
            this.panel4.Controls.Add(this.btnEditData);
            this.panel4.Controls.Add(this.btnWhatData);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 67);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(404, 32);
            this.panel4.TabIndex = 5;
            // 
            // cmbData
            // 
            this.cmbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbData.FormattingEnabled = true;
            this.cmbData.Location = new System.Drawing.Point(59, 0);
            this.cmbData.Name = "cmbData";
            this.cmbData.Size = new System.Drawing.Size(248, 20);
            this.cmbData.TabIndex = 0;
            this.cmbData.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // btnEditData
            // 
            this.btnEditData.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEditData.Location = new System.Drawing.Point(307, 0);
            this.btnEditData.Name = "btnEditData";
            this.btnEditData.Size = new System.Drawing.Size(75, 20);
            this.btnEditData.TabIndex = 1;
            this.btnEditData.Text = "Edit";
            this.btnEditData.UseVisualStyleBackColor = true;
            this.btnEditData.Click += new System.EventHandler(this.btnEditData_Click);
            // 
            // btnWhatData
            // 
            this.btnWhatData.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWhatData.Location = new System.Drawing.Point(382, 0);
            this.btnWhatData.Name = "btnWhatData";
            this.btnWhatData.Size = new System.Drawing.Size(22, 20);
            this.btnWhatData.TabIndex = 2;
            this.btnWhatData.Text = "?";
            this.btnWhatData.UseVisualStyleBackColor = true;
            this.btnWhatData.Click += new System.EventHandler(this.btnWhatData_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label5.Size = new System.Drawing.Size(59, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Data     ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(0, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "Command to process data files.";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbTexture);
            this.panel3.Controls.Add(this.btnEditTexture);
            this.panel3.Controls.Add(this.btnWhatTex);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(404, 32);
            this.panel3.TabIndex = 4;
            // 
            // cmbTexture
            // 
            this.cmbTexture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTexture.FormattingEnabled = true;
            this.cmbTexture.Location = new System.Drawing.Point(59, 0);
            this.cmbTexture.Name = "cmbTexture";
            this.cmbTexture.Size = new System.Drawing.Size(248, 20);
            this.cmbTexture.TabIndex = 0;
            this.cmbTexture.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // btnEditTexture
            // 
            this.btnEditTexture.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEditTexture.Location = new System.Drawing.Point(307, 0);
            this.btnEditTexture.Name = "btnEditTexture";
            this.btnEditTexture.Size = new System.Drawing.Size(75, 20);
            this.btnEditTexture.TabIndex = 1;
            this.btnEditTexture.Text = "Edit";
            this.btnEditTexture.UseVisualStyleBackColor = true;
            this.btnEditTexture.Click += new System.EventHandler(this.btnEditTexture_Click);
            // 
            // btnWhatTex
            // 
            this.btnWhatTex.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWhatTex.Location = new System.Drawing.Point(382, 0);
            this.btnWhatTex.Name = "btnWhatTex";
            this.btnWhatTex.Size = new System.Drawing.Size(22, 20);
            this.btnWhatTex.TabIndex = 2;
            this.btnWhatTex.Text = "?";
            this.btnWhatTex.UseVisualStyleBackColor = true;
            this.btnWhatTex.Click += new System.EventHandler(this.btnWhatTex_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Texture  ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(0, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Command to process textures.";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbFolder);
            this.panel2.Controls.Add(this.btnEditFolder);
            this.panel2.Controls.Add(this.btnWhatFolder);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(404, 32);
            this.panel2.TabIndex = 3;
            // 
            // cmbFolder
            // 
            this.cmbFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFolder.FormattingEnabled = true;
            this.cmbFolder.Location = new System.Drawing.Point(59, 0);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(248, 20);
            this.cmbFolder.TabIndex = 0;
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // btnEditFolder
            // 
            this.btnEditFolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEditFolder.Location = new System.Drawing.Point(307, 0);
            this.btnEditFolder.Name = "btnEditFolder";
            this.btnEditFolder.Size = new System.Drawing.Size(75, 20);
            this.btnEditFolder.TabIndex = 1;
            this.btnEditFolder.Text = "Edit";
            this.btnEditFolder.UseVisualStyleBackColor = true;
            this.btnEditFolder.Click += new System.EventHandler(this.btnEditFolder_Click);
            // 
            // btnWhatFolder
            // 
            this.btnWhatFolder.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWhatFolder.Location = new System.Drawing.Point(382, 0);
            this.btnWhatFolder.Name = "btnWhatFolder";
            this.btnWhatFolder.Size = new System.Drawing.Size(22, 20);
            this.btnWhatFolder.TabIndex = 2;
            this.btnWhatFolder.Text = "?";
            this.btnWhatFolder.UseVisualStyleBackColor = true;
            this.btnWhatFolder.Click += new System.EventHandler(this.btnWhatFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder   ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Command to process target folder.";
            // 
            // tabMisc
            // 
            this.tabMisc.Controls.Add(this.panel9);
            this.tabMisc.Controls.Add(this.panel8);
            this.tabMisc.Controls.Add(this.panel5);
            this.tabMisc.Location = new System.Drawing.Point(4, 22);
            this.tabMisc.Name = "tabMisc";
            this.tabMisc.Padding = new System.Windows.Forms.Padding(3);
            this.tabMisc.Size = new System.Drawing.Size(410, 169);
            this.tabMisc.TabIndex = 2;
            this.tabMisc.Text = "Misc";
            this.tabMisc.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.ckbAddFolderRecursively);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(3, 43);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(404, 20);
            this.panel9.TabIndex = 2;
            // 
            // ckbAddFolderRecursively
            // 
            this.ckbAddFolderRecursively.AutoSize = true;
            this.ckbAddFolderRecursively.Location = new System.Drawing.Point(5, 3);
            this.ckbAddFolderRecursively.Name = "ckbAddFolderRecursively";
            this.ckbAddFolderRecursively.Size = new System.Drawing.Size(210, 16);
            this.ckbAddFolderRecursively.TabIndex = 0;
            this.ckbAddFolderRecursively.Text = "Add texture folders recursively";
            this.ckbAddFolderRecursively.UseVisualStyleBackColor = true;
            this.ckbAddFolderRecursively.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.ckbAskBeforeRemoveFrame);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(3, 23);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(404, 20);
            this.panel8.TabIndex = 1;
            // 
            // ckbAskBeforeRemoveFrame
            // 
            this.ckbAskBeforeRemoveFrame.AutoSize = true;
            this.ckbAskBeforeRemoveFrame.Location = new System.Drawing.Point(5, 3);
            this.ckbAskBeforeRemoveFrame.Name = "ckbAskBeforeRemoveFrame";
            this.ckbAskBeforeRemoveFrame.Size = new System.Drawing.Size(204, 16);
            this.ckbAskBeforeRemoveFrame.TabIndex = 0;
            this.ckbAskBeforeRemoveFrame.Text = "Ensure before removing a frame";
            this.ckbAskBeforeRemoveFrame.UseVisualStyleBackColor = true;
            this.ckbAskBeforeRemoveFrame.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ckbFileRel);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(404, 20);
            this.panel5.TabIndex = 0;
            // 
            // ckbFileRel
            // 
            this.ckbFileRel.AutoSize = true;
            this.ckbFileRel.Location = new System.Drawing.Point(5, 3);
            this.ckbFileRel.Name = "ckbFileRel";
            this.ckbFileRel.Size = new System.Drawing.Size(252, 16);
            this.ckbFileRel.TabIndex = 0;
            this.ckbFileRel.Text = "Relate Frameshop project (*.fsp) files";
            this.ckbFileRel.UseVisualStyleBackColor = true;
            this.ckbFileRel.CheckedChanged += new System.EventHandler(this.ckb_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 195);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 24);
            this.panel1.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOk.Location = new System.Drawing.Point(193, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 24);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnReset
            // 
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReset.Location = new System.Drawing.Point(268, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 24);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(343, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormOption
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(418, 219);
            this.Controls.Add(this.tabOptions);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(426, 243);
            this.Name = "FormOption";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Option";
            this.Load += new System.EventHandler(this.FormOption_Load);
            this.tabOptions.ResumeLayout(false);
            this.tabPostEvent.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabMisc.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabOptions;
        private System.Windows.Forms.TabPage tabPostEvent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEditFolder;
        private System.Windows.Forms.ComboBox cmbFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox cmbData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEditData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbTexture;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEditTexture;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabMisc;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox ckbFileRel;
        private System.Windows.Forms.Button btnWhatData;
        private System.Windows.Forms.Button btnWhatTex;
        private System.Windows.Forms.Button btnWhatFolder;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ComboBox cmbAnim;
        private System.Windows.Forms.Button btnEditAnim;
        private System.Windows.Forms.Button btnWhatAnim;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.CheckBox ckbConvertAnim;
        private System.Windows.Forms.Button btnWhatConvertAnim;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.CheckBox ckbAskBeforeRemoveFrame;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.CheckBox ckbAddFolderRecursively;
    }
}