#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ultra77Razor.DataContext;
using Ultra77Razor.Models;

namespace Ultra77Razor.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Ultra77Razor.DataContext.MssqlContext _context;

        public IndexModel(Ultra77Razor.DataContext.MssqlContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
