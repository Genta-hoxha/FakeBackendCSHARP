//using System.ComponentModel.DataAnnotations;

//namespace FakeBackend.Server.Models
//{
//    public class TaskWid
//    {
//        [Required]
//        public string Title { get; set; }

//        [Required]      
//        public string Description { get; set; }

//        public List<TagWid> Tags { get; set; }
//        public bool? Completed { get; set; }
//        public bool Deleted { get; internal set; }
//    }
//}



using FakeBackend.Server.Models;
using System.ComponentModel.DataAnnotations;

public class TaskWid
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    public List<TagWid> Tags { get; set; }
    public bool? Completed { get; set; }
}
