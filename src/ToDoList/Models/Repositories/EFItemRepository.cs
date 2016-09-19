using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class EFItemRepository : IItemRepository
    {
        ToDoListContext db = new ToDoListContext();
        public EFItemRepository(ToDoListContext connection = null)
        {
            if (connection == null)
            {
                this.db = new ToDoListContext();
            }
            else
            {
                this.db = connection;
            }
        }

        public IQueryable<Item> Items
        { get { return db.Items; } }

        public Item Save(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
            return item;
        }

        public Item Edit(Item item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return item;
        }

        public void Remove(Item item)
        {
            db.Items.Remove(item);
            db.SaveChanges();
        }

        public List<Category> GetAllCategories()
        {
            return(db.Categories.ToList());
        }
    }
}
