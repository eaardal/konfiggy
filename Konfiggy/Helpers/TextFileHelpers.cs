namespace KonfiggyFramework.Helpers
{
    public static class TextFileHelpers
    {
        public static string GetEnvironmentTag(string filepath)
        {
            return FileHelpers.GetFileContent(filepath);
        }

        public static void ModifyEnvironmentTag(string filepath, string newEnvironmentTag)
        {
            FileHelpers.CreateFileWithContent(filepath, newEnvironmentTag);
        }
    }
}
