using System;
using System.Data.SQLite;

//Откат транзакции при ошибке

// Создание SQLite базы данных в памяти
string connectionString = "Data Source=:memory:;Version=3;New=True;";

using (SQLiteConnection connection = new SQLiteConnection(connectionString))
{
    connection.Open();

    // Создаем таблицу
    using (var command = new SQLiteCommand(
        "CREATE TABLE Accounts (Id INTEGER PRIMARY KEY, Name TEXT, Balance REAL);",
        connection))
    {
        command.ExecuteNonQuery();
    }

    // Начинаем транзакцию
    SQLiteTransaction transaction = connection.BeginTransaction();

    try
    {
        // Первая команда
        SQLiteCommand command1 = new SQLiteCommand(
            "INSERT INTO Accounts (Name, Balance) VALUES ('Alice', 1000);",
            connection, transaction);
        command1.ExecuteNonQuery();

        // Вторая команда с ошибкой: некорректное имя столбца "WrongColumn"
        SQLiteCommand command2 = new SQLiteCommand(
            "INSERT INTO Accounts (Name, WrongColumn) VALUES ('Bob', 500);",
            connection, transaction);
        command2.ExecuteNonQuery();

        // Подтверждение транзакции
        transaction.Commit();
        Console.WriteLine("Транзакция завершена успешно.");
    }
    catch (Exception ex)
    {
        // Откат транзакции
        transaction.Rollback();
        Console.WriteLine($"Ошибка: {ex.Message}");
    }

    // Проверяем содержимое таблицы
    using (var command = new SQLiteCommand("SELECT * FROM Accounts;", connection))
    using (var reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["Id"]}, Name: {reader["Name"]}, Balance: {reader["Balance"]}");
        }
    }
}