﻿using Microsoft.EntityFrameworkCore;
using StudentManagement.Model;

namespace StudentManagementDataAccess.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
