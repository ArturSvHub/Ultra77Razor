using Microsoft.AspNetCore.Http;

using System.Text.Json;

namespace UpakUtilitiesLibrary.Utility.Extentions
{
	public static class SessionExtentions
	{
		public static void Set<T>(this ISession session,string key,T value)
		{
			session.SetString(key, JsonSerializer.Serialize(value));
		}
		public static T? Get<T>(this ISession session, string key)
		{
			var value = session.GetString(key);
			return value != null ? JsonSerializer.Deserialize<T>(value) : default;
		}
	}
}
