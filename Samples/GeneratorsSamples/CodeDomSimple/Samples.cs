using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CodeDomSimple
{
    [TestClass]
    public class Samples
    {
        [TestMethod]
        public void CodeDomSample()
        {
            Console.WriteLine(Generator.Generate(5));
        }
    }
}
