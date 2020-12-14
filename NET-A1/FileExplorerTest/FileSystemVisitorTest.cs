using FileExplorer;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace FileExplorerTest
{
    public class FileSystemVisitorTest
    {
        public FileSystemVisitor FileSystemVisitor { get; set; }
        [SetUp]
        public void Setup()
        {
            FileSystemVisitor = new FileSystemVisitor();
        }

        [Test]
        public void ExploreDirectoryFilter_Success()
        {
            var root = FileSystemVisitor.ExploreDirectory($@"{Directory.GetCurrentDirectory()}\Assets", "player");

            Assert.IsTrue(root.FileItems.Any(x=>x.Name == "player.txt"));
        }
    }
}