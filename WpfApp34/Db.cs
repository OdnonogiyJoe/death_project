using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp34
{
    public class Db : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sb = new SqlConnectionStringBuilder();
            sb.DataSource = @"PREPOD-CAB1\MSSQLSERVER2";
            sb.InitialCatalog = "books";
            sb.IntegratedSecurity = true;
            optionsBuilder.UseSqlServer(sb.ToString());
            base.OnConfiguring(optionsBuilder);
        }

        private Db()
        {
            Database.EnsureCreated();
        }

        static Db db;
        public static Db GetDb()
        {
            if (db == null)
                db = new Db();
            return db;
        }
    }
}
