using dotNet.DAO.Data;
using dotNet.Models;

using Microsoft.AspNetCore.Mvc;
using dotNet.DAO.Repository.IRepository;
using dotNet.DAO.Repository;
using dotNet.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace dotNetApp.Areas.Admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        
        // private readonly ApplicationDbContext _db;
        //private readonly ICategoryRepository _categoryRepo;    // we are asking dependency injection to give us the object of ICategoryRepository
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objList = _unitOfWork.Category.GetAll().ToList();
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("description", "Description should be different from name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
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
                Category cat = _unitOfWork.Category.Get(u => u.category_id == id);

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

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category modified successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();

        }

        public IActionResult Delete(int id)
        {
            Category cat = _unitOfWork.Category.Get(u => u.category_id == id);
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Category.Remove(cat);
                _unitOfWork.Save();
                TempData["success"] = "Category delete successfully";
                return RedirectToAction("Index", "Category");
            }
        }

     

    }


}



