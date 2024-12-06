using System;
using System.Data.SQLite;

// Успешное выполнение транзакции 

using (var connection = new SQLiteConnection("Data Source=:memory:;Version=3;New=True;"))
{
    connection.Open();

    // Создаем таблицу
    using (var command = new SQLiteCommand("CREATE TABLE Accounts (Id INTEGER PRIMARY KEY, Name TEXT, Balance REAL);",connection))
    {
        command.ExecuteNonQuery();
    }

    // Начинаем транзакцию
    using (var transaction = connection.BeginTransaction())
    {
        try
        {
            // Вставляем данные
            using (var command = new SQLiteCommand(
                "INSERT INTO Accounts (Name, Balance) VALUES ('Alice', 1000);", connection, transaction))
            {
                command.ExecuteNonQuery();
            }

            using (var command = new SQLiteCommand(
                "INSERT INTO Accounts (Name, Balance) VALUES ('Bob', 500);", connection, transaction))
            {
                command.ExecuteNonQuery();
            }

            // Подтверждаем транзакцию
            transaction.Commit();
            Console.WriteLine("Транзакция успешно выполнена.");
        }
        catch (Exception ex)
        {
            // Откат при ошибке
            transaction.Rollback();
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    // Проверяем результаты
    using (var command = new SQLiteCommand("SELECT * FROM Accounts;", connection))
    using (var reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Balance: {reader["Balance"]}");
        }
    }
}