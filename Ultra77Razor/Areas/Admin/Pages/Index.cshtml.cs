using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using UpakUtilitiesLibrary;

namespace Ultra77Razor.Areas.Admin.Pages
{
    [Authorize(Roles =WebConstants.AdminRole)]
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("/Category/Index");
        }
    }
}
