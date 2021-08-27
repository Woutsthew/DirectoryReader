using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static DirectoryReader.objDirFile;

namespace DirectoryReader
{
    class Program
    {
        static string path = Environment.CurrentDirectory;
        static void Main(string[] args)
        {
            string help = "you can used two command:\n1.dir - to select a directory\n2.search - run the program with the selected path";
            DirReader reader = new DirReader(path);
            showcurrentdir(path);
            while(true)
            {
                string message = Console.ReadLine();
                if (message.Contains("dir")) { reader.directorypath = message.Split(new char[] { ' ' }, 2).Last(); showcurrentdir(reader.directorypath); }
                else if (message.Contains("search")) { searchcommand(reader); }
                else if (message.Contains("?") || message.Contains("help")) Console.WriteLine(help);
                else Console.WriteLine($"\nunknown command\n{help}");
            }
        }

        static private void showcurrentdir(string path)
        {
            string mes = $"explore directory: \n{path}";
            Console.Clear(); Console.WriteLine(mes);
        }
        static private void searchcommand(DirReader reader)
        {
            reader.SearchFolderAndFile();
            List<string> dirfilecolunms = new List<string> { "Name", "Size", "MimeType" };
            List<string> statistics = new List<string> { "MimeType", "% from all", "Average size" };

            HTMLFile table = new HTMLFile(statistics);
            foreach (MimeType type in AllMimeType())
            {
                List<objDirFile> tmp = reader.ls.Where(fl => fl.mimeType == type).ToList();
                double ratio = (double)tmp.Count / reader.ls.Count * 100;
                string percent = Math.Round(ratio, 2).ToString();
                long sumsize = tmp.Sum(fl => fl.size);
                string averagesize = Math.Round((double)sumsize / tmp.Count, 2).ToString();
                table.ADDRow(new List<string> { type.ToString(), percent, averagesize });
            }
            foreach (MimeType type in AllMimeType())
            {
                List<objDirFile> tmp = reader.ls.Where(fl => fl.mimeType == type).ToList();
                if (tmp.Count == 0) continue;
                table.ADDTable(dirfilecolunms); table.ADDRows(tmp);
            }
            if (reader.lsnonaccess.Count != 0)
            {
                table.ADDTable(dirfilecolunms);
                table.ADDRows(reader.lsnonaccess);
            }
            table.SaveFile(path, "file");

            Console.WriteLine("operation done");
        }
    }
}
