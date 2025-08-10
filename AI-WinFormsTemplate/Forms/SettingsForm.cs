using System;
using System.IO;
using System.Windows.Forms;

namespace GDB_Editor.Forms
{
    public partial class SettingsForm : Form
    {
        public string DatabaseRoot
        {
            get => txtDatabaseRoot.Text;
            set => txtDatabaseRoot.Text = value ?? string.Empty;
        }

        public string ModsRoot
        {
            get => txtModsRoot.Text;
            set => txtModsRoot.Text = value ?? string.Empty;
        }

        public SettingsForm()
        {
            InitializeComponent();
            // Initialize with current settings
            txtDatabaseRoot.Text = Properties.Settings.Default.DatabaseRootPath;
            var mods = Properties.Settings.Default.ModsRootPath;
            if (string.IsNullOrWhiteSpace(mods))
            {
                var defMods = Path.Combine(GetProjectRoot(), "dataMods");
                try { Directory.CreateDirectory(defMods); } catch { }
                mods = defMods;
            }
            txtModsRoot.Text = mods;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                try
                {
                    // Start at project root files folder if it exists
                    var projectRoot = GetProjectRoot();
                    var filesPath = Path.Combine(projectRoot, "dataSource");
                    if (Directory.Exists(filesPath))
                    {
                        dlg.SelectedPath = filesPath;
                    }
                }
                catch { }

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    // Now accepts any folder root (no longer requires Database)
                    txtDatabaseRoot.Text = dlg.SelectedPath;
                }
            }
        }

        private void btnBrowseMods_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                try
                {
                    var projectRoot = GetProjectRoot();
                    var modsPath = Path.Combine(projectRoot, "dataMods");
                    if (Directory.Exists(modsPath)) dlg.SelectedPath = modsPath;
                }
                catch { }

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    txtModsRoot.Text = dlg.SelectedPath;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var path = txtDatabaseRoot.Text?.Trim();
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                MessageBox.Show(this, "Please provide a valid Database folder path.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Properties.Settings.Default.DatabaseRootPath = path;
            Properties.Settings.Default.ModsRootPath = string.IsNullOrWhiteSpace(txtModsRoot.Text) ? string.Empty : txtModsRoot.Text.Trim();
            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private static string GetProjectRoot()
        {
            // Try walking up from current directory to find the solution folder containing dataSource\
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 6 && !string.IsNullOrEmpty(dir); i++)
            {
                var candidate = Path.Combine(dir, "dataSource");
                if (Directory.Exists(candidate))
                {
                    return dir;
                }
                dir = Path.GetDirectoryName(dir?.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
            }
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
} 