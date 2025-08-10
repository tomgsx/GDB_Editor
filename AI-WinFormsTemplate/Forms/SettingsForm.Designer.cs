namespace GDB_Editor.Forms
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtDatabaseRoot;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblModsPath;
        private System.Windows.Forms.TextBox txtModsRoot;
        private System.Windows.Forms.Button btnBrowseMods;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblPath = new System.Windows.Forms.Label();
            this.txtDatabaseRoot = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblModsPath = new System.Windows.Forms.Label();
            this.txtModsRoot = new System.Windows.Forms.TextBox();
            this.btnBrowseMods = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(12, 15);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(153, 13);
            this.lblPath.TabIndex = 0;
            this.lblPath.Text = "Input root (dataSource folder):";
            // 
            // txtDatabaseRoot
            // 
            this.txtDatabaseRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDatabaseRoot.Location = new System.Drawing.Point(15, 31);
            this.txtDatabaseRoot.Name = "txtDatabaseRoot";
            this.txtDatabaseRoot.Size = new System.Drawing.Size(420, 20);
            this.txtDatabaseRoot.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(441, 29);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(360, 111);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(441, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblModsPath
            // 
            this.lblModsPath.AutoSize = true;
            this.lblModsPath.Location = new System.Drawing.Point(12, 63);
            this.lblModsPath.Name = "lblModsPath";
            this.lblModsPath.Size = new System.Drawing.Size(131, 13);
            this.lblModsPath.TabIndex = 5;
            this.lblModsPath.Text = "Output root (dataMods dir):";
            // 
            // txtModsRoot
            // 
            this.txtModsRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtModsRoot.Location = new System.Drawing.Point(15, 79);
            this.txtModsRoot.Name = "txtModsRoot";
            this.txtModsRoot.Size = new System.Drawing.Size(420, 20);
            this.txtModsRoot.TabIndex = 6;
            // 
            // btnBrowseMods
            // 
            this.btnBrowseMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseMods.Location = new System.Drawing.Point(441, 77);
            this.btnBrowseMods.Name = "btnBrowseMods";
            this.btnBrowseMods.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseMods.TabIndex = 7;
            this.btnBrowseMods.Text = "Browse...";
            this.btnBrowseMods.UseVisualStyleBackColor = true;
            this.btnBrowseMods.Click += new System.EventHandler(this.btnBrowseMods_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(528, 146);
            this.Controls.Add(this.btnBrowseMods);
            this.Controls.Add(this.txtModsRoot);
            this.Controls.Add(this.lblModsPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtDatabaseRoot);
            this.Controls.Add(this.lblPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
} 