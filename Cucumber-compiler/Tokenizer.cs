using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cucumber_compiler
{
    public class Tokenizer
    {
        private class Data
        {
            public int Position { get; set; }
            public string Str { get; set; }
            public string Comment { get; set; }
            public SourceInfo SrcInfo { get; set; }

            public Data(int position, SourceInfo srcInfo)
            {
                Position = position;
                SrcInfo = srcInfo;
            }
        }

        private string FileName { get; set; }
        private string SourceText { get; set; }

        private int SourceTextPosition { get; set; } = 0;
        private int SourceTextLineNumber { get; set; } = 1;
        private int SourceTextLinePosition { get; set; } = 1;

        //--------------------------------------------
        private Stack<int> LinePositions { get; set; } = new Stack<int>();
        private Stack<Data> Tokens { get; set; } = new Stack<Data>();
        private Stack<Data> Results { get; set; } = new Stack<Data>();

        public SourceInfo SrcInfo
        {
            get
            {
                if(Results.Count > 0)
                    return Results.Peek().SrcInfo;
                SkipSpaces();
                return new SourceInfo(FileName, SourceTextLineNumber, SourceTextLinePosition);
            }
        }

        public Tokenizer(string fileName, string sourceText)
        {
            FileName = fileName;
            SourceText = sourceText;
        }

        public string Read()
        {
            Data dataRet = ReadLineData();
            if (dataRet == null)
                return null;

            switch(dataRet.Str)
            {
                case "//":
                    {
                        dataRet.Comment = ReadSingleLineComnet(dataRet.Str);
                        return Read(); 
                    }
                case "/*":
                    {
                        dataRet.Comment = ReadMultiLineComnet(dataRet.Str);
                        return Read();
                    }
            }
            Tokens.Push(dataRet);
            return dataRet.Str;
        }

        private string ReadSingleLineComnet(string sourceText)
        {
            var strBuilder = new StringBuilder(sourceText);
            while (true)
            {
                var ch = ReadChar();
                if(ch == null || ch == '\r' || ch == '\n')
                    break;
                strBuilder.Append(ch);
            }
            return strBuilder.ToString();
        }

        private string ReadMultiLineComnet(string sourceText)
        {
            var strBuilder = new StringBuilder(sourceText);
            bool findCommentTerminator = false;
            while (true)
            {
                var ch = ReadChar();
                if (ch == null)
                    break;

                strBuilder.Append(ch);
                if (findCommentTerminator && ch == '/')
                    break;
                findCommentTerminator = ch == '*';
            }
            return strBuilder.ToString();
        }

        private bool CanRead()
        {
            return SourceText != null && SourceTextPosition < SourceText.Length;
        }

        private void SkipSpaces()
        {
            while(true)
            {
                var c = ReadChar();
                if (c == null)
                    break;
                if(c > ' ')
                {
                    RewindChar();
                    break;
                }
            }
        }

        private char? ReadChar()
        {
            if (!CanRead())
                return null;

            char ret = SourceText[SourceTextPosition++];
            if (ret == '\n')
            {
                LinePositions.Push(SourceTextLinePosition);

                SourceTextLinePosition = 1;
                SourceTextLineNumber++;
            }
            else
                SourceTextLinePosition++;

            return ret;
        }

        private void RewindChar()
        {
            if (SourceTextPosition <= 0) return;

            SourceTextPosition--;
            SourceTextLinePosition--;
            if (SourceTextLinePosition < 1)
            {
                SourceTextLinePosition = LinePositions.Pop();
                SourceTextLineNumber--;
            }
        }

        private Data ReadLineData()
        {
            if (Results.Count > 0)
                return Results.Pop();

            var dataRet = new Data(SourceTextPosition, SrcInfo);

            SkipSpaces();
            char? ch = null;
            char? st = null;
            bool isWord = true;
            bool useEscape = true;
            StringBuilder strBuilder = new StringBuilder();

            while ((ch = ReadChar()) != null)
            {
                if (st == null && ch == '\\' && useEscape)
                {
                    strBuilder.Append(ch);
                    ch = ReadChar();
                    if (ch == null)
                        break;
                    strBuilder.Append(ch);
                }
                else if (ch == '@' && strBuilder.Length == 0)
                {
                    strBuilder.Append(ch);
                    ch = ReadChar();
                    if (ch == null)
                        break;
                    else if (ch == '"')
                    {
                        st = ch;
                        useEscape = false;
                        strBuilder.Append(ch);
                    }
                    else
                        RewindChar();
                }
                else if (ch == '\'' || ch == '"')
                {
                    if (strBuilder.Length == 0)
                        st = ch;
                    else if(st == null)
                    {
                        RewindChar();
                        break;
                    }
                    else if(st == ch)
                    {
                        strBuilder.Append(ch);
                        break;
                    }
                    strBuilder.Append(ch);
                }
                else if (st != null)
                    strBuilder.Append(ch);
                else if (ch <= ' ')
                {
                    if (strBuilder.Length > 0)
                        break;
                }
                else if (IsWordChar((char)ch))
                {
                    if(!isWord)
                    {
                        RewindChar();
                        break;
                    }
                    strBuilder.Append(ch);
                }
                else
                {
                    isWord = false;
                    if (strBuilder.Length > 0)
                    {
                        // Verifica se a palavra é reservada...
                        bool? isReserved = true;
                        if (isReserved == false)
                            RewindChar();
                        else
                            strBuilder.Append(ch);

                        if (isReserved != null)
                            break;
                    }
                    else
                        strBuilder.Append(ch);
                }
            }
            if (strBuilder.Length == 0)
                return null;

            dataRet.Str = strBuilder.ToString();
            return dataRet;
        }

        private bool IsWordChar(char ch)
        {
            return ch > 128 || ch == '_' || char.IsLetterOrDigit(ch);
        }
    }
}
