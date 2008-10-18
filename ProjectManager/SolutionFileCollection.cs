using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ProjectManager
{
    public class SolutionFileCollection : List<SolutionFile>
    {
        public void Load(string rootPath)
        {
            Clear();

            var dir = new DirectoryInfo(rootPath);
            if (!dir.Exists)
                throw new ArgumentException("Path does not exist!");

            foreach (var file in dir.GetFiles("*.sln", SearchOption.AllDirectories))
            {
                Add(new SolutionFile { FullName = file.FullName });
            }
        }
    }
}