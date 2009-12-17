using System.Text.RegularExpressions;

namespace ProjectManager
{
    public class Tag : BranchTagBase
    {
        public override string Name
        {
            get
            {
                return Regex.Match(Solution.FullName, @"\\tags\\([^\\]+)\\").Groups[1].Value;
            }
        }
    }
}