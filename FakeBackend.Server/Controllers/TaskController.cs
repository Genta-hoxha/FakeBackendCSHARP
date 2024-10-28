//using Microsoft.AspNetCore.Mvc;
//using FakeBackend.Server.Models;
//using System.Collections.Generic;
//using System.Linq;
//using System;

//namespace FakeBackend.Server.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class TaskController : ControllerBase
//    {
//        private static List<DTask> tasks = new List<DTask>
//        {
//            new DTask
//            {
//                Id = "1",
//                Title = "Task 1",
//                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
//                Tags = new DTag[] { new DTag { Id = "1", Title = "Work" } },
//                Completed = false,
//                Deleted = false,
//                //CreationDate = DateTime.Now,
//                 CreationDate = DateTime.Now.AddDays(-1),
//                Status = "Uncompleted"
//            },
//            new DTask
//            {
//                Id = "2",
//                Title = "Task 2",
//                Description = "Lorem Ipsum has been the industry's standard dummy text.",
//                Tags = new DTag[] { new DTag { Id = "2", Title = "Personal" } },
//                Completed = false,
//                Deleted = false,
//                CreationDate = DateTime.Now,
//                Status = "Uncompleted"
//            }
//        };

//        //[HttpGet]
//        //public IActionResult GetTasks()
//        //{
//        //    var activeTasks = tasks.Where(t => !t.Deleted).ToList();
//        //    return Ok(activeTasks);
//        //}

//        //SORTING BY CREATION DATE
//        //[HttpGet]
//        //public IActionResult GetTasks([FromQuery] string sortOrder = "desc") //descending 
//        //{
//        //    var activeTasks = tasks.Where(t => !t.Deleted);

//        //    // Sort tasks by creation date (latest first)
//        //    activeTasks = sortOrder.ToLower() == "asc"
//        //        ? activeTasks.OrderBy(t => t.CreationDate)
//        //        : activeTasks.OrderByDescending(t => t.CreationDate); // ascending 

//        //    return Ok(activeTasks.ToList());
//        //}



//        //SORTING BY TITLE
//        [HttpGet]
//        public IActionResult GetTasks(
//            [FromQuery] string sortOrder = "asc", 
//            [FromQuery] string sortBy = "creationDate")
//        {
//            var activeTasks = tasks.Where(t => !t.Deleted);

//            // Sort tasks based on the specified sortBy parameter
//            if (sortBy.ToLower() == "title")
//            {
//                activeTasks = sortOrder.ToLower() == "desc"
//                    ? activeTasks.OrderByDescending(t => t.Title)
//                    : activeTasks.OrderBy(t => t.Title);
//            }
//            else // Default sorting by creation date
//            {
//                activeTasks = sortOrder.ToLower() == "desc"
//                    ? activeTasks.OrderByDescending(t => t.CreationDate)
//                    : activeTasks.OrderBy(t => t.CreationDate);
//            }

//            return Ok(activeTasks.ToList());
//        }



//        // ADD TASK
//        [HttpPost]
//        public IActionResult AddTask([FromBody] TaskWid taskWid)
//        {
//            if (taskWid == null) return BadRequest("Task cannot be null.");
//            if (string.IsNullOrWhiteSpace(taskWid.Title) || string.IsNullOrWhiteSpace(taskWid.Description))
//                return BadRequest("Title and Description cannot be empty.");

//            var task = new DTask
//            {
//                Id = (tasks.Count + 1).ToString(),
//                Title = taskWid.Title,
//                Description = taskWid.Description,
//                Tags = taskWid.Tags?.Select((t, index) => new DTag
//                {
//                    Id = (tasks.SelectMany(t => t.Tags).Count() + index + 1).ToString(),
//                    Title = t.Title
//                }).ToArray() ?? new DTag[] { },
//                Completed = taskWid.Completed ?? false, // Default to false if null
//                Deleted = false,
//                CreationDate = DateTime.Now,
//                Status = (taskWid.Completed == true) ? "Completed" : "Uncompleted"
//            };

//            tasks.Add(task);
//            return Ok(task);
//        }



//        // DELETE BY ID, TITLE, TAG

