using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TextTemplating;

namespace T4Simple
{
    [TestClass]
    public class Samples
    {
        [TestMethod]
        public void RuntimeTextTemplate()
        {
            var rt = new RuntimeTextTemplate
            {
                Session = new Dictionary<string, object>()
            };
            rt.Session["Count"] = 7;
            rt.Initialize();
            Console.WriteLine(rt.TransformText());
        }

        [TestMethod]
        public void WithEngine()
        {
            var engine = new Engine();
            var host = new T4Host
            {
                TemplateFileValue = "RuntimeTextTemplate.tt"
            };
            host.Session["Count"] = 7;

            var templateString = File.ReadAllText("RuntimeTextTemplate.tt");

            string output = engine.ProcessTemplate(templateString, host);
            Console.WriteLine(output);
        }
    }
}
