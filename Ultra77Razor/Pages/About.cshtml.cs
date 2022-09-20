using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Text.Json;

using UpakModelsLibrary.Models.SEO;

namespace Ultra77Razor.Pages
{
    public class AboutModel : PageModel
    {
        [BindProperty]
        public List<int> imgCounts { get; set; } = new();
		public void OnGet()
        {
            string? file = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "MainPages.json"));
            List<MainPage> meta = JsonSerializer.Deserialize<List<MainPage>>(file);
            if (meta != null)
            {
                ViewData["Title"] = meta[3].Title;
                ViewData["Description"] = meta[3].Description;
                ViewData["Keywords"] = meta[3].KeyWords;
            }
            for (int i = 0; i < 12; i++)
			{
                imgCounts.Add(i+1);
			}
        }
    }
}
