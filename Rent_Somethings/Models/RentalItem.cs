using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent_Somethings.Models
{
    public class RentalItem
    {
        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")] 
        public virtual Product Product { get; set; }
         public int RentalId { get; set; }
        [ForeignKey("RentalId")]
        public Rental Rental { get; set; }

    }
}
