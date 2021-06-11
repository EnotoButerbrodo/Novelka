using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelkaLib.Models
{
    public class Storage<T>
    {
        Dictionary<string, T> items = new();

        public bool ContainsItem(string itemName) => items.ContainsKey(itemName);
        public bool Add(string itemName, T item)
        {
            if (items.ContainsValue(item) || items.ContainsKey(itemName))
                return false;
            items.Add(itemName, item);
            return true;
        }
        public bool Delete(string itemName)
        {
            if (!items.ContainsKey(itemName))
                return false;
            items.Remove(itemName);
            return true;
        }
        public T GetItem(string itemName)
        {
            return items[itemName];
        }
    }
}
