using Newtonsoft.Json;

namespace EduPlatform.WPF.Service.Utilities
{
    public static class SerializationCopier
    {
        public static T? DeepCopy<T>(T obj)
        {
            JsonSerializerSettings settings = new()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(obj, settings);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
