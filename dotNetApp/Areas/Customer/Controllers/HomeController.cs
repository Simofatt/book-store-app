using dotNet.DAO.Repository.IRepository;
using dotNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace dotNetApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
         
        //private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork; 

        public HomeController(IUnitOfWork unitOfWork)
        {
            //_logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            
           
            IEnumerable<Product> products = _unitOfWork.Product.GetAll("Category").ToList();
            return View(products);
        }
      public IActionResult Detail(int? id)
        {
            Product product = _unitOfWork.Product.Get(p => p.Id == id, "Category");
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