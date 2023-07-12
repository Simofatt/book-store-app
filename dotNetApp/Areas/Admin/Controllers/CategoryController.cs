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
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> objList = await _unitOfWork.Category.GetAllAsync();
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category obj)
        {
            if (obj.name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("description", "Description should be different from name");
            }
            if (ModelState.IsValid)
            {
                await _unitOfWork.Category.AddAsync(obj);
                await _unitOfWork.Commit();
                
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                Category? cat = await _unitOfWork.Category.GetAsync(c => c.category_id == id);

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
        public async Task <IActionResult> Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                await _unitOfWork.Category.Update(obj);
                await _unitOfWork.Commit();

                TempData["success"] = "Category modified successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();

        }

        public async Task<IActionResult> Delete(int id)
        {
            Category? cat = await _unitOfWork.Category.GetAsync(u => u.category_id == id);
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                await _unitOfWork.Category.RemoveAsync(cat);
                await _unitOfWork.Commit();

                TempData["success"] = "Category delete successfully";
                return RedirectToAction("Index", "Category");
            }
        }

     

    }


}



