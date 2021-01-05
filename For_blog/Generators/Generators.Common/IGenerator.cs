namespace Generators.Common
{
    public interface IGenerator<TModel> where TModel: class
    {
        string Generate(TModel model);
    }
}
