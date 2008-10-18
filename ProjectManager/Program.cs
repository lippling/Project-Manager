using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProjectManager
{
    class Program
    {
        static void Main(string[] args)
        {
            SolutionFileCollection container;
            using (var sr = new StreamReader("SolutionContainer.ser"))
            {
                var x = new XmlSerializer(typeof(SolutionFileCollection));
                container = (SolutionFileCollection)x.Deserialize(sr);
            }

            //var container = new SolutionFileCollection();
            //container.Load(@"D:\SVN_Working_Copies\GAdvance");

            //using (var sw = new StreamWriter("SolutionContainer.ser"))
            //{
            //    var x = new XmlSerializer(typeof(SolutionFileCollection));
            //    x.Serialize(sw, container);    
            //}

            var projects = new ProjectCollection();
            projects.Load(container);

            //using (var sw = new StreamWriter("ProjectCollection.ser"))
            //{
            //    var x = new XmlSerializer(typeof(ProjectCollection));
            //    x.Serialize(sw, projects);
            //}

            

            Console.WriteLine("\nPress any key!");
            Console.ReadKey();
        }
    }
}
