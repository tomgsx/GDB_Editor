using GDB_Editor.Forms;
using GDB_Editor.Gdb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GDB_Editor
{
    public partial class Form1 : Form
    {
        private BindingList<PropertyRow> propRows = new BindingList<PropertyRow>();
        private string propFilterText = string.Empty;
        private enum PropFilterMode { All, Name, Value }
        private PropFilterMode currentPropFilterMode = PropFilterMode.All;

        private sealed class PropertyRow
        {
            public int Seq { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
            public GdbProperty Backing { get; set; }
        }

        private void ConfigureGrid()
        {
            dgvProps.AutoGenerateColumns = false;
            dgvProps.Columns.Clear();
            var colSeq = new DataGridViewTextBoxColumn { HeaderText = "#", DataPropertyName = "Seq", Width = 50, ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells }; 
            var colName = new DataGridViewTextBoxColumn { HeaderText = "Name", DataPropertyName = "Key", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells };
            var colValue = new DataGridViewTextBoxColumn { HeaderText = "Value", DataPropertyName = "Value", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill };
            dgvProps.Columns.Add(colSeq);
            dgvProps.Columns.Add(colName);
            dgvProps.Columns.Add(colValue);
            dgvProps.DataSource = propRows;
        }

        private void LoadGridForNode(GdbNode node)
        {
            propRows.RaiseListChangedEvents = false;
            propRows.Clear();
            int seq = 0;
            foreach (var l in node.Lines)
            {
                if (l.LineType == GdbNodeLineType.Property && l.Property != null)
                {
                    seq++;
                    propRows.Add(new PropertyRow { Seq = seq, Key = l.Property.Key, Value = l.Property.Value, Backing = l.Property });
                }
            }
            propRows.RaiseListChangedEvents = true;
            propRows.ResetBindings();
            ApplyGridFilter();
            dgvProps.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void ApplyGridFilter()
        {
            var node = lbNodes.SelectedItem as GdbNode;
            if (node == null) { dgvProps.DataSource = null; return; }
            // Remember current selection by backing reference
            var selectedBacking = GetSelectedRow()?.Backing;

            Func<PropertyRow, bool> predicate = r =>
            {
                if (string.IsNullOrWhiteSpace(propFilterText)) return true;
                switch (currentPropFilterMode)
                {
                    case PropFilterMode.Name:
                        return r.Key?.IndexOf(propFilterText, StringComparison.OrdinalIgnoreCase) >= 0;
                    case PropFilterMode.Value:
                        return r.Value?.IndexOf(propFilterText, StringComparison.OrdinalIgnoreCase) >= 0;
                    default:
                        return (r.Key?.IndexOf(propFilterText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                               (r.Value?.IndexOf(propFilterText, StringComparison.OrdinalIgnoreCase) >= 0);
                }
            };

            var view = new BindingList<PropertyRow>(propRows.Where(predicate).ToList());
            dgvProps.DataSource = view;
            dgvProps.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // Restore selection and ensure visible
            if (selectedBacking != null)
            {
                var target = view.FirstOrDefault(r => ReferenceEquals(r.Backing, selectedBacking));
                if (target != null)
                {
                    int idx = view.IndexOf(target);
                    if (idx >= 0 && idx < dgvProps.Rows.Count)
                    {
                        dgvProps.ClearSelection();
                        dgvProps.Rows[idx].Selected = true;
                        dgvProps.CurrentCell = dgvProps.Rows[idx].Cells[0];
                        dgvProps.FirstDisplayedScrollingRowIndex = Math.Max(0, idx - 3);
                    }
                }
            }
            UpdateMoveButtons();
        }

        private void FilterModeChanged(object sender, EventArgs e)
        {
            if (rbFilterName.Checked) currentPropFilterMode = PropFilterMode.Name;
            else if (rbFilterValue.Checked) currentPropFilterMode = PropFilterMode.Value;
            else currentPropFilterMode = PropFilterMode.All;
            ApplyGridFilter();
        }

        private void btnClearPropFilter_Click(object sender, EventArgs e)
        {
            var selectedBacking = GetSelectedRow()?.Backing;
            txtPropFilter.Text = string.Empty;
            propFilterText = string.Empty;
            currentPropFilterMode = PropFilterMode.All;
            rbFilterAll.Checked = true;
            ApplyGridFilter();
            // Ensure previously selected backing is still selected/visible
            var view = dgvProps.DataSource as BindingList<PropertyRow>;
            if (selectedBacking != null && view != null)
            {
                var target = view.FirstOrDefault(r => ReferenceEquals(r.Backing, selectedBacking));
                if (target != null)
                {
                    int idx = view.IndexOf(target);
                    if (idx >= 0 && idx < dgvProps.Rows.Count)
                    {
                        dgvProps.ClearSelection();
                        dgvProps.Rows[idx].Selected = true;
                        dgvProps.CurrentCell = dgvProps.Rows[idx].Cells[0];
                        dgvProps.FirstDisplayedScrollingRowIndex = Math.Max(0, idx - 3);
                    }
                }
            }
        }

        private void txtPropFilter_TextChanged(object sender, EventArgs e)
        {
            propFilterText = txtPropFilter.Text ?? string.Empty;
            ApplyGridFilter();
        }

        private void dgvProps_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (!(dgvProps.Rows[e.RowIndex].DataBoundItem is PropertyRow row)) return;
            // Update backing property
            row.Backing.Value = row.Value;
        }

        private void dgvProps_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvProps.IsCurrentCellDirty)
            {
                dgvProps.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvProps_SelectionChanged(object sender, EventArgs e)
        {
            UpdateMoveButtons();
        }

        private PropertyRow GetSelectedRow()
        {
            if (dgvProps.SelectedRows.Count == 0) return null;
            return dgvProps.SelectedRows[0].DataBoundItem as PropertyRow;
        }

        // Hook grid selection into move/remove
        private void btnRemoveProp_Click(object sender, EventArgs e)
        {
            var node = lbNodes.SelectedItem as GdbNode;
            if (node == null) return;

            var sel = GetSelectedRow();
            if (sel == null) return;

            int lineIndex = node.Lines.FindIndex(l => l.LineType == GdbNodeLineType.Property && ReferenceEquals(l.Property, sel.Backing));
            if (lineIndex < 0) return;
            node.Properties.Remove(sel.Backing);
            node.Lines.RemoveAt(lineIndex);

            LoadGridForNode(node);
            // Reselect nearest row
            int newSeq = Math.Max(1, sel.Seq); // try keep index
            var view = (BindingList<PropertyRow>)dgvProps.DataSource;
            var target = view.FirstOrDefault(r => r.Seq >= newSeq) ?? view.LastOrDefault();
            if (target != null)
            {
                int idx = view.IndexOf(target);
                dgvProps.ClearSelection();
                if (idx >= 0 && idx < dgvProps.Rows.Count)
                {
                    dgvProps.Rows[idx].Selected = true;
                    dgvProps.CurrentCell = dgvProps.Rows[idx].Cells[0];
                }
            }
            UpdateMoveButtons();
        }

        private void MoveSelectedProperty(int direction)
        {
            var node = lbNodes.SelectedItem as GdbNode;
            if (node == null) return;
            var sel = GetSelectedRow();
            if (sel == null) return;

            int idx = node.Lines.FindIndex(l => l.LineType == GdbNodeLineType.Property && ReferenceEquals(l.Property, sel.Backing));
            if (idx < 0) return;

            int swapWith = idx + direction;
            if (direction < 0) { while (swapWith >= 0 && node.Lines[swapWith].LineType != GdbNodeLineType.Property) swapWith--; }
            else { while (swapWith < node.Lines.Count && node.Lines[swapWith].LineType != GdbNodeLineType.Property) swapWith++; }
            if (swapWith < 0 || swapWith >= node.Lines.Count) return;

            var temp = node.Lines[idx];
            node.Lines[idx] = node.Lines[swapWith];
            node.Lines[swapWith] = temp;

            LoadGridForNode(node);
            // Reselect moved row by backing reference
            var view = (BindingList<PropertyRow>)dgvProps.DataSource;
            var target = view.FirstOrDefault(r => ReferenceEquals(r.Backing, ((GdbNodeLine)temp).Property ?? sel.Backing));
            if (target == null) target = view.FirstOrDefault(r => ReferenceEquals(r.Backing, sel.Backing));
            if (target != null)
            {
                int ridx = view.IndexOf(target);
                dgvProps.ClearSelection();
                if (ridx >= 0 && ridx < dgvProps.Rows.Count)
                {
                    dgvProps.Rows[ridx].Selected = true;
                    dgvProps.CurrentCell = dgvProps.Rows[ridx].Cells[0];
                }
            }
            UpdateMoveButtons();
        }

        private void UpdateMoveButtons()
        {
            var node = lbNodes.SelectedItem as GdbNode;
            var sel = GetSelectedRow();
            if (node == null || sel == null)
            {
                btnMoveUp.Enabled = btnMoveDown.Enabled = false; return;
            }
            int idx = node.Lines.FindIndex(l => l.LineType == GdbNodeLineType.Property && ReferenceEquals(l.Property, sel.Backing));
            if (idx < 0) { btnMoveUp.Enabled = btnMoveDown.Enabled = false; return; }
            int prev = idx - 1; while (prev >= 0 && node.Lines[prev].LineType != GdbNodeLineType.Property) prev--;
            int next = idx + 1; while (next < node.Lines.Count && node.Lines[next].LineType != GdbNodeLineType.Property) next++;
            btnMoveUp.Enabled = prev >= 0;
            btnMoveDown.Enabled = next < node.Lines.Count;
        }

        private void lbNodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var node = lbNodes.SelectedItem as GdbNode;
            if (node == null)
            {
                propRows.Clear();
                dgvProps.DataSource = propRows;
                btnMoveUp.Enabled = btnMoveDown.Enabled = false;
                return;
            }
            LoadGridForNode(node);
            if (dgvProps.Rows.Count > 0)
            {
                dgvProps.ClearSelection();
                dgvProps.Rows[0].Selected = true;
                dgvProps.CurrentCell = dgvProps.Rows[0].Cells[0];
                // Ensure first row is visible at very top
                try { dgvProps.FirstDisplayedScrollingRowIndex = 0; } catch { }
            }
            UpdateMoveButtons();
        }

        private readonly Dictionary<string, GdbFile> openFiles = new Dictionary<string, GdbFile>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, CompareForm> openCompares = new Dictionary<string, CompareForm>(StringComparer.OrdinalIgnoreCase);
        private string selectedFilePath; // actual file opened for editing (may be from dataMods)
        private string selectedSourceFilePath; // original source file path from Settings root (tree node)
        private List<GdbNode> currentNodes = new List<GdbNode>();
        private ImageList treeImages;
        private const string FolderImageKey = "folder";
        private const string GdbImageKey = "gdb";

        public Form1()
        {
            InitializeComponent();
            tvFiles.NodeMouseClick += TvFiles_NodeMouseClick;

            // Setup icons using shell small icons
            treeImages = new ImageList { ImageSize = new Size(16, 16), ColorDepth = ColorDepth.Depth32Bit };
            try
            {
                var folderBmp = ShellIconHelper.GetSmallIconBitmap(isFolder: true, ".");
                var gdbBmp = ShellIconHelper.GetSmallIconBitmap(isFolder: false, ".gdb");
                treeImages.Images.Add(FolderImageKey, folderBmp ?? SystemIcons.WinLogo.ToBitmap());
                treeImages.Images.Add(GdbImageKey, gdbBmp ?? SystemIcons.Application.ToBitmap());
            }
            catch
            {
                treeImages.Images.Add(FolderImageKey, SystemIcons.WinLogo.ToBitmap());
                treeImages.Images.Add(GdbImageKey, SystemIcons.Application.ToBitmap());
            }
            tvFiles.ImageList = treeImages;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            // Grid fills between top and bottom toolbars by Dock setting in designer.
            LoadTree();
            // Ensure headers are visible above contents
            try
            {
                splitContainer1.Panel1.Controls.SetChildIndex(tvFiles, 1);
                splitContainer1.Panel1.Controls.SetChildIndex(lblFileViewHeader, 0);

                splitContainer2.Panel1.Controls.SetChildIndex(lblSelectedFile, 1);
                splitContainer2.Panel1.Controls.SetChildIndex(lblFilter, 2);
                splitContainer2.Panel1.Controls.SetChildIndex(txtFilter, 3);
                splitContainer2.Panel1.Controls.SetChildIndex(btnClearNodeFilter, 4);
                splitContainer2.Panel1.Controls.Add(lblNodeViewHeader);
                splitContainer2.Panel1.Controls.SetChildIndex(lblNodeViewHeader, 0);

                splitContainer2.Panel2.Controls.SetChildIndex(lblPropertyViewHeader, 0);
            }
            catch { }
        }

        // removed BringEditorButtonsToFront as layout is handled by docking/anchors

        private void LoadTree()
        {
            tvFiles.Nodes.Clear();
            var root = Properties.Settings.Default.DatabaseRootPath;
            if (string.IsNullOrWhiteSpace(root) || !Directory.Exists(root))
            {
                // Attempt to default to dataSource under project root
                var projectRoot = FindProjectRoot();
                var defaultPath = Path.Combine(projectRoot, "dataSource");
                if (Directory.Exists(defaultPath))
                {
                    Properties.Settings.Default.DatabaseRootPath = defaultPath;
                    Properties.Settings.Default.Save();
                    root = defaultPath;
                }
                else
                {
                    var result = MessageBox.Show(this, "Database path is not set. Open Settings now?", "Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        OpenSettings();
                    }
                    return;
                }
            }

            // Ensure mods root has a sensible default
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.ModsRootPath))
            {
                var modsDefault = Path.Combine(FindProjectRoot(), "dataMods");
                try { Directory.CreateDirectory(modsDefault); } catch { }
                Properties.Settings.Default.ModsRootPath = modsDefault;
                Properties.Settings.Default.Save();
            }

            var rootNode = new TreeNode(Path.GetFileName(root)) { Tag = root, ImageKey = FolderImageKey, SelectedImageKey = FolderImageKey };
            tvFiles.Nodes.Add(rootNode);
            AddDirectoryNodes(rootNode, root);
            UpdateModifiedFlags();
            rootNode.Expand();
        }

        private void AddDirectoryNodes(TreeNode parentNode, string directoryPath)
        {
            try
            {
                foreach (var dir in Directory.GetDirectories(directoryPath))
                {
                    var dirNode = new TreeNode(Path.GetFileName(dir)) { Tag = dir, ImageKey = FolderImageKey, SelectedImageKey = FolderImageKey };
                    parentNode.Nodes.Add(dirNode);
                    AddDirectoryNodes(dirNode, dir);
                }

                foreach (var file in Directory.GetFiles(directoryPath, "*.gdb"))
                {
                    var fileNode = new TreeNode(Path.GetFileName(file)) { Tag = file, ImageKey = GdbImageKey, SelectedImageKey = GdbImageKey };
                    parentNode.Nodes.Add(fileNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error reading directory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MarkParentsIfModified(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                MarkParentsIfModified(child);
            }

            bool selfModified = IsNodeFileModified(node);
            bool anyChildModified = false;
            foreach (TreeNode child in node.Nodes)
            {
                if (child.Tag is ModifiedFlag mf && mf.IsModified)
                {
                    anyChildModified = true; break;
                }
            }
            bool isMod = selfModified || anyChildModified;
            if (isMod)
            {
                node.Tag = new ModifiedFlag { Path = (node.Tag as string) ?? (node.Tag as ModifiedFlag)?.Path, IsModified = true };
            }
        }

        private void UpdateModifiedFlags()
        {
            if (tvFiles.Nodes.Count == 0) return;
            RecomputeModified(tvFiles.Nodes[0]);
            tvFiles.Invalidate();
        }

        private bool RecomputeModified(TreeNode node)
        {
            // Get original path (directory or file) from tag
            string path = (node.Tag as string) ?? (node.Tag as ModifiedFlag)?.Path;
            bool selfMod = false;
            if (!string.IsNullOrEmpty(path) && File.Exists(path) && path.EndsWith(".gdb", StringComparison.OrdinalIgnoreCase))
            {
                selfMod = IsNodeFileModified(node);
            }

            bool childMod = false;
            foreach (TreeNode child in node.Nodes)
            {
                childMod |= RecomputeModified(child);
            }

            bool isMod = selfMod || childMod;
            node.Tag = isMod ? (object)new ModifiedFlag { Path = path, IsModified = true } : (object)path;
            return isMod;
        }

        private bool IsNodeFileModified(TreeNode node)
        {
            var p = (node.Tag as string) ?? (node.Tag as ModifiedFlag)?.Path;
            if (string.IsNullOrEmpty(p)) return false;
            if (File.Exists(p) && p.EndsWith(".gdb", StringComparison.OrdinalIgnoreCase))
            {
                var projectRoot = FindProjectRoot();
                var dbRoot = Properties.Settings.Default.DatabaseRootPath?.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) ?? string.Empty;
                string relative = p;
                if (!string.IsNullOrEmpty(dbRoot) && p.StartsWith(dbRoot, StringComparison.OrdinalIgnoreCase))
                {
                    relative = p.Substring(dbRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                }
                var modsRoot = string.IsNullOrWhiteSpace(Properties.Settings.Default.ModsRootPath)
                    ? Path.Combine(FindProjectRoot(), "dataMods")
                    : Properties.Settings.Default.ModsRootPath;
                var modPath = Path.Combine(modsRoot, relative);
                return File.Exists(modPath);
            }
            return false;
        }

        private sealed class ModifiedFlag
        {
            public string Path { get; set; }
            public bool IsModified { get; set; }
        }

        private void tvFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var path = (e.Node.Tag as string) ?? (e.Node.Tag as ModifiedFlag)?.Path;
            if (string.IsNullOrWhiteSpace(path)) return;

            if (Directory.Exists(path))
            {
                selectedFilePath = null;
                selectedSourceFilePath = null;
                lblSelectedFile.Text = "(no file selected)";
                lbNodes.DataSource = null;
                currentNodes.Clear();
                propRows.Clear();
                dgvProps.DataSource = propRows;
                return;
            }

            if (File.Exists(path) && string.Equals(Path.GetExtension(path), ".gdb", StringComparison.OrdinalIgnoreCase))
            {
                selectedSourceFilePath = path;
                var projectRoot = FindProjectRoot();
                var dbRoot = Properties.Settings.Default.DatabaseRootPath?.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) ?? string.Empty;
                string relative = path;
                if (!string.IsNullOrEmpty(dbRoot) && path.StartsWith(dbRoot, StringComparison.OrdinalIgnoreCase))
                {
                    relative = path.Substring(dbRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                }
                var modsRoot = string.IsNullOrWhiteSpace(Properties.Settings.Default.ModsRootPath)
                    ? Path.Combine(projectRoot, "dataMods")
                    : Properties.Settings.Default.ModsRootPath;
                var modsPath = Path.Combine(modsRoot, relative);

                // If a modded version exists, open it instead
                var openPath = File.Exists(modsPath) ? modsPath : path;
                selectedFilePath = openPath;
                lblSelectedFile.Text = File.Exists(modsPath) ? (Path.GetFileName(openPath) + " (edited)") : Path.GetFileName(openPath);

                GdbFile gdb;
                if (!openFiles.TryGetValue(openPath, out gdb))
                {
                    try
                    {
                        gdb = GdbParser.Parse(openPath);
                        openFiles[openPath] = gdb;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"Failed to parse file: {ex.Message}", "Parse Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                currentNodes = gdb.Nodes;
                ApplyNodeFilter();
            }
        }

        private void ApplyNodeFilter()
        {
            var filter = txtFilter.Text ?? string.Empty;
            var filtered = string.IsNullOrWhiteSpace(filter)
                ? currentNodes
                : currentNodes.Where(n => (n.Name ?? string.Empty).IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            lbNodes.DataSource = null;
            lbNodes.DataSource = filtered;
            lbNodes.DisplayMember = nameof(GdbNode.Name);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ApplyNodeFilter();
        }

        private void TvFiles_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvFiles.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                cmFiles.Show(tvFiles, e.Location);
            }
        }

        private void cmFiles_Opening(object sender, CancelEventArgs e)
        {
            string p = (tvFiles.SelectedNode?.Tag as string) ?? (tvFiles.SelectedNode?.Tag as ModifiedFlag)?.Path;
            bool enable = tvFiles.SelectedNode != null && !string.IsNullOrEmpty(p) && File.Exists(p) && p.EndsWith(".gdb", StringComparison.OrdinalIgnoreCase);
            compareToolStripMenuItem.Enabled = enable;
            if (!enable) e.Cancel = true;
        }

        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = (tvFiles.SelectedNode?.Tag as string) ?? (tvFiles.SelectedNode?.Tag as ModifiedFlag)?.Path;
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return;
            if (!path.EndsWith(".gdb", StringComparison.OrdinalIgnoreCase)) return;

            var projectRoot = FindProjectRoot();
            var dbRoot = Properties.Settings.Default.DatabaseRootPath?.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) ?? string.Empty;
            string relative = path;
            if (!string.IsNullOrEmpty(dbRoot) && path.StartsWith(dbRoot, StringComparison.OrdinalIgnoreCase))
            {
                relative = path.Substring(dbRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }
            var modsRoot = string.IsNullOrWhiteSpace(Properties.Settings.Default.ModsRootPath)
                ? Path.Combine(projectRoot, "dataMods")
                : Properties.Settings.Default.ModsRootPath;
            var editedPath = Path.Combine(modsRoot, relative);

            if (openCompares.TryGetValue(path, out var existing) && !existing.IsDisposed)
            {
                existing.Activate();
                existing.LoadDiff();
                return;
            }

            var form = new CompareForm(path, editedPath);
            form.FormClosed += (s, args) => { openCompares.Remove(path); };
            openCompares[path] = form;
            form.Show();
        }

        private void tvFiles_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            var tagPath = (e.Node.Tag as string) ?? (e.Node.Tag as ModifiedFlag)?.Path;
            bool isModified = (e.Node.Tag is ModifiedFlag mflag) && mflag.IsModified;
            if (!isModified && !string.IsNullOrEmpty(tagPath) && File.Exists(tagPath) && tagPath.EndsWith(".gdb", StringComparison.OrdinalIgnoreCase))
            {
                var projectRoot = FindProjectRoot();
                var dbRoot = Properties.Settings.Default.DatabaseRootPath?.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) ?? string.Empty;
                string relative = tagPath;
                if (!string.IsNullOrEmpty(dbRoot) && tagPath.StartsWith(dbRoot, StringComparison.OrdinalIgnoreCase))
                {
                    relative = tagPath.Substring(dbRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                }
                var modsRoot = string.IsNullOrWhiteSpace(Properties.Settings.Default.ModsRootPath)
                    ? Path.Combine(projectRoot, "dataMods")
                    : Properties.Settings.Default.ModsRootPath;
                var edited = Path.Combine(modsRoot, relative);
                if (File.Exists(edited)) isModified = true;
            }

            int left = e.Bounds.Left;
            Rectangle rowBounds = new Rectangle(left, e.Bounds.Top, tvFiles.Width - left, e.Bounds.Height);
            var backColor = isModified ? Color.FromArgb(255, 255, 180) : tvFiles.BackColor;
            var foreColor = tvFiles.ForeColor;
            using (var b = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(b, rowBounds);
            }
            bool selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected;
            var font = selected ? new Font(tvFiles.Font, FontStyle.Bold) : tvFiles.Font;
            var textBounds = new Rectangle(e.Bounds.Left, e.Bounds.Top, tvFiles.Width - e.Bounds.Left, e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, e.Node.Text, font, textBounds, foreColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            if (selected)
            {
                using (var pen = new Pen(Color.DodgerBlue, 1))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, e.Bounds.Top, tvFiles.Width - 1, e.Bounds.Height - 1));
                }
            }
            e.DrawDefault = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenSettings()
        {
            using (var dlg = new SettingsForm())
            {
                dlg.DatabaseRoot = Properties.Settings.Default.DatabaseRootPath;

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    Properties.Settings.Default.DatabaseRootPath = dlg.DatabaseRoot;
                    Properties.Settings.Default.Save();
                    openFiles.Clear();
                    LoadTree();
                }
            }
        }

        private static string FindProjectRoot()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 8 && !string.IsNullOrEmpty(dir); i++)
            {
                if (File.Exists(Path.Combine(dir, "AI-WinFormsTemplate.sln")) || Directory.Exists(Path.Combine(dir, "files")))
                {
                    return dir;
                }
                dir = Path.GetDirectoryName(dir?.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            }
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedFilePath)) return;
            if (!openFiles.TryGetValue(selectedFilePath, out var gdb)) return;

            try
            {
            var projectRoot = FindProjectRoot();
            var modsRoot = string.IsNullOrWhiteSpace(Properties.Settings.Default.ModsRootPath)
                ? Path.Combine(projectRoot, "dataMods")
                : Properties.Settings.Default.ModsRootPath;
            try { Directory.CreateDirectory(modsRoot); } catch { }

            var dbRoot = Properties.Settings.Default.DatabaseRootPath?.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) ?? string.Empty;
            string relative = selectedFilePath;
            if (!string.IsNullOrEmpty(dbRoot) && selectedFilePath.StartsWith(dbRoot, StringComparison.OrdinalIgnoreCase))
            {
                relative = selectedFilePath.Substring(dbRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }
            else
            {
                // If we opened from dataMods, derive relative from mods root
                var modsRootFull = modsRoot.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                if (selectedFilePath.StartsWith(modsRootFull, StringComparison.OrdinalIgnoreCase))
                {
                    relative = selectedFilePath.Substring(modsRootFull.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                }
                else if (!string.IsNullOrEmpty(selectedSourceFilePath) && selectedSourceFilePath.StartsWith(dbRoot, StringComparison.OrdinalIgnoreCase))
                {
                    relative = selectedSourceFilePath.Substring(dbRoot.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                }
            }

            var outputPath = Path.Combine(modsRoot, relative);
                GdbParser.Write(gdb, outputPath);
                MessageBox.Show(this, $"Saved to {outputPath}", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (openCompares.TryGetValue(selectedFilePath, out var cmp) && !cmp.IsDisposed)
                {
                    cmp.LoadDiff();
                }
                UpdateModifiedFlags();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to save: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddProp_Click(object sender, EventArgs e)
        {
            var node = lbNodes.SelectedItem as GdbNode;
            if (node == null) return;

            IEnumerable<string> availableKeys = Enumerable.Empty<string>();
            if (!string.IsNullOrWhiteSpace(selectedFilePath) && openFiles.TryGetValue(selectedFilePath, out var gdb))
            {
                availableKeys = gdb.Nodes.SelectMany(n => n.Properties.Select(p => p.Key));
            }

            using (var dlg = new AddPropertyForm(availableKeys))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var prop = new GdbProperty { Key = dlg.PropertyKey, Value = dlg.PropertyValue };
                    node.Properties.Add(prop);
                    node.Lines.Add(new GdbNodeLine { LineType = GdbNodeLineType.Property, Property = prop, RawText = $"\t{prop.Key}\t\t{prop.Value}" });
                    LoadGridForNode(node);
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e) => MoveSelectedProperty(-1);
        private void btnMoveDown_Click(object sender, EventArgs e) => MoveSelectedProperty(1);

        private void btnClearNodeFilter_Click(object sender, EventArgs e)
        {
            // Preserve selected node name
            var selectedNode = lbNodes.SelectedItem as GdbNode;
            string selectedName = selectedNode?.Name;
            txtFilter.Text = string.Empty;
            ApplyNodeFilter();
            if (!string.IsNullOrEmpty(selectedName))
            {
                var list = lbNodes.DataSource as IEnumerable<GdbNode>;
                if (list != null)
                {
                    var match = list.FirstOrDefault(n => string.Equals(n.Name, selectedName, StringComparison.Ordinal));
                    if (match != null)
                    {
                        int idx = list.ToList().IndexOf(match);
                        if (idx >= 0)
                        {
                            lbNodes.SelectedIndex = idx;
                        }
                    }
                }
            }
        }

        private static class ShellIconHelper
        {
            private const uint SHGFI_ICON = 0x000000100;
            private const uint SHGFI_SMALLICON = 0x000000001;
            private const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
            private const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
            private const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
            private struct SHFILEINFO
            {
                public IntPtr hIcon;
                public int iIcon;
                public uint dwAttributes;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string szDisplayName;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szTypeName;
            }

            [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

            [DllImport("User32.dll", SetLastError = true)]
            private static extern bool DestroyIcon(IntPtr hIcon);

            public static Bitmap GetSmallIconBitmap(bool isFolder, string extOrPath)
            {
                SHFILEINFO shinfo;
                uint attribs = isFolder ? FILE_ATTRIBUTE_DIRECTORY : FILE_ATTRIBUTE_NORMAL;
                string path = isFolder ? extOrPath : (extOrPath?.StartsWith(".") == true ? extOrPath : Path.GetFileName(extOrPath ?? string.Empty));
                IntPtr hImg = SHGetFileInfo(path, attribs, out shinfo, (uint)Marshal.SizeOf(typeof(SHFILEINFO)), SHGFI_ICON | SHGFI_SMALLICON | SHGFI_USEFILEATTRIBUTES);
                if (shinfo.hIcon != IntPtr.Zero)
                {
                    try
                    {
                        using (var ic = Icon.FromHandle(shinfo.hIcon))
                        {
                            return ic.ToBitmap();
                        }
                    }
                    finally
                    {
                        DestroyIcon(shinfo.hIcon);
                    }
                }
                return null;
            }
        }
    }
}
