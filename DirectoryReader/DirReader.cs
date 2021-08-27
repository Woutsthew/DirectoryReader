using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryReader
{
    class DirReader
    {
        public List<objDirFile> ls { get; private set; }
        public List<objDirFile> lsnonaccess { get; private set; }
        public string directirypath { get; set; }

        public DirReader(string directirypath)
        {
            this.directirypath = directirypath;
        }
        public void SearchFolderAndFile()
        {
            if (!Directory.Exists(directirypath)) { Console.WriteLine("this path is no directory"); return; }
            ls = new List<objDirFile>();
            lsnonaccess = new List<objDirFile>();
            SearchFolder(directirypath);
            ls.Reverse(); lsnonaccess.Reverse();
        }
        private long SearchFolder(string folderpath)
        {
            long size = 0;
            try
            {
                List<string> folders = Directory.GetDirectories(folderpath).ToList();
                foreach (string foldername in folders) size += SearchFolder(foldername);
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
