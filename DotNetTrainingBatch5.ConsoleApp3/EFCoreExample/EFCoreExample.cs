using DotNetTrainingBatch5.ConsoleApp3.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTrainingBatch5.ConsoleApp3.EFCoreExample
{
    public class EFCoreExample
    {
        private readonly AppDbContext _db;

        public EFCoreExample(AppDbContext db)
        {
            _db = db;
        }

        public void Read()
        {
            var lst = _db.Blogs.Where(x => x.DeleteFlag == false).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {
            BlogDataModel blog = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            _db.Blogs.Add(blog);
            var result = _db.SaveChanges();
            Console.WriteLine(result == 1 ? "Insert Successful" : "Insert Failed");
        }

        public void Edit(int id)
        {
            var item = _db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }

        public void Update(int id, string title, string author, string content)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            if (!string.IsNullOrEmpty(title))
            {
                item.BlogTitle = title;
            }
            if (!string.IsNullOrEmpty(author))
            {
                item.BlogAuthor = author;
            }
            if (!string.IsNullOrEmpty(content))
            {
                item.BlogContent = content;
            }

            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();
            Console.WriteLine(result == 1 ? "Update Successful" : "Update Failed");
        }

        public void Delete(int id)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            _db.Entry(item).State = EntityState.Deleted;
            var result = _db.SaveChanges();
            Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");
        }
    }
}
