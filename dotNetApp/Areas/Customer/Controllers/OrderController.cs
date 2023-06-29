using dotNet.DAO.Repository.IRepository;
using dotNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace dotNetApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
         
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IUnitOfWork unitOfWork, SignInManager<IdentityUser> SignInManager, UserManager<IdentityUser> UserManager)
        {
            
            _unitOfWork = unitOfWork;
            _signInManager = SignInManager;
            _userManager = UserManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(Order entity)
        {
            if (entity != null)
            {
                _unitOfWork.Order.Add(entity);
                _unitOfWork.Save();
            }
            else
            {
                return Json(new { message = "null" });
            }
            return RedirectToAction("Order", "Order");


        }
        [HttpGet]
        [Authorize]
        public IActionResult Order()
        {
            
            var userId = _userManager.GetUserId(User);
            Console.Write($"USER ID {userId}");
           
            IQueryable<Order> orders = _unitOfWork.Order.GetOrders(o => o.UserId == userId, "Product");
            return View(orders);
        }
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Order entity = _unitOfWork.Order.Get(o => o.Id == id);
                if (entity != null)
                {
                    _unitOfWork.Order.Remove(entity);
                    _unitOfWork.Save();
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("Order", "Order");
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