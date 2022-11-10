using KenoShop.WebApp.ViewModels.Products;
using KenoShop.WebApp.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using KenoShop.WebApp.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace KenoShop.WebApp.Controllers.Products
{
    public class ProductController : Controller
    {
        #region ctor

        private readonly KenoShopDbContext _context;

        public ProductController(KenoShopDbContext context)
        {
            _context = context;
        }

        #endregion


        #region Product Lists

        [HttpGet("Product")]
        public IActionResult Index(FilterProductViewModel filterProduct, int? categoryID)
        {
            IQueryable<Product> query;
            
            if (categoryID != null)
            {
                query = _context.Product
                    .Where(p => p.IsInActive == false && p.IsDeleted == false)
                    .AsQueryable();
            }
            else
            {
                query = _context.Product
                    .Where(p => p.IsInActive == false && p.IsDeleted == false)
                    .AsQueryable();
            }

            //Filter
            if (filterProduct.Name != null)
            {
                query = query.Where(q => EF.Functions.Like(q.ProductName, $"%{filterProduct.Name}%"));
            }

            //Sort
            switch (filterProduct.OrderBy)
            {
                case OrderProducts.NoOrder:
                    break;
                case OrderProducts.Price_ASC:
                    query = query.OrderBy(q => q.ProductPrice);
                    break;
                case OrderProducts.Price_DESC:
                    query = query.OrderByDescending(q => q.ProductPrice);
                    break;
                //case OrderProducts.Inventory_ASC:
                //    query = query.OrderBy(q => q.);
                //    break;
                //case OrderProducts.Inventory_DESC:
                //    break;
                case OrderProducts.ProductName_ASC:
                    query = query.OrderBy(q => q.ProductName);
                    break;
                case OrderProducts.ProductName_DESC:
                    query = query.OrderByDescending(q => q.ProductName);
                    break;
            }

            //Paging
            filterProduct.SetPaging(query);

            return View("/Views/Products/Index.cshtml", filterProduct);
        }

        #endregion


        #region Product Detail

        //[Route("ProductDetail")]
        [HttpGet("products/{productId}/{productName}")]
        public IActionResult ProductDetail(int productId, string productName)
        {
            var Products = _context.Product.SingleOrDefault(p => p.ProductID == productId);
            
            if (productId == null)
            {
                return NotFound();
            }

            return View("/Views/Products/ProductDetail.cshtml",Products);
        }

        #endregion
        
    }
}
