using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory;
            string help = "you can used two command:\n1.dir - to select a directory\n2.search - run the program with the selected path";
            DirReader reader = new DirReader(path);
            showcurrentdir(path);
            while(true)
            {
                string message = Console.ReadLine();
                if (message.Contains("dir")) { reader.directirypath = message.Split(new char[] { ' ' }, 2).Last(); showcurrentdir(path); }
                else if (message.Contains("search")) {
                    reader.SearchFolderAndFile();
                    reader.ls.ForEach(f => Console.WriteLine(f.Info()));
                    Console.WriteLine("\n\n\n\n");
                    reader.lsnonaccess.ForEach(f => Console.WriteLine(f.Info()));
                }
                else if (message.Contains("?") || message.Contains("help")) Console.WriteLine(help);
                else Console.WriteLine($"\nunknown command\n{help}");
            }
        }

        static private void showcurrentdir(string path)
        {
            string mes = $"explore directory: \n{path}";
            Console.Clear(); Console.WriteLine(mes);
        }
    }
}
