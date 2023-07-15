using Rent_Somethings.Models;

namespace Rent_Somethings.Data.Services
{
    public interface IRentalsService
    {
        Task StoreRentalAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress, string? userPhoneNumber);
        Task<List<Rental>> GetRentalsByUserIdAndRoleAsync(string userId, string userRole);
    }
}
