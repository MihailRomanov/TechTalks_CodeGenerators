namespace CmdScaffolders.TemplateBasedGenerator
{
    public class TemplateModel
    {
        public string Namespace { get; set; }
        public TemplateModelType[] Types { get; set; }
    }

    public class TemplateModelType
    {
        public string Name { get; set; }
        public TemplateModelProperty[] Properties { get; set; }
    }

    public class TemplateModelProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

}
