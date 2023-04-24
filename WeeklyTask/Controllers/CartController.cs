using Microsoft.AspNetCore.Mvc;
using WeeklyTask.Infrastructure;
using WeeklyTask.Models.ViewModels;
using WeeklyTask.Models;
using WeeklyTask.Models.Helpers;
using Newtonsoft.Json;

namespace WeeklyTask.Controllers
{
    public class CartController : Controller
    {
        private readonly FoodDbContext _context;
       

        public CartController(FoodDbContext context)
        {
            _context = context;
           
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };

            return View(cartVM);
        }

        /*public IActionResult AddToCart(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Add code to add item to cart here...

            HttpContext.Session.SetJson("Cart", cart);

            return RedirectToAction("Index");
        }*/

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // Your logic to add the food item to the cart
            var foodItem = _context.Foods.FirstOrDefault(f => f.Id == id);
            if (foodItem != null)
            {
                var cartItem = cart.FirstOrDefault(c => c.ProductId == foodItem.Id);
                if (cartItem == null)
                {
                    cart.Add(new CartItem(foodItem));
                }
                else
                {
                    cartItem.Quantity++;
                }
            }

            HttpContext.Session.SetJson("Cart", cart);

            // Return the updated SmallCartViewModel object as JSON
            var smallCartVM = new SmallCartViewModel
            {
                NumberOfItems = cart.Sum(item => item.Quantity),
                TotalAmount = cart.Sum(item => item.Total)
            };
             

            System.Diagnostics.Debug.WriteLine($"smallCartVM: {JsonConvert.SerializeObject(smallCartVM)}");

            return Json(smallCartVM);
        }

        [HttpGet]
        public IActionResult GetCartItemCount()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            int itemCount = cart.Sum(item => item.Quantity);
            return Json(itemCount);
        }



        public async Task<IActionResult> Add(int id)
        {
             Food food = await _context.Foods.FindAsync(id);

             List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

             CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

             if (cartItem == null)
             {
                 cart.Add(new CartItem(food));
             }
             else
             {
                 cartItem.Quantity += 1;
             }

             HttpContext.Session.SetJson("Cart", cart);

             TempData["Success"] = "The product has been added!";

             return Redirect(Request.Headers["Referer"].ToString());
        }





        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }

       
       

    }
}
