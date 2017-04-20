using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetTest.Models
{
    public class MoviesCommentModels
    {
        public Comment Comment { get; set; }
        public Movie Movie { get; set; }
    }
}