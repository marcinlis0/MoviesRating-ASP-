using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetTest.Models
{
    public class Type
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "Typ musi mieć od 3 do 30 znaków!")]
        [Required(ErrorMessage = "Podaj typ filmu!")]
        [Display(Name = "Typ filmu")]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}