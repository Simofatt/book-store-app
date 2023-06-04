using dotNetApp.Data;
using dotNetApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotNetApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objList = _db.Categories.ToList();
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.name == obj.description)
            {
                ModelState.AddModelError("description", "Description should be different from name");
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index" , "Category");
            }   
          
             return View();
            
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                Category cat = _db.Categories.Find(id);
                //USED IF THE ID IS NOT PRIMARY KEY
                //Category? cat2 = _db.Categories.FirstOrDefault(u => u.category_id == id);
                //Category? cat3 = _db.Categories.Where(u => u.category_id == id).FirstOrDefault();


                if (cat == null)
                {
                    return NotFound();
                }

                return View(cat);
            }
        }


        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if(ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category modified successfully";
                return RedirectToAction("Index" , "Category");
            }

            return View();

        }

        public IActionResult Delete(int id)
        {
            Category cat = _db.Categories.Find(id);
            if (cat == null)
            {
                return NotFound();
            }else {  
                _db.Categories.Remove(cat);
                _db.SaveChanges();
                TempData["success"] = "Category delete successfully";
                return RedirectToAction("Index", "Category");
            }   

            

        }
    }


}



