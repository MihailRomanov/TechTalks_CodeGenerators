namespace VsScaffolders.Model
{
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
