using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace ProjectManager
{
    public class Solution
    {
        private readonly FileInfo solution;

        public Solution(string fullName)
        {
            solution = new FileInfo(fullName);
            var b = new BuildConfigurationCollection();
            b.Load(this);
            BuildConfigurations = b;
        }

        public string FullName { get { return solution.FullName; } }
        public string Path { get { return solution.DirectoryName; } }
        public IEnumerable<BuildConfiguration> BuildConfigurations { get; private set; }

        public void Open()
        {
            Process.Start(FullName);
        }

        public void OpenContainingFolder()
        {
            Process.Start(Path);
        }
    }
}