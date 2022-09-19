using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Text.Json;

using UpakModelsLibrary.Models.SEO;

namespace Ultra77Razor.Pages
{
    public class ServicesModel : PageModel
    {
        public void OnGet()
        {
            string? file = System.IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "MainPages.json"));
            List<MainPage> meta = JsonSerializer.Deserialize<List<MainPage>>(file);
            if (meta != null)
            {
                ViewData["Title"] = meta[1].Title;
                ViewData["Description"] = meta[1].Description;
                ViewData["Keywords"] = meta[1].KeyWords;
            }
        }
    }
}
