using System.Collections.Generic;

namespace ProjectManager
{
    public class StructuredProjectDefinition : ProjectDefinition
    {
        public ICollection<BranchTagBase> BranchesSolutions { get; private set; }
        public ICollection<BranchTagBase> TagsSolutions { get; private set; }

        public StructuredProjectDefinition(string name)
            : base(name)
        {
            BranchesSolutions = new List<BranchTagBase>();
            TagsSolutions = new List<BranchTagBase>();
        }
    }
}