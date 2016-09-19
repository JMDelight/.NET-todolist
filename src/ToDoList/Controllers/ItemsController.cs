using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        private IItemRepository itemRepo;
        public ItemsController(IItemRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.itemRepo = new EFItemRepository();
            }
            else
            {
                this.itemRepo = thisRepo;
            } 
        }
        
        //private ToDoListContext db = new ToDoListContext();
        public IActionResult Index()
        {
            //return View(itemRepo.Items.Include(items => items.Category).ToList());
            return View(itemRepo.Items.ToList());
        }
        public IActionResult Details(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }
        public IActionResult Create()
        {
            ViewBag.Categories =itemRepo.GetAllCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            itemRepo.Save(item);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            ViewBag.Categories = itemRepo.GetAllCategories();
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            itemRepo.Edit(item);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            itemRepo.Remove(thisItem);
            return RedirectToAction("Index");
        }

        public IActionResult Check(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            thisItem.Done = !thisItem.Done;
            itemRepo.Edit(thisItem);
            return RedirectToAction("Index");
        }
        public void DeleteAll()
        {
            List<Item> allItems = itemRepo.Items.ToList();
            foreach(Item item in allItems)
            {
                itemRepo.Remove(item);
            }

        }
    }
}