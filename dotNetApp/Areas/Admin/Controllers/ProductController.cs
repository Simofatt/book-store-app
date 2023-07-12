using dotNet.DAO.Data;
using dotNet.Models;

using Microsoft.AspNetCore.Mvc;
using dotNet.DAO.Repository.IRepository;
using dotNet.DAO.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using dotNet.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using dotNet.Utility;
using Microsoft.AspNetCore.Authorization;

namespace dotNetApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles=SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork; 
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? Id)
        {

            var categories = await _unitOfWork.Category.GetAllAsync();
            ProductVM product = new()
            {

                //TRANSFORMING THE LIST OF CATEGORY INTO SELECT LIST ITEM EACH ITEM WE CREATE AN INSTANCE AND IT HAS A TEXT AND VALUE
                CategoryList = categories.Select(c => new SelectListItem
                {
                    Text = c.name,
                    Value = c.category_id.ToString()
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
                var ProductAsync = await _unitOfWork.Product.GetAsync(u => u.Id == Id);
                    if (ProductAsync is null)
                        return NotFound();

              
                    product.Product= ProductAsync;  
                    return View(product);
                
                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ProductVM obj,IFormFile? file)
        {
         
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                       // string path = Path.Combine(_webHostEnvironment.WebRootPath, @"/images/products");
                        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                  
                   
                        //DELETE THE OLD IMAGE
                        if(!string.IsNullOrEmpty(obj.Product.ImgUrl)) {
                            var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, obj.Product.ImgUrl);
                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }
                        }
                        using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        obj.Product.ImgUrl = "/images/products/" + fileName;
                }
                if(obj.Product.Id!=0)
                {
                   await _unitOfWork.Product.Update(obj.Product);
                   await _unitOfWork.Commit();
                    TempData["success"] = "Product updated successfully";
                }
                else
                {
                   await _unitOfWork.Product.AddAsync(obj.Product);
                   await _unitOfWork.Commit();
                    TempData["success"] = "Product created successfully";
                }
                    
               
               
                return RedirectToAction("Index", "Product");

                }
            
          
            return View(obj);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                var product = await _unitOfWork.Product.GetAsync(u => u.Id == id);

                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                await _unitOfWork.Product.Update(obj);
                await _unitOfWork.Commit();

                TempData["success"] = "Product modified successfully";
                return RedirectToAction("Index", "Product");
            }

            return View();

        }
     

        #region api Calls
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            IEnumerable<Product?> objList =await _unitOfWork.Product.GetAllAsync("Category"); 

            return Json (new  { data = objList}) ;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            Product? product = await _unitOfWork.Product.GetAsync(u => u.Id == id);
            if (product == null)
            {
                return Json(new { success=false , message = "Product not found" });
            }
            else
            {
                await _unitOfWork.Product.RemoveAsync(product);
                await _unitOfWork.Commit();


                return Json(new { success = true, message = "Product delete successfully" });
            }



        }


    }


}



#endregion API calls