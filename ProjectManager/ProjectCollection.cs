using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProjectManager
{
    public class ProjectCollection : List<Project>
    {
        public void Load(SolutionCollection solutionCollection)
        {
            foreach (var file in solutionCollection)
            {
                var nameMatch = Regex.Match(file.FullName, @"(trunk|tags|branches)\\[^\\]+\\src\\([^\\]+)\.sln$");
                var name = nameMatch.Groups[2].Value;
                if (nameMatch.Success)
                {
                    var project = Find(p => p.Name == name) as StructuredProject;
                    if (project == null)
                    {
                        project = new StructuredProject { Name = name };
                        Add(project);
                    }
                    switch (nameMatch.Groups[1].Value)
                    {
                        case "trunk": project.Solution = file; break;
                        case "branches": project.BranchesSolutions.Add(new Branch { Solution = file }); break;
                        case "tags": project.TagsSolutions.Add(new Tag { Solution = file }); break;
                    }
                }
                else
                {
                    nameMatch = Regex.Match(file.FullName, @"([^\\]+)\.sln$");
                    name = nameMatch.Groups[1].Value;
                    var project = Find(p => p.Name == name) as Project;
                    if (project == null)
                        Add(new Project { Name = name, Solution = file });
                }
            }
        }
    }
}