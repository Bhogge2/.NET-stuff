using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal
{
    public class CreateModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public CreateModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            IQueryable<Books> typeList = from b in _context.Books select b;
            BooksOfScripture = new SelectList(typeList, "Name", "Name");

            return Page();
        }

        [BindProperty]
        public Scriptures Scriptures { get; set; }

        public SelectList BooksOfScripture { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Scriptures.Add(Scriptures);
            Scriptures.dateAdded = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
