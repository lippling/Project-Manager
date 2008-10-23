using System.Collections.Generic;

namespace ProjectManager
{
    public class StructuredProjectDefinition : ProjectDefinition
    {
        public List<BranchTagBase> BranchesSolutions { get; private set; }
        public List<BranchTagBase> TagsSolutions { get; private set; }

        public StructuredProjectDefinition(string name)
            : base(name)
        {
            BranchesSolutions = new List<BranchTagBase>();
            TagsSolutions = new List<BranchTagBase>();
        }
    }
}