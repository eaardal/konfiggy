using System;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Threading;

namespace Konfiggy.Helpers
{
    public static class FileHelpers
    {
        public static FileInfo CreateFileIfNotExists(string filepath)
        {
            var file = new FileInfo(filepath);
            if (!file.Exists)
            {
                CreateFile(filepath);
            }
            return file;
        }

        public static string GetFileContent(string filepath)
        {
            while (IsFileLocked(filepath))
            {
                Thread.Sleep(20);
            }

            return File.ReadAllText(filepath);
        }

        private static bool IsFileLocked(string filepath)
        {
            try
            {
                using (File.Open(filepath, FileMode.Open))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        public static void CreateFile(string filepath)
        {
            EnsurePathExists(filepath);
            var fileStream = File.Create(filepath);
            fileStream.Dispose(); //Dispose unlocks the file on disk.
        }

        public static void EnsurePathExists(string filepath)
        {
            var pathWithoutFile = filepath.Substring(0, filepath.LastIndexOf("\\", StringComparison.InvariantCulture));
            if (!Directory.Exists(pathWithoutFile))
            {
                Directory.CreateDirectory(pathWithoutFile);
            }
        }
        
        public static void CreateFileWithContent(string filepath, string content)
        {
            EnsurePathExists(filepath);
            File.Delete(filepath);
            File.AppendAllText(filepath, content, Encoding.UTF8);
        }
    }
}
