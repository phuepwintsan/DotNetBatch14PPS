using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.EFCoreExamples
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(AppSetting.SqlConnectionStringBuilder.ConnectionString);
            }
        }
        public DbSet<TblBlog> Blogs { get; set; }
    }
    [Table("Tbl_Blog")]
    public class TblBlog
    {
        [Key]
        [Column("BlogId")]
        public string Id { get; set; }

        [Column("BlogTitle")]
        public string Title { get; set; }

        [Column("BlogAuthor")]
        public string Author { get; set; }

        [Column("BlogContent")]
        public string Content { get; set; }

    }
}
