using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBatch14PPS.RestApi5.MiniPOS
{
    [Table("tbl_sale")]
    public class SaleModel
    {
        [Key]
        public int? SaleId { get; set; }
        public int ProductId { get; set; }
        public int SaleQuantity { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? SaleDate { get; set; }
    }

    [Table("tbl_product")]
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; }
    }
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
