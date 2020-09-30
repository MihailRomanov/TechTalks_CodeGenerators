using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace T4WithModel
{
    public class Samples
    {
        [Test]
        public void Test1()
        {
            var rt = new Template
            {
                Session = new Dictionary<string, object>()
            };
            rt.Session["model"] = Model.ForGeneration.Model;
            rt.Initialize();
            Console.WriteLine(rt.TransformText());
        }
    }
}