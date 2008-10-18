using System.Xml.Serialization;

namespace ProjectManager
{
    [XmlInclude(typeof(Branch))]
    [XmlInclude(typeof(Tag))]
    public abstract class BranchTagBase
    {
        public abstract string Name { get; }
        public SolutionFile SolutionFile { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}