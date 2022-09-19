using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data;

using UpakDataAccessLibrary.DataContext;

using UpakUtilitiesLibrary;

namespace Ultra77Razor.Areas.Admin.Pages.SEO
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class IndexModel : PageModel
    {
        private readonly MssqlContext context;

        public IndexModel(MssqlContext context)
        {
            this.context = context;
        }
        public IActionResult OnGet()
        {
            return RedirectToPage("/SEO/MainPages");
        }
    }
}
