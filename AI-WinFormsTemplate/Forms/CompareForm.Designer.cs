namespace GDB_Editor.Forms
{
    partial class CompareForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnRefresh;
        private DiffViewer diffViewer1;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblAdded;
        private System.Windows.Forms.Label lblRemoved;
        private System.Windows.Forms.Label lblModified;

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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.diffViewer1 = new GDB_Editor.Forms.DiffViewer();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblAdded = new System.Windows.Forms.Label();
            this.lblRemoved = new System.Windows.Forms.Label();
            this.lblModified = new System.Windows.Forms.Label();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(897, 566);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // diffViewer1
            // 
            this.diffViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diffViewer1.Location = new System.Drawing.Point(12, 12);
            this.diffViewer1.Name = "diffViewer1";
            this.diffViewer1.Size = new System.Drawing.Size(960, 548);
            this.diffViewer1.TabIndex = 2;
            // 
            // panelStatus
            // 
            this.panelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStatus.Controls.Add(this.lblModified);
            this.panelStatus.Controls.Add(this.lblRemoved);
            this.panelStatus.Controls.Add(this.lblAdded);
            this.panelStatus.Controls.Add(this.lblTotal);
            this.panelStatus.Location = new System.Drawing.Point(12, 563);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(879, 28);
            this.panelStatus.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(3, 7);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(46, 13);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total: 0";
            // 
            // lblAdded
            // 
            this.lblAdded.AutoSize = true;
            this.lblAdded.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAdded.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblAdded.Location = new System.Drawing.Point(90, 7);
            this.lblAdded.Name = "lblAdded";
            this.lblAdded.Size = new System.Drawing.Size(63, 13);
            this.lblAdded.TabIndex = 1;
            this.lblAdded.Text = "Added: 0";
            this.lblAdded.Click += new System.EventHandler(this.lblAdded_Click);
            // 
            // lblRemoved
            // 
            this.lblRemoved.AutoSize = true;
            this.lblRemoved.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRemoved.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblRemoved.Location = new System.Drawing.Point(180, 7);
            this.lblRemoved.Name = "lblRemoved";
            this.lblRemoved.Size = new System.Drawing.Size(78, 13);
            this.lblRemoved.TabIndex = 2;
            this.lblRemoved.Text = "Removed: 0";
            this.lblRemoved.Click += new System.EventHandler(this.lblRemoved_Click);
            // 
            // lblModified
            // 
            this.lblModified.AutoSize = true;
            this.lblModified.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblModified.ForeColor = System.Drawing.Color.Goldenrod;
            this.lblModified.Location = new System.Drawing.Point(282, 7);
            this.lblModified.Name = "lblModified";
            this.lblModified.Size = new System.Drawing.Size(74, 13);
            this.lblModified.TabIndex = 3;
            this.lblModified.Text = "Modified: 0";
            this.lblModified.Click += new System.EventHandler(this.lblModified_Click);
            // 
            // CompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 601);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.diffViewer1);
            this.Controls.Add(this.btnRefresh);
            this.Name = "CompareForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Compare";
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);
        }
    }
} 