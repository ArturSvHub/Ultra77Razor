using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data;

using UpakDataAccessLibrary.DataContext;

using UpakUtilitiesLibrary;

namespace Ultra77Razor.Areas.Admin.Pages.SEO
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class ProductPagesModel : PageModel
    {
        private readonly MssqlContext context;

        public ProductPagesModel(MssqlContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
        }
    }
}
