using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Text.Json;

using UpakModelsLibrary.Models.SEO;

namespace Ultra77Razor.Pages
{
    public class ProvidersModel : PageModel
    {
        public void OnGet()
        {
            string? file = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "MainPages.json"));
            List<MainPage> meta = JsonSerializer.Deserialize<List<MainPage>>(file);
            if (meta != null)
            {
                ViewData["Title"] = meta[2].Title;
                ViewData["Description"] = meta[2].Description;
                ViewData["Keywords"] = meta[2].KeyWords;
            }
        }
    }
}
