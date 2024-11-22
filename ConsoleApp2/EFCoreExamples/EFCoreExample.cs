using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ConsoleApp2.EFCoreExamples
{
    public class EFCoreExample
    {
        private readonly AppDbContext _db = new AppDbContext();

        public void Read()
        {
            var lst = _db.Blogs.ToList();
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
            //var item = _db.Blogs.Where(x => x.Id == id).FirstOrDefault();
            var item = _db.Blogs.FirstOrDefault(x => x.Id == id);
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

        public void Create(string title, string author, string content)
        {
            var blog = new TblBlog
            {
                Title = title,
                Author = author,
                Content = content
            };
            _db.Blogs.Add(blog);
            var result = _db.SaveChanges();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            Console.WriteLine(message);
        }

        public void Update(string id, string title, string author, string content)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            item.Title = title;
            item.Author = author;
            item.Content = content;

            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            Console.WriteLine(message);
        }

        public void Delete(string id)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            _db.Entry(item).State = EntityState.Deleted;
            var result = _db.SaveChanges();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            Console.WriteLine(message);
        }
    }

    public class Test
    {
        List<Student> students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "John Doe",
                FatherName = "Robert Doe",
                Age = 20,
                Address = "123 Elm Street, Springfield",
                DateOfBirth = new DateTime(2003, 5, 15),
                MobileNo = "123-456-7890",
                Gender = "Male"
            },
            new Student
            {
                Id = 2,
                Name = "Jane Smith",
                FatherName = "Michael Smith",
                Age = 22,
                Address = "456 Oak Avenue, Metropolis",
                DateOfBirth = new DateTime(2001, 3, 10),
                MobileNo = "987-654-3210",
                Gender = "Female"
            },
            new Student
            {
                Id = 3,
                Name = "Alice Johnson",
                FatherName = "William Johnson",
                Age = 19,
                Address = "789 Pine Road, Gotham",
                DateOfBirth = new DateTime(2004, 8, 20),
                MobileNo = "555-666-7777",
                Gender = "Female"
            },
            new Student
            {
                Id = 4,
                Name = "Mark Lee",
                FatherName = "Thomas Lee",
                Age = 21,
                Address = "101 Maple Drive, Star City",
                DateOfBirth = new DateTime(2002, 12, 5),
                MobileNo = "222-333-4444",
                Gender = "Male"
            }
        };

        List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Code = "P1001",
                Name = "Wireless Mouse",
                Price = 25.99,
                Stock = 120,
                Description = "Ergonomic wireless mouse with USB receiver.",
                Discount = 0.10m, // 10% discount
                ProductCategory = "Electronics"
            },
            new Product
            {
                Id = 2,
                Code = "P1002",
                Name = "Gaming Keyboard",
                Price = 79.99,
                Stock = 75,
                Description = "RGB mechanical gaming keyboard.",
                Discount = 0.15m, // 15% discount
                ProductCategory = "Electronics"
            },
            new Product
            {
                Id = 3,
                Code = "P1003",
                Name = "Coffee Mug",
                Price = 9.99,
                Stock = 200,
                Description = "Ceramic coffee mug with handle.",
                Discount = 0.05m, // 5% discount
                ProductCategory = "Home & Kitchen"
            },
            new Product
            {
                Id = 4,
                Code = "P1004",
                Name = "Notebook",
                Price = 2.49,
                Stock = 500,
                Description = "100-page spiral notebook for students.",
                Discount = 0.00m, // No discount
                ProductCategory = "Stationery"
            }
        };

        public void Example()
        {
            var lst = students.Where(x => x.Age > 20).ToList();
            var lstFemale = students.Where(x => x.Gender == "Female").ToList();
            var lstMale = students.Where(x => x.Gender == "Male").ToList();

            var lstOrder = students.OrderBy(x => x.Name).ToList();

            var max = students.Max(x => x.Age);

            var dob = students.Where(x => x.DateOfBirth.Year <= 2003).ToList();

            var stock = products.Where(x => x.Stock < 100).ToList();

            var price = products.Where(x => x.Price > 5).ToList();

            var order2 = products.OrderByDescending(x => x.Price).ToList();
            var order3 = products.OrderByDescending(x => x.Stock).ToList();
            var order4 = products.OrderBy(x => x.Stock).ToList();

            var totalAmount = products.Sum(x => Convert.ToDecimal(x.Price * 10));

            products.Add(new Product
            {
                Id = 5,
                Code = "P1009",
                Name = "Gaming Keyboard",
                Price = 800,
                Stock = 1,
                Description = "RGB mechanical gaming keyboard.",
                Discount = 0.15m, // 15% discount
                ProductCategory = "Electronics"
            });
        }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobileNo { get; set; }
        public string Gender { get; set; }
    }


    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public string ProductCategory { get; set; }
    }
}
