using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GDB_Editor.Gdb
{
    public static class GdbParser
    {
        public static GdbFile Parse(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var file = new GdbFile { FilePath = filePath };
            int index = 0;
            while (index < lines.Length)
            {
                if (IsBlank(lines[index]))
                {
                    file.Items.Add(new GdbRawLine { Text = lines[index] });
                    index++; continue;
                }
                if (IsComment(lines[index]))
                {
                    file.Items.Add(new GdbRawLine { Text = lines[index] });
                    index++; continue;
                }

                string header = lines[index].Trim();
                if (string.IsNullOrWhiteSpace(header)) { index++; continue; }
                if (header.StartsWith("//")) { file.Items.Add(new GdbRawLine { Text = lines[index++] }); continue; }
                string nodeName = header;
                index++;

                while (index < lines.Length && IsBlank(lines[index]))
                {
                    file.Items.Add(new GdbRawLine { Text = lines[index++] });
                }
                if (index >= lines.Length || !lines[index].TrimStart().StartsWith("{"))
                {
                    file.Items.Add(new GdbRawLine { Text = header });
                    continue;
                }
                index++;

                var node = new GdbNode { Name = nodeName };
                file.Items.Add(node);

                while (index < lines.Length)
                {
                    var raw = lines[index];
                    var trim = raw.Trim();
                    index++;

                    if (trim.StartsWith("}"))
                    {
                        break;
                    }
                    if (IsBlank(raw))
                    {
                        node.Lines.Add(new GdbNodeLine { LineType = GdbNodeLineType.Blank, RawText = raw });
                        continue;
                    }
                    if (IsComment(raw))
                    {
                        node.Lines.Add(new GdbNodeLine { LineType = GdbNodeLineType.Comment, RawText = raw });
                        continue;
                    }

                    // Parse key and spacing directly from raw to keep indices
                    int pos = 0;
                    // consume leading whitespace
                    while (pos < raw.Length && char.IsWhiteSpace(raw[pos])) pos++;
                    int keyStart = pos;
                    while (pos < raw.Length && !char.IsWhiteSpace(raw[pos])) pos++;
                    if (pos <= keyStart)
                    {
                        node.Lines.Add(new GdbNodeLine { LineType = GdbNodeLineType.Other, RawText = raw });
                        continue;
                    }
                    string key = raw.Substring(keyStart, pos - keyStart);
                    // consume whitespace to value start
                    while (pos < raw.Length && char.IsWhiteSpace(raw[pos])) pos++;
                    int valueStart = pos;

                    int commentPos = raw.IndexOf("//", valueStart, StringComparison.Ordinal);
                    string valueText = commentPos >= 0 ? raw.Substring(valueStart, commentPos - valueStart) : raw.Substring(valueStart);
                    string value = valueText.TrimEnd();

                    var prop = new GdbProperty { Key = key, Value = value };
                    var line = new GdbNodeLine
                    {
                        LineType = GdbNodeLineType.Property,
                        RawText = raw,
                        Property = prop,
                        ValueStartIndex = valueStart,
                        CommentStartIndex = commentPos
                    };
                    node.Properties.Add(prop);
                    node.Lines.Add(line);
                }
            }

            return file;
        }

        public static void Write(GdbFile file, string outputPath)
        {
            var sb = new StringBuilder();
            foreach (var item in file.Items)
            {
                if (item is GdbRawLine raw)
                {
                    sb.AppendLine(raw.Text);
                }
                else if (item is GdbNode node)
                {
                    sb.AppendLine(node.Name);
                    sb.AppendLine("{");
                    foreach (var line in node.Lines)
                    {
                        if (line.LineType == GdbNodeLineType.Property && line.Property != null)
                        {
                            // rebuild the line preserving original spacing and comment position
                            if (line.ValueStartIndex >= 0)
                            {
                                string beforeValue = line.RawText.Substring(0, line.ValueStartIndex);
                                string after = string.Empty;
                                if (line.CommentStartIndex >= 0)
                                {
                                    after = line.RawText.Substring(line.CommentStartIndex);
                                }
                                sb.AppendLine(beforeValue + line.Property.Value + after);
                            }
                            else
                            {
                                // fallback to tabbed format
                                sb.AppendLine($"\t{line.Property.Key}\t\t{line.Property.Value}");
                            }
                        }
                        else
                        {
                            sb.AppendLine(line.RawText);
                        }
                    }
                    sb.AppendLine("}");
                }
            }

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        private static bool IsBlank(string s) => string.IsNullOrWhiteSpace(s);
        private static bool IsComment(string s) => s.TrimStart().StartsWith("//");
    }
} 