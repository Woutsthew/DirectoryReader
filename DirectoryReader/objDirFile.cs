using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryReader
{
    class objDirFile
    {
        public string name { get; private set; }
        public long size { get; private set; }
        public MimeType mimeType { get; private set; }
        #region ext
        List<string> app = new List<string> { "exe", "lnk", "msi", "sln", "cpp", "zip", "rar", "7z", "jar", "cmd", "bat", "script" };
        List<string> video = new List<string> { "mp4", "avi", "mkv", "mpeg", "webm" };
        List<string> audio = new List<string> { "mp3", "wav" };
        List<string> image = new List<string> { "png", "jpg", "bmp", "gif", "webp", "ico" };
        List<string> text = new List<string> { "txt", "dll", "ini", "log", "cs", "h", "sys", "inf", "cat", "info", "dat", "css", "csv", "html", "php", "json", "xml", "py", "msg" };
        List<string> vendor = new List<string> { "pdf", "doc", "docx", "pptx", "ppt", "accdb", "pub", "xlsx", "xls", "odt", "odp", "ods"  };
        #endregion
        public objDirFile(string Name)
        {
            name = Name;
            size = new FileInfo(Name).Length;
            string ext = Name.Split('.').Last().ToLower();
            if (app.Contains(ext)) mimeType = MimeType.Application;
            else if (video.Contains(ext)) mimeType = MimeType.VideoFile;
            else if (audio.Contains(ext)) mimeType = MimeType.AudioFile;
            else if (image.Contains(ext)) mimeType = MimeType.ImageFile;
            else if (text.Contains(ext)) mimeType = MimeType.TextFile;
            else if (vendor.Contains(ext)) mimeType = MimeType.VendorFile;
            else mimeType = MimeType.NonStandartFile;
        }
        public objDirFile(string Name, long Size, MimeType MimeType)
        {
            name = Name;
            size = Size;
            mimeType = MimeType;
        }

        public string Info() { return $"{name} {size} {mimeType}"; }
        public List<string> InfoList() { return new List<string> { name, size.ToString(), mimeType.ToString() }; }
        public enum MimeType
        {
            Directory,
            Application,
            VideoFile,
            AudioFile,
            ImageFile,
            TextFile,
            VendorFile,
            NonStandartFile
        }
        static public List<MimeType> AllMimeType()
        {
            return new List<MimeType> { 
                MimeType.Directory, MimeType.Application, 
                MimeType.VideoFile, MimeType.AudioFile, 
                MimeType.ImageFile, MimeType.TextFile, 
                MimeType.VendorFile, MimeType.NonStandartFile };
        }
    }
}
