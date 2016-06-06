using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using CloudNoteV1.AWS.DynamoDb;
using CloudNoteV1.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;


namespace CloudNoteV1.Controllers
{
    public class toDoListController : Controller
    {
       
        // GET: toDoList
        public ActionResult Index()
        {
            return View();
        }
        DynamoService ds = new DynamoService();
        [HttpGet]
        public JsonResult GetTodoLists()
        {
            string activeUser = "";
            Dictionary<string, AttributeValue> item = null;
            DynamoService ds = new DynamoService();
            List<toDoList> list = new List<toDoList>();


            List<string> attrToGet = new List<string>();
            attrToGet.Add("item_id");
            attrToGet.Add("Title");
            attrToGet.Add("TaskDescription");
            attrToGet.Add("Severity");
            attrToGet.Add("TargetDate");
            attrToGet.Add("taskStatus");
            attrToGet.Add("Owner");

            if (User.Identity.IsAuthenticated)
                activeUser = User.Identity.Name;
            else
                activeUser = "";

            ScanResponse res = ds.DynamoClient.Scan("ToDoList", attrToGet);

            for (int i = 0; i < res.ScannedCount; i++)
            {
                if (res.Items[i]["Owner"].S == activeUser)
                {
                    item = res.Items[i];

                    list.Add(LogItem(item));
                }
            }


            var result = new
            {
                page = "1",
                total = "1",
                records = "10",
                rows = list.ToArray()
            };

         //   string Jlist = Newtonsoft.Json.JsonConvert.SerializeObject(result);

            return Json(result, JsonRequestBehavior.AllowGet);

      
            //Dictionary<string, AttributeValue> dict = new Dictionary<string, AttributeValue>();
            //List<HomeViewModel> list = (List<HomeViewModel>)ds.GetAll<HomeViewModel>();

          //  return list;
        }
        
        public string Delete(int Id)
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            DynamoService ds = new DynamoService();

            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
                {
                  { "Title", new AttributeValue { S = "asd" } }
                };

            // Create DeleteItem request
            DeleteItemRequest request = new DeleteItemRequest
            {
                TableName = "ToDoList",
                Key = key
            };

            var response = client.DeleteItemAsync(request);

            return "Item Deleted Successfuly";
        }

        [HttpPost]
        public string Create([Bind(Exclude = "Id")] toDoList model)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    ds = new DynamoService();

                    int id = model.Id;
                    string taskName = Regex.Replace(model.Title, "<.*?>", string.Empty);
                    string taskDescription = Regex.Replace(model.TaskDescription, "<.*?>", string.Empty);
                    string targetDate = model.TargetDate.ToString();
                    string taskStatus = model.taskStatus;
                    string severity = model.Severity;
                    string activeUser = User.Identity.Name;
                    DateTime now = DateTime.Now;
                    long dateInMilliseconds = (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
                    Dictionary<string, Amazon.DynamoDBv2.Model.AttributeValue> dc = new Dictionary<string, AttributeValue>();
                    dc.Add("item_id", new AttributeValue(id.ToString()));
                    dc.Add("Title", new AttributeValue(taskName));
                    dc.Add("TaskDescription", new AttributeValue(taskDescription));
                    dc.Add("TargetDate", new AttributeValue(targetDate));
                    dc.Add("Severity", new AttributeValue(severity));
                    dc.Add("taskStatus", new AttributeValue(taskStatus));
                    dc.Add("Owner", new AttributeValue(activeUser));
                    dc.Add("Note_Type", new AttributeValue("ToDo List"));
                    ds.DynamoClient.PutItem("ToDoList", dc);
                    msg = "Item Successfully Added";
                    ds = null;
         
                }
                else
                {
                    msg = "Validation data not successfull";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        private toDoList LogItem(Dictionary<string, AttributeValue> attributeList)
        {
            toDoList returnModel = new toDoList();
            foreach (KeyValuePair<string, AttributeValue> kvp in attributeList)
            {
                string attributeName = kvp.Key;

                AttributeValue value = kvp.Value;
                if (attributeName.Equals("Title"))
                {
                    returnModel.Title = value.S;
                }
                else if (attributeName.Equals("TargetDate"))
                {
                    returnModel.TargetDate = value.S;
                }
                else if (attributeName.Equals("TaskDescription"))
                {
                    returnModel.TaskDescription = value.S;
                }
                else if (attributeName.Equals("Severity"))
                {
                    returnModel.Severity = value.S;
                }
          
                else if (attributeName.Equals("taskStatus"))
                {
                    returnModel.taskStatus = value.S;
                }


            }
            return returnModel;
        }
    }
}