using Newtonsoft.Json;
using System.Text.Json;

namespace WebUI.Helpers
{
    public static class SessionHelper
    {
        public static void SetAsJson<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if(value == null)
            {
                return default(T);
            }
            else
            {
                var value2 = JsonConvert.DeserializeObject<T>(value);
                return value2;
            }

            //return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        }
    }
}
