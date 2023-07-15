using Rent_Somethings.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Rent_Somethings.Models
{
    public class District:IEntityBase
    {
        public int Id { get; set; }
        [Display(Name = "District")]
        [Required(ErrorMessage = "District is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "District must be between 2 and 50 chars")]
        public string DistrictName { get; set; }
        [Display(Name = "Features")]
        public string? Features { get; set; }

        public List<District_Product>? Districts_Products { get; set; }
    }
}
