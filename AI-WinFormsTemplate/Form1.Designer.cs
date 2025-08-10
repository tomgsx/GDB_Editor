namespace GDB_Editor
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tvFiles;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox lbNodes;
        // removed unused PropertyGrid
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSelectedFile;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Button btnAddProp;
        private System.Windows.Forms.Button btnRemoveProp;
        private System.Windows.Forms.ContextMenuStrip cmFiles;
        private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Label lblPropFilter;
        private System.Windows.Forms.Label lblFileViewHeader;
        private System.Windows.Forms.Label lblNodeViewHeader;
        private System.Windows.Forms.Label lblPropertyViewHeader;
        private System.Windows.Forms.TextBox txtPropFilter;
        private System.Windows.Forms.DataGridView dgvProps;
        private System.Windows.Forms.RadioButton rbFilterAll;
        private System.Windows.Forms.RadioButton rbFilterName;
        private System.Windows.Forms.RadioButton rbFilterValue;
        private System.Windows.Forms.Button btnClearPropFilter;
        private System.Windows.Forms.Button btnClearNodeFilter;
        // removed obsolete editor subpanels used in earlier layout
        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel panelFiles;
        private System.Windows.Forms.Panel panelNodes;
        private System.Windows.Forms.Panel panelProps;
        private System.Windows.Forms.TableLayoutPanel tlpFiles;
        private System.Windows.Forms.TableLayoutPanel tlpNodes;
        private System.Windows.Forms.TableLayoutPanel tlpProps;
        private System.Windows.Forms.FlowLayoutPanel flpNodeFilter;
        private System.Windows.Forms.FlowLayoutPanel flpPropFilter;
        private System.Windows.Forms.FlowLayoutPanel flpPropButtons;
        private System.Windows.Forms.FlowLayoutPanel flpPropRadios;
        private System.Windows.Forms.FlowLayoutPanel flpSave;

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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvFiles = new System.Windows.Forms.TreeView();
            this.cmFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFileViewHeader = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lbNodes = new System.Windows.Forms.ListBox();
            this.btnClearNodeFilter = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lblNodeViewHeader = new System.Windows.Forms.Label();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAddProp = new System.Windows.Forms.Button();
            this.btnRemoveProp = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.rbFilterValue = new System.Windows.Forms.RadioButton();
            this.rbFilterName = new System.Windows.Forms.RadioButton();
            this.rbFilterAll = new System.Windows.Forms.RadioButton();
            this.btnClearPropFilter = new System.Windows.Forms.Button();
            this.txtPropFilter = new System.Windows.Forms.TextBox();
            this.lblPropFilter = new System.Windows.Forms.Label();
            this.lblPropertyViewHeader = new System.Windows.Forms.Label();
            this.dgvProps = new System.Windows.Forms.DataGridView();
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.panelFiles = new System.Windows.Forms.Panel();
            this.tlpFiles = new System.Windows.Forms.TableLayoutPanel();
            this.panelNodes = new System.Windows.Forms.Panel();
            this.tlpNodes = new System.Windows.Forms.TableLayoutPanel();
            this.flpNodeFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.panelProps = new System.Windows.Forms.Panel();
            this.tlpProps = new System.Windows.Forms.TableLayoutPanel();
            this.flpPropFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.flpPropRadios = new System.Windows.Forms.FlowLayoutPanel();
            this.flpPropButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.flpSave = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProps)).BeginInit();
            this.tlpRoot.SuspendLayout();
            this.panelFiles.SuspendLayout();
            this.tlpFiles.SuspendLayout();
            this.panelNodes.SuspendLayout();
            this.tlpNodes.SuspendLayout();
            this.flpNodeFilter.SuspendLayout();
            this.panelProps.SuspendLayout();
            this.tlpProps.SuspendLayout();
            this.flpPropFilter.SuspendLayout();
            this.flpPropRadios.SuspendLayout();
            this.flpPropButtons.SuspendLayout();
            this.flpSave.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2400, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(71, 36);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(233, 44);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(233, 44);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(2400, 1306);
            this.splitContainer1.SplitterDistance = 720;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 1;
            // 
            // tvFiles
            // 
            this.tvFiles.ContextMenuStrip = this.cmFiles;
            this.tvFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tvFiles.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvFiles.FullRowSelect = true;
            this.tvFiles.HideSelection = false;
            this.tvFiles.Location = new System.Drawing.Point(6, 48);
            this.tvFiles.Margin = new System.Windows.Forms.Padding(6);
            this.tvFiles.Name = "tvFiles";
            this.tvFiles.Size = new System.Drawing.Size(580, 1244);
            this.tvFiles.TabIndex = 0;
            this.tvFiles.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvFiles_DrawNode);
            this.tvFiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFiles_AfterSelect);
            // 
            // cmFiles
            // 
            this.cmFiles.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compareToolStripMenuItem});
            this.cmFiles.Name = "cmFiles";
            this.cmFiles.Size = new System.Drawing.Size(186, 42);
            this.cmFiles.Opening += new System.ComponentModel.CancelEventHandler(this.cmFiles_Opening);
            // 
            // compareToolStripMenuItem
            // 
            this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            this.compareToolStripMenuItem.Size = new System.Drawing.Size(185, 38);
            this.compareToolStripMenuItem.Text = "Compare";
            this.compareToolStripMenuItem.Click += new System.EventHandler(this.compareToolStripMenuItem_Click);
            // 
            // lblFileViewHeader
            // 
            this.lblFileViewHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.lblFileViewHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFileViewHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileViewHeader.ForeColor = System.Drawing.Color.Black;
            this.lblFileViewHeader.Location = new System.Drawing.Point(6, 0);
            this.lblFileViewHeader.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFileViewHeader.Name = "lblFileViewHeader";
            this.lblFileViewHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.lblFileViewHeader.Size = new System.Drawing.Size(580, 42);
            this.lblFileViewHeader.TabIndex = 1;
            this.lblFileViewHeader.Text = "FileView";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(6);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Size = new System.Drawing.Size(1672, 1306);
            this.splitContainer2.SplitterDistance = 600;
            this.splitContainer2.SplitterWidth = 8;
            this.splitContainer2.TabIndex = 0;
            // 
            // lbNodes
            // 
            this.lbNodes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbNodes.FormattingEnabled = true;
            this.lbNodes.IntegralHeight = false;
            this.lbNodes.ItemHeight = 25;
            this.lbNodes.Location = new System.Drawing.Point(6, 159);
            this.lbNodes.Margin = new System.Windows.Forms.Padding(6);
            this.lbNodes.Name = "lbNodes";
            this.lbNodes.Size = new System.Drawing.Size(820, 1133);
            this.lbNodes.TabIndex = 3;
            this.lbNodes.SelectedIndexChanged += new System.EventHandler(this.lbNodes_SelectedIndexChanged);
            // 
            // btnClearNodeFilter
            // 
            this.btnClearNodeFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearNodeFilter.Location = new System.Drawing.Point(544, 6);
            this.btnClearNodeFilter.Margin = new System.Windows.Forms.Padding(6);
            this.btnClearNodeFilter.Name = "btnClearNodeFilter";
            this.btnClearNodeFilter.Size = new System.Drawing.Size(48, 44);
            this.btnClearNodeFilter.TabIndex = 5;
            this.btnClearNodeFilter.Text = "X";
            this.btnClearNodeFilter.UseVisualStyleBackColor = true;
            this.btnClearNodeFilter.Click += new System.EventHandler(this.btnClearNodeFilter_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.Location = new System.Drawing.Point(84, 6);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(6);
            this.txtFilter.MaxLength = 128;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(448, 31);
            this.txtFilter.TabIndex = 2;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(6, 0);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(66, 25);
            this.lblFilter.TabIndex = 1;
            this.lblFilter.Text = "Filter:";
            // 
            // lblNodeViewHeader
            // 
            this.lblNodeViewHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.lblNodeViewHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNodeViewHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNodeViewHeader.ForeColor = System.Drawing.Color.Black;
            this.lblNodeViewHeader.Location = new System.Drawing.Point(6, 0);
            this.lblNodeViewHeader.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblNodeViewHeader.Name = "lblNodeViewHeader";
            this.lblNodeViewHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.lblNodeViewHeader.Size = new System.Drawing.Size(820, 42);
            this.lblNodeViewHeader.TabIndex = 6;
            this.lblNodeViewHeader.Text = "NodeView";
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.AutoEllipsis = true;
            this.lblSelectedFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSelectedFile.Location = new System.Drawing.Point(6, 42);
            this.lblSelectedFile.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Padding = new System.Windows.Forms.Padding(6);
            this.lblSelectedFile.Size = new System.Drawing.Size(820, 38);
            this.lblSelectedFile.TabIndex = 0;
            this.lblSelectedFile.Text = "(no file selected)";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(6, 6);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 44);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAddProp
            // 
            this.btnAddProp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddProp.Location = new System.Drawing.Point(6, 6);
            this.btnAddProp.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddProp.Name = "btnAddProp";
            this.btnAddProp.Size = new System.Drawing.Size(180, 44);
            this.btnAddProp.TabIndex = 2;
            this.btnAddProp.Text = "Add Property";
            this.btnAddProp.UseVisualStyleBackColor = true;
            this.btnAddProp.Click += new System.EventHandler(this.btnAddProp_Click);
            // 
            // btnRemoveProp
            // 
            this.btnRemoveProp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveProp.Location = new System.Drawing.Point(198, 6);
            this.btnRemoveProp.Margin = new System.Windows.Forms.Padding(6);
            this.btnRemoveProp.Name = "btnRemoveProp";
            this.btnRemoveProp.Size = new System.Drawing.Size(206, 44);
            this.btnRemoveProp.TabIndex = 3;
            this.btnRemoveProp.Text = "Remove Property";
            this.btnRemoveProp.UseVisualStyleBackColor = true;
            this.btnRemoveProp.Click += new System.EventHandler(this.btnRemoveProp_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMoveUp.Location = new System.Drawing.Point(416, 6);
            this.btnMoveUp.Margin = new System.Windows.Forms.Padding(6);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(150, 44);
            this.btnMoveUp.TabIndex = 4;
            this.btnMoveUp.Text = "Move Up";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMoveDown.Location = new System.Drawing.Point(578, 6);
            this.btnMoveDown.Margin = new System.Windows.Forms.Padding(6);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(168, 44);
            this.btnMoveDown.TabIndex = 5;
            this.btnMoveDown.Text = "Move Down";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // rbFilterValue
            // 
            this.rbFilterValue.AutoSize = true;
            this.rbFilterValue.Location = new System.Drawing.Point(196, 6);
            this.rbFilterValue.Margin = new System.Windows.Forms.Padding(6);
            this.rbFilterValue.Name = "rbFilterValue";
            this.rbFilterValue.Size = new System.Drawing.Size(98, 29);
            this.rbFilterValue.TabIndex = 11;
            this.rbFilterValue.Text = "Value";
            this.rbFilterValue.UseVisualStyleBackColor = true;
            this.rbFilterValue.CheckedChanged += new System.EventHandler(this.FilterModeChanged);
            // 
            // rbFilterName
            // 
            this.rbFilterName.AutoSize = true;
            this.rbFilterName.Location = new System.Drawing.Point(85, 6);
            this.rbFilterName.Margin = new System.Windows.Forms.Padding(6);
            this.rbFilterName.Name = "rbFilterName";
            this.rbFilterName.Size = new System.Drawing.Size(99, 29);
            this.rbFilterName.TabIndex = 10;
            this.rbFilterName.Text = "Name";
            this.rbFilterName.UseVisualStyleBackColor = true;
            this.rbFilterName.CheckedChanged += new System.EventHandler(this.FilterModeChanged);
            // 
            // rbFilterAll
            // 
            this.rbFilterAll.AutoSize = true;
            this.rbFilterAll.Checked = true;
            this.rbFilterAll.Location = new System.Drawing.Point(6, 6);
            this.rbFilterAll.Margin = new System.Windows.Forms.Padding(6);
            this.rbFilterAll.Name = "rbFilterAll";
            this.rbFilterAll.Size = new System.Drawing.Size(67, 29);
            this.rbFilterAll.TabIndex = 9;
            this.rbFilterAll.TabStop = true;
            this.rbFilterAll.Text = "All";
            this.rbFilterAll.UseVisualStyleBackColor = true;
            this.rbFilterAll.CheckedChanged += new System.EventHandler(this.FilterModeChanged);
            // 
            // btnClearPropFilter
            // 
            this.btnClearPropFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearPropFilter.Location = new System.Drawing.Point(544, 6);
            this.btnClearPropFilter.Margin = new System.Windows.Forms.Padding(6);
            this.btnClearPropFilter.Name = "btnClearPropFilter";
            this.btnClearPropFilter.Size = new System.Drawing.Size(48, 44);
            this.btnClearPropFilter.TabIndex = 12;
            this.btnClearPropFilter.Text = "X";
            this.btnClearPropFilter.UseVisualStyleBackColor = true;
            this.btnClearPropFilter.Click += new System.EventHandler(this.btnClearPropFilter_Click);
            // 
            // txtPropFilter
            // 
            this.txtPropFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPropFilter.Location = new System.Drawing.Point(84, 6);
            this.txtPropFilter.Margin = new System.Windows.Forms.Padding(6);
            this.txtPropFilter.MaxLength = 128;
            this.txtPropFilter.Name = "txtPropFilter";
            this.txtPropFilter.Size = new System.Drawing.Size(448, 31);
            this.txtPropFilter.TabIndex = 7;
            this.txtPropFilter.TextChanged += new System.EventHandler(this.txtPropFilter_TextChanged);
            // 
            // lblPropFilter
            // 
            this.lblPropFilter.AutoSize = true;
            this.lblPropFilter.Location = new System.Drawing.Point(6, 0);
            this.lblPropFilter.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPropFilter.Name = "lblPropFilter";
            this.lblPropFilter.Size = new System.Drawing.Size(66, 25);
            this.lblPropFilter.TabIndex = 6;
            this.lblPropFilter.Text = "Filter:";
            // 
            // lblPropertyViewHeader
            // 
            this.lblPropertyViewHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.lblPropertyViewHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPropertyViewHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPropertyViewHeader.ForeColor = System.Drawing.Color.Black;
            this.lblPropertyViewHeader.Location = new System.Drawing.Point(6, 0);
            this.lblPropertyViewHeader.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPropertyViewHeader.Name = "lblPropertyViewHeader";
            this.lblPropertyViewHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.lblPropertyViewHeader.Size = new System.Drawing.Size(940, 42);
            this.lblPropertyViewHeader.TabIndex = 13;
            this.lblPropertyViewHeader.Text = "PropertyView";
            // 
            // dgvProps
            // 
            this.dgvProps.AllowUserToAddRows = false;
            this.dgvProps.AllowUserToDeleteRows = false;
            this.dgvProps.AllowUserToResizeRows = false;
            this.dgvProps.ColumnHeadersHeight = 46;
            this.dgvProps.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvProps.Location = new System.Drawing.Point(6, 157);
            this.dgvProps.Margin = new System.Windows.Forms.Padding(6);
            this.dgvProps.MultiSelect = false;
            this.dgvProps.Name = "dgvProps";
            this.dgvProps.RowHeadersVisible = false;
            this.dgvProps.RowHeadersWidth = 82;
            this.dgvProps.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProps.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProps.Size = new System.Drawing.Size(940, 1011);
            this.dgvProps.TabIndex = 8;
            this.dgvProps.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProps_CellValueChanged);
            this.dgvProps.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvProps_CurrentCellDirtyStateChanged);
            this.dgvProps.SelectionChanged += new System.EventHandler(this.dgvProps_SelectionChanged);
            // 
            // tlpRoot
            // 
            this.tlpRoot.ColumnCount = 3;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpRoot.Controls.Add(this.panelFiles, 0, 0);
            this.tlpRoot.Controls.Add(this.panelNodes, 1, 0);
            this.tlpRoot.Controls.Add(this.panelProps, 2, 0);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 40);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.RowCount = 1;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Size = new System.Drawing.Size(2400, 1306);
            this.tlpRoot.TabIndex = 1;
            // 
            // panelFiles
            // 
            this.panelFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFiles.Controls.Add(this.tlpFiles);
            this.panelFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFiles.Location = new System.Drawing.Point(3, 3);
            this.panelFiles.Name = "panelFiles";
            this.panelFiles.Size = new System.Drawing.Size(594, 1300);
            this.panelFiles.TabIndex = 0;
            // 
            // tlpFiles
            // 
            this.tlpFiles.ColumnCount = 1;
            this.tlpFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFiles.Controls.Add(this.lblFileViewHeader, 0, 0);
            this.tlpFiles.Controls.Add(this.tvFiles, 0, 1);
            this.tlpFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFiles.Location = new System.Drawing.Point(0, 0);
            this.tlpFiles.Name = "tlpFiles";
            this.tlpFiles.RowCount = 2;
            this.tlpFiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFiles.Size = new System.Drawing.Size(592, 1298);
            this.tlpFiles.TabIndex = 0;
            // 
            // panelNodes
            // 
            this.panelNodes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNodes.Controls.Add(this.tlpNodes);
            this.panelNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNodes.Location = new System.Drawing.Point(603, 3);
            this.panelNodes.Name = "panelNodes";
            this.panelNodes.Size = new System.Drawing.Size(834, 1300);
            this.panelNodes.TabIndex = 1;
            // 
            // tlpNodes
            // 
            this.tlpNodes.ColumnCount = 1;
            this.tlpNodes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNodes.Controls.Add(this.lblNodeViewHeader, 0, 0);
            this.tlpNodes.Controls.Add(this.lblSelectedFile, 0, 1);
            this.tlpNodes.Controls.Add(this.flpNodeFilter, 0, 2);
            this.tlpNodes.Controls.Add(this.lbNodes, 0, 3);
            this.tlpNodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpNodes.Location = new System.Drawing.Point(0, 0);
            this.tlpNodes.Name = "tlpNodes";
            this.tlpNodes.RowCount = 4;
            this.tlpNodes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpNodes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpNodes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpNodes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNodes.Size = new System.Drawing.Size(832, 1298);
            this.tlpNodes.TabIndex = 0;
            // 
            // flpNodeFilter
            // 
            this.flpNodeFilter.AutoSize = true;
            this.flpNodeFilter.Controls.Add(this.lblFilter);
            this.flpNodeFilter.Controls.Add(this.txtFilter);
            this.flpNodeFilter.Controls.Add(this.btnClearNodeFilter);
            this.flpNodeFilter.Location = new System.Drawing.Point(3, 83);
            this.flpNodeFilter.Name = "flpNodeFilter";
            this.flpNodeFilter.Size = new System.Drawing.Size(598, 56);
            this.flpNodeFilter.TabIndex = 7;
            // 
            // panelProps
            // 
            this.panelProps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProps.Controls.Add(this.tlpProps);
            this.panelProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProps.Location = new System.Drawing.Point(1443, 3);
            this.panelProps.Name = "panelProps";
            this.panelProps.Size = new System.Drawing.Size(954, 1300);
            this.panelProps.TabIndex = 2;
            // 
            // tlpProps
            // 
            this.tlpProps.ColumnCount = 1;
            this.tlpProps.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProps.Controls.Add(this.lblPropertyViewHeader, 0, 0);
            this.tlpProps.Controls.Add(this.flpPropFilter, 0, 1);
            this.tlpProps.Controls.Add(this.flpPropRadios, 0, 2);
            this.tlpProps.Controls.Add(this.dgvProps, 0, 3);
            this.tlpProps.Controls.Add(this.flpPropButtons, 0, 4);
            this.tlpProps.Controls.Add(this.flpSave, 0, 5);
            this.tlpProps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProps.Location = new System.Drawing.Point(0, 0);
            this.tlpProps.Name = "tlpProps";
            this.tlpProps.RowCount = 6;
            this.tlpProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpProps.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpProps.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpProps.Size = new System.Drawing.Size(952, 1298);
            this.tlpProps.TabIndex = 0;
            // 
            // flpPropFilter
            // 
            this.flpPropFilter.AutoSize = true;
            this.flpPropFilter.Controls.Add(this.lblPropFilter);
            this.flpPropFilter.Controls.Add(this.txtPropFilter);
            this.flpPropFilter.Controls.Add(this.btnClearPropFilter);
            this.flpPropFilter.Location = new System.Drawing.Point(3, 45);
            this.flpPropFilter.Name = "flpPropFilter";
            this.flpPropFilter.Size = new System.Drawing.Size(598, 56);
            this.flpPropFilter.TabIndex = 14;
            // 
            // flpPropRadios
            // 
            this.flpPropRadios.AutoSize = true;
            this.flpPropRadios.Controls.Add(this.rbFilterAll);
            this.flpPropRadios.Controls.Add(this.rbFilterName);
            this.flpPropRadios.Controls.Add(this.rbFilterValue);
            this.flpPropRadios.Location = new System.Drawing.Point(3, 107);
            this.flpPropRadios.Name = "flpPropRadios";
            this.flpPropRadios.Size = new System.Drawing.Size(300, 41);
            this.flpPropRadios.TabIndex = 15;
            // 
            // flpPropButtons
            // 
            this.flpPropButtons.AutoSize = true;
            this.flpPropButtons.Controls.Add(this.btnMoveDown);
            this.flpPropButtons.Controls.Add(this.btnMoveUp);
            this.flpPropButtons.Controls.Add(this.btnRemoveProp);
            this.flpPropButtons.Controls.Add(this.btnAddProp);
            this.flpPropButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpPropButtons.Location = new System.Drawing.Point(3, 1177);
            this.flpPropButtons.Name = "flpPropButtons";
            this.flpPropButtons.Size = new System.Drawing.Size(752, 56);
            this.flpPropButtons.TabIndex = 16;
            // 
            // flpSave
            // 
            this.flpSave.AutoSize = true;
            this.flpSave.Controls.Add(this.btnSave);
            this.flpSave.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpSave.Location = new System.Drawing.Point(3, 1239);
            this.flpSave.Name = "flpSave";
            this.flpSave.Size = new System.Drawing.Size(162, 56);
            this.flpSave.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2400, 1346);
            this.Controls.Add(this.tlpRoot);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GDB Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProps)).EndInit();
            this.tlpRoot.ResumeLayout(false);
            this.panelFiles.ResumeLayout(false);
            this.tlpFiles.ResumeLayout(false);
            this.panelNodes.ResumeLayout(false);
            this.tlpNodes.ResumeLayout(false);
            this.tlpNodes.PerformLayout();
            this.flpNodeFilter.ResumeLayout(false);
            this.flpNodeFilter.PerformLayout();
            this.panelProps.ResumeLayout(false);
            this.tlpProps.ResumeLayout(false);
            this.tlpProps.PerformLayout();
            this.flpPropFilter.ResumeLayout(false);
            this.flpPropFilter.PerformLayout();
            this.flpPropRadios.ResumeLayout(false);
            this.flpPropRadios.PerformLayout();
            this.flpPropButtons.ResumeLayout(false);
            this.flpSave.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

