using IMS.infrastructure.IRepository;
using IMS.models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.web.Controllers
{
    public class RackInfoController : Controller
    {
        private readonly ICrudService<RackInfo> _rackInfo;
        private readonly UserManager<ApplicationUser> _userManager;
        public RackInfoController(ICrudService<RackInfo> rackInfo, UserManager<ApplicationUser> userManager)
        {
            _rackInfo = rackInfo;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var rackInfo = await _rackInfo.GetAllAsync(e => e.StoreInfoId == user.StoreId);
            return View(rackInfo);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            RackInfo rackInfo = new RackInfo();
            rackInfo.IsActive = true;
            if (id > 0)
            {
                rackInfo = await _rackInfo.GetAsync(id);
            }
            return View(rackInfo);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(RackInfo rackInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var user = await _userManager.FindByIdAsync(userId);
                    if (rackInfo.Id == 0)
                    {
                        rackInfo.CreatedDate = DateTime.Now;
                        rackInfo.CreatedBy = userId;
                        rackInfo.StoreInfoId = user.StoreId;
                        await _rackInfo.InsertAsync(rackInfo);
                        TempData["Success"] = "Rack Info Added Successfully";
                    }
                    else
                    {
                        var OrgRackInfo = await _rackInfo.GetAsync(rackInfo.Id);
                        OrgRackInfo.RackName = rackInfo.RackName;
                        OrgRackInfo.IsActive = rackInfo.IsActive;
                        OrgRackInfo.ModifiedDate = DateTime.Now;
                        await _rackInfo.UpdateAsync(OrgRackInfo);
                        TempData["Success"] = "Rack Info Updated Successfully";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception )
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
