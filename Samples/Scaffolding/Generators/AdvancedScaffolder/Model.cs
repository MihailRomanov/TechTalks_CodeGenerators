namespace AdvancedScaffolder
{
    public class Model
    {
        public string Namespace { get; set; }
        public MyType[] Types { get; set; }
    }

    public class MyType
    {
        public string Name { get; set; }
        public MyProperty[] Properties { get; set; }
    }

    public class MyProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
