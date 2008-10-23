using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class ProjectDefinition
    {
        public string Name { get; private set; }
        public Solution Solution { get; set; }

        public ProjectDefinition(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
