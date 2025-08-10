using System.Collections.Generic;

namespace GDB_Editor.Gdb
{
    public class GdbFile
    {
        public string FilePath { get; set; }

        // Ordered items in the file: either comment/blank lines or nodes
        public List<IGdbFileItem> Items { get; set; } = new List<IGdbFileItem>();

        // Convenience: all nodes
        public List<GdbNode> Nodes
        {
            get
            {
                var list = new List<GdbNode>();
                foreach (var item in Items)
                {
                    if (item is GdbNode node) list.Add(node);
                }
                return list;
            }
        }
    }

    public interface IGdbFileItem { }

    public class GdbRawLine : IGdbFileItem
    {
        public string Text { get; set; }
    }

    public class GdbNode : IGdbFileItem
    {
        public string Name { get; set; }

        // Lines contained within the node, preserving comments/blank lines and property order
        public List<GdbNodeLine> Lines { get; set; } = new List<GdbNodeLine>();

        public List<GdbProperty> Properties { get; set; } = new List<GdbProperty>();

        public override string ToString() => Name;
    }

    public enum GdbNodeLineType
    {
        Property,
        Comment,
        Blank,
        Other
    }

    public class GdbNodeLine
    {
        public GdbNodeLineType LineType { get; set; }
        public string RawText { get; set; }
        public GdbProperty Property { get; set; }

        // For Property lines, indices into RawText to preserve formatting
        // Start index (in RawText) where the value begins (after key and spacing)
        public int ValueStartIndex { get; set; } = -1;
        // Start index of inline comment (//) in RawText if present, else -1
        public int CommentStartIndex { get; set; } = -1;
    }

    public class GdbProperty
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
} 