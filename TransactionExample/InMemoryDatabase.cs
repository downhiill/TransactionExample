using System;
using System.Collections.Generic;
using System.Linq;

public class InMemoryDatabase<T> where T : class
{
    private List<T> _data = new List<T>();

    public void BulkInsert(IEnumerable<T> items)
    {
        _data.AddRange(items);
        Console.WriteLine($"{items.Count()} записей добавлено.");
    }

    public void BulkUpdate(IEnumerable<T> updatedItems, Func<T, T, bool> matchPredicate)
    {
        foreach (var updatedItem in updatedItems)
        {
            var existingItem = _data.FirstOrDefault(item => matchPredicate(item, updatedItem));
            if (existingItem != null)
            {
                _data.Remove(existingItem);
                _data.Add(updatedItem);
            }
        }
        Console.WriteLine($"{updatedItems.Count()} записей обновлено.");
    }

    public void BulkDelete(IEnumerable<T> itemsToDelete, Func<T, T, bool> matchPredicate)
    {
        foreach (var item in itemsToDelete)
        {
            var existingItem = _data.FirstOrDefault(dbItem => matchPredicate(dbItem, item));
            if (existingItem != null)
            {
                _data.Remove(existingItem);
            }
        }
        Console.WriteLine($"{itemsToDelete.Count()} записей удалено.");
    }

    public void PrintData()
    {
        Console.WriteLine("Текущие данные:");
        foreach (var item in _data)
        {
            Console.WriteLine(item);
        }
    }
}
