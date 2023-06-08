using dotNet.DAO.Data;
using dotNet.Models;

using Microsoft.AspNetCore.Mvc;
using dotNet.DAO.Repository.IRepository;
using dotNet.DAO.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using dotNet.Models.ViewModels;
using Microsoft.AspNetCore.Http;


namespace dotNetApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        
        // private readonly ApplicationDbContext _db;
        //private readonly IProductRepository _categoryRepo;    // we are asking dependency injection to give us the object of IProductRepository
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {

            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objList = _unitOfWork.Product.GetAll().ToList();

          
             
            return View(objList);
        }

        public IActionResult Upsert(int? Id)
        {
            

            //C# 9.0  syntax  for creating object 
            //Initialize the object and its properties in one line of code
            ProductVM product = new()
            {

                //TRANSFORMING THE LIST OF CATEGORY INTO SELECT LIST ITEM EACH ITEM WE CREATE AN INSTANCE AND IT HAS A TEXT AND VALUE
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.name,
                    Value = u.category_id.ToString()
                }),
                Product = new Product()

            };
            if(Id==null)
            {
                //CREATE 
                return View(product);
            }
             else
            {
                //UPDATE 
                product.Product = _unitOfWork.Product.Get(u => u.Id == Id);
                return View(product);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)
        {
          
            if (ModelState.IsValid)
            {
                
                
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                   // string path = Path.Combine(_webHostEnvironment.WebRootPath, @"/images/products");
                    string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.Product.ImgUrl = "/images/products/" + fileName;
                }
                    
                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index", "Product");
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
                Product product = _unitOfWork.Product.Get(u => u.Id == id);

                //USED IF THE ID IS NOT PRIMARY KEY
                //Product? cat2 = _db.Categories.FirstOrDefault(u => u.category_id == id);
                //Product? cat3 = _db.Categories.Where(u => u.category_id == id).FirstOrDefault();


                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
        }


        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product modified successfully";
                return RedirectToAction("Index", "Product");
            }

            return View();

        }

        public IActionResult Delete(int id)
        {
            Product product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Product.Remove(product);
                _unitOfWork.Save();
                TempData["success"] = "Product delete successfully";
                return RedirectToAction("Index", "Product");
            }



        }
    }


}



