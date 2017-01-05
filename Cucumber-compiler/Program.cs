using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cucumber_compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo fi = new FileInfo("arquivo.txt");
            var tok = new Tokenizer(fi.Name, File.ReadAllText(fi.FullName));
            while (true)
            {
                var tk = tok.Read();
                if (tk == null)
                    break;
            }
        }
    }
}
