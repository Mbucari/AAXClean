using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.Test
{
    public static class TestFiles
    {
        private static List<FileStream> OpenTempFiles { get; } = new List<FileStream>();
        private static string HP_WebPath { get; } = "https://drive.google.com/uc?export=download&id=1UIc0ouxIspS2RjGstX1Rzvp3QdeidDrR&confirm=t";
        private static string BA_WebPath { get; } = "https://drive.google.com/uc?export=download&id=1Sl8m7-qEFtCVRVRcC09q66DledzWxTP8&confirm=t";
        private static string AC_WebPath { get; } = "https://drive.google.com/uc?export=download&id=11hz3AjNJOK3GGDl5V9buicvVNdJ5acFC&confirm=t";
        private static string SH_WebPath { get; } = "https://drive.google.com/uc?export=download&id=1qZLKhWh61R8C2MhKqXwipC5uPcffVt--&confirm=t";

        private static string _HP_BookPath { get; } = @"..\..\..\..\..\..\TestFiles\Harry Potter and the Sorcerer's Stone, Book 1 [B017V4IM1G] - Zero.aax";
        private static string _BA_BookPath { get; } = @"..\..\..\..\..\..\TestFiles\Broken Angels [B002V8H59I] - Zero.aax";
        private static string _AC_BookPath { get; } = @"..\..\..\..\..\..\TestFiles\Altered Carbon [B002V1O6X8] - Zero.aax";
        private static string _SH_BookPath { get; } = @"..\..\..\..\..\..\TestFiles\50 Self-Help Classics to Guide You to Financial Fr [2291082140] - Zero.aax";

        public static string HP_BookPath => FindOrDownload(_HP_BookPath, HP_WebPath);
        public static string BA_BookPath => FindOrDownload(_BA_BookPath, BA_WebPath);
        public static string AC_BookPath => FindOrDownload(_AC_BookPath, AC_WebPath);
        public static string SH_BookPath => FindOrDownload(_SH_BookPath, SH_WebPath);

        private static string FindOrDownload(string path, string url)
        {
            if (!File.Exists(path))
            {
                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                using WebClient cli = new();
                cli.DownloadFile(url, path);
            }
            return path;
        }

        public static void CloseAllFiles()
        {
            foreach (var fs in OpenTempFiles)
            {
                fs.Close();
                File.Delete(fs.Name);
            }
            OpenTempFiles.Clear();
        }

        public static FileStream NewTempFile()
        {
            var fs = File.Open(Path.GetTempFileName(), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            OpenTempFiles.Add(fs);
            return fs;
        }
    }
}
