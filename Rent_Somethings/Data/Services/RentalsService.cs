using Microsoft.EntityFrameworkCore;
using Rent_Somethings.Models;

namespace Rent_Somethings.Data.Services
{
    public class RentalsService : IRentalsService
    {
        private readonly AppDbContext _context;
        public RentalsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Rental>> GetRentalsByUserIdAndRoleAsync(string userId, string userRole)
        {
            var rentals = await _context.Rentals.Include(n => n.RentalItems).ThenInclude(n => n.Product).Include(n => n.User).ToListAsync();
            if (userRole != "Admin")
            {
                rentals = rentals.Where(n => n.UserId == userId).ToList();
            }
            return rentals;
        }

        public async Task StoreRentalAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress, string? userPhoneNumber)
        {
            var rental = new Rental()
            {
                UserId = userId,
                Email = userEmailAddress,
                PhoneNumber = userPhoneNumber
            };
            await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var rentalItem = new RentalItem()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.Id,
                    RentalId = rental.Id,
                    Price = item.Product.Price,

                };
                await _context.RentalsItems.AddAsync(rentalItem);

            }
            await _context.SaveChangesAsync();
        }
    }
}
