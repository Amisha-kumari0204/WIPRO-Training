using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestDemo2;

namespace TestProject1

{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()

        { 
            Assert.AreEqual(2, 1 + 1);
            Assert.AreEqual(0, 1 - 1);
        }
        [TestMethod]
        public void wrongOutput()
        {
            //Console.WriteLine("This is wrong");
            Assert.AreEqual(3, 5 + 2);
        }
    }
}
