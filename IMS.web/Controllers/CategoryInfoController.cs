using IMS.infrastructure.IRepository;
using IMS.models.Entity;
using IMS.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IMS.web.Controllers
{
    public class CategoryInfoController : Controller
    {

        private readonly ICrudService<CategoryInfo> _categoryInfo;
        private readonly ICrudService<StoreInfo> _storeInfo;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoryInfoController(ICrudService<CategoryInfo> categoryInfo, UserManager<ApplicationUser> userManager, 
            ICrudService<StoreInfo> storeInfo)
        {
            _categoryInfo = categoryInfo;
            _userManager = userManager;
            _storeInfo = storeInfo;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            var categoryInfo = await _categoryInfo.GetAllAsync(e =>e.StoreInfoId==user.StoreId);
            return View(categoryInfo);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            CategoryInfo categoryInfo = new CategoryInfo();
            categoryInfo.IsActive = true;
            
          
            if (id > 0)
            {
                categoryInfo = await _categoryInfo.GetAsync(id);
            }
            return View(categoryInfo);
        }
    

        [HttpPost]
        public async Task<IActionResult> AddEdit(CategoryInfo categoryinfo)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(HttpContext.User);
                    var user = await _userManager.FindByIdAsync(userId);
                    if (categoryinfo.Id == 0)
                    {
                        categoryinfo.CreatedDate = DateTime.Now;
                        categoryinfo.CreatedBy = userId;
                        categoryinfo.StoreInfoId = user.StoreId;
                        await _categoryInfo.InsertAsync(categoryinfo);

                        TempData["success"] = "Data Added Successfully";
                    }
                    else
                    {
                        var OrgCategoryInfo = await _categoryInfo.GetAsync(categoryinfo.Id);
                        OrgCategoryInfo.CategoryName = categoryinfo.CategoryName;
                        OrgCategoryInfo.CategoryDescription = categoryinfo.CategoryDescription;
                        OrgCategoryInfo.IsActive = categoryinfo.IsActive;
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
    }
}
