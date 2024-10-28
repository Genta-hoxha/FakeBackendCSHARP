using System.ComponentModel.DataAnnotations;

namespace FakeBackend.Server.Models
{
    public class TagWid
    {
        [Required]
        public string Title { get; set; }
    }
}
