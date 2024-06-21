using IMS.infrastructure.IRepository;
using IMS.models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.web.Controllers
{
    public class ProductRateInfoController : Controller
    {
        private readonly ICrudService<ProductInfo> _productInfo;
        private readonly ICrudService<CategoryInfo> _categoryInfo;
        private readonly ICrudService<UnitInfo> _unitInfo;
        private readonly ICrudService<StoreInfo> _storeInfo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICrudService<ProductRateInfo> _productRateInfo;
        private readonly ICrudService<RackInfo> _rackInfo;
        private readonly ICrudService<StockInfo> _stockInfo;
        private readonly ICrudService<TransationInfo> _transactionInfo;
        private readonly ICrudService<SupplierInfo> _supplierInfo;

        public ProductRateInfoController(ICrudService<ProductInfo> productInfo,
            ICrudService<CategoryInfo> categoryInfo,
            ICrudService<UnitInfo> unitInfo,
            ICrudService<StoreInfo> storeInfo,
            UserManager<ApplicationUser> userManager,
            ICrudService<ProductRateInfo> productRateInfo,
            ICrudService<RackInfo> rackInfo,
            ICrudService<StockInfo> stockInfo,
            ICrudService<TransationInfo> transactionInfo,
            ICrudService<SupplierInfo> supplierInfo)
        {
            _productInfo = productInfo;
            _categoryInfo = categoryInfo;
            _unitInfo = unitInfo;
            _storeInfo = storeInfo;
            _userManager = userManager;
            _productRateInfo = productRateInfo;
            _rackInfo = rackInfo;
            _stockInfo = stockInfo;
            _transactionInfo = transactionInfo;
            _supplierInfo = supplierInfo;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.CategoryInfos = await _categoryInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.ProductInfos = await _productInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.UnitInfos = await _unitInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.RackInfos = await _rackInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.SupplierInfos = await _supplierInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            var rateRateInfo = await _productRateInfo.GetAllAsync();

            return View(rateRateInfo);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int Id)
        {
            ProductRateInfo productRateInfo = new ProductRateInfo();
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.CategoryInfos = await _categoryInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.UnitInfos = await _unitInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.ProductInfos = await _productInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.RackInfos = await _rackInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.SupplierInfos = await _supplierInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            productRateInfo.PurchaseDate = DateTime.Now;
            productRateInfo.IsActive = true;

            if (Id > 0)
            {
                productRateInfo = await _productRateInfo.GetAsync(Id);
            }
            return View(productRateInfo);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(ProductRateInfo productRateInfo)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.CategoryInfos = await _categoryInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.UnitInfos = await _unitInfo.GetAllAsync(p => p.IsActive == true);
            ViewBag.ProductInfos = await _productInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.RackInfos = await _rackInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.SupplierInfos = await _supplierInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            if (ModelState.IsValid)
            {
                try
                {
         

                    var product = await _productInfo.GetAsync(productRateInfo.ProductInfoId);

                    if (productRateInfo.Id == 0)
                    {
                        productRateInfo.CreatedDate = DateTime.Now;
                        productRateInfo.CreatedBy = userId;
                        productRateInfo.StoreInfoId = user.StoreId;
                        productRateInfo.RemainingQuantity = productRateInfo.Quantity;
                        var rateInfoId = await _productRateInfo.InsertAsync(productRateInfo);

                        TransationInfo transactionInfo = new TransationInfo();
                        transactionInfo.TransactionType = "Purchase";
                        transactionInfo.CategoryInfoId = productRateInfo.CategroyInfoId;
                        transactionInfo.ProductInfoId = productRateInfo.ProductInfoId;
                        transactionInfo.UnitInfoId = product.UnitInfoId;
                        transactionInfo.ProductRateInfoId = rateInfoId;
                        transactionInfo.Rate = productRateInfo.CostPrice;
                        transactionInfo.Quantity = productRateInfo.Quantity;
                        transactionInfo.Amount = productRateInfo.CostPrice * productRateInfo.Quantity;
                        transactionInfo.IsActive = true;
                        transactionInfo.CreatedDate = DateTime.Now;
                        transactionInfo.CreatedBy = userId;
                        transactionInfo.StoreInfoId = user.StoreId;
                        await _transactionInfo.InsertAsync(transactionInfo);


                        var stockdet = await _stockInfo.GetAsync(p => p.ProductInfoId == productRateInfo.ProductInfoId);
                        if (stockdet == null)
                        {

                            StockInfo stockInfo = new StockInfo();
                            stockInfo.CategoryInfoId = productRateInfo.CategroyInfoId;
                            stockInfo.ProductInfoId = productRateInfo.ProductInfoId;
                            stockInfo.ProductRateInfoId = rateInfoId;
                            stockInfo.Quantity = productRateInfo.Quantity;
                            stockInfo.IsActive = true;
                            stockInfo.CreatedDate = DateTime.Now;
                            stockInfo.CreatedBy = userId;
                            stockInfo.StoreInfoId = user.StoreId;
                            await _stockInfo.InsertAsync(stockInfo);
                        }
                        else
                        {
                            var qty = stockdet.Quantity + productRateInfo.Quantity;
                            stockdet.Quantity = qty;
                            stockdet.ModifiedBy = userId;
                            stockdet.ModifiedDate = DateTime.Now;
                            await _stockInfo.UpdateAsync(stockdet);
                        }

                        TempData["success"] = "Data Added Sucessfully";
                    }
                    else
                    {
                        var OrgproductRateInfo = await _productRateInfo.GetAsync(productRateInfo.Id);

                        var productId = OrgproductRateInfo.ProductInfoId;
                        var orgqty = OrgproductRateInfo.Quantity;
                        var changeqty = OrgproductRateInfo.Quantity - productRateInfo.Quantity;

                        OrgproductRateInfo.CategroyInfoId = productRateInfo.CategroyInfoId;
                        OrgproductRateInfo.ProductInfoId = productRateInfo.ProductInfoId;
                        OrgproductRateInfo.CostPrice = productRateInfo.CostPrice;
                        OrgproductRateInfo.SellingPrice = productRateInfo.SellingPrice;
                        var qqty = OrgproductRateInfo.RemainingQuantity - OrgproductRateInfo.Quantity + productRateInfo.Quantity;
                        OrgproductRateInfo.Quantity = productRateInfo.Quantity;
                        OrgproductRateInfo.RemainingQuantity = qqty;

                        OrgproductRateInfo.BatchNo = productRateInfo.BatchNo;
                        OrgproductRateInfo.PurchaseDate = productRateInfo.PurchaseDate;
                        OrgproductRateInfo.ExpiryDate = productRateInfo.ExpiryDate;
                        OrgproductRateInfo.SupplierInfoId = productRateInfo.SupplierInfoId;
                        OrgproductRateInfo.RackInfoId = productRateInfo.RackInfoId;
                        OrgproductRateInfo.IsActive = productRateInfo.IsActive;
                        OrgproductRateInfo.ModifiedBy = userId;
                        OrgproductRateInfo.ModifiedDate = DateTime.Now;
                        await _productRateInfo.UpdateAsync(OrgproductRateInfo);

                        TransationInfo transactionInfo = new TransationInfo();
                        transactionInfo.TransactionType = "Purchase";
                        transactionInfo.CategoryInfoId = productRateInfo.CategroyInfoId;
                        transactionInfo.ProductInfoId = productRateInfo.ProductInfoId;
                        transactionInfo.UnitInfoId = product.UnitInfoId;
                        transactionInfo.ProductRateInfoId = OrgproductRateInfo.Id;
                        transactionInfo.Rate = productRateInfo.CostPrice;
                        transactionInfo.Quantity = productRateInfo.Quantity;
                        transactionInfo.Amount = productRateInfo.CostPrice * productRateInfo.Quantity;
                        transactionInfo.IsActive = true;
                        transactionInfo.CreatedDate = DateTime.Now;
                        transactionInfo.CreatedBy = userId;
                        transactionInfo.StoreInfoId = user.StoreId;
                        transactionInfo.ModifiedBy = userId;
                        transactionInfo.ModifiedDate = DateTime.Now;

                        await _transactionInfo.InsertAsync(transactionInfo);


                        if (productId == productRateInfo.ProductInfoId)
                        {
                            if (changeqty != 0)
                            {
                                var stockdet = await _stockInfo.GetAsync(p => p.ProductInfoId == productRateInfo.ProductInfoId);


                                var qty = stockdet.Quantity + changeqty;
                                stockdet.Quantity = qty;

                                stockdet.ModifiedBy = userId;
                                stockdet.ModifiedDate = DateTime.Now;
                                await _stockInfo.UpdateAsync(stockdet);
                            }
                        }
                        else
                        {
                            var oldstockdet = await _stockInfo.GetAsync(p => p.ProductInfoId == productId);

                            var qty = oldstockdet.Quantity - orgqty;
                            oldstockdet.Quantity = qty;

                            oldstockdet.ModifiedBy = userId;
                            oldstockdet.ModifiedDate = DateTime.Now;
                            await _stockInfo.UpdateAsync(oldstockdet);


                            var stockdet = await _stockInfo.GetAsync(p => p.ProductInfoId == productRateInfo.ProductInfoId);
                            if (stockdet == null)
                            {
                                StockInfo stockInfo = new StockInfo();
                                stockInfo.CategoryInfoId = productRateInfo.CategroyInfoId;
                                stockInfo.ProductInfoId = productRateInfo.ProductInfoId;
                                stockInfo.ProductRateInfoId = OrgproductRateInfo.Id;
                                stockInfo.Quantity = productRateInfo.Quantity;
                                stockInfo.IsActive = true;
                                stockInfo.CreatedDate = DateTime.Now;
                                stockInfo.CreatedBy = userId;
                                stockInfo.StoreInfoId = user.StoreId;
                                await _stockInfo.InsertAsync(stockInfo);
                            }
                            else
                            {
                                var qtyy = stockdet.Quantity + productRateInfo.Quantity;
                                stockdet.Quantity = qtyy;
                                stockdet.ModifiedBy = userId;
                                stockdet.ModifiedDate = DateTime.Now;
                                await _stockInfo.UpdateAsync(stockdet);
                            }

                        }


                        TempData["success"] = "Data Updated Sucessfully";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["error"] = "Something went wrong, please try again later";
                    return RedirectToAction(nameof(AddEdit));
                }
            }

            TempData["error"] = "Please input Valid Data";
            return RedirectToAction(nameof(AddEdit));
        }



        [HttpPost]
        [Route("/api/ProductRate/getproduct")]
        public async Task<IActionResult> GetProduct(int CategoryId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var productList = await _productInfo.GetAllAsync(p => p.CategoryInfoId == CategoryId && p.StoreInfoId == user.StoreId);

            return Json(new { productList });
        }

        [HttpPost]
        [Route("/api/ProductRate/getUnit")]
        public async Task<IActionResult> GetUnit(int ProductId)
        {
            var product = await _productInfo.GetAsync(ProductId);

            return Json(new { product });
        }
    }
}
