using System;
using System.IO;
using System.Security.AccessControl;
using System.Text;

namespace Konfiggy.Helpers
{
    public static class FileHelpers
    {
        public static FileInfo GetFile(string filepath)
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
            var file = GetFile(filepath);

            var text = String.Empty;
            using (var stream = file.OpenText())
            {
                text = stream.ReadToEnd();
            }
            return text;
        }

        public static void CreateFile(string filepath)
        {
            EnsurePathExists(filepath);
            File.Create(filepath);
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
