namespace ProjectManager
{
    public abstract class BranchTagBase
    {
        public abstract string Name { get; }
        public Solution Solution { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}