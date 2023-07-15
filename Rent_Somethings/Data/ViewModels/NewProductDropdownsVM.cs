using Rent_Somethings.Models;

namespace Rent_Somethings.Data.ViewModels
{
    public class NewProductDropdownsVM
    {
        public NewProductDropdownsVM()
        {
            
            Cities = new List<City>();
            Districts = new List<District>();
        }
        
        public List<City> Cities { get; set; }
        public List<District> Districts { get; set; }
    }
}
