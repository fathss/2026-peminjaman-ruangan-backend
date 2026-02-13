using Microsoft.EntityFrameworkCore;
using PeminjamanRuanganAPI.Models;

namespace PeminjamanRuanganAPI.Seeders
{
    public class RoomSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Name = "HH-203",
                    Location = "Gedung D3, LT. 2",
                    Capacity = 30,
                    Description = "Ruang kelas",
                    IsActive = true
                },
                new Room
                {
                    Id = 2,
                    Name = "SAW-06.06",
                    Location = "Gedung SAW, LT. 6",
                    Capacity = 30,
                    Description = "Ruang kelas",
                    IsActive = true
                },
                new Room
                {
                    Id = 3,
                    Name = "SAW-07.10",
                    Location = "Gedung SAW, LT. 7",
                    Capacity = 120,
                    Description = "Ruang kelas",
                    IsActive = true
                }
            );
        }
    }
}