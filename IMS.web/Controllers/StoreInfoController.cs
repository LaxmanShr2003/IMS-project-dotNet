using IMS.infrastructure.IRepository;
using IMS.models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.web.Controllers
{
    [Authorize]
    public class StoreInfoController : Controller
    {
        private readonly ICrudService<StoreInfo> _storeCrudService;
        private readonly UserManager<ApplicationUser> _userManager;
        public StoreInfo StoreInfo { get; set; }
        public StoreInfoController(ICrudService<StoreInfo> storeCrudService, UserManager<ApplicationUser> usermanager)

        {
            _storeCrudService = storeCrudService;
            _userManager = usermanager;
        }
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var storeInfoList = await _storeCrudService.GetAllAsync();
            return View(storeInfoList);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int Id)
        {
            StoreInfo storeInfo = new StoreInfo();
            if (Id > 0)
            {
                storeInfo = await _storeCrudService.GetAsync(Id);
            }
            return View(storeInfo);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(StoreInfo storeInfo)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserId(HttpContext.User);
                if (storeInfo.Id == 0)
                {
                    storeInfo.CreatedDate = DateTime.Now;
                    storeInfo.CreatedBy = user;
                    await _storeCrudService.InsertAsync(storeInfo);
                    TempData["success"] = "Data Added Successfully";
                }
                else
                {
                    var OrgStoreInfo = await _storeCrudService.GetAsync(storeInfo.Id);
                    OrgStoreInfo.StoreName = storeInfo.StoreName;
                    OrgStoreInfo.Address = storeInfo.Address;
                    OrgStoreInfo.PhoneNumber = storeInfo.PhoneNumber;
                    OrgStoreInfo.PanNo = storeInfo.PanNo;
                    OrgStoreInfo.RegistrationNumber = storeInfo.RegistrationNumber;
                    OrgStoreInfo.IsActive = storeInfo.IsActive;
                    OrgStoreInfo.ModifiedDate = DateTime.Now;
                    OrgStoreInfo.ModifiedBy = user;
                    await _storeCrudService.UpdateAsync(OrgStoreInfo);
                    TempData["success"] = "Data Updated Successfully";

                }
            
            
                return RedirectToAction(nameof(Index));
            }
           
            TempData["error"] = "Insert Data Properly";
            return RedirectToAction(nameof(AddEdit));
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            var storeInfo = await _storeCrudService.GetAsync(id);
            _storeCrudService.Delete(storeInfo);
            TempData["error"] = "Data Deleted Successfully";
            return RedirectToAction(nameof(Index));
            

        }
    }
}