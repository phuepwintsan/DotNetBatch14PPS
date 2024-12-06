using DotNetBatch14PPS.RestApi5.MiniPOS;
using DotNetBatch14PPS.RestApi5.Transfer;
using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PPS.RestApi5.MiniPOS
{
    public class POSEFCoreService
    {
        public AppDbContext db = new AppDbContext();
        public TransferResponseModel CreateProduct(ProductModel requestProduct)
        {
            db.Products.Add(requestProduct);
            int result = db.SaveChanges();

            string message = result > 0 ? "Create success" : "Create fail";
            TransferResponseModel model = new TransferResponseModel();
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }
        public ResponseModel CreateSale(SaleModel requestSale, string productName)
        {
            ResponseModel model = new ResponseModel();

            int productId = requestSale.ProductId;
            int saleQuantity = requestSale.SaleQuantity;
            ProductModel productData = GetProduct(productId, productName);
            if (productData is null)
            {
                model.IsSuccess = false;
                model.Message = "This product doesn't have in the market.";
                return model;
            }
            if (saleQuantity > productData.StockQuantity)
            {
                model.IsSuccess = false;
                model.Message = productData.StockQuantity + " are available to sale now.";
                return model;
            }
            decimal totalAmount = requestSale.SaleQuantity * productData.Price;
            requestSale.TotalAmount = totalAmount;
            requestSale.SaleDate = DateTime.Now;

            productData.StockQuantity -= saleQuantity;
            db.Products.Entry(productData).State = EntityState.Modified;

            db.Sales.Add(requestSale);
            int result = db.SaveChanges();

            string message = result > 0 ? "Create success" : "Create fail";
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }
        public ProductModel GetProduct(int productId, string productName)
        {
            ProductModel model = new ProductModel();
            model = db.Products.FirstOrDefault(x => x.ProductId == productId && x.ProductName == productName);
            return model!;
        }
        public List<SaleModel> GetSale(int productId)
        {
            var lst = db.Sales.AsNoTracking().Where(x => x.ProductId == productId).ToList();
            return lst;
        }
    }
}
