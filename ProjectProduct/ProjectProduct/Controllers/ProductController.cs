using Microsoft.AspNetCore.Mvc;
using ProjectProduct.Data;
using ProjectProduct.Models;

namespace ProjectProduct.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _context;

        public int Id { get; private set; }

        public ProductController(ApplicationDbContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var productinfo = _context.Products.ToList();
            return View(productinfo);
        }

        [HttpGet]
        public IActionResult AddEdit(int id)
        {
            Product product = new Product();
            if(id > 0)
            {
                product = _context.Products.FirstOrDefault(x => x.Id == id);

            }

            return View(product);
        }
        [HttpPost]
        public IActionResult AddEdit(Product product) 
        {
            if(product.Id == 0) 
            {

                _context.Add(product);
                _context.SaveChanges();
            }
            else
            {
                var productinfo = _context.Products.FirstOrDefault(x=>x.Id==product.Id);

                productinfo.ProductName = product.ProductName;
                productinfo.ProductTitle = product.ProductTitle;
                productinfo.ProductCount = product.ProductCount;
                product.ProductDescription = product.ProductDescription;



               

                _context.Update(productinfo);
                _context.SaveChanges();
            }

            return RedirectToAction("ProductList");
        }
    }
}
