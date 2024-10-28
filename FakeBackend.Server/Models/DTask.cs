
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Newtonsoft.Json;

//namespace FakeBackend.Server.Models
//{
//    public class DTask
//    {

//        public string Id { get; set; }  // Task identifier
//        public string Title { get; set; }  // Task title
//        public string Description { get; set; }  // Task description
//        public DateTime CreationDate { get; set; }  // Task creation date
//        public DTag[] Tags { get; set; }  // Task tags
//        public bool Completed { get; set; }  // Task state

//        public bool Deleted { get; set; }  // Logical delete
//        public string Status { get; internal set; }
//    }
////}

//using System.ComponentModel.DataAnnotations;
//using System.Collections.Generic;
//using System;
//using System.ComponentModel.DataAnnotations.Schema;
//using Newtonsoft.Json;
//namespace FakeBackend.Server.Models
//{
//    public class DTask
//    {
//        [Key]
//        public string Id { get; set; }  
//        public string Title { get; set; }  
//        public string Description { get; set; }  
//        public bool Completed { get; set; } 
//        public bool Deleted { get; set; } 
//        public List<DTag> Tags { get; set; } 
//        public DateTime CreationDate { get; set; } 
//        public string Status { get; internal set; }
//    }
//}


using FakeBackend.Server.Models;
using System.ComponentModel.DataAnnotations;

public class DTask
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    public bool Completed { get; set; }
    public bool Deleted { get; set; }

    public List<DTag> Tags { get; set; } = new List<DTag>();

    public DateTime CreationDate { get; set; }

    public string Status { get; internal set; }
}

