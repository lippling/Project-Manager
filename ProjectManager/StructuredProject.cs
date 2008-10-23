using System.Collections.Generic;

namespace ProjectManager
{
    public class StructuredProject : Project
    {
        public List<BranchTagBase> BranchesSolutions { get; set; }
        public List<BranchTagBase> TagsSolutions { get; set; }

        public StructuredProject()
        {
            BranchesSolutions = new List<BranchTagBase>();
            TagsSolutions = new List<BranchTagBase>();
        }
    }
}