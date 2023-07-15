using System.ComponentModel.DataAnnotations;
using Rent_Somethings.Data.Base;

namespace Rent_Somethings.Models
{
    public class City:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        public string CityName { get; set; }
        
        
        // Şehir oluştururken ürün eklemediğimiz için nullable olmalı.
        public List<Product>? Products { get; set; }
    }
}
