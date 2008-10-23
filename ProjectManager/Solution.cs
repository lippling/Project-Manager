using System.IO;
using System.Diagnostics;
using System;

namespace ProjectManager
{
    public class Solution
    {
        public string FullName { get; set; }

        public void Open()
        {
            Process.Start(FullName);
        }
    }
}