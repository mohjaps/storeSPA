#nullable disable
namespace storeSPA.Data
{
    public record Perfume
    {
        public String Id { get; set; }
        [Required] // 30
        public String Name { get; set; }
        [Required] // 255
        public String Description { get; set; }
        public String Image { get; set; } // not null
        public DateTime Add_Date { get; set; }
        [Required] // > 0
        public int Quantity { get; set; }
        [Required] // > 0
        public double Price { get; set; }
        public String Saler_Id { get; set; }
    }
}
