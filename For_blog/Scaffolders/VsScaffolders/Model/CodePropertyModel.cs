using System;

namespace VsScaffolders.Model
{
    [Serializable]
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