//        [HttpDelete("id/{id:int}")]
//        public IActionResult DeleteTask(int id)
//        {
//            var task = tasks.FirstOrDefault(t => t.Id == id.ToString());
//            if (task == null) return NotFound();

//            task.Deleted = true;
//            return Ok();
//        }

//        // DELETE task by title (logical delete)
//        [HttpDelete("{title}")]
//        public IActionResult DeleteTask(string title)
//        {
//            var task = tasks.FirstOrDefault(t => t.Title == title && !t.Deleted);
//            if (task == null) return NotFound();

//            task.Deleted = true;
//            return Ok();
//        }

//        // DELETE tasks by tag (logical delete)
//        [HttpDelete("tags")]
//        public IActionResult DeleteTasksByTags([FromQuery] string tags)
//        {
//            var tagList = tags.Split(',')
//                              .Select(t => t.Trim())
//                              .Where(t => !string.IsNullOrWhiteSpace(t))
//                              .ToList();

//            if (!tagList.Any()) return BadRequest("No valid tags provided.");

//            var tasksToDelete = tasks
//                .Where(t => !t.Deleted && t.Tags.Any(tg => tagList.Contains(tg.Title, StringComparer.OrdinalIgnoreCase)))
//                .ToList();

//            if (!tasksToDelete.Any()) return NotFound("No tasks found with the specified tags.");

//            foreach (var task in tasksToDelete)
//            {
//                task.Deleted = true;
//            }

//            return Ok(new { DeletedCount = tasksToDelete.Count });
//        }

//        // Get a task by id
//        [HttpGet("id/{id:int}")]
//        public IActionResult GetTaskById(int id)
//        {
//            var task = tasks.FirstOrDefault(t => t.Id == id.ToString());
//            if (task == null) return NotFound();

//            return Ok(task);
//        }

//        // Get a task by title
//        [HttpGet("{title}")]
//        public IActionResult GetTaskByTitle(string title)
//        {
//            // Normalize title to be case-insensitive
//            var task = tasks.FirstOrDefault(t => string.Equals(t.Title, title, StringComparison.OrdinalIgnoreCase) && !t.Deleted);

//            if (task == null) return NotFound();

//            return Ok(task);
//        }


//        // Get tasks by multiple tags
//        [HttpGet("tags/{tags}")]
//        public IActionResult GetTasksByTags(string tags)
//        {
//            var tagList = tags.Split(',')
//                              .Select(t => t.Trim())
//                              .Where(t => !string.IsNullOrWhiteSpace(t))
//                              .ToList();

//            if (!tagList.Any()) return BadRequest("No valid tags provided.");

//            var matchingTasks = tasks
//                .Where(t => !t.Deleted && t.Tags.Any(tg => tagList.Contains(tg.Title, StringComparer.OrdinalIgnoreCase)))
//                .ToList();

//            if (!matchingTasks.Any()) return NotFound();

//            return Ok(matchingTasks);
//        }

//        // Get tasks by status (completed or uncompleted)
//        [HttpGet("status/{status}")]
//        public IActionResult GetTasksByStatus(string status)
//        {
//            var normalizedStatus = status.Trim().ToLower();

//            var matchingTasks = tasks
//                .Where(t => !t.Deleted &&
//                            ((normalizedStatus == "completed" && t.Completed) ||
//                             (normalizedStatus == "uncompleted" && !t.Completed)))
//                .ToList();

//            if (!matchingTasks.Any()) return NotFound("No tasks found with the specified status.");

//            return Ok(matchingTasks);
//        }

//        // PUT (EDIT)
//        [HttpPut("{id:int}")]
//        public IActionResult UpdateTask(int id, [FromBody] TaskWid taskWid)
//        {
//            var task = tasks.FirstOrDefault(t => t.Id == id.ToString());
//            if (task == null) return NotFound();

//            // Update Title and Description
//            if (!string.IsNullOrWhiteSpace(taskWid.Title))
//                task.Title = taskWid.Title;

//            if (!string.IsNullOrWhiteSpace(taskWid.Description))
//                task.Description = taskWid.Description;

