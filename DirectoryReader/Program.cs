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
        static List<objDirFile> ls;
        static void Main(string[] args)
        {
            ls = new List<objDirFile>();
            string path = Environment.CurrentDirectory;
            string help = "you can used two command:\n1.dir - to select a directory\n2.search - run the program with the selected path";
            showcurrentdir(path);
            while(true)
            {
                string message = Console.ReadLine();
                if (message.Contains("dir")) { path = message.Split(' ').Last(); showcurrentdir(path); }
                else if (message.Contains("search")) SearchFolderAndFile(path);
                else if (message.Contains("?") || message.Contains("help")) Console.WriteLine(help);
                else Console.WriteLine($"\nunknown command\n{help}");
            }
        }

        static private void showcurrentdir(string path)
        {
            string mes = $"explore directory: \n{path}";
            Console.Clear(); Console.WriteLine(mes);
        }
        static private void SearchFolderAndFile(string path)
        {
            if (!Directory.Exists(path)) { Console.WriteLine("this path is no directory"); return; }

            List<string> listdir = Directory.GetDirectories(path,"*", SearchOption.AllDirectories).ToList();
            foreach (string line in listdir) ls.Add(new objDirFile(line, objDirFile.MimeType.Directory));

            List<string> listfile = Directory.GetFiles(path,"*", SearchOption.AllDirectories).ToList();
            foreach (string line in listfile) ls.Add(new objDirFile(line));

            ls.ForEach(f => Console.WriteLine(f.Info()));
        }
    }
}
