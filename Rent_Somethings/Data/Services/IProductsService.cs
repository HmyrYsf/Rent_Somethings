using Rent_Somethings.Data.Base;
using Rent_Somethings.Data.ViewModels;
using Rent_Somethings.Models;

namespace Rent_Somethings.Data.Services
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<NewProductDropdownsVM> GetNewProductDropdownsValues();
        Task AddNewProductAsync(NewProductVM data);
        Task UpdateProductAsync(NewProductVM data);
       


    }
}
