using RzhadBids.Models;
using RzhadBids.Services;

namespace RzhadBids.Seeder
{
    public static class DatabaseSeeder
    {
        public static void SeedData(DatabaseContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            context.Categories.AddRange(
                new Category { Id = 1, Title = "На ЗСУ" },
                new Category { Id = 2, Title = "Постраждалим" },
                new Category { Id = 3, Title = "Тваринам" },
                new Category { Id = 4, Title = "Дитячий будинок" },
                new Category { Id = 5, Title = "Інвалідам" },
                new Category { Id = 6, Title = "Волонтерство" },
                new Category { Id = 7, Title = "Гуманітарна допомога" }
            );

            context.SaveChanges();
        }
    }
}
