using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsScaffolders.Model
{
    public class CodePropertyModel
    {
        public string Name { get; }
        public Type Type { get; }

        public CodePropertyModel(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}
