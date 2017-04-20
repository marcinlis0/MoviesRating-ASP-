using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetTest.Controllers
{
    public class MoviesCommentController : Controller
    {
        // GET: MoviesComment
        public ActionResult Index()
        {
            return View();
        }
    }
}