using IMS.infrastructure;
using IMS.infrastructure.IRepository;
using IMS.models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IMS.web.Controllers
{
    public class CategoryInfoController : Controller
    {
        private readonly ICrudService<CategoryInfo> _categoryInfo;
        private readonly ICrudService<StoreInfo> _storeInfo;
        private readonly UserManager<ApplicationUser> _userManager;
        public CategoryInfoController(ICrudService<CategoryInfo> categoryInfo, ICrudService<StoreInfo> storeInfo,
            UserManager<ApplicationUser> userManager)
        {
            _categoryInfo = categoryInfo;
            _storeInfo = storeInfo;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var storeInfo = await _storeInfo.GetAsync(user.StoreId);
            var categoryInfos = await _categoryInfo.GetAllAsync(p => p.StoreInfoId == storeInfo.Id);

            return View(categoryInfos);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(CategoryInfo categoryInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    if (categoryInfo.Id == 0)
                    {
                        categoryInfo.CreatedDate = DateTime.Now;
                        categoryInfo.CreatedBy = userId;
                        await _categoryInfo.InsertAsync(categoryInfo);
                        TempData["success"] = "Data Added Successfully";
                    }
                    else
                    {
                        var OrgCategoryInfo = await _categoryInfo.GetAsync(categoryInfo.Id);
                        OrgCategoryInfo.CategoryName = categoryInfo.CategoryName;
                        OrgCategoryInfo.CategoryDescription = categoryInfo.CategoryDescription;
                        OrgCategoryInfo.IsActive = categoryInfo.IsActive;
                        OrgCategoryInfo.ModifiedDate = DateTime.Now;
                        OrgCategoryInfo.ModifiedBy = userId;
                        await _categoryInfo.UpdateAsync(OrgCategoryInfo);

                        TempData["success"] = "Data Updated Successfully";
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["error"] = "Something went wrong, please try again later";
                    //TempData["error"] = ex.Message;
                    return RedirectToAction(nameof(AddEdit));
                }
            }
            TempData["error"] = "Please Input Valid Data";
            return RedirectToAction(nameof(AddEdit));
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {

            var categoryInfo = await _categoryInfo.GetAsync(id);
            _categoryInfo.Delete(categoryInfo);
            TempData["success"] = "Data Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        
}
