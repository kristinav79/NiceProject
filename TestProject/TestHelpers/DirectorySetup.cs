using System;
using System.IO;


namespace TestProject.TestHelpers
{
    public static class DirectorySetup
    {
        public static string GetPath(string realativePath)
        {
            var workingDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            return workingDirectory + realativePath;
            
        }

        public static void Empty(this DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
    }
}
