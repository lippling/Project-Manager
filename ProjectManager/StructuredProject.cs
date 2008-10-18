using System.Collections.Generic;

namespace ProjectManager
{
    public class StructuredProject : Project
    {
        public List<BranchTagBase> BranchesSolutionFiles { get; set; }
        public List<BranchTagBase> TagsSolutionFiles { get; set; }

        public StructuredProject()
        {
            BranchesSolutionFiles = new List<BranchTagBase>();
            TagsSolutionFiles = new List<BranchTagBase>();
        }
    }
}