using Rent_Somethings.Data.Base;
using Rent_Somethings.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent_Somethings.Data.ViewModels
{
    public class NewProductVM
    {
        public int Id { get; set; }
        [Display(Name = "Product name")]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Display(Name = "Product description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Display(Name = "Price in TL")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [Display(Name = "Product image URL")]
        [Required(ErrorMessage = "Product image URL is required")]
        public string ImageURL { get; set; }
        [Display(Name = "Rental item start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Rental item end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Product category is required")]
        public ProductCategory ProductCategory { get; set; }
        [Display(Name = "Select district(s)")]
        [Required(ErrorMessage = "Product district(s) is required")]
        //Relationships
        public List<int> DistrictIds { get; set; }
        [Display(Name = "Select a City")]
        [Required(ErrorMessage = "Product city is required")]
        public int CityId { get; set; }
    }   
}

