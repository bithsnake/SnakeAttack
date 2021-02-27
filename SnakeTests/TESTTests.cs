using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snake;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Tests
{
    [TestClass()]
    public class TESTTests
    {
        [TestMethod()]
        public void ChangePositionTest()
        {
            Position a = new Position(2, 2);
            Position b = new Position(2, 2);

            a = TEST.ChangePosition(a);
            Position c = a + b;

            Assert.AreNotEqual(a.Y, b.Y, "Test Failed");
            Assert.AreNotEqual(a.X, b.X, "Test Failed");
            Assert.AreNotEqual(a, c, "Test Failed");

            b = TEST.ChangePosition(b);
            Assert.AreEqual(a, b, "Are equal");
            Position d = a - b;

            Assert.AreNotEqual(c.X, d.X, "Test Failed");
        }

        [TestMethod()]
        public void CreateGameWorldTest()
        {
            GameWorld world = TEST.CreateGameWorld();
            Player snake = new Player('S', Direction.None);
            {
                snake.Position = new Position(16, 8);
            }
            world.AddPlayer(snake.InstanceChar, snake.CurrentDirection);
            Assert.IsTrue(world.gameObjectList.Count > 0, "Test Failed");
            world.CreateFood(world);
            Assert.IsTrue(world.gameObjectList.Count > 0, "Test Failed");
            world.CreateFood(world);
            Assert.IsTrue(world.gameObjectList.Count > 0, "Test Failed");
            world.CreateFood(world);
            Assert.IsTrue(world.gameObjectList.Count > 0, "Test Failed");
        }

        [TestMethod()]
        public void MovePlayerTest()
        {
            var snake = new Player('S', Direction.None);
            snake.Position = new Position(8, 8);
            var positionBuffer = snake.Position;
            TEST.MovePlayer(snake,Direction.Up);

            Assert.AreNotEqual(snake.Position,positionBuffer,"Test Failed");
        }
    }
}