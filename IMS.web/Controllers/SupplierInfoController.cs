using IMS.infrastructure.IRepository;
using IMS.models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.web.Controllers
{
    public class SupplierInfoController : Controller
    {
        private readonly ICrudService<SupplierInfo> _supplierInfo;
        private readonly ICrudService<StoreInfo> _storeInfo;
        private readonly UserManager<ApplicationUser> _userManager;
        public SupplierInfoController(ICrudService<SupplierInfo> supplierInfo,ICrudService<StoreInfo> storeInfo, UserManager<ApplicationUser> userManager)
        {
            _storeInfo = storeInfo;
            _supplierInfo = supplierInfo;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var supplierInfo = await _supplierInfo.GetAllAsync(e => e.StoreInfoId == user.StoreId);
            return View(supplierInfo);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            SupplierInfo supplierInfo = new SupplierInfo();
            supplierInfo.IsActive = true;
            if (id > 0)
            {
                supplierInfo = await _supplierInfo.GetAsync(id);
            }
            return View(supplierInfo);
        }

        [HttpPost]  
        public async Task<IActionResult> AddEdit(SupplierInfo supplierInfo){
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var user = await _userManager.FindByIdAsync(userId);
                    if (supplierInfo.Id == 0)
                    {
                        supplierInfo.CreatedDate = DateTime.Now;
                        supplierInfo.CreatedBy = userId;
                        supplierInfo.StoreInfoId = user.StoreId;
                        await _supplierInfo.InsertAsync(supplierInfo);
                        TempData["Success"] = "Supplier Info Added Successfully";
                    }
                    else
                    {
                        var OrgSupplierInfo = await _supplierInfo.GetAsync(supplierInfo.Id);
                        OrgSupplierInfo.SupplierName = supplierInfo.SupplierName;
                        OrgSupplierInfo.ContactPerson = supplierInfo.ContactPerson;
                        OrgSupplierInfo.Address = supplierInfo.Address;
                        OrgSupplierInfo.PhoneNumber = supplierInfo.PhoneNumber;
                        OrgSupplierInfo.Email = supplierInfo.Email;
                        OrgSupplierInfo.IsActive = supplierInfo.IsActive;
                        OrgSupplierInfo.ModifiedDate = DateTime.Now;
                        await _supplierInfo.UpdateAsync(OrgSupplierInfo);
                        TempData["Success"] = "Supplier Info Updated Successfully";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception )
                {
                    TempData["error"] = "Something went wrong, please try again later";
                    //TempData["error"] = ex.Message;
                    return RedirectToAction(nameof(AddEdit));
                }
            }
            TempData["Error"] = "Please Enter Valid Data";
            return RedirectToAction(nameof(AddEdit));
        }
    }
}