//            // Update Completed status if it's provided
//            task.Completed = taskWid.Completed ?? task.Completed;
//            task.Status = taskWid.Completed.Value ? "Completed" : "Uncompleted";
//            // Update Tags if provided
//            if (taskWid.Tags != null)
//            {
//                task.Tags = taskWid.Tags.Select(t => new DTag
//                {
//                    Id = (tasks.SelectMany(t => t.Tags).Count() ).ToString(), 
//                    Title = t.Title
//                }).ToArray();
//            }

//            return Ok(task);
//        }





//    }
//}

//using FakeBackend.Server.Models;
//using Microsoft.AspNetCore.Mvc;
//using System.Linq;
//using System;
//namespace FakeBackend.Server.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class TaskController : ControllerBase
//    {
//        private static List<DTask> tasks = new List<DTask>
//            {
//                new DTask
//                {
//                    Id = "1",
//                    Title = "Task 1",
//                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
//                    Tags = new DTag[] { new DTag { Id = "1", Title = "Work" } },
//                    Completed = false,
//                    Deleted = false,
//                    //CreationDate = DateTime.Now,
//                     CreationDate = DateTime.Now.AddDays(-1),
//                    Status = "Uncompleted"
//                },
//                new DTask
//                {
//                    Id = "2",
//                    Title = "Task 2",
//                    Description = "Lorem Ipsum has been the industry's standard dummy text.",
//                    Tags = new DTag[] { new DTag { Id = "2", Title = "Personal" } },
//                    Completed = false,
//                    Deleted = false,
//                    CreationDate = DateTime.Now,
//                    Status = "Uncompleted"
//                }
//            };

//        [HttpGet]
//        public IActionResult GetTasks(
//            [FromQuery] string sortOrder = "asc",
//            [FromQuery] string sortBy = "creationDate")
//        {
//            var activeTasks = tasks.Where(t => !t.Deleted);

//            activeTasks = sortBy.ToLower() switch
//            {
//                "title" => sortOrder.ToLower() == "desc" ? activeTasks.OrderByDescending(t => t.Title) : activeTasks.OrderBy(t => t.Title),
//                _ => sortOrder.ToLower() == "desc" ? activeTasks.OrderByDescending(t => t.CreationDate) : activeTasks.OrderBy(t => t.CreationDate),
//            };

//            return Ok(activeTasks.ToList());
//        }

//        [HttpPost]
//        public IActionResult AddTask([FromBody] TaskWid taskWid)
//        {
//            if (taskWid == null) return BadRequest("Task cannot be null.");
//            if (string.IsNullOrWhiteSpace(taskWid.Title) || string.IsNullOrWhiteSpace(taskWid.Description))
//                return BadRequest("Title and Description cannot be empty.");

//            var task = new DTask
//            {
//                Id = GenerateTaskId(),
//                CreationDate = DateTime.Now,
//                Deleted = false,
//                Title = taskWid.Title,
//                Description = taskWid.Description,
//                Tags = taskWid.Tags?.Select(t => new DTag
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Title = t.Title
//                }).ToList() ?? new List<DTag>(),
//                Completed = taskWid.Completed ?? false,
//                Status = taskWid.Completed == true ? "Completed" : "Uncompleted"
//            };

//            tasks.Add(task);
//            return Ok(task);

//            //return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
//        }

//        [HttpPut("{id:int}")]
//        public IActionResult UpdateTask(int id, [FromBody] TaskWid taskWid)
//        {
//            var task = tasks.FirstOrDefault(t => t.Id == id.ToString());
//            if (task == null) return NotFound();

//            if (!string.IsNullOrWhiteSpace(taskWid.Title))
//                task.Title = taskWid.Title;

//            if (!string.IsNullOrWhiteSpace(taskWid.Description))
//                task.Description = taskWid.Description;

//            task.Completed = taskWid.Completed ?? task.Completed;
//            task.Status = task.Completed ? "Completed" : "Uncompleted";

//            if (taskWid.Tags != null)
//            {
//                task.Tags = taskWid.Tags.Select(t => new DTag
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Title = t.Title
//                }).ToList();
//            }

//            return Ok(task);
//        }

