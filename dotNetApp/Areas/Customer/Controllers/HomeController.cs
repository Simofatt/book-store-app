using dotNet.DAO.Repository.IRepository;
using dotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace dotNetApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
         
       
        private readonly IUnitOfWork _unitOfWork; 

        public HomeController(IUnitOfWork unitOfWork)
        {
      
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _unitOfWork.Product.GetAllAsync("Category");
            return View(products);
        }
        [Authorize]
        public async Task<IActionResult> Detail(int? id)
        {
            Product? product = await _unitOfWork.Product.GetAsync(p => p.Id == id, "Category");
            if (product is null)
                return NotFound();
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}