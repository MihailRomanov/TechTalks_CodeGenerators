using System;

namespace VsScaffolders.Model
{
    [Serializable]
    public class CodeTypeModel
    {
        public string Name { get; }
        public CodePropertyModel[] Properties { get; }

        public CodeTypeModel(string name, CodePropertyModel[] properties)
        {
            Name = name;
            Properties = properties;
        }
    }
}
