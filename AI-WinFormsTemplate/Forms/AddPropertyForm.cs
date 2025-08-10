using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace GDB_Editor.Forms
{
    public partial class AddPropertyForm : Form
    {
        private readonly List<string> allKeys;
        private BindingList<string> viewKeys;
        private bool _suppressTextUpdate;

        public string PropertyKey => cmbKey.Text.Trim();
        public string PropertyValue => txtValue.Text;

        public AddPropertyForm(IEnumerable<string> availableKeys)
        {
            InitializeComponent();
            allKeys = (availableKeys ?? Enumerable.Empty<string>())
                .Where(k => !string.IsNullOrWhiteSpace(k))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(k => k, StringComparer.OrdinalIgnoreCase)
                .ToList();

            viewKeys = new BindingList<string>(new List<string>(allKeys));
            cmbKey.DataSource = viewKeys;
            cmbKey.SelectedIndex = -1;
            cmbKey.TextUpdate += cmbKey_TextUpdate;
            cmbKey.DropDown += cmbKey_DropDown;
            cmbKey.DropDownStyle = ComboBoxStyle.DropDown; // allow typing
            cmbKey.AutoCompleteMode = AutoCompleteMode.None;
            cmbKey.AutoCompleteSource = AutoCompleteSource.None;
        }

        private void cmbKey_DropDown(object sender, EventArgs e)
        {
            try
            {
                Cursor.Show();
                Cursor.Current = Cursors.Default;
            }
            catch { }
        }

        private void cmbKey_TextUpdate(object sender, EventArgs e)
        {
            if (_suppressTextUpdate) return;

            var text = cmbKey.Text;
            var selStart = cmbKey.SelectionStart;
            var filtered = (string.IsNullOrWhiteSpace(text)
                ? allKeys
                : allKeys.Where(k => k.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0))
                .ToList();

            _suppressTextUpdate = true;
            cmbKey.BeginUpdate();
            cmbKey.DataSource = null; // force rebuild
            cmbKey.Items.Clear();
            viewKeys = new BindingList<string>(filtered);
            cmbKey.DataSource = viewKeys;
            cmbKey.SelectedIndex = -1;
            cmbKey.EndUpdate();

            if (!cmbKey.DroppedDown)
            {
                cmbKey.DroppedDown = true;
            }
            try
            {
                Cursor.Show();
                Cursor.Current = Cursors.Default;
            }
            catch { }

            cmbKey.Text = text; // restore exactly what user typed
            cmbKey.SelectionStart = selStart;
            cmbKey.SelectionLength = 0;
            _suppressTextUpdate = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PropertyKey))
            {
                MessageBox.Show(this, "Key is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbKey.Focus();
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
} 