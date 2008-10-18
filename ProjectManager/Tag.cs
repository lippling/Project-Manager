using System.Text.RegularExpressions;

namespace ProjectManager
{
    public class Tag : BranchTagBase
    {
        public override string Name
        {
            get
            {
                return Regex.Match(SolutionFile.FullName, @"\\tags\\([^\\]+)\\").Groups[1].Value;
            }
        }
    }
}