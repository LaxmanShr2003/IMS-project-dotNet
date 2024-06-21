using IMS.infrastructure.IRepository;
using IMS.models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.web.Controllers
{
    public class UnitInfoController : Controller
    {
        private readonly ICrudService<UnitInfo> _unitInfoService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnitInfoController(ICrudService<UnitInfo> unitInfoService, UserManager<ApplicationUser> userManager)
        {
            _unitInfoService = unitInfoService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var unitInfoList = await _unitInfoService.GetAllAsync();
            return View(unitInfoList);
        }

        public async Task<IActionResult> AddEdit(int id)
        {

            UnitInfo unitInfo = new UnitInfo();
            unitInfo.IsActive = true;
            if (id > 0)
            {
                unitInfo = await _unitInfoService.GetAsync(id);
            }

            return View(unitInfo);
        }

        [HttpPost]

        public async Task<IActionResult> AddEdit(UnitInfo unitInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    if (unitInfo.Id == 0)
                    {
                        unitInfo.CreatedDate = DateTime.Now;
                        unitInfo.CreatedBy = userId;
                        await _unitInfoService.InsertAsync(unitInfo);

                        TempData["Success"] = "Unit Info Added Successfully";
                    }
                    else
                    {
                        var OrgUnitInfo = await _unitInfoService.GetAsync(unitInfo.Id);
                        OrgUnitInfo.UnitName = unitInfo.UnitName;
                        OrgUnitInfo.IsActive = unitInfo.IsActive;
                        OrgUnitInfo.ModifiedDate = DateTime.Now;
                        OrgUnitInfo.ModifiedBy = userId;
                        await _unitInfoService.UpdateAsync(OrgUnitInfo);

                        TempData["Success"] = "Unit Info Updated Successfully";
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Error"] = "Error Occured While Adding/Updating Unit Info";
                    return RedirectToAction(nameof(AddEdit));
                }

            }
            TempData["Error"] = "Please Enter Valid Data";
            return RedirectToAction(nameof(AddEdit));

        }
    }


}

