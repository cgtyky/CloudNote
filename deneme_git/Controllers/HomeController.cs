using CloudNoteV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudNoteV1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "CloudNote v0.1 (Beta)";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "CloudNote contact info";

            return View();
        }

        public ActionResult CreateNote(HomeViewModel model)
        {
            return View(model);
        }

        public ActionResult CreateAdvancedNote(HomeViewModel model)
        {
            return View(model);
        }


        public ActionResult MyNotes()
        {
            return View();
        }
    }
}