using TransactionExample;

// BulkDelete

var database = new InMemoryDatabase<Account>();

var accounts = new List<Account>
        {
            new Account { Id = 1, Name = "Alice", Balance = 1000 },
            new Account { Id = 2, Name = "Bob", Balance = 2000 },
            new Account { Id = 3, Name = "Charlie", Balance = 1500 }
        };

database.BulkInsert(accounts);

var accountsToDelete = new List<Account>
        {
            new Account { Id = 1, Name = "Alice", Balance = 1000 },
            new Account { Id = 3, Name = "Charlie", Balance = 1500 }
        };

database.BulkDelete(accountsToDelete, (dbItem, deleteItem) => dbItem.Id == deleteItem.Id);
database.PrintData();