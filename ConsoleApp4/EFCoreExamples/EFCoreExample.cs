using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.EFCoreExamples
{
    public class EFCoreExample : DbContext
    {
        private readonly AppDbContext _dp = new AppDbContext();

        public void Read()
        {
            var lst = _dp.Blogs.ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.Id);
                Console.WriteLine(item.Title);
                Console.WriteLine(item.Author);
                Console.WriteLine(item.Content);
            }
        }
        public void Edit(string id)
        {
            var item = _dp.Blogs.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine(item.Id);
            Console.WriteLine(item.Title);
            Console.WriteLine(item.Author);
            Console.WriteLine(item.Content);
        }
        public void Create(string BlogTitle, string BlogAuthor, string BlogContent)
        {
            var Blog = new TblBlog()
            {
                Id = Guid.NewGuid().ToString(),
                Title = BlogTitle,
                Author = BlogAuthor,
                Content = BlogContent
            };
            _dp.Blogs.Add(Blog);
            int result = _dp.SaveChanges();

            string message = result > 0 ? "Success Message" : "Failed Message";
            Console.WriteLine(message);
        }
        public void Update(string id, string title, string author, string content)
        {
            var item = _dp.Blogs.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            item.Title = title;
            item.Author = author;
            item.Content = content;

            _dp.Entry(item).State = EntityState.Modified;
            var result = _dp.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }
        public void Delete(string id)
        {
            var item = _dp.Blogs.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            _dp.Entry(item).State = EntityState.Deleted;
            var result = _dp.SaveChanges();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }
}
