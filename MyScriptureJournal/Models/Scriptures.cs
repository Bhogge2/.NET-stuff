using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyScriptureJournal.Models
{
    public class Scriptures 
    {
        public int ID { get; set; }

        [Display(Name = "Book")]
        [Required]
        public String bookName { get; set; }

        [Display(Name = "Chapter")]
        [Range(1, 100)]
        [Column(TypeName = "integer")]
        [Required]
        public int chapter { get; set; }

        [Display(Name = "Verse")]
        [Range(1, 100)]
        //[Column(TypeName = "integer")]
        [Required]
        public int verse { get; set; }

        [Display(Name = "Notes")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(255)]
        public String notes { get; set; }

        [Display(Name = "Date Added")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime dateAdded { get; set; }
    }
}
