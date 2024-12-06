
using TransactionExample;

// BulkUpdate

var database = new InMemoryDatabase<Account>();

var initialAccounts = new List<Account>
        {
            new Account { Id = 1, Name = "Alice", Balance = 1000 },
            new Account { Id = 2, Name = "Bob", Balance = 2000 }
        };

database.BulkInsert(initialAccounts);

var updatedAccounts = new List<Account>
        {
            new Account { Id = 1, Name = "Alice Updated", Balance = 1500 },
            new Account { Id = 2, Name = "Bob Updated", Balance = 2500 }
        };

database.BulkUpdate(updatedAccounts, (oldItem, newItem) => oldItem.Id == newItem.Id);
database.PrintData();