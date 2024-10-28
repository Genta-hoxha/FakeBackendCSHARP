//using System.Collections.Generic;
//using System.Linq;
//using FakeBackend.Server.Models;
//using Microsoft.EntityFrameworkCore;

//namespace FakeBackend.Server.Data
//{
//    public class TaskService
//    {
//        private readonly TaskContext _context;

//        public TaskService(TaskContext context)
//        {
//            _context = context;
//        }

//        public void AddTask(DTask task)
//        {
//            _context.DTask.Add(task);
//            _context.SaveChanges();
//        }

//        public List<DTask> GetAllTasks()
//        {
//            return _context.DTask.Where(t => !t.Deleted).ToList();
//        }

//        public DTask GetTaskById(string id)
//        {
//            return _context.DTask.FirstOrDefault(t => t.Id == id && !t.Deleted);
//        }

//        public void UpdateTask(DTask updatedTask)
//        {
//            var task = GetTaskById(updatedTask.Id);
//            if (task != null)
//            {
//                task.Title = updatedTask.Title;
//                task.Description = updatedTask.Description;
//                task.Completed = updatedTask.Completed;
//                _context.SaveChanges();
//            }
//        }

//        public void DeleteTask(string id)
//        {
//            var task = GetTaskById(id);
//            if (task != null)
//            {
//                task.Deleted = true;
//                UpdateTask(task); 
//            }
//        }
//    }
//}

