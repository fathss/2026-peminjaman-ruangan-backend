using Microsoft.EntityFrameworkCore;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Seeders
{
    public class UserSeeders
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@campus.com",
                    PasswordHash = "$2a$11$ec/jgyW.C8Q61nprj0IvVesMTiFyg3eaF0hMc/eNSo1PaWIR6uPiC", // admin123
                    Role = "Admin",
                    IsDeleted = false,
                    DeletedAt = null
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    Email = "user@campus.com",
                    PasswordHash = "$2a$11$Op6pjkCaU.9FkqNrtWkJ2OpWe/FE8f5urbgfPZhZRaP8d5743/eUu", // user123
                    Role = "User",
                    IsDeleted = false,
                    DeletedAt = null
                }
            );
        }
    }
}