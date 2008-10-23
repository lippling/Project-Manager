using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager
{
    public class Project
    {
        public string Name { get; set; }
        public Solution SolutionFile { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
