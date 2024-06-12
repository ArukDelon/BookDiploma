using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookProject.Models;
using BookProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Google.Apis.Storage.v1;

namespace BookProject.Pages.AuthorsMaster
{
    [Authorize(Roles = "admin")]
    public class EditModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly FirebaseStorageService _storageService;

        public EditModel(BookProject.Services.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager, FirebaseStorageService storageService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _storageService = storageService;
        }

        [BindProperty]
        public Authors Authors { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors =  await _context.authors.FirstOrDefaultAsync(m => m.Id == id);
            if (authors == null)
            {
                return NotFound();
            }
            Authors = authors;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Authors.Avatar != null && Authors.Avatar.Length > 0)
            {
                var photoUri = await _storageService.UploadFile(Authors.FullName, Authors.Avatar);

                Authors.AvatarPath = photoUri.ToString();
            }

            _context.Attach(Authors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorsExists(Authors.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Logger.LogAction("EDIT", "Edit data for author:'" + Authors.FullName + "'", adminId: user.Id, authorId: Authors.Id);
            }

            return RedirectToPage("./Index");
        }

        private bool AuthorsExists(int id)
        {
            return _context.authors.Any(e => e.Id == id);
        }
    }
}
