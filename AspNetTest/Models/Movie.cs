using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetTest.Models
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Wpisz tytuł filmu!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć od 3 do 30 znaków!")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Wypełnij skrócony opis!")]
        [StringLength(100, ErrorMessage = "Opis może mieć maksymalnie 100 znaków!")]
        [Display(Name = "Skrócony opis")]
        [DataType(DataType.MultilineText)]
        public string ShortDesc { get; set; }

        [Required(ErrorMessage = "Wypełnij opis!")]
        [StringLength(400, ErrorMessage = "Opis może mieć maksymalnie 400 znaków!")]
        [Display(Name = "Pełny opis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Typ filmu")]
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}