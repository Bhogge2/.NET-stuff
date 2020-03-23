using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyScriptureJournal
{
    public class IndexModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IList<Scriptures> Scriptures { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public SelectList Books { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ScriptureBook { get; set; }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<string> bookQuery = from m in _context.Scriptures
                                            orderby m.bookName
                                            select m.bookName;

            var scriptures = from m in _context.Scriptures
                         select m;

            switch (sortOrder)
            {
                case "name_desc":
                    scriptures = scriptures.OrderByDescending(s => s.bookName);
                    break;
                case "Date":
                    scriptures = scriptures.OrderBy(s => s.dateAdded);
                    break;
                case "date_desc":
                    scriptures = scriptures.OrderByDescending(s => s.dateAdded);
                    break;
                case "name":
                    scriptures = scriptures.OrderBy(s => s.bookName);
                    break;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                scriptures = scriptures.Where(s => s.notes.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(ScriptureBook))
            {
                scriptures = scriptures.Where(x => x.bookName == ScriptureBook);
            }
            Books = new SelectList(await bookQuery.Distinct().ToListAsync());

            Scriptures = await scriptures.ToListAsync();
        }
    }
}
