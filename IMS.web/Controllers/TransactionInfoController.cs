

using IMS.infrastructure.IRepository;
using IMS.models.Entity;
using IMS.models.ViewModels;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.web.Controllers
{
    public class TransactionInfoController : Controller
    {

        private readonly ICrudService<ProductInfo> _productInfo;
        private readonly ICrudService<CategoryInfo> _categoryInfo;
        private readonly ICrudService<UnitInfo> _unitInfo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICrudService<ProductRateInfo> _productRateInfo;
        private readonly ICrudService<RackInfo> _rackInfo;
        private readonly ICrudService<StockInfo> _stockInfo;
        private readonly ICrudService<TransationInfo> _transactionInfo;
        private readonly ICrudService<SupplierInfo> _supplierInfo;
        private readonly ICrudService<CustomerInfo> _customerInfo;
        private readonly ICrudService<ProductInvoiceInfo> _productInvoiceInfo;
        private readonly ICrudService<ProductInvoiceDetailInfo> _productInvoiceDetailInfo;
        private readonly ICrudService<StoreInfo> _storeInfo;
        private readonly IRawSqlRepository _rawSqlRepository;

        public TransactionInfoController(ICrudService<ProductInfo> productInfo,
            ICrudService<CategoryInfo> categoryInfo,
            ICrudService<UnitInfo> unitInfo,
            UserManager<ApplicationUser> userManager,
            ICrudService<ProductRateInfo> productRateInfo,
            ICrudService<RackInfo> rackInfo,
            ICrudService<StockInfo> stockInfo,
            ICrudService<TransationInfo> transactionInfo,
            ICrudService<SupplierInfo> supplierInfo,
            ICrudService<CustomerInfo> customerInfo,
            ICrudService<ProductInvoiceInfo> productInvoiceInfo,
            ICrudService<ProductInvoiceDetailInfo> productInvoiceDetailInfo,
             ICrudService<StoreInfo> storeInfo,
            IRawSqlRepository rawSqlRepository
            )
        {
            _productInfo = productInfo;
            _categoryInfo = categoryInfo;
            _unitInfo = unitInfo;
            _userManager = userManager;
            _productRateInfo = productRateInfo;
            _rackInfo = rackInfo;
            _stockInfo = stockInfo;
            _transactionInfo = transactionInfo;
            _supplierInfo = supplierInfo;
            _customerInfo = customerInfo;
            _productInvoiceInfo = productInvoiceInfo;
            _productInvoiceDetailInfo = productInvoiceDetailInfo;
            _storeInfo = storeInfo;
            _rawSqlRepository = rawSqlRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var store = await _storeInfo.GetAsync(user.StoreId);

            var transactioninfo = await _productInvoiceInfo.GetAllAsync(p => p.StoreinfoId == user.StoreId);
            ViewBag.CustomerInfo = await _customerInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);

            return View(transactioninfo);
        }

        [HttpPost]
        [Route("/api/Transaction/getproduct")]
        public async Task<IActionResult> GetProduct(int CategoryId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var productList = await _productInfo.GetAllAsync(p => p.CategoryInfoId == CategoryId && p.StoreInfoId == user.StoreId);

            return Json(new { productList });
        }

        public async Task<IActionResult> Transaction()
        {
            TransationViewModel transactionViewModel = new TransationViewModel();
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.CategoryInfos = await _categoryInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.ProductInfos = await _productInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.UnitInfos = await _unitInfo.GetAllAsync(p => p.IsActive == true);

            ViewBag.RackInfos = await _rackInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.SupplierInfos = await _supplierInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.CustomerInfos = await _customerInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);
            return View(transactionViewModel);
        }


        [HttpPost]
        [Route("/api/Transaction/getUnit")]
        public async Task<IActionResult> GetUnit(int ProductId)
        {
            var product = await _productInfo.GetAsync(ProductId);

            return Json(new { product });
        }

        [HttpPost]
        [Route("/api/Transaction/getBatch")]
        public async Task<IActionResult> GetBatch(int ProductId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var batchList = await _productRateInfo.GetAllAsync(p => p.ProductInfoId == ProductId && p.StoreInfoId == user.StoreId);

            return Json(new { batchList });
        }
        [HttpPost]
        [Route("/api/Transaction/getProductRate")]
        public async Task<IActionResult> GetProductRate(int ProductRateId)
        {
            var productDetail = await _productRateInfo.GetAsync(ProductRateId);

            return Json(new { productDetail });
        }


        [HttpPost]
        [Route("/api/Transaction/saveTransaction")]
        public async Task<int> saveTransaction(TransationViewModel transactionViewModel)
        {
            try
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                var user = await _userManager.FindByIdAsync(userId);

                //var InvoiceNo = 0;
                //var productinvoiceinfo = await _productInvoiceInfo.GetAllAsync();
                transactionViewModel.ProductInvoiceInfo.TransactionDate = DateTime.Now;
                transactionViewModel.ProductInvoiceInfo.CreatedBy = userId;
                transactionViewModel.ProductInvoiceInfo.CreatedDate = DateTime.Now;
                transactionViewModel.ProductInvoiceInfo.StoreinfoId = user.StoreId;
                transactionViewModel.ProductInvoiceInfo.BillStatus = 1;

                transactionViewModel.ProductInvoiceInfo.IsActive = true;
                transactionViewModel.ProductInvoiceInfo.InvoiceNo = "1";
                var invoiceId = await _productInvoiceInfo.InsertAsync(transactionViewModel.ProductInvoiceInfo);

                if (transactionViewModel.ProductInvoiceDetailInfos.Count() > 0)
                {

                    foreach (var items in transactionViewModel.ProductInvoiceDetailInfos)
                    {
                        ProductInvoiceDetailInfo productInvoiceDetailInfo = new ProductInvoiceDetailInfo();
                        productInvoiceDetailInfo.ProductInvoiceInfoId = invoiceId;
                     //   productInvoiceDetailInfo.ProductInvoiceInfoId = items.ProductInvoiceInfoId;
                        productInvoiceDetailInfo.Rate = items.Rate;
                        productInvoiceDetailInfo.Quantity = items.Quantity;
                        productInvoiceDetailInfo.Amount = items.Amount;
                        productInvoiceDetailInfo.CreatedBy = userId;
                        productInvoiceDetailInfo.CreatedDate = DateTime.Now;
                        await _productInvoiceDetailInfo.InsertAsync(productInvoiceDetailInfo);

                        var rateinfo = await _productRateInfo.GetAsync(items.ProductRateInfoId);
                        var product = await _productInfo.GetAsync(rateinfo.ProductInfoId);

                        TransationInfo transactionInfo = new TransationInfo();
                        transactionInfo.TransactionType = "Sell";
                        transactionInfo.CategoryInfoId = rateinfo.CategroyInfoId;
                        transactionInfo.ProductInfoId = rateinfo.ProductInfoId;
                        transactionInfo.UnitInfoId = product.UnitInfoId;
                        transactionInfo.ProductRateInfoId = items.ProductRateInfoId;
                        transactionInfo.Rate = items.Rate;
                        transactionInfo.Quantity = items.Quantity;
                        transactionInfo.Amount = items.Amount;
                        transactionInfo.IsActive = true;
                        transactionInfo.CreatedDate = DateTime.Now;
                        transactionInfo.CreatedBy = userId;
                        transactionInfo.StoreInfoId = user.StoreId;
                        await _transactionInfo.InsertAsync(transactionInfo);


                        rateinfo.SoldQuantity += items.Quantity;
                        rateinfo.RemainingQuantity -= items.Quantity;
                        rateinfo.ModifiedBy = userId;
                        rateinfo.ModifiedDate = DateTime.Now;
                        await _productRateInfo.UpdateAsync(rateinfo);


                        var stockdet = await _stockInfo.GetAsync(p => p.ProductInfoId == rateinfo.ProductInfoId && p.StoreInfoId == user.StoreId);
                        var qty = stockdet.Quantity - items.Quantity;
                        stockdet.Quantity = qty;
                        stockdet.ModifiedBy = userId;
                        stockdet.ModifiedDate = DateTime.Now;
                        await _stockInfo.UpdateAsync(stockdet);
                    }
                }
                return invoiceId;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> PrintReport(int Id)
        {


            ViewBag.CategoryInfos = await _categoryInfo.GetAllAsync();
            ViewBag.UnitInfos = await _unitInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.ProductInfos = await _productInfo.GetAllAsync();
            ViewBag.CustomerInfos = await _customerInfo.GetAllAsync();
            ViewBag.ProductRateInfos = await _productRateInfo.GetAllAsync(p => p.IsActive == true);



            var invoiceInfo = await _productInvoiceInfo.GetAsync(Id);
            var user = await _userManager.FindByIdAsync(invoiceInfo.CreatedBy);
            ViewBag.TaxCreater = user.FirstName + ' ' + user.MiddleName + ' ' + user.LastName + ' ';


            TransationViewModel transactionViewModel = new TransationViewModel();
            transactionViewModel.ProductInvoiceInfo = invoiceInfo;
            transactionViewModel.StoreInfo = await _storeInfo.GetAsync(user.StoreId);
            transactionViewModel.ProductInvoiceDetailInfos = await _productInvoiceDetailInfo.GetAllAsync(p => p.ProductInvoiceInfoId == Id);
            ViewBag.TotalAmount = ConvertMoneyToWords((int)transactionViewModel.ProductInvoiceInfo.TotalAmount);


            return View(transactionViewModel);
        }
        public static string ConvertMoneyToWords(int amount)
        {
            if (amount == 0)
                return "zero rupees";

            string rupeeWords = ConvertNumberToWords(amount) + " rupee" + (amount == 1 ? "" : "s");
            return rupeeWords;
        }

        private static string ConvertNumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            string[] unitsMap = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tensMap = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 0)
                return "minus " + ConvertNumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 10000000) > 0)
            {
                words += ConvertNumberToWords(number / 10000000) + " crore ";
                number %= 10000000;
            }

            if ((number / 100000) > 0)
            {
                words += ConvertNumberToWords(number / 100000) + " lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertNumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertNumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.Trim();
        }

        [HttpPost]
        [Route("/api/Transaction/CancelTransaction")]
        public async Task<IActionResult> CancelTransaction(int invoiceId, string cancellationRemarks)
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            var productinvoiceinfo = await _productInvoiceInfo.GetAsync(invoiceId);

            productinvoiceinfo.BillStatus = 2;
            productinvoiceinfo.CancellationRemarks = cancellationRemarks;
            productinvoiceinfo.ModifiedDate = productinvoiceinfo.ModifiedDate = DateTime.Now;
            productinvoiceinfo.ModifiedBy = userId;
            await _productInvoiceInfo.UpdateAsync(productinvoiceinfo);
            return Json(1);
        }
    }


}