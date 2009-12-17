using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectManager
{
    public class SolutionCollection : List<Solution>
    {
        public void Load(string rootPath)
        {
            Clear();

            var dir = new DirectoryInfo(rootPath);
            if (!dir.Exists)
                throw new ArgumentException("Path does not exist!");

            foreach (var file in GetAllFileNames(rootPath))
            {
                Add(new Solution(file));
            }
        }

        /// <summary>
        /// Find all files in a directory, and all files within every nested
        /// directory.
        /// </summary>
        /// <param name="baseDir">The starting directory you want to use.</param>
        /// <returns>A string array containing all the file names.</returns>
        private static IList<string> GetAllFileNames(string baseDir)
        {
            var fileResults = new List<string>();
            var directoryStack = new Stack<string>();
            directoryStack.Push(baseDir);

            while (directoryStack.Count > 0)
            {
                var currentDir = directoryStack.Pop();
                foreach (var fileName in Directory.GetFiles(currentDir, "*.sln"))
                    fileResults.Add(fileName);

                foreach (var directoryName in Directory.GetDirectories(currentDir))
                {
                    if (!directoryName.Contains("\\.svn"))
                        directoryStack.Push(directoryName);
                }
            }
            return fileResults;
        }
    }
}