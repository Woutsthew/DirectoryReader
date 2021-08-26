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
        List<string> app = new List<string> { "exe", "lnk", "msi", "sln", "zip", "rar", "7z", "cmd" };
        List<string> video = new List<string> { "mp4", "avi", "mkv", "mpeg", "webm" };
        List<string> audio = new List<string> { "mp3" };
        List<string> image = new List<string> { "png", "jpg", "bmp", "gif", "webp" };
        List<string> text = new List<string> { "txt", "dll", "css", "csv", "html", "php", "json", "xml", "py" };
        List<string> vendor = new List<string> { "pdf", "doc", "docx", "pptx", "ppt", "accdb", "pub", "xlsx", "xls", "odt", "odp", "ods" };
        #endregion
        public objDirFile(string Name)
        {
            name = Name;
            size = new FileInfo(Name).Length;
            string ext = Name.Split('.').Last();
            if (app.Contains(ext)) mimeType = MimeType.Application;
            else if (video.Contains(ext)) mimeType = MimeType.VideoFile;
            else if (audio.Contains(ext)) mimeType = MimeType.AudioFile;
            else if (image.Contains(ext)) mimeType = MimeType.ImageFile;
            else if (text.Contains(ext)) mimeType = MimeType.TextFile;
            else if (vendor.Contains(ext)) mimeType = MimeType.VendorFile;
            else mimeType = MimeType.NonStandartFile;
        }
        public objDirFile(string Name, MimeType MimeType)
        {
            name = Name;
            size = new DirectoryInfo(Name).GetFiles("*", SearchOption.AllDirectories).Sum(fl => fl.Length);
            mimeType = MimeType;
        }

        public string Info() { return $"{name} {size} {mimeType}"; }
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
    }
}
