using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionExample
{
    // Контекст базы данных, использующий Entity Framework
    public class ApplicationDbContext : DbContext
    {
        public DbSet<StorageTransaction> StorageTransactions { get; set; }

        // Настройка контекста
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Указываем, что мы будем использовать In-Memory базу данных для демонстрации
            optionsBuilder.UseInMemoryDatabase("TestDatabase");
        }

        // Настройка модели данных
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Создание индекса на столбцы Column1 и Column2
            modelBuilder.Entity<StorageTransaction>()
                        .HasIndex(t => new { t.Column1, t.Column2 })
                        .HasDatabaseName("IDX_Column1_Column2");
        }
    }
}
