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
            dc.Add("Title", new AttributeValue(msgTitle));
            dc.Add("Content", new AttributeValue(msgContent));
            dc.Add("Username", new AttributeValue("default"));
            ds.DynamoClient.PutItem("CloudNoteDb", dc);
       
            ds = null;
          
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
            List<HomeViewModel> listResult = RetrieveNotes();
            return View(listResult);
        }

        public ActionResult SharedNotes()
        {
            return View();
        }

        public List<HomeViewModel> RetrieveNotes()
        {
            DynamoService ds = new DynamoService();
            List<HomeViewModel> list = new List<HomeViewModel>();

            List<string> attrToGet = new List<string>();
            attrToGet.Add("Title");
            attrToGet.Add("Content");

            ScanResponse res = ds.DynamoClient.Scan("CloudNoteDb", attrToGet);

            //foreach (Dictionary<string, AttributeValue> item
            //      in res.Items)
            //{
            //    item.Values.
            //    PrintItem(item);
            //}

            for (int i = 0; i < res.ScannedCount; i++)
            {
                Dictionary<string, AttributeValue> item = res.Items[i];
               
                HomeViewModel hvm = new HomeViewModel();
                hvm = LogItem(item);
                list.Add(hvm);
            }
            //Dictionary<string, AttributeValue> dict = new Dictionary<string, AttributeValue>();
            //List<HomeViewModel> list = (List<HomeViewModel>)ds.GetAll<HomeViewModel>();

            return list;
        }

        private HomeViewModel LogItem(Dictionary<string, AttributeValue> attributeList)
        {
            HomeViewModel returnModel = new HomeViewModel();
            foreach (KeyValuePair<string, AttributeValue> kvp in attributeList)
            {
                string attributeName = kvp.Key;
                AttributeValue value = kvp.Value;
                if (attributeName.Equals("Title"))
                {
                    returnModel.Title = value.ToString();
                }
                else if (attributeName.Equals("Content"))
                {
                    returnModel.Content = value.ToString();
                }

            }
            return returnModel;
        }

        public ActionResult GetNotes()
        {
            return View(RetrieveNotes());
        }


    }
}