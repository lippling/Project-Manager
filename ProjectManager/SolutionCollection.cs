using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                Add(new Solution { FullName = file });
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
            //
            // Store results in the file results list.
            //
            List<string> fileResults = new List<string>();

            //
            // Store a stack of our directories.
            //
            Stack<string> directoryStack = new Stack<string>();
            directoryStack.Push(baseDir);

            //
            // While there are directories to process
            //
            while (directoryStack.Count > 0)
            {
                string currentDir = directoryStack.Pop();

                //
                // Add all files at this directory.
                //
                foreach (string fileName in Directory.GetFiles(currentDir, "*.sln"))
                {
                    fileResults.Add(fileName);
                }

                //
                // Add all directories at this directory.
                //
                foreach (string directoryName in Directory.GetDirectories(currentDir))
                {
                    if (!directoryName.Contains("\\.svn"))
                        directoryStack.Push(directoryName);
                }
            }
            return fileResults;
        }
    }
}