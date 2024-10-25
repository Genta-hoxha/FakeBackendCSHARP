using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeBackend.Server.Models
{
    public class DTag
    {
        public string Id { get; set; }  // Tag identifier
        public string Title { get; set; }  // Tag title
    }
}
