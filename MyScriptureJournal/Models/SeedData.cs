using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyScriptureJournal.Data;
using System.Text;
using System.IO;

namespace MyScriptureJournal.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyScriptureJournalContext(
                serviceProvider.GetRequiredService<DbContextOptions<MyScriptureJournalContext>>()))
            {
                if (context.Books.Any())
                {
                    return;   
                }

                string[] file = File.ReadLines(@"ListOfScriptures.txt").ToArray();

                int i;
                for(i = 0; i < file.Length; i++)
                {
                    context.Books.AddRange(
                        new Books
                        {
                            Name = file[i]
                        });
                }
                
                context.SaveChanges();


            }

        }

    }
}
