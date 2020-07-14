using System;
using System.Text;

namespace MobileCore
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendLine(this StringBuilder builder, string line, int indentationLevel)
        {
            var lineWithIndentation = line.PadLeft(line.Length + indentationLevel, '\t');
            return builder.AppendLine(lineWithIndentation);
        }
    }
}
