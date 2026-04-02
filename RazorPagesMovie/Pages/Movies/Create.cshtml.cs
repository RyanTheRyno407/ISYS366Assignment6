using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using RazorPagesMovie.Pages.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        private readonly IWebHostEnvironment _env;

        public CreateModel(RazorPagesMovie.Data.RazorPagesMovieContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
            
        }



        public IActionResult OnGet()
        {
            return Page();
        }

       

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(HttpContext.Request.Form.Files.Count > 0)
            {
                Movie.Image = PictureHelper.UploadNewImage(_env,
            HttpContext.Request.Form.Files[0]);
            }


            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
