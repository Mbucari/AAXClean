using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AAXClean.Test
{
	public static class TestFiles
	{
		private static readonly List<FileStream> OpenTempFiles = new List<FileStream>();
		private const string TEST_FILE_DIR = @"..\..\..\..\..\TestFiles";
		private const string HP_URL = "https://drive.google.com/uc?export=download&id=1UIc0ouxIspS2RjGstX1Rzvp3QdeidDrR&confirm=t";
		private const string BA_URL = "https://drive.google.com/uc?export=download&id=1Sl8m7-qEFtCVRVRcC09q66DledzWxTP8&confirm=t";
		private const string AC_URL = "https://drive.google.com/uc?export=download&id=11hz3AjNJOK3GGDl5V9buicvVNdJ5acFC&confirm=t";
		private const string SH_URL = "https://drive.google.com/uc?export=download&id=1qZLKhWh61R8C2MhKqXwipC5uPcffVt--&confirm=t";

		private static readonly string HP_FILENAME = Path.Combine(TEST_FILE_DIR, "HP_Zero.aax");
		private static readonly string BA_FILENAME = Path.Combine(TEST_FILE_DIR, "BA_Zero.aax");
		private static readonly string AC_FILENAME = Path.Combine(TEST_FILE_DIR, "AC_Zero.aax");
		private static readonly string SH_FILENAME = Path.Combine(TEST_FILE_DIR, "SH_Zero.aax");

		public static string HP_BookPath => FindOrDownload(HP_FILENAME, HP_URL);
		public static string BA_BookPath => FindOrDownload(BA_FILENAME, BA_URL);
		public static string AC_BookPath => FindOrDownload(AC_FILENAME, AC_URL);
		public static string SH_BookPath => FindOrDownload(SH_FILENAME, SH_URL);

		private static string FindOrDownload(string path, string url)
		{
			if (!File.Exists(path))
			{
				if (!Directory.Exists(TEST_FILE_DIR))
					Directory.CreateDirectory(TEST_FILE_DIR);
				using WebClient cli = new();
				cli.DownloadFile(url, path);
			}
			return path;
		}

		public static void CloseAllFiles()
		{
			foreach (FileStream fs in OpenTempFiles)
			{
				fs.Close();
				File.Delete(fs.Name);
			}
			OpenTempFiles.Clear();
		}

		public static FileStream NewTempFile()
		{
			FileStream fs = File.Open(Path.GetTempFileName(), FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
			OpenTempFiles.Add(fs);
			return fs;
		}
	}
}
