using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mono.TextTemplating;
using System;
using System.Collections.Generic;
using System.IO;

namespace T4SimpleCore
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
            var engine = new TemplatingEngine();
            var host = new TemplateGenerator();
            host.Refs.Add("System.CodeDom");

            var session = host.GetOrCreateSession();
            session["Count"] = 2;

            var templateString = File.ReadAllText("RuntimeTextTemplateForEngine.tt");

            string output = engine.ProcessTemplate(templateString, host);
            Console.WriteLine(output);
        }
    }
}
