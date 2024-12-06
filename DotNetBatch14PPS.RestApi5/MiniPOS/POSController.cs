
using DotNetBatch14PPS.RestApi5.MiniPOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14PPS.RestApi5.MiniPOS
{
    [Route("api/[controller]")]
    [ApiController]
    public class POSController : ControllerBase
    {
        public readonly POSEFCoreService _service;
        public POSController()
        {
            _service = new POSEFCoreService();
        }
        [HttpPost]
        public IActionResult Post([FromBody] ProductModel requestProduct)
        {
            var model = _service.CreateProduct(requestProduct);

            if (!model.IsSuccess)
            {
                BadRequest(model);
            }
            return Ok(model);
        }

        [HttpPost("{productName}")]
        public IActionResult PostTransfer([FromBody] SaleModel requestSale, string productName)
        {
            var model = _service.CreateSale(requestSale, productName);

            if (!model.IsSuccess)
            {
                BadRequest(model);
            }
            return Ok(model);
        }

        [HttpGet("{productId}")]
        public IActionResult GetSale(int productId)
        {
            var model = _service.GetSale(productId);

            if (model is null)
            {
                return NotFound("No Data Found");
            }
            return Ok(model);
        }
    }
}
