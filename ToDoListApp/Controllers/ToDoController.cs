using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using ToDoListDb.Models;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : Controller
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetTodos()
        {
            var lst = _db.ToDoLists.AsNoTracking().Where(x => x.Status == "Pending").ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetTodo(int id)
        {
            var item = _db.ToDoLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateTodo(ToDoList toDo)
        {
            _db.ToDoLists.Add(toDo);
            _db.SaveChanges();
            return Ok(toDo);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateTodo(int id, ToDoList toDo)
        {
            var item = _db.ToDoLists.AsNoTracking().FirstOrDefault(x => x.TaskId == id);
            if (item is null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(toDo.TaskTitle))
            {
                item.TaskTitle = toDo.TaskTitle;
            }
            if (!string.IsNullOrEmpty(toDo.TaskDescription))
            {
                item.TaskDescription = toDo.TaskDescription;
            }
            if (toDo.CategoryId.HasValue)
            {
                item.CategoryId = toDo.CategoryId;
            }
            if (toDo.PriorityLevel.HasValue)
            {
                item.PriorityLevel = toDo.PriorityLevel;
            }
            if (!string.IsNullOrEmpty(toDo.Status))
            {
                item.Status = toDo.Status;
            }
            if (toDo.DueDate.HasValue)
            {
                item.DueDate = toDo.DueDate;
            }if (toDo.CompletedDate.HasValue)
            {
                item.CompletedDate = toDo.CompletedDate;
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok();
        }
    }
}
