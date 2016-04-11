using Amazon.DynamoDBv2.Model;
using CloudNoteV1.AWS.DynamoDb;
using CloudNoteV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SaveNote(HomeViewModel model)
        {
            DynamoService ds;
            
            ds = new DynamoService();
            string msgContent = Regex.Replace(model.Content, "<.*?>", string.Empty);
            string msgTitle = Regex.Replace(model.Title, "<.*?>", string.Empty);
            DateTime now = DateTime.Now;
            long dateInMilliseconds = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
            Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue> dc = new Dictionary<string, AttributeValue>();
            dc.Add("item_id", new AttributeValue(dateInMilliseconds.ToString()));
            dc.Add("Title", new AttributeValue(msgContent));
            dc.Add("Content", new AttributeValue(msgTitle));
            dc.Add("Username", new AttributeValue("default"));
            ds.DynamoClient.PutItem("CloudNoteDb", dc);
          
          
            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult CreateNote(HomeViewModel model)
        {
            return View(model);
        }

        public ActionResult CreateAdvancedNote(HomeViewModel model)
        {
            return View(model);
        }

        public ActionResult CreateToDoList()
        {
            return View();
        }

        public ActionResult MyNotes()
        {
            return View();
        }

        public ActionResult SharedNotes()
        {
            return View();
        }



    }
}