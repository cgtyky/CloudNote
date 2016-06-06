using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudNoteV1.Models
{
    [DynamoDBTable("CloudNoteDb")]
    public class HomeViewModel
    {
        [DynamoDBHashKey]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Date { get; set; }

        public string Severity { get; set; }

        public string SubmissionDate { get; set; }

        public bool isAlarmSetted { get; set; }
        
        public bool sendMailNotification { get; set; }

        public string Note_Type { get; set; }

        public string Owner { get; set; }
    }
}
