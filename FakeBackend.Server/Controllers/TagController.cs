using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using FakeBackend.Server.Models;
using System.Threading.Tasks;

namespace FakeBackend.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private static List<DTag> tags = new List<DTag>
        {
             new DTag
            {
                Id = 1,
                Title = "work",

            },
            new DTag
            {
                Id = 2,
                Title = "personal",

            }
        };

        [HttpGet]
        public IActionResult GetTags()
        {
            return Ok(tags);
        }


        [HttpPost]
        public IActionResult AddTask([FromBody] TagWid tagWid)
        {
            if (tagWid == null) return BadRequest("Task cannot be null.");
            if (string.IsNullOrWhiteSpace(tagWid.Title))
                return BadRequest("Title cannot be empty.");

            var tag = new DTag
            {
                Id = (tags.Count + 1),
                Title = tagWid.Title,
            };
            tags.Add(tag);
            return Ok(tag);


        }



        [HttpDelete("id/{id}")]
        public IActionResult DeleteTag(int id)
        {
            var tag = tags.FirstOrDefault(t => t.Id == id);
            if (tag == null) return NotFound();

            tags.Remove(tag);
            return Ok();
        }
    }
}

