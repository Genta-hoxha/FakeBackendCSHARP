using Microsoft.AspNetCore.Mvc;
using FakeBackend.Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace FakeBackend.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private static List<DTask> tasks = new List<DTask>
        {
            new DTask { id = 1, title = "Task 1", description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s", tag =  "Work"},
            new DTask { id = 2, title = "Task 2", description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s", tag =  "Personal" }
        };

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult AddTask([FromBody] DTask task)
        {

            int maxId = tasks.Max(t => t.id);
            int newId = maxId + 1;
            task.id = newId;
            tasks.Add(task);
            return Ok(task);
        }


        //BEJME FSHIRJEN ME ANE TE TITULLIT (JO ME ID)
        [HttpDelete("{title}")]
        public IActionResult DeleteTask(string title)
        {
            var task = tasks.FirstOrDefault(t => t.title == title);
            if (task == null) return NotFound();
            tasks.Remove(task);
            return Ok();
        }
    }
}