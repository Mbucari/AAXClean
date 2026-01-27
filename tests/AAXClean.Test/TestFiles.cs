using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AAXClean.Test
{
	public static class TestFiles
	{
		private static readonly List<FileStream> OpenTempFiles = new List<FileStream>();
		private const string TEST_FILE_DIR = @"..\..\..\..\..\..\TestFiles";

		private static readonly string HP_FILENAME = Path.Combine(TEST_FILE_DIR, "HP_Zero.aax");
		private static readonly string BA_FILENAME = Path.Combine(TEST_FILE_DIR, "BA_Zero.aax");
		private static readonly string AC_FILENAME = Path.Combine(TEST_FILE_DIR, "AC_Zero.aax");
		private static readonly string SH_FILENAME = Path.Combine(TEST_FILE_DIR, "SH_Zero.aax");
		private static readonly string SampleMoovInFront = Path.Combine(TEST_FILE_DIR, "sampleMoovInFront.m4a");
		private static readonly string SampleMoovInBack = Path.Combine(TEST_FILE_DIR, "sampleMoovInBack.m4a");

		public static string HP_BookPath => FindOrDownload(HP_FILENAME);
		public static string BA_BookPath => FindOrDownload(BA_FILENAME);
		public static string AC_BookPath => FindOrDownload(AC_FILENAME);
		public static string SH_BookPath => FindOrDownload(SH_FILENAME);
		public static string SampleMoovInFront_BookPath => FindOrDownload(SampleMoovInFront);
		public static string SampleMoovInBack_BookPath => FindOrDownload(SampleMoovInBack);

		public static string GetTestFilePath(string filename) => FindOrDownload(Path.Combine(TEST_FILE_DIR, filename));
		private static string FindOrDownload(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException($"Test file not found: {path}");
			return path;
		}

		public static void CloseAllFiles()
		{
			foreach (FileStream fs in OpenTempFiles.ToList())
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
