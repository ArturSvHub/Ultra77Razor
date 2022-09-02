using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data;

using UpakDataAccessLibrary.DataContext;

using UpakModelsLibrary.Models;
using UpakUtilitiesLibrary;

namespace Ultra77Razor.Areas.Admin.Pages.Order
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class IndexModel : PageModel
    {
		private readonly MssqlContext _context;

		public IndexModel(MssqlContext context)
		{
			_context = context;
		}
		public void OnGet()
        {
        }

	}
}