//        // DELETE BY ID, TITLE, TAG

//        [HttpDelete("id/{id:int}")]
//        public IActionResult DeleteTask(int id)
//        {
//            var task = tasks.FirstOrDefault(t => t.Id == id.ToString());
//            if (task == null) return NotFound();

//            task.Deleted = true;
//            return Ok();
//        }

//        // DELETE task by title (logical delete)
//        [HttpDelete("{title}")]
//        public IActionResult DeleteTask(string title)
//        {
//            var task = tasks.FirstOrDefault(t => t.Title == title && !t.Deleted);
//            if (task == null) return NotFound();

//            task.Deleted = true;
//            return Ok();
//        }

//        // DELETE tasks by tag (logical delete)
//        [HttpDelete("tags")]
//        public IActionResult DeleteTasksByTags([FromQuery] string tags)
//        {
//            var tagList = tags.Split(',')
//                              .Select(t => t.Trim())
//                              .Where(t => !string.IsNullOrWhiteSpace(t))
//                              .ToList();

//            if (!tagList.Any()) return BadRequest("No valid tags provided.");

//            var tasksToDelete = tasks
//                .Where(t => !t.Deleted && t.Tags.Any(tg => tagList.Contains(tg.Title, StringComparer.OrdinalIgnoreCase)))
//                .ToList();

//            if (!tasksToDelete.Any()) return NotFound("No tasks found with the specified tags.");

//            foreach (var task in tasksToDelete)
//            {
//                task.Deleted = true;
//            }

//            return Ok(new { DeletedCount = tasksToDelete.Count });
//        }


//    }
//}



///////////////////////////////
///
using FakeBackend.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;

