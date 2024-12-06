
using TransactionExample;

// BulkInsert

var database = new InMemoryDatabase<Account>();

var accounts = new List<Account>
        {
            new Account { Id = 1, Name = "Alice", Balance = 1000 },
            new Account { Id = 2, Name = "Bob", Balance = 2000 }
        };

database.BulkInsert(accounts);
database.PrintData();