using System;
using System.Drawing;
using System.Windows.Forms;

namespace GDB_Editor.Forms
{
    public partial class CompareForm : Form
    {
        private readonly string originalPath;
        private readonly string editedPath;

        public CompareForm(string originalPath, string editedPath)
        {
            this.originalPath = originalPath;
            this.editedPath = editedPath;
            InitializeComponent();
            diffViewer1.SetTitles("Original", "Edited");
            LoadDiff();
            UpdateCounts();
        }

        public void LoadDiff()
        {
            string left = SafeReadAllText(originalPath);
            string right = SafeReadAllText(editedPath);
            diffViewer1.LoadText(left, right);
            UpdateCounts();
        }

        private void UpdateCounts()
        {
            var (total, added, removed, modified) = diffViewer1.GetChangeCounts();
            lblTotal.Text = $"Total: {total}";
            lblAdded.Text = $"Added: {added}";
            lblRemoved.Text = $"Removed: {removed}";
            lblModified.Text = $"Modified: {modified}";
        }

        private static string SafeReadAllText(string path)
        {
            try
            {
                return System.IO.File.Exists(path) ? System.IO.File.ReadAllText(path) : string.Empty;
            }
            catch (Exception ex)
            {
                return $"(error reading file)\r\n{ex.Message}";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDiff();
        }

        private void ToggleFilter(DiffMarkerFilter flag, Label label)
        {
            if ((diffViewer1.MarkerFilter & flag) != 0)
            {
                diffViewer1.MarkerFilter &= ~flag;
                label.Font = new Font(label.Font, FontStyle.Regular);
                label.ForeColor = ControlPaint.Dark(label.ForeColor);
            }
            else
            {
                diffViewer1.MarkerFilter |= flag;
                label.Font = new Font(label.Font, FontStyle.Bold);
                // restore bright color
                if (flag == DiffMarkerFilter.Added) label.ForeColor = Color.LimeGreen;
                if (flag == DiffMarkerFilter.Removed) label.ForeColor = Color.OrangeRed;
                if (flag == DiffMarkerFilter.Modified) label.ForeColor = Color.Goldenrod;
            }
            diffViewer1.Invalidate();
        }

        private void lblAdded_Click(object sender, EventArgs e)
        {
            ToggleFilter(DiffMarkerFilter.Added, lblAdded);
        }

        private void lblRemoved_Click(object sender, EventArgs e)
        {
            ToggleFilter(DiffMarkerFilter.Removed, lblRemoved);
        }

        private void lblModified_Click(object sender, EventArgs e)
        {
            ToggleFilter(DiffMarkerFilter.Modified, lblModified);
        }
    }
} 