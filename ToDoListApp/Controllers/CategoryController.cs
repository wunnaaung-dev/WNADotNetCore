using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListDb.Models;
namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {

        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetCategories()
        {
            var lst = _db.TaskCategories.AsNoTracking().ToList();
            return Ok(lst);
        }

        [HttpPost]
        public IActionResult CreateCategory(TaskCategory category)
        {
            _db.TaskCategories.Add(category);
            _db.SaveChanges();
            return Ok(category);
        }

        [HttpPatch]
        public IActionResult UpdateCategory(int id, TaskCategory category)
        {
            var item = _db.TaskCategories.AsNoTracking().FirstOrDefault(x => x.CategoryId == id);
            if (item is null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(category.CategoryName))
            {
                item.CategoryName = category.CategoryName;
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok();
        }
    }
}
