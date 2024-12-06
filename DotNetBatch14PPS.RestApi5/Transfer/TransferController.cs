
using DotNetBatch14PPS.RestApi5.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace DotNetBatch14PPS.RestApi5.Transfer
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        public ITransferService transferService;

        public TransferController()
        {
            transferService = new TransferEFCoreService();
        }

        [HttpPost]
        public IActionResult PostUser([FromBody] UserModel requestUserModel)
        {
            var model = transferService.CreateUser(requestUserModel);

            if (!model.IsSuccess)
            {
                BadRequest(model);
            }
            return Ok(model);
        }

        [HttpPost("{Password}")]
        public IActionResult PostTransfer([FromBody] TransactionModel requestTransactionModel, int Password)
        {
            var model = transferService.CreateTransaction(requestTransactionModel, Password);

            if (!model.IsSuccess)
            {
                BadRequest(model);
            }
            return Ok(model);
        }

        [HttpGet("{MobileNo}")]
        public IActionResult Get(string MobileNo)
        {
            var model = transferService.GetTransaction(MobileNo);
            if (model is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(model);
        }

        [HttpPatch]
        public IActionResult PatchBalance(string mobileNo, decimal increasedAmount)
        {
            var model = transferService.PatchBalance(mobileNo, increasedAmount);

            if (!model.IsSuccess)
            {
                BadRequest(model);
            }
            return Ok(model);
        }

    }
}
