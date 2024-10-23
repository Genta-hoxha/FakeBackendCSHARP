using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeBackend.Server.Models
{
    public class DTask
    {
        [Key]
        public int id { get; set; }

        [Column(TypeName ="nvarchar(16)")]
        public string title { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string description { get; set; }

        [Column(TypeName = "nvarchar(16)")]
        public string tag { get; set; }
      
    }
}
