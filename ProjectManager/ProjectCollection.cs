using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProjectManager
{
    public class ProjectCollection : List<ProjectDefinition>
    {
        public void Load(SolutionCollection solutionCollection)
        {
            foreach (var file in solutionCollection)
            {
                var nameMatch = Regex.Match(file.FullName, @"(trunk|tags|branches)\\[^\\]+\\src\\([^\\]+)\.sln$");
                var name = nameMatch.Groups[2].Value;
                if (nameMatch.Success)
                {
                    var project = Find(p => p.Name == name) as StructuredProjectDefinition;
                    if (project == null)
                    {
                        project = new StructuredProjectDefinition(name);
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
                    var project = Find(p => p.Name == name);
                    if (project == null)
                        Add(new ProjectDefinition(name) { Solution = file });
                }
            }
        }
    }
}