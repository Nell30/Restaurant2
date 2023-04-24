using Microsoft.EntityFrameworkCore;
using WeeklyTask.Areas.Identity.Data;
using WeeklyTask.Models;

namespace Restaurant.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(ApplicationDbContext context, ILogger<OrderService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all orders for a specific user
        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            _logger.LogInformation("Fetching orders for user ID: {userId}", userId);
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
            _logger.LogInformation("Fetched {count} orders for user ID: {userId}", orders.Count, userId);
            return orders;
        }
       

        // Get all orders for admin
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            _logger.LogInformation("Fetching all orders");
            var orders = await _context.Orders.ToListAsync();
            _logger.LogInformation("Fetched {count} orders", orders.Count);
            return orders;
        }

        // Create a new order
        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        // Update an existing order
        public async Task UpdateOrderAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Get a single order by Id
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        // Delete an order
        public async Task DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        

    }

}
