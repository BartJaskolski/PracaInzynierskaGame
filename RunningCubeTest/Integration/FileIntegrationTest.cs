using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace RunningCubeTest
{
    [TestClass]
    public class FileIntegrationTest
    {
        [TestMethod]
        public void IfContentFilesExists()
        {
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\GameRunningCube\Content"));
            
            Assert.IsTrue(File.Exists(Path.Combine(path +@"\2D", "Enemy.xnb")));
            Assert.IsTrue(File.Exists(Path.Combine(path + @"\2D", "Player.xnb")));
            Assert.IsTrue(File.Exists(Path.Combine(path, "FontArial.xnb")));
        }
    }
}
