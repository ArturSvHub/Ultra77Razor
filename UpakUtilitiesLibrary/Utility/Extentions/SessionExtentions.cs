using Blazored.SessionStorage;

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
		public static async Task SetToStorage<T>(this ISessionStorageService session, string key, T value)
		{
			await session.SetItemAsync(key, JsonSerializer.Serialize(value));
		}
		public static async Task<T?> GetFromStorage<T>(this ISessionStorageService session, string key)
		{
			string value =await session.GetItemAsync<string>(key);
			return value != null ? JsonSerializer.Deserialize<T>(value) : default;
		}
	}
}
