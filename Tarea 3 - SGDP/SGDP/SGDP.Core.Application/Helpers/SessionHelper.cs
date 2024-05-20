using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;


namespace SGDP.Core.Application.Helpers
{
    public static class SessionHelper
    {
        public static void Set<T>(this ISession sesion, string key, T value)
        {
            sesion.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
