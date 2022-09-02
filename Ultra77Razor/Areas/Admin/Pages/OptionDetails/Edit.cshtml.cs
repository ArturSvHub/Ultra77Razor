using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data;
using UpakUtilitiesLibrary;

namespace Ultra77Razor.Areas.Admin.Pages.OptionDetails
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class EditModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
