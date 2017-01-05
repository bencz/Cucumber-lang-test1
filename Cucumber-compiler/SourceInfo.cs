using System.Xml;

namespace Cucumber_compiler
{
    public class SourceInfo
    {
        public string Source { get; private set; }
        public int LineNumber { get; private set; }
        public int Position { get; private set; }

        public SourceInfo(string source, int lineNumber, int position)
        {
            Source = source;
            LineNumber = lineNumber;
            Position = position;
        }

        public SourceInfo(string source, XmlTextReader xmlReader)
            : this(source, xmlReader.LineNumber, xmlReader.LinePosition)
        {
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} - {2}", Source, LineNumber, Position);
        }
    }
}
