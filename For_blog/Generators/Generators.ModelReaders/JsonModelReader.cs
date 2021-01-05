using Generators.ModelReaders.ReadingModel;
using Newtonsoft.Json;

namespace Generators.ModelReaders
{
    public class JsonModelReader
    {
        public static Common.EntitiesModel.Entity[] Read(string jsonContent)
        {
            var readedModel = JsonConvert.DeserializeObject<Entity[]>(jsonContent);
            return readedModel.ToEntitiesModel();
        }
    }
}
