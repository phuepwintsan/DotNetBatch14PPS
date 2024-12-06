using DotNetBatch14PPS.RestApi5.Transfer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace DotNetBatch14PPS.RestApi5.Transfer
{
    public class TransferService : ITransferService
    {
        public readonly SqlConnectionStringBuilder sqlConnectionStringBuilder;
        public TransferService()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = "LAPTOP-73Q5S0H6",
                InitialCatalog = "Transfer",
                UserID = "sa",
                Password = "p@ssw0rd",
                TrustServerCertificate = true
            };
        }

        public TransferResponseModel CreateTransaction(TransactionModel requestTransactionModel, int Password)
        {
            TransferResponseModel model = new TransferResponseModel();

            string toMobileNo = requestTransactionModel.ToMobileNo!;
            string fromMobileNo = requestTransactionModel.FromMobileNo!;
            decimal amount = requestTransactionModel.Amount;
            int password = Password;

            var SendUserData = GetUserData(fromMobileNo);
            if (SendUserData is null)
            {
                return new TransferResponseModel()
                {
                    IsSuccess = false,
                    Message = $"{fromMobileNo} doesn't create the bank account"
                };
            }
            if (SendUserData.Password != password)
            {
                return new TransferResponseModel()
                {
                    IsSuccess = false,
                    Message = "Invalid password"
                };
            }
            if (amount > SendUserData.Balance)
            {
                return new TransferResponseModel()
                {
                    IsSuccess = false,
                    Message = "The balance is insufficient"
                };
            }
            var ReceiveUserData = GetUserData(toMobileNo);
            if (ReceiveUserData is null)
            {
                return new TransferResponseModel()
                {
                    IsSuccess = false,
                    Message = $"{toMobileNo} doesn't create the bank account"
                };
            }

            string query = @"
BEGIN TRY
    BEGIN TRANSACTION;

    -- Insert into tbl_transaction
    INSERT INTO [dbo].[tbl_transaction]
    ([FromMobileNo], [ToMobileNo], [Amount], [TransactionDate], [Notes])
    VALUES
    (@FromMobileNo, @ToMobileNo, @Amount, @TransactionDate, @Notes);

    -- Update the sender's balance
    UPDATE tbl_user
    SET Balance = Balance - @Amount
    WHERE MobileNo = @FromMobileNo;

    -- Update the receiver's balance
    UPDATE tbl_user
    SET Balance = Balance + @Amount
    WHERE MobileNo = @ToMobileNo;

    -- Commit the transaction
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    -- Rollback if any error occurs
    ROLLBACK TRANSACTION;
    THROW;
END CATCH;
";

            SqlConnection con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FromMobileNo", fromMobileNo);
            cmd.Parameters.AddWithValue("@ToMobileNo", toMobileNo);
            cmd.Parameters.AddWithValue("@Amount", amount);
            cmd.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@Notes", requestTransactionModel.Notes);

            int result = cmd.ExecuteNonQuery();

            con.Close();

            string message = result > 0 ? "Create success" : "Create fail";
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }

        public TransferResponseModel CreateUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public List<TransactionModel> GetTransaction(string MobileNo)
        {
            string query = "select * from tbl_transaction where FromMobileNo = @mobileNo OR ToMobileNo = @mobileNo";

            SqlConnection con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@mobileNo", MobileNo);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            con.Close();

            List<TransactionModel> lst = dt.AsEnumerable().Select(row => new TransactionModel()
            {
                TransactionId = Convert.ToInt32(row["TransactionId"].ToString()),
                FromMobileNo = row["FromMobileNo"].ToString(),
                ToMobileNo = row["ToMobileNo"].ToString(),
                Amount = Convert.ToDecimal(row["Amount"].ToString()),
                TransactionDate = Convert.ToDateTime(row["TransactionDate"].ToString()),
                Notes = row["Notes"].ToString()
            }).ToList();
            return lst;
        }

        public UserModel GetUserData(string MobileNo)
        {
            SqlConnection con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from tbl_user where MobileNo = @mobileNo", con);
            cmd.Parameters.AddWithValue("@mobileNo", MobileNo);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            con.Close();

            if (dt.Rows.Count == 0) return null;

            UserModel model = new UserModel();
            DataRow row = dt.Rows[0];

            model.UserId = Convert.ToInt32(row["UserId"].ToString());
            model.UserName = row["UserName"].ToString();
            model.MobileNo = row["MobileNo"].ToString();
            model.Balance = Convert.ToDecimal(row["Balance"].ToString());
            model.Password = Convert.ToInt32(row["Password"].ToString());
            return model;
        }

        public TransferResponseModel PatchBalance(string MobileNo, decimal increasedAmount)
        {
            throw new NotImplementedException();
        }
    }
}