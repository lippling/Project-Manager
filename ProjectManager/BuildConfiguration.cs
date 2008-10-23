using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Win32;

namespace ProjectManager
{
    public class BuildConfiguration
    {
        public string Name { get; private set; }
        public Solution Solution { get; private set; }
        
        public BuildConfiguration(string name, Solution solution)
        {
            Name = name;
            Solution = solution;
        }

        public int Compile()
        {
            //var vsPath = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\9.0", "InstallDir", null);
            //var p = Process.Start(vsPath + "devenv.exe", Solution.FullName + " /build " + Name);
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Windows\Microsoft.NET\Framework\v3.5\msbuild.exe";
            psi.Arguments = Solution.FullName + " /property:Configuration=" + Name;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            var p = Process.Start(psi);
            p.WaitForExit();
            return p.ExitCode;
        }
    }
}
