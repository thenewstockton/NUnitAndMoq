using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitAndMoq
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void MyMethod()
        {
            Assert.That(1, Is.EqualTo(2));
        }
    }
}
