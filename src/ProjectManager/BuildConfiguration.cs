using System.Diagnostics;
using System.Globalization;
using System.IO;

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
            var psi = new ProcessStartInfo
            {
                FileName = @"C:\Windows\Microsoft.NET\Framework\v3.5\msbuild.exe",
                Arguments = string.Format(CultureInfo.InvariantCulture, "\"{0}\" /property:Configuration={1}", Solution.FullName, Name), 
                WindowStyle = ProcessWindowStyle.Hidden
            };
            var p = Process.Start(psi);
            p.WaitForExit();
            File.Delete(Solution.FullName + ".cache");
            return p.ExitCode;
        }
    }
}