using System.Collections.Generic;

namespace ProjectManager
{
    public class BuildConfigurationCollection : List<BuildConfiguration>
    {
        public void Load(Solution solution)
        {
            Add(new BuildConfiguration("Debug", solution));
            Add(new BuildConfiguration("Release", solution));
        }
    }
}
