using System.Text.RegularExpressions;

namespace ProjectManager
{
    public class Branch : BranchTagBase
    {
        public override string Name
        {
            get
            {
                return Regex.Match(Solution.FullName, @"\\branches\\([^\\]+)\\").Groups[1].Value;
            }
        }
    }
}