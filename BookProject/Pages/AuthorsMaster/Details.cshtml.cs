using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookProject.Models;
using BookProject.Services;
using Microsoft.AspNetCore.Authorization;

namespace BookProject.Pages.AuthorsMaster
{
    [Authorize(Roles = "admin")]
    public class DetailsModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;

        public DetailsModel(BookProject.Services.ApplicationDbContext context)
        {
            _context = context;
        }

        public Authors Authors { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.authors.FirstOrDefaultAsync(m => m.Id == id);
            if (authors == null)
            {
                return NotFound();
            }
            else
            {
                Authors = authors;
            }
            return Page();
        }
    }
}
