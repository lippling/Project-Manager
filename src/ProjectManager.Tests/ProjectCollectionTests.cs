using System.Linq;
using NUnit.Framework;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class ProjectCollectionTests
    {
        private const string projectName = "ProjectXYZ";

        private ProjectCollection projectCollection;

        [SetUp]
        public void Setup()
        {
            projectCollection = new ProjectCollection();
        }

        [Test]
        public void Empty()
        {
            projectCollection.Load(new Solution[] { });
            Assert.IsEmpty(projectCollection);
        }

        [Test]
        public void NonStructured(
            [Values(
                @"c:\src",
                @"c:\dir1\src",
                @"c:\dir1\dir2\src"
                )] string path)
        {
            var solution = new Solution(string.Format(@"{0}\{1}.sln", path, projectName));
            projectCollection.Load(new[] { solution });
            Assert.AreEqual(1, projectCollection.Count);
            var p = projectCollection[0];
            Assert.IsNotInstanceOf<StructuredProjectDefinition>(p);
            AssertNonStructured(solution, p);
        }

        private static void AssertNonStructured(Solution solution, ProjectDefinition project)
        {
            Assert.AreEqual(projectName, project.Name);
            Assert.AreSame(solution, project.Solution);
            Assert.AreEqual(projectName, project.ToString());
        }

        [Test]
        public void Structured(
            [Values(
                @"c:\trunk\current\src",
                @"c:\dir1\trunk\current\src",
                @"c:\dir1\dir2\trunk\current\src"
                )] string path)
        {
            const string name = "ProjectXYZ";
            var solution = new Solution(string.Format(@"{0}\{1}.sln", path, name));
            projectCollection.Load(new[] { solution });
            Assert.AreEqual(1, projectCollection.Count);
            Assert.IsInstanceOf<StructuredProjectDefinition>(projectCollection[0]);
            var p = (StructuredProjectDefinition)projectCollection[0];
            AssertNonStructured(solution, p);
            Assert.IsEmpty(p.BranchesSolutions.ToList());
            Assert.IsEmpty(p.TagsSolutions.ToList());
        }
    }
}