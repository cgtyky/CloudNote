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

        [DynamoDBRangeKey]
        public int SubmissionDate { get; set; }
    }
}
