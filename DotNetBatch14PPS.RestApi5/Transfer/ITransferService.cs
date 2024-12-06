
using DotNetBatch14PPS.RestApi5.Transfer;

namespace DotNetBatch14PPS.RestApi5.Transfer
{
    public interface ITransferService
    {
        TransferResponseModel CreateTransaction(TransactionModel requestTransactionModel, int Password);
        UserModel GetUserData(string MobileNo);
        TransferResponseModel CreateUser(UserModel user);
        List<TransactionModel> GetTransaction(string MobileNo);
        TransferResponseModel PatchBalance(string MobileNo, decimal increasedAmount);

    }
}
