using Rent_Somethings.Data.Base;
using Rent_Somethings.Models;

namespace Rent_Somethings.Data.Services
{
    public class DistrictsService:EntityBaseRepository<District>, IDistrictsService
    {
        public DistrictsService(AppDbContext context):base(context) { }
    }
}