namespace FakeBackend.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private static List<DTask> tasks = new List<DTask>
        {
            new DTask
            {
                Id = 1,
                Title = "Task 1",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                Tags = new List<DTag> { new DTag { Id = 1, Title = "Work" } },
                Completed = false,
                Deleted = false,
                CreationDate = DateTime.Now.AddDays(-1),
                Status = "Uncompleted"
            },
            new DTask
            {
                Id = 2,
                Title = "Task 2",
                Description = "Lorem Ipsum has been the industry's standard dummy text.",
                Tags = new List<DTag> { new DTag { Id = 2, Title = "Personal" } },
                Completed = false,
                Deleted = false,
                CreationDate = DateTime.Now,
                Status = "Uncompleted"
            }
        };


        [HttpGet]
        public ActionResult<IEnumerable<DTask>> GetTasks(
            [FromQuery] string sortOrder = "asc",
            [FromQuery] string sortBy = "creationDate")
        {
            var activeTasks = tasks.Where(t => !t.Deleted);

            activeTasks = sortBy.ToLower() switch
            {
                "title" => sortOrder.ToLower() == "desc" ? activeTasks.OrderByDescending(t => t.Title) : activeTasks.OrderBy(t => t.Title),
                _ => sortOrder.ToLower() == "desc" ? activeTasks.OrderByDescending(t => t.CreationDate) : activeTasks.OrderBy(t => t.CreationDate),
            };

            return Ok(activeTasks.ToList());
        }



        // Get a task by id
        [HttpGet("id/{id:int}")]
        public IActionResult GetTaskById(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            return Ok(task);
        }

        // Get a task by title
        [HttpGet("{title}")]
        public IActionResult GetTaskByTitle(string title)
        {
          var task = tasks.FirstOrDefault(t => string.Equals(t.Title, title, StringComparison.OrdinalIgnoreCase) && !t.Deleted);

            if (task == null) return NotFound();

            return Ok(task);
        }


        // Get tasks by multiple tags
        [HttpGet("tags/{tags}")]
        public IActionResult GetTasksByTags(string tags)
        {
            var tagList = tags.Split(',')
                              .Select(t => t.Trim())
                              .Where(t => !string.IsNullOrWhiteSpace(t))
                              .ToList();

            if (!tagList.Any()) return BadRequest("No valid tags provided.");

            var matchingTasks = tasks
                .Where(t => !t.Deleted && t.Tags.Any(tg => tagList.Contains(tg.Title, StringComparer.OrdinalIgnoreCase)))
                .ToList();

            if (!matchingTasks.Any()) return NotFound();

            return Ok(matchingTasks);
        }


        // Get tasks by status
        [HttpGet("status/{status}")]
        public IActionResult GetTasksByStatus(string status)
        {
            var normalizedStatus = status.Trim().ToLower();

            var matchingTasks = tasks
                .Where(t => !t.Deleted &&
                            ((normalizedStatus == "completed" && t.Completed) ||
                             (normalizedStatus == "uncompleted" && !t.Completed)))
                .ToList();

            if (!matchingTasks.Any()) return NotFound("No tasks found with the specified status.");

            return Ok(matchingTasks);
        }


        //Add new task
        [HttpPost]
        public ActionResult<DTask> AddTask([FromBody] TaskWid taskWid)
        {
            if (taskWid == null) return BadRequest("Task cannot be null.");
            if (string.IsNullOrWhiteSpace(taskWid.Title) || string.IsNullOrWhiteSpace(taskWid.Description))
                return BadRequest("Title and Description cannot be empty.");

           
            var taskId = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;

            var task = new DTask
            {
                Id = taskId, 
                Title = taskWid.Title,
                Description = taskWid.Description,
                CreationDate = DateTime.Now,
                Deleted = false,
                Completed = taskWid.Completed ?? false,
                Status = taskWid.Completed == true ? "Completed" : "Uncompleted",
                Tags = taskWid.Tags?.Select(t => new DTag
                {
                    Id = tasks.Count > 0 ? tasks.SelectMany(t => t.Tags).Max(tag => tag.Id) + 1 : 1,
                    Title = t.Title,
                }).ToList() ?? new List<DTag>()
            };

            tasks.Add(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }


        //Edit task by id
       [HttpPut("{id:int}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskWid taskWid)
        {
            if (taskWid == null) return BadRequest("Task cannot be null.");
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(taskWid.Title))
                task.Title = taskWid.Title;

            if (!string.IsNullOrWhiteSpace(taskWid.Description))
                task.Description = taskWid.Description;

            task.Completed = taskWid.Completed ?? task.Completed;
            task.Status = task.Completed ? "Completed" : "Uncompleted";

            if (taskWid.Tags != null)
            {
                task.Tags = taskWid.Tags.Select(t => new DTag
                {
                    Id = tasks.Count > 0 ? tasks.SelectMany(t => t.Tags).Max(tag => tag.Id) + 1 : 1,
                    Title = t.Title
                }).ToList();
            }

            return Ok(task);
        }

        //Delete task by id
        [HttpDelete("{id:int}")]
        public IActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            task.Deleted = true;
            return Ok();
        }

        //Delete task by title
        [HttpDelete("title/{title}")]
        public IActionResult DeleteTaskByTitle(string title)
        {
            var task = tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && !t.Deleted);
            if (task == null) return NotFound();

            task.Deleted = true;
            return Ok();
        }

        //Delete task by tags
        [HttpDelete("tags")]
        public IActionResult DeleteTasksByTags([FromQuery] string tags)
        {
            var tagList = tags.Split(',')
                              .Select(t => t.Trim())
                              .Where(t => !string.IsNullOrWhiteSpace(t))
                              .ToList();

            if (!tagList.Any()) return BadRequest("No valid tags provided.");

            var tasksToDelete = tasks
                .Where(t => !t.Deleted && t.Tags.Any(tg => tagList.Contains(tg.Title, StringComparer.OrdinalIgnoreCase)))
                .ToList();

            if (!tasksToDelete.Any()) return NotFound("No tasks found with the specified tags.");

            foreach (var task in tasksToDelete)
            {
                task.Deleted = true;
            }

            return Ok(new { DeletedCount = tasksToDelete.Count });
        }

        private string GenerateTaskId()
        {
            return (tasks.Count + 1).ToString();
        }

        // Delete all tasks
        [HttpDelete("all")]
        public IActionResult DeleteAllTasks()
        {
            foreach (var task in tasks)
            {
                task.Deleted = true;
            }
            return Ok();
        }

    }
}
