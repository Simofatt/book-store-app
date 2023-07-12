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
        public async Task<IActionResult> AddAsync(Order entity)
        {
            if (entity != null)
            {
               await _unitOfWork.Order.AddAsync(entity);
               await _unitOfWork.Commit();

            }
            else
            {
                return Json(new { message = "null" });
            }
            return RedirectToAction("Order", "Order");


        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Order()
        {
            
            var userId = _userManager.GetUserId(User);
            Console.Write($"USER ID {userId}");
           
            IEnumerable<Order> orders = await _unitOfWork.Order.GetOrdersAsync(o => o.UserId == userId, "Product");
            if (orders is null || orders.Count() == 0)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            return View(orders);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Order? entity =await _unitOfWork.Order.GetAsync(o => o.Id == id);
                if (entity != null)
                {
                    await _unitOfWork.Order.RemoveAsync(entity);
                    await _unitOfWork.Commit();
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