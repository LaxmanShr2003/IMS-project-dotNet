
using IMS.infrastructure.IRepository;
using IMS.models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace IMS.web.Controllers
{
    public class CustomerInfoController : Controller
    {
        private readonly ICrudService<CustomerInfo> _customerInfo;
        private readonly ICrudService<StoreInfo> _storeInfo;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerInfoController(ICrudService<CustomerInfo> customerInfo,ICrudService<StoreInfo> storeInfo,
            UserManager<ApplicationUser> userManager) 
        {
            _customerInfo = customerInfo;
            _storeInfo = storeInfo;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId =  _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var customerInfo =await _customerInfo.GetAllAsync(e => e.StoreInfoId == user.StoreId);

            return View(customerInfo);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            CustomerInfo customerInfo = new CustomerInfo();
            
            if (id > 0)
            {
                customerInfo = await _customerInfo.GetAsync(id);
            }
            return View(customerInfo);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(CustomerInfo customerInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var user = await _userManager.FindByIdAsync(userId);
                    if (customerInfo.Id == 0)
                    {
                        customerInfo.CreatedDate = DateTime.Now;
                        customerInfo.CreatedBy = userId;
                        customerInfo.StoreInfoId = user.StoreId;
                        await _customerInfo.InsertAsync(customerInfo);
                        TempData["Success"] = "Customer Info Added Successfully";
                    }
                    else
                    {
                        var OrgCustomerInfo = await _customerInfo.GetAsync(customerInfo.Id);
                        OrgCustomerInfo.CustomerName = customerInfo.CustomerName;
                        OrgCustomerInfo.Email = customerInfo.Email;
                        OrgCustomerInfo.PhoneNumber = customerInfo.PhoneNumber;
                        OrgCustomerInfo.Address = customerInfo.Address;
                        OrgCustomerInfo.PanNo = customerInfo.PanNo;
                     
                        await _customerInfo.UpdateAsync(OrgCustomerInfo);
                        TempData["Success"] = "Customer Info Updated Successfully";
                    }
                    return RedirectToAction("AddEdit");
                }
                catch (Exception)
                {
                    TempData["error"] = "Something went wrong, please try again later";
                    
                    return RedirectToAction(nameof(AddEdit));
                }
            }
            return RedirectToAction("AddEdit");
        }
        [HttpPost]
        [Route("/api/Customer/addCustomer")]
        public async Task<IActionResult> AddCustomer(CustomerInfo model)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
      
            model.CreatedDate = DateTime.Now;
            model.CreatedBy = userId;
            model.StoreInfoId = user.StoreId;

            var result = await _customerInfo.InsertAsync(model);
            model.Id = result;

            return Json(model);
        }
    }
   
}
