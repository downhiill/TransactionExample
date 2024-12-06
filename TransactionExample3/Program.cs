using System.Data.SQLite;

//  изоляции "Serializabl"

string connectionString = "Data Source=:memory:;Version=3;New=True;";

using (SQLiteConnection connection = new SQLiteConnection(connectionString))
{
    await connection.OpenAsync();

    // Создаем таблицу
    using (var createTableCommand = new SQLiteCommand(
        "CREATE TABLE Accounts (Id INTEGER PRIMARY KEY, Name TEXT, Balance REAL);", connection))
    {
        await createTableCommand.ExecuteNonQueryAsync();
    }

    // Начинаем транзакцию
    SQLiteTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.Serializable);

    try
    {
        // Проверяем количество записей
        SQLiteCommand command1 = new SQLiteCommand(
            "SELECT COUNT(*) FROM Accounts", connection, transaction);
        long count = (long)await command1.ExecuteScalarAsync();
        Console.WriteLine($"Начальное количество записей: {count}");

        // Добавляем новую запись
        SQLiteCommand command2 = new SQLiteCommand(
            "INSERT INTO Accounts (Name, Balance) VALUES ('Charlie', 700);", connection, transaction);
        await command2.ExecuteNonQueryAsync();

        // Подтверждаем транзакцию
        transaction.Commit();
        Console.WriteLine("Транзакция завершена успешно.");
    }
    catch (Exception ex)
    {
        // Откатываем транзакцию при ошибке
        transaction.Rollback();
        Console.WriteLine($"Ошибка: {ex.Message}");
    }

    // Проверяем содержимое таблицы
    using (var checkCommand = new SQLiteCommand("SELECT * FROM Accounts;", connection))
    using (var reader = await checkCommand.ExecuteReaderAsync())
    {
        while (await reader.ReadAsync())
        {
            Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Balance: {reader["Balance"]}");
        }
    }
}