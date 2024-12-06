
using DotNetBatch14PPS.RestApi5.Transfer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DotNetBatch14PPS.RestApi5.Transfer
{
    public class TransferEFCoreService : ITransferService
    {
        public AppDbContext db = new AppDbContext();

        public TransferResponseModel CreateUser(UserModel requestUserModel)
        {
            db.Users.Add(requestUserModel);
            int result = db.SaveChanges();

            string message = result > 0 ? "Create success" : "Create fail";
            TransferResponseModel model = new TransferResponseModel();
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }

        public TransferResponseModel CreateTransaction(TransactionModel requestTransactionModel, int Password)
        {
            TransferResponseModel model = new TransferResponseModel();
            requestTransactionModel.TransactionDate = DateTime.Now;

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
            SendUserData.Balance -= amount;
            ReceiveUserData.Balance += amount;

            db.Users.Entry(SendUserData).State = EntityState.Modified;
            db.Users.Entry(ReceiveUserData).State = EntityState.Modified;

            db.Transactions.Add(requestTransactionModel);
            int result = db.SaveChanges();

            string message = result > 0 ? "Create success" : "Create fail";
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }

        public UserModel GetUserData(string MobileNo)
        {
            string mobileNo = MobileNo.Trim();
            var UserData = db.Users.AsNoTracking().FirstOrDefault(x => x.MobileNo == mobileNo);
            return UserData!;
        }

        public List<TransactionModel> GetTransaction(string MobileNo)
        {
            //var item  = GetUserData(MobileNo);
            //if (item is null)
            //{
            //    return null;
            //}
            //if (item.Password != Password)
            //{
            //    return null;
            //}
            var lst = db.Transactions.AsNoTracking().Where(x => x.ToMobileNo == MobileNo || x.FromMobileNo == MobileNo).ToList();
            return lst;
        }

        public TransferResponseModel PatchBalance(string MobileNo, decimal increasedAmount)
        {
            UserModel userData = GetUserData(MobileNo);
            TransferResponseModel model = new TransferResponseModel();

            userData.Balance += increasedAmount;

            db.Users.Entry(userData).State = EntityState.Modified;
            int result = db.SaveChanges();

            string message = result > 0 ? "Update success" : "Update fail";
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }
    }
}
