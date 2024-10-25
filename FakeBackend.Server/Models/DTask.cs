//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace FakeBackend.Server.Models
//{
//    public class DTask
//    {
//        [Key]
//        public int id { get; set; }

//        [Column(TypeName ="nvarchar(16)")]
//        public string title { get; set; }

//        [Column(TypeName = "nvarchar(100)")]
//        public string description { get; set; }

//        [Column(TypeName = "nvarchar(16)")]
//        public string tag { get; set; }

//    }
//}



using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace FakeBackend.Server.Models
{
    public class DTask
    {
        public string Id { get; set; }  // Task identifier
        public string Title { get; set; }  // Task title
        public string Description { get; set; }  // Task description
        public DateTime CreationDate { get; set; }  // Task creation date
        public DTag[] Tags { get; set; }  // Task tags
        public bool Completed { get; set; }  // Task state

        public bool Deleted { get; set; }  // Logical delete
        public string Status { get; internal set; }
    }
}
