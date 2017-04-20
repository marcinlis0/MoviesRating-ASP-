using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetTest.Models
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Dodaj ocenę!")]
        [Range(0, 5)]
        [Display(Name = "Ocena")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Wypełnij pole komentarz!")]
        [StringLength(100, ErrorMessage = "Komentarz może mieć maksymalnie 100 znaków!")]
        [Display(Name = "Komentarz")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Dodane")]
        public DateTime Created { get; set; }

        [Display(Name = "Film")]
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        [Display(Name = "Użytkownik")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}