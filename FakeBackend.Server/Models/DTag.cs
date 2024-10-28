//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace FakeBackend.Server.Models
//{
//    public class DTag
//    {
//        [Key]
//        public string Id { get; set; }  
//        public string Title { get; set; }  
//    }
//}


using System.ComponentModel.DataAnnotations;

public class DTag
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }
}
