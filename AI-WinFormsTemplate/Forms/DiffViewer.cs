using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GDB_Editor.Forms
{
    [Flags]
    public enum DiffMarkerFilter
    {
        None = 0,
        Added = 1,
        Removed = 2,
        Modified = 4,
        All = Added | Removed | Modified
    }

    public class DiffViewer : Control
    {
        private VScrollBar vScroll;
        private Panel panel;
        private Panel markerPanel; // gutter for change markers
        private string titleLeft = "Left";
        private string titleRight = "Right";

        private class DiffLine
        {
            public string LeftText;
            public string RightText;
            public DiffChangeType ChangeType;
        }

        private enum DiffChangeType
        {
            Unchanged,
            Added,
            Removed,
            Modified
        }

        private DiffMarkerFilter _markerFilter = DiffMarkerFilter.All;
        public DiffMarkerFilter MarkerFilter
        {
            get => _markerFilter;
            set
            {
                _markerFilter = value;
                if (markerPanel != null)
                {
                    markerPanel.Invalidate();
                }
            }
        }

        private readonly List<DiffLine> diffLines = new List<DiffLine>();
        private readonly int lineHeight = 18;
        private readonly int gutterWidth = 50;
        private readonly int colGap = 12;
        private readonly Font monoFont = new Font("Consolas", 9f);

        private bool isSelecting;
        private int selectStartLine = -1;
        private int selectEndLine = -1;

        public DiffViewer()
        {
            DoubleBuffered = true;
            vScroll = new VScrollBar { Dock = DockStyle.Right, Width = 18 };
            vScroll.Scroll += (s, e) => { panel.Invalidate(); markerPanel.Invalidate(); };

            markerPanel = new Panel { Dock = DockStyle.Right, Width = 14 };
            markerPanel.Paint += MarkerPanel_Paint;
            markerPanel.MouseDown += MarkerPanel_MouseDown;

            panel = new Panel { Dock = DockStyle.Fill };
            panel.Paint += Panel_Paint;
            panel.Resize += (s, e) => { UpdateScroll(); panel.Invalidate(); markerPanel.Invalidate(); };
            panel.MouseWheel += Panel_MouseWheel;
            panel.MouseDown += Panel_MouseDown;
            panel.MouseMove += Panel_MouseMove;
            panel.MouseUp += Panel_MouseUp;

            Controls.Add(panel);
            Controls.Add(markerPanel);
            Controls.Add(vScroll);
        }

        public (int total, int added, int removed, int modified) GetChangeCounts()
        {
            int added = diffLines.Count(d => d.ChangeType == DiffChangeType.Added);
            int removed = diffLines.Count(d => d.ChangeType == DiffChangeType.Removed);
            int modified = diffLines.Count(d => d.ChangeType == DiffChangeType.Modified);
            int total = added + removed + modified;
            return (total, added, removed, modified);
        }

        private void MarkerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // Jump to approximate position in file when clicking marker gutter
            int total = Math.Max(1, diffLines.Count);
            int trackTop = 2;
            int trackHeight = Math.Max(1, markerPanel.ClientSize.Height - 4);
            int y = Math.Max(0, Math.Min(trackHeight, e.Y - trackTop));
            int line = (int)Math.Round((y / (float)trackHeight) * (total - 1));
            vScroll.Value = Math.Max(vScroll.Minimum, Math.Min(vScroll.Maximum - vScroll.LargeChange + 1, line));
            panel.Invalidate();
            markerPanel.Invalidate();
        }

        private void MarkerPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            int total = Math.Max(1, diffLines.Count);
            int trackTop = 2;
            int trackHeight = Math.Max(1, markerPanel.ClientSize.Height - 4);

            foreach (var group in diffLines.Select((d, i) => new { d.ChangeType, Index = i })
                                           .Where(x => x.ChangeType != DiffChangeType.Unchanged)
                                           .GroupBy(x => x.ChangeType))
            {
                if (group.Key == DiffChangeType.Added && (MarkerFilter & DiffMarkerFilter.Added) == 0) continue;
                if (group.Key == DiffChangeType.Removed && (MarkerFilter & DiffMarkerFilter.Removed) == 0) continue;
                if (group.Key == DiffChangeType.Modified && (MarkerFilter & DiffMarkerFilter.Modified) == 0) continue;

                Color c = group.Key == DiffChangeType.Added ? Color.LimeGreen :
                           group.Key == DiffChangeType.Removed ? Color.OrangeRed :
                           Color.Gold;
                using (var b = new SolidBrush(c))
                {
                    foreach (var item in group)
                    {
                        int y = trackTop + (int)(item.Index / (float)total * trackHeight);
                        e.Graphics.FillRectangle(b, new Rectangle(1, y, markerPanel.ClientSize.Width - 2, 3));
                    }
                }
            }

            // viewport indicator
            int visible = Math.Max(1, (panel.ClientSize.Height - lineHeight - 6) / lineHeight);
            float fracStart = vScroll.Value / (float)Math.Max(1, total);
            int thumbY = trackTop + (int)(fracStart * trackHeight);
            int thumbH = Math.Max(6, (int)(visible / (float)total * trackHeight));
            using (var pen = new Pen(Color.DimGray))
            using (var b = new SolidBrush(Color.FromArgb(60, Color.DimGray)))
            {
                e.Graphics.FillRectangle(b, new Rectangle(1, thumbY, markerPanel.ClientSize.Width - 2, thumbH));
                e.Graphics.DrawRectangle(pen, new Rectangle(1, thumbY, markerPanel.ClientSize.Width - 3, thumbH - 1));
            }
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            int line = YToLine(e.Y);
            isSelecting = true;
            selectStartLine = selectEndLine = line;
            panel.Invalidate();
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isSelecting) return;
            selectEndLine = YToLine(e.Y);
            panel.Invalidate();
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            isSelecting = false;
            panel.Invalidate();
        }

        private int YToLine(int y)
        {
            int startIndex = vScroll.Value;
            int rel = Math.Max(0, y - (lineHeight + 4));
            int delta = rel / lineHeight;
            return Math.Max(0, Math.Min(diffLines.Count - 1, startIndex + delta));
        }

        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            int deltaLines = -(e.Delta / 120) * 3;
            int newVal = Math.Max(vScroll.Minimum, Math.Min(vScroll.Maximum - vScroll.LargeChange + 1, vScroll.Value + deltaLines));
            if (newVal != vScroll.Value)
            {
                vScroll.Value = newVal;
                panel.Invalidate();
                markerPanel.Invalidate();
            }
        }

        public void SetTitles(string left, string right)
        {
            titleLeft = left;
            titleRight = right;
            panel.Invalidate();
        }

        public void LoadText(string leftText, string rightText)
        {
            var left = (leftText ?? string.Empty).Replace("\r\n", "\n").Split('\n');
            var right = (rightText ?? string.Empty).Replace("\r\n", "\n").Split('\n');
            BuildDiff(left, right);
            UpdateScroll();
            panel.Invalidate();
            markerPanel.Invalidate();
        }

        private void UpdateScroll()
        {
            int visibleLines = Math.Max(1, (panel.ClientSize.Height - lineHeight - 6) / lineHeight);
            vScroll.Minimum = 0;
            vScroll.Maximum = Math.Max(0, diffLines.Count - 1);
            vScroll.LargeChange = visibleLines;
            vScroll.SmallChange = 1;
            vScroll.Enabled = diffLines.Count > visibleLines;
            vScroll.Value = Math.Min(vScroll.Value, Math.Max(0, vScroll.Maximum - vScroll.LargeChange + 1));
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.Window);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int width = panel.ClientSize.Width - vScroll.Width - markerPanel.Width;
            int half = (width - colGap) / 2;
            int leftStart = gutterWidth;
            int rightStart = gutterWidth + half + colGap;

            using (var headerFont = new Font(Font, FontStyle.Bold))
            {
                e.Graphics.DrawString(titleLeft, headerFont, Brushes.Black, leftStart, 2);
                e.Graphics.DrawString(titleRight, headerFont, Brushes.Black, rightStart, 2);
            }

            int startIndex = vScroll.Value;
            int y = lineHeight + 4;
            int maxY = panel.ClientSize.Height;

            int selMin = Math.Min(selectStartLine, selectEndLine);
            int selMax = Math.Max(selectStartLine, selectEndLine);

            for (int i = startIndex; i < diffLines.Count && y + lineHeight <= maxY; i++)
            {
                var dl = diffLines[i];
                Color back;
                switch (dl.ChangeType)
                {
                    case DiffChangeType.Added: back = Color.FromArgb(220, 255, 220); break;
                    case DiffChangeType.Removed: back = Color.FromArgb(255, 220, 220); break;
                    case DiffChangeType.Modified: back = Color.FromArgb(255, 255, 180); break;
                    default: back = SystemColors.Window; break;
                }

                using (var b = new SolidBrush(back))
                {
                    e.Graphics.FillRectangle(b, new Rectangle(0, y, width, lineHeight));
                }

                if (selectStartLine >= 0 && selectEndLine >= 0 && i >= selMin && i <= selMax)
                {
                    using (var sb = new SolidBrush(Color.FromArgb(60, Color.DodgerBlue)))
                    {
                        e.Graphics.FillRectangle(sb, new Rectangle(0, y, width, lineHeight));
                    }
                }

                string num = (i + 1).ToString();
                TextRenderer.DrawText(e.Graphics, num, monoFont, new Rectangle(0, y, gutterWidth - 6, lineHeight), Color.Gray, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                TextRenderer.DrawText(e.Graphics, dl.LeftText ?? string.Empty, monoFont, new Rectangle(leftStart, y, half - gutterWidth, lineHeight), Color.Black, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                TextRenderer.DrawText(e.Graphics, dl.RightText ?? string.Empty, monoFont, new Rectangle(rightStart, y, half - gutterWidth, lineHeight), Color.Black, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                y += lineHeight;
            }

            using (var pen = new Pen(Color.Silver))
            {
                e.Graphics.DrawLine(pen, gutterWidth + half + colGap / 2, lineHeight + 4, gutterWidth + half + colGap / 2, maxY);
            }
        }

        private void BuildDiff(string[] left, string[] right)
        {
            diffLines.Clear();
            var lcs = LongestCommonSubsequence(left, right);
            int i = 0, j = 0;
            foreach (var match in lcs)
            {
                EmitSegment(left, i, match.I, right, j, match.J);
                i = match.I; j = match.J;
                // Guard against reaching end due to previous loops pairing items (i/j may equal length)
                if (i >= left.Length || j >= right.Length)
                {
                    break;
                }
                string lt = left[i];
                string rt = right[j];
                bool equal = LinesEqual(lt, rt);
                diffLines.Add(new DiffLine
                {
                    LeftText = lt,
                    RightText = rt,
                    ChangeType = equal ? DiffChangeType.Unchanged : DiffChangeType.Modified
                });
                i++; j++;
            }
            EmitSegment(left, i, left.Length, right, j, right.Length);
        }

        private void EmitSegment(string[] left, int iStart, int iEnd, string[] right, int jStart, int jEnd)
        {
            // Fast path: if either side empty, emit pure adds/removes (ignoring pure blanks)
            if (iStart >= iEnd)
            {
                for (int r = jStart; r < jEnd; r++)
                {
                    string rr = right[r];
                    if (!string.IsNullOrWhiteSpace(rr))
                        diffLines.Add(new DiffLine { LeftText = string.Empty, RightText = rr, ChangeType = DiffChangeType.Added });
                }
                return;
            }
            if (jStart >= jEnd)
            {
                for (int l = iStart; l < iEnd; l++)
                {
                    string ll = left[l];
                    if (!string.IsNullOrWhiteSpace(ll))
                        diffLines.Add(new DiffLine { LeftText = ll, RightText = string.Empty, ChangeType = DiffChangeType.Removed });
                }
                return;
            }

            // Build queues of right indices by key for stable pairing
            var rightQueues = new Dictionary<string, Queue<int>>(StringComparer.Ordinal);
            for (int r = jStart; r < jEnd; r++)
            {
                string rk = GetKey(right[r]);
                if (!rightQueues.TryGetValue(rk, out var q)) { q = new Queue<int>(); rightQueues[rk] = q; }
                q.Enqueue(r);
            }

            // Map left index -> paired right index (or -1)
            var pairMap = new Dictionary<int, int>();
            for (int l = iStart; l < iEnd; l++)
            {
                string lk = GetKey(left[l]);
                if (rightQueues.TryGetValue(lk, out var q) && q.Count > 0)
                {
                    int rIdx = q.Dequeue();
                    pairMap[l] = rIdx;
                }
                else
                {
                    pairMap[l] = -1;
                }
            }

            // Walk both segments in order, using the planned pairs to keep relative order
            int i = iStart, j = jStart;
            while (i < iEnd || j < jEnd)
            {
                bool iHas = i < iEnd;
                bool jHas = j < jEnd;
                if (iHas && jHas && pairMap.TryGetValue(i, out int paired) && paired == j)
                {
                    bool eq = LinesEqual(left[i], right[j]);
                    diffLines.Add(new DiffLine { LeftText = left[i], RightText = right[j], ChangeType = eq ? DiffChangeType.Unchanged : DiffChangeType.Modified });
                    i++; j++;
                }
                else if (iHas && (!pairMap.ContainsKey(i) || pairMap[i] == -1))
                {
                    if (!string.IsNullOrWhiteSpace(left[i]))
                        diffLines.Add(new DiffLine { LeftText = left[i], RightText = string.Empty, ChangeType = DiffChangeType.Removed });
                    i++;
                }
                else if (jHas)
                {
                    if (!string.IsNullOrWhiteSpace(right[j]))
                        diffLines.Add(new DiffLine { LeftText = string.Empty, RightText = right[j], ChangeType = DiffChangeType.Added });
                    j++;
                }
            }
        }

        private static string GetKey(string line)
        {
            if (string.IsNullOrEmpty(line)) return string.Empty;
            int pos = 0;
            while (pos < line.Length && char.IsWhiteSpace(line[pos])) pos++;
            int keyStart = pos;
            while (pos < line.Length && !char.IsWhiteSpace(line[pos])) pos++;
            return NormalizeForCompare(pos > keyStart ? line.Substring(keyStart, pos - keyStart) : string.Empty);
        }

        private static int FirstValueIndex(string line)
        {
            if (line == null) return -1;
            int pos = 0;
            while (pos < line.Length && char.IsWhiteSpace(line[pos])) pos++;
            while (pos < line.Length && !char.IsWhiteSpace(line[pos])) pos++;
            while (pos < line.Length && char.IsWhiteSpace(line[pos])) pos++;
            return pos < line.Length ? pos : -1;
        }

        private struct Match { public int I; public int J; }

        private static List<Match> LongestCommonSubsequence(string[] a, string[] b)
        {
            int n = a.Length, m = b.Length;
            // Pre-normalize for stable equality that ignores indentation and internal spacing differences
            var na = new string[n];
            var nb = new string[m];
            for (int ii = 0; ii < n; ii++) na[ii] = NormalizeForCompare(a[ii]);
            for (int jj = 0; jj < m; jj++) nb[jj] = NormalizeForCompare(b[jj]);
            int[,] dp = new int[n + 1, m + 1];
            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = m - 1; j >= 0; j--)
                {
                    if (na[i] == nb[j]) dp[i, j] = dp[i + 1, j + 1] + 1; else dp[i, j] = Math.Max(dp[i + 1, j], dp[i, j + 1]);
                }
            }
            var matches = new List<Match>();
            int x = 0, y = 0;
            while (x < n && y < m)
            {
                if (na[x] == nb[y])
                {
                    matches.Add(new Match { I = x, J = y });
                    x++; y++;
                }
                else if (dp[x + 1, y] >= dp[x, y + 1]) x++; else y++;
            }
            return matches;
        }

        private static bool LinesEqual(string a, string b)
        {
            return string.Equals(NormalizeForCompare(a), NormalizeForCompare(b), StringComparison.Ordinal);
        }

        private static string NormalizeForCompare(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            // Trim right, collapse internal whitespace to single spaces, and drop leading indentation
            string text = s.TrimEnd();
            var sb = new StringBuilder(text.Length);
            bool inWhitespace = false;
            bool started = false; // have we seen a non-whitespace character yet?
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsWhiteSpace(c))
                {
                    if (!started)
                    {
                        // skip leading indentation entirely
                        continue;
                    }
                    inWhitespace = true;
                    continue;
                }
                if (inWhitespace && sb.Length > 0)
                {
                    sb.Append(' ');
                }
                inWhitespace = false;
                started = true;
                sb.Append(c);
            }
            // Remove any single space immediately before inline comment markers
            var mid = sb.ToString();
            if (mid.IndexOf("//", StringComparison.Ordinal) >= 0)
            {
                var outSb = new StringBuilder(mid.Length);
                for (int i = 0; i < mid.Length; i++)
                {
                    if (mid[i] == ' ' && i + 2 < mid.Length && mid[i + 1] == '/' && mid[i + 2] == '/')
                    {
                        continue; // skip the space before //
                    }
                    outSb.Append(mid[i]);
                }
                return outSb.ToString();
            }
            return mid;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateScroll();
            panel.Invalidate();
            markerPanel.Invalidate();
        }
    }
} 