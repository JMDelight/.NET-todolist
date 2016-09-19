using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoList.Controllers
{
    public class CategoriesController : Controller
    {
        private ToDoListContext db = new ToDoListContext();
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }
        public IActionResult Details(int id)
        {
            Category selectedCategory = db.Categories.FirstOrDefault(c => c.CategoryId == id);
            List<Item> items = db.Items.Where(i => i.CategoryId == id).ToList();
            ViewBag.Category = selectedCategory;
            return View(items);
        }
        public IActionResult Check(int id)
        {
            var thisItem = db.Items.FirstOrDefault(items => items.ItemId == id);
            thisItem.Done = !thisItem.Done;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = thisItem.CategoryId } ) ;
        }
    }
}
