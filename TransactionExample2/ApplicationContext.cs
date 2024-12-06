using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        // Статический экземпляр LoggerFactory для настройки логирования
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name
                        && level == LogLevel.Information) // Фильтр уровня логирования
                    .AddConsole(); // Вывод в консоль
            });

        // Настройка контекста
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory) // Подключение логирования
                .EnableSensitiveDataLogging() // Выводить конфиденциальные данные в логах (например, параметры запросов)
                .UseInMemoryDatabase("TestDatabase"); // Использование In-Memory базы данных
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
