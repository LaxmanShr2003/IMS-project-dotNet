using IMS.infrastructure.IRepository;
using IMS.models.Entity;
using IMS.models.ViewModels;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Data;

namespace IMS.web.Controllers
{
    public class ReportController : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly ICrudService<SupplierInfo> _supplierInfo;
        public readonly ICrudService<CustomerInfo> _customerInfo;
        public readonly ICrudService<CategoryInfo> _categoryInfo;
        public readonly IRawSqlRepository _rawSqlRepository;
        public readonly ICrudService<ProductInfo> _productInfo;

        public ReportController(UserManager<ApplicationUser> userManager, ICrudService<SupplierInfo> supplierInfo,
            ICrudService<CustomerInfo> customerInfo, IRawSqlRepository rawSqlRepository, ICrudService<CategoryInfo> categoryInfo,
            ICrudService<ProductInfo> productInfo)
        {
            _userManager = userManager;
            _supplierInfo = supplierInfo;
            _customerInfo = customerInfo;
            _rawSqlRepository = rawSqlRepository;
            _categoryInfo = categoryInfo;
            _productInfo = productInfo;
        }

        public async Task<IActionResult> Index(ReportViewModel reportViewModel)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.supplierInfo = await _supplierInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.customerInfo = await _customerInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);
           
            reportViewModel.search = new SearchCriteria();

           
            return View(reportViewModel);
        }

        public async Task<IActionResult> search(ReportViewModel reportViewModel)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.supplierInfo = await _supplierInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.customerInfo = await _customerInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);


            var customerId = reportViewModel.search.CustomerId.HasValue
                ? new SqlParameter("@CustomerId", SqlDbType.Int) { Value = reportViewModel.search.CustomerId }
                : new SqlParameter("@CustomerId", SqlDbType.Int) { Value = DBNull.Value };

            var PaymentMethodId = reportViewModel.search.PaymentMethod.HasValue
               ? new SqlParameter("@PaymentMethodId", SqlDbType.Int) { Value = reportViewModel.search.PaymentMethod }
               : new SqlParameter("@PaymentMethodId", SqlDbType.Int) { Value = DBNull.Value };

            var startDate = reportViewModel.search.StartDate.HasValue
               ? new SqlParameter("@startDate", SqlDbType.DateTime) { Value = reportViewModel.search.StartDate }
               : new SqlParameter("@startDate", SqlDbType.DateTime) { Value = DBNull.Value };

            var endDate = reportViewModel.search.EndDate.HasValue
          ? new SqlParameter("@endDate", SqlDbType.DateTime) { Value = reportViewModel.search.EndDate }
          : new SqlParameter("@endDate", SqlDbType.DateTime) { Value = DBNull.Value };

            var result = _rawSqlRepository.FromSql<CustomerReportViewModel>(
                "usp_GetTransactionInfo @CustomerId, @PaymentMethodId, @startDate, @endDate, @storeId",
                customerId, PaymentMethodId, startDate, endDate, new SqlParameter("@storeId", user.StoreId)).ToList();
                
            
            reportViewModel.customerReportViewModels = result;

            return View(nameof(Index), reportViewModel);
        }

        public async Task<IActionResult> DetailReport(ReportViewModel reportViewModel)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.supplierInfo = await _supplierInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.customerInfo = await _customerInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);
            ViewBag.categoryInfo = await _categoryInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);
            ViewBag.productInfo = await _productInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);


            reportViewModel.search = new SearchCriteria();


            return View(reportViewModel);
        }


        public async Task<IActionResult> SearchDetail(ReportViewModel reportViewModel)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            ViewBag.supplierInfo = await _supplierInfo.GetAllAsync(p => p.IsActive == true && p.StoreInfoId == user.StoreId);
            ViewBag.customerInfo = await _customerInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);
            ViewBag.categoryInfo = await _categoryInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);
            ViewBag.productInfo = await _productInfo.GetAllAsync(p => p.StoreInfoId == user.StoreId);

            var customerId = reportViewModel.search.CustomerId.HasValue
                ? new SqlParameter("@CustomerId", SqlDbType.Int) { Value = reportViewModel.search.CustomerId }
                : new SqlParameter("@CustomerId", SqlDbType.Int) { Value = DBNull.Value };

            var supplierId = reportViewModel.search.SupplierId.HasValue
               ? new SqlParameter("@supplierId", SqlDbType.Int) { Value = reportViewModel.search.SupplierId }
               : new SqlParameter("@supplierId", SqlDbType.Int) { Value = DBNull.Value };

            var categoryId = reportViewModel.search.CategoryId.HasValue
               ? new SqlParameter("@categoryId", SqlDbType.Int) { Value = reportViewModel.search.CategoryId }
               : new SqlParameter("@categoryId", SqlDbType.Int) { Value = DBNull.Value };

            var productId = reportViewModel.search.ProductId.HasValue
               ? new SqlParameter("@productId", SqlDbType.Int) { Value = reportViewModel.search.ProductId }
               : new SqlParameter("@productId", SqlDbType.Int) { Value = DBNull.Value };

            var PaymentMethodId = reportViewModel.search.PaymentMethod.HasValue
               ? new SqlParameter("@PaymentMethodId", SqlDbType.Int) { Value = reportViewModel.search.PaymentMethod }
               : new SqlParameter("@PaymentMethodId", SqlDbType.Int) { Value = DBNull.Value };

            var startDate = reportViewModel.search.StartDate.HasValue
               ? new SqlParameter("@startDate", SqlDbType.DateTime) { Value = reportViewModel.search.StartDate }
               : new SqlParameter("@startDate", SqlDbType.DateTime) { Value = DBNull.Value };

            var endDate = reportViewModel.search.EndDate.HasValue
          ? new SqlParameter("@endDate", SqlDbType.DateTime) { Value = reportViewModel.search.EndDate }
          : new SqlParameter("@endDate", SqlDbType.DateTime) { Value = DBNull.Value };

            var result = _rawSqlRepository.FromSql<CustomerReportViewModel>(
                "usp_GetTransactionInfo @CustomerId, supplierId, @categoryId, @productId, @PaymentMethodId, @startDate, @endDate, @storeId",
                customerId, PaymentMethodId, startDate, endDate, new SqlParameter("@storeId", user.StoreId)).ToList();


            reportViewModel.customerReportViewModels = result;

            return View(nameof(Index), reportViewModel);
        }

    }
}
