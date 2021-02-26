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

            Assert.AreNotEqual(a.Y, b.Y, "Are not equal");
            Assert.AreNotEqual(a.X, b.X, "Are not equal");
            Assert.AreNotEqual(a, c,"Are not equal");

            b = TEST.ChangePosition(b);
            Assert.AreEqual(a, b,"Are equal");
            Position d = a - b;

            Assert.AreNotEqual(c.X, d.X, "Are not equal"); 
        }
    }
}