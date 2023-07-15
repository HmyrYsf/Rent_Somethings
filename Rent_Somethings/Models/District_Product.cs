namespace Rent_Somethings.Models
{
    public class District_Product
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
    }
}
