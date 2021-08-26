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
        static List<objDirFile> lsnonaccess;
        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory;
            string help = "you can used two command:\n1.dir - to select a directory\n2.search - run the program with the selected path";
            showcurrentdir(path);
            while(true)
            {
                ls = new List<objDirFile>();
                lsnonaccess = new List<objDirFile>();
                string message = Console.ReadLine();
                if (message.Contains("dir")) { path = message.Split(new char[] { ' ' }, 2).Last(); showcurrentdir(path); }
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

            SF(path);

            ls.ForEach(f => Console.WriteLine(f.Info()));
            Console.WriteLine("\n\n\n\n");
            lsnonaccess.ForEach(f => Console.WriteLine(f.Info()));
        }
        static private long SearchFolder(string folderpath)
        {
            List<string> folders = Directory.GetDirectories(folderpath).ToList();
            long size = 0;
            foreach(string line in folders)
            {
                long sizefile = 0;
                try
                {
                    Directory.GetDirectories(line);
                    List<string> lsFile = Directory.GetFiles(line).ToList();
                    foreach (string filename in lsFile)
                    {
                        objDirFile fl = new objDirFile(filename);
                        //ls.Add(fl);
                        sizefile += fl.size;
                    }
                    sizefile += SearchFolder(line);
                    ls.Add(new objDirFile(line, sizefile, objDirFile.MimeType.Directory));
                }
                catch
                {
                    lsnonaccess.Add(new objDirFile(line, 0, objDirFile.MimeType.Directory));
                }
                size += sizefile;
            }
            return size;
        }
        static private long SF(string folderpath)
        {
            long size = 0;
            try
            {
                List<string> folders = Directory.GetDirectories(folderpath).ToList();
                foreach(string foldername in folders) size += SF(foldername);
                List<string> files = Directory.GetFiles(folderpath).ToList();
                foreach (string filename in files)
                {
                    objDirFile fl = new objDirFile(filename);
                    ls.Add(fl); size += fl.size;
                }
                ls.Add(new objDirFile(folderpath, size, objDirFile.MimeType.Directory));
            }
            catch { lsnonaccess.Add(new objDirFile(folderpath, 0, objDirFile.MimeType.Directory)); }

            return size;
        }
    }
}
