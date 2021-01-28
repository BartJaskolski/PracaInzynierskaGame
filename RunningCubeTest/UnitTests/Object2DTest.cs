using GameRunningCube;
using GameRunningCube.Source.GameEngine;
using GameRunningCube.Source.GamePlay;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace RunningCubeTest
{
    [TestClass]
    public class Object2DTest
    {
        [TestMethod]
        public void IsObjects2DCollidingTest()
        {
            Object2DEngine obj = new Object2DEngine();

            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\GameRunningCube\Content\2D\"));

            var player = new Player(new Vector2(300, 600), new Vector2(19, 19), path+"Player.xnb");
            var enemy = new Enemy(new Vector2(300, 600), new Vector2(19, 19), path + "Enemy.xnb");

            Assert.IsTrue(obj.IfTwoObjectsColiding(enemy, player));
        }
    }
}
