
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBatch14PPS.RestApi5.Transfer
{
    [Table("tbl_user")]
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? MobileNo { get; set; }
        public decimal? Balance { get; set; }
        public int? Password { get; set; }
    }

    [Table("tbl_transaction")]
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }
        public string? FromMobileNo { get; set; }
        public string? ToMobileNo { get; set; }
        public decimal Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Notes { get; set; }
    }

    public class TransferResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
