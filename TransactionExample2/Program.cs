
using TransactionExample;

// Инициализация базы данных и выполнение запросов
using (var context = new ApplicationDbContext())
{
    // Вставка данных
    context.StorageTransactions.AddRange(new List<StorageTransaction>
                {
                    new StorageTransaction { Column1 = "Value1", Column2 = "ValueA" },
                    new StorageTransaction { Column1 = "Value1", Column2 = "ValueB" },
                    new StorageTransaction { Column1 = "Value2", Column2 = "ValueC" }
                });
    context.SaveChanges();

    Console.WriteLine("Данные успешно добавлены.");

    // Выполнение запроса с фильтрацией по Column1 и Column2
    var result = context.StorageTransactions
                        .Where(t => t.Column1 == "Value1" && t.Column2 == "ValueA")
                        .ToList();

    // Вывод результата
    Console.WriteLine($"Найдено записей: {result.Count}");
    foreach (var record in result)
    {
        Console.WriteLine($"ID: {record.Id}, Column1: {record.Column1}, Column2: {record.Column2}");
    }
}