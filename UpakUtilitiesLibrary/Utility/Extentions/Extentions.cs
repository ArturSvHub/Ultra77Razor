using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using System.Text.Json;

using UpakModelsLibrary.Models;

namespace UpakUtilitiesLibrary.Utility.Extentions
{
	public static class Extentions
	{
		
		public static string DataArrayToImageUrl(this byte[] ImageData)
		{
			string imageBase64Data = Convert.ToBase64String(ImageData);
			string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
			return imageDataURL;
		}
		public static async Task<byte[]> ImageToImageDataAsync(this IFormFile file)
		{
			MemoryStream ms = new MemoryStream();
			await file.CopyToAsync(ms);
			byte[] imageData = ms.ToArray();
			ms.Close();
			await ms.DisposeAsync();
			return imageData;
		}
        public static string GetFirstFileName(this Category category, string path)
        {
            string name = "";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			if(Directory.GetFiles(path,"*.*",SearchOption.AllDirectories).Length>0)
			{ 
                name = new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories).FirstOrDefault().Name;
            }

            return name;
        }
        public static string GetFirstFileName(this Product product, string path)
        {
            string name = "";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Length > 0)
            {
                name = new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories).FirstOrDefault().Name;
            }

            return name;
        }
    }
}
