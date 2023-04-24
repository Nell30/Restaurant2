using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Restaurant.Services;
using Restaurant.Models;
using System.Security.Claims;
using WeeklyTask.Areas.Identity.Data;
using WeeklyTask.Models;

namespace YourProjectNamespace.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(OrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            var orders = await _orderService.GetAllOrdersAsync();

            return View(orders);
        }

        /*public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return View(orders);
        }*/
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var allOrders = await _orderService.GetAllOrdersAsync();
            var orders = allOrders.Where(o => o.UserId == userId).ToList();

            return View(orders);
        }





        // Add other actions for order management as needed
    }
}
