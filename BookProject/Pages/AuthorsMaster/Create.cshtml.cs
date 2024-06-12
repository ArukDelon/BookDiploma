using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookProject.Models;
using BookProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace BookProject.Pages.AuthorsMaster
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly BookProject.Services.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<AppUser> _userManager;
        private readonly FirebaseStorageService _storageService;


        public CreateModel(BookProject.Services.ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> userManager, FirebaseStorageService storageService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _storageService = storageService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Authors Authors { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Authors.AvatarPath = "/uploads/";
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (Authors.Avatar != null && Authors.Avatar.Length > 0)
            {
                var photoUri = await _storageService.UploadFile(Authors.FullName, Authors.Avatar);

                Authors.AvatarPath = photoUri.ToString();
            }

            _context.authors.Add(Authors);
            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Logger.LogAction("CREATE", " Author:'" + Authors.FullName + "' has been added to the database", adminId: user.Id, authorId: Authors.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
