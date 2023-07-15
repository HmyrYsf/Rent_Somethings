using Rent_Somethings.Data.Base;
using Rent_Somethings.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Rent_Somethings.Models
{
    public class Product : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public List<District_Product> Districts_Products {get; set;}
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }


    }
}
