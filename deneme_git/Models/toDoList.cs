using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudNoteV1.Models
{
    [DynamoDBTable("CloudNoteDb")]
    public class toDoList
    {
        public int Id { get; set; }
        [DynamoDBHashKey]
        public string Title { get; set; }
        [DynamoDBRangeKey]
        public string TaskDescription { get; set; }
        public string TargetDate { get; set; }
        public string Severity { get; set; }
        public string taskStatus { get; set; }
    }
}