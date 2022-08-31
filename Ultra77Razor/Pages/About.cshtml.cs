using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ultra77Razor.Pages
{
    public class AboutModel : PageModel
    {
        [BindProperty]
        public List<int> imgCounts { get; set; } = new();
		public void OnGet()
        {
            for(int i = 0; i < 12; i++)
			{
                imgCounts.Add(i+1);
			}
        }
    }
}
