using Rent_Somethings.Data.Base;
using Rent_Somethings.Models;

namespace Rent_Somethings.Data.Services
{
    public class CitiesService:EntityBaseRepository<City>, ICitiesService
    {
        
        public CitiesService(AppDbContext context) : base(context)
        {
            
        }
    }
}
