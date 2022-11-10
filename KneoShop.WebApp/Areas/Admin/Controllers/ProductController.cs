using KenoShop.WebApp.ViewModels.Products;
using KenoShop.WebApp.Context;
using KenoShop.WebApp.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KenoShop.WebApp.ImageTools;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KenoShop.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : AdminBaseController
    {
        #region ctor

        private readonly KenoShopDbContext _context;

        public ProductController(KenoShopDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Product List

        public IActionResult Index(FilterProductViewModel filterProduct)
        {
            var query = _context.Product
                .Where(p => p.IsInActive == false && p.IsDeleted == false)
                .AsQueryable();

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

            return View(filterProduct);
        }

        #endregion

        #region Create Product

        [HttpGet("create-product")]
        public IActionResult CreateProduct()
        {
            var createProduct = new CreateProductViewModel()
            {
                BrandNames = BrandProduct.NoBrand,
                ProductCategories = _context.ProductCategories.Where(p => p.IsActive == true).ToList()
            };
            
            var brandsList = (from brand in _context.Brands
                select new SelectListItem()
                {
                    Text = brand.BrandName,
                    Value = brand.BrandID.ToString(),
                }).ToList();

            brandsList.Insert(0, new SelectListItem()
            {
                Text = "لطفا برند محصول را انتخاب کنید",
                Value = string.Empty
            });
            
            ViewBag.ListofBrands = brandsList;
            
            return View(createProduct);
        }

        [HttpPost("create-product"), ValidateAntiForgeryToken]
        public IActionResult CreateProduct(CreateProductViewModel createProduct)
        {
            if (ModelState.IsValid)
            {
                string imageName = null;

                if (createProduct.Image != null)
                {
                    imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(createProduct.Image.FileName);

                    createProduct.Image.AddImageToServer(imageName, PathTools.ProductImageUploadPath, 500, 357,
                        PathTools.ProductThumbImageUploadPath);
                }

                string InventoryLocation = null;

                if (createProduct.InventoryLocationID == 1)
                {
                    InventoryLocation = "مغازه";
                }
                else if (createProduct.InventoryLocationID == 2)
                {
                    InventoryLocation = "آنلاین شاپ";
                }

                string ProductSexType = null;

                if (createProduct.ProductSexTypeID == 1)
                {
                    ProductSexType = "مردانه";
                }
                else if (createProduct.ProductSexTypeID == 2)
                {
                    ProductSexType = "زنانه";
                }
                else if (createProduct.ProductSexTypeID == 3)
                {
                    ProductSexType = "بدون جنسیت";
                }

                Product product = new Product()
                {
                    ProductName = createProduct.ProductName,
                    ProductSexTypeID = createProduct.ProductSexTypeID,
                    ProductSexTypeTitle = ProductSexType,
                    ProductDescription = createProduct.ProductDescription,
                    ProductPrice = createProduct.ProductPrice,
                    ProductCode = createProduct.ProductCode,
                    ProductLiraPrice = createProduct.ProductLiraPrice,
                    ProductPurchasedPrice = createProduct.ProductPurchasedPrice,
                    ProductShortDescription = createProduct.ProductShortDescription,
                    InventoryLocationID = createProduct.InventoryLocationID,
                    InventoryLocationTitle = InventoryLocation,
                    BrandID = createProduct.BrandID,
                    // BrandID = _context.Brands.Where(b=>b.BrandName==createProduct.BrandNames), 
                    Image = imageName,
                    IsInActive = false,
                    IsDeleted = false
                };

                /*switch (createProduct.BrandNames)
                {
                    case BrandProduct.NoBrand:
                        product.BrandID = null;
                        break;
                    case BrandProduct.Zara:
                        product.BrandID = 1;
                        break;
                    case BrandProduct.PullandBear:
                        product.BrandID = 2;
                        break;
                    case BrandProduct.Bershka:
                        product.BrandID = 3;
                        break;
                    case BrandProduct.HandM:
                        product.BrandID = 4;
                        break;
                    case BrandProduct.LC:
                        product.BrandID = 5;
                        break;
                }*/

                _context.Product.Add(product);
                _context.SaveChanges();

                #region Insert Images Gallery

                if (createProduct.Galleries != null && createProduct.Galleries.Any())
                {
                    foreach (var gallery in createProduct.Galleries)
                    {
                        string galleryName = Guid.NewGuid().ToString("N") + Path.GetExtension(gallery.FileName);
                        gallery.AddImageToServer(imageName, PathTools.ProductGalleryImageUploadPath, 500, 357,
                            PathTools.ProductGalleryThumbImageUploadPath, product.Image);

                        var newGallery = new ProductGallery
                        {
                            ProductID = createProduct.ProductID,
                            Image = imageName
                        };

                        _context.ProductGalleries.Add(newGallery);
                    }
                }

                #endregion

                #region Insert ProductSelectedCategories

                //Insert Product Categories
                if (createProduct.SelectedCategories.Any() && createProduct.SelectedCategories != null)
                {
                    foreach (var selctedCategory in createProduct.SelectedCategories)
                    {
                        var addSelectedCategories = new ProductSelectedCategory()
                        {
                            ProductID = product.ProductID,
                            ProductCategoryID = selctedCategory
                        };
                        _context.ProductSelectedCategoies.Add(addSelectedCategories);
                    }
                }

                #endregion

                /*#region Insert ProductSelectedColors

                //Insert Product Colors
                if (createProduct.SelectedColors != null)
                {
                    foreach (var selctedColors in createProduct.SelectedColors)
                    {
                        var addSelectedColors = new ProductSelectedColor()
                        {
                            ProductID = product.ProductID,
                            ColorID = selctedColors
                        };
                        _context.ProductSelectedColors.Add(addSelectedColors);
                    }
                }

                #endregion

                #region Insert ProductSelectedSizes

                //Insert Product Sizes
                if (createProduct.SelectedSizes != null)
                {
                    foreach (var selctedSizes in createProduct.SelectedSizes)
                    {
                        var addSelectedSzies = new ProductSelectedSize()
                        {
                            ProductID = product.ProductID,
                            SizeID = selctedSizes
                        };
                        _context.ProductSelectedSizes.Add(addSelectedSzies);
                    }
                }

                #endregion*/

                #region Insert Images

                #endregion

                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        #endregion

        #region Edit Product

        [HttpGet("edit-product")]
        public IActionResult EditProduct(int Id)
        {
            var product = _context.Product.SingleOrDefault(p => p.ProductID == Id);

            if (product == null)
            {
                return NotFound();
            }


            var editModel = new EditProductViewModel
            {
                ProductID = product.ProductID,
                ProductCode = product.ProductCode,
                ProductSexTypeID = product.ProductSexTypeID,
                IsInActive = product.IsInActive,
                ProductPrice = product.ProductPrice,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductLiraPrice = product.ProductLiraPrice,
                ProductPurchasedPrice = product.ProductPurchasedPrice,
                ProductShortDescription = product.ProductShortDescription,
                ImageName = product.Image,
                BrandID = product.BrandID,
                ProductGalleries = _context.ProductGalleries.Where(p => p.ProductID == Id).ToList(),

                ProductCategories = _context.ProductCategories.Where(p => p.IsActive == true).ToList(),
                SelectedCategories = _context.ProductSelectedCategoies
                    .Where(p => p.ProductID == Id)
                    .Select(a => a.ProductCategoryID).ToList()
            };

            var brandsList = (from brand in _context.Brands
                select new SelectListItem()
                {
                    Text = brand.BrandName,
                    Value = brand.BrandID.ToString(),
                }).ToList();

            ViewBag.ListofBrands = brandsList;
            
            return View("EditProduct", editModel);
        }

        [HttpPost("edit-product"), ValidateAntiForgeryToken]
        public IActionResult EditProduct(EditProductViewModel editProduct)
        {
            if (ModelState.IsValid)
            {
                var product = _context.Product.SingleOrDefault(p => p.ProductID == editProduct.ProductID);

                if (product == null) return NotFound();

                if (editProduct.Image != null && editProduct.Image.IsImage())
                {
                    string imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(editProduct.Image.FileName);
                    editProduct.Image.AddImageToServer(imageName, PathTools.ProductImageUploadPath, 500, 357,
                        PathTools.ProductThumbImageUploadPath, product.Image);
                    product.Image = imageName;
                }

                product.ProductCode = editProduct.ProductCode;
                product.ProductName = editProduct.ProductName;
                product.ProductPrice = editProduct.ProductPrice;
                product.ProductDescription = editProduct.ProductDescription;
                product.ProductSexTypeID = editProduct.ProductSexTypeID;
                product.BrandID = editProduct.BrandID;

                _context.Product.Update(product);

                #region Insert Images Gallery

                if (editProduct.Galleries != null && editProduct.Galleries.Any())
                {
                    foreach (var gallery in editProduct.Galleries)
                    {
                        string imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(gallery.FileName);
                        gallery.AddImageToServer(imageName, PathTools.ProductGalleryImageUploadPath, 500, 357,
                            PathTools.ProductGalleryThumbImageUploadPath, product.Image);

                        var newGallery = new ProductGallery
                        {
                            ProductID = editProduct.ProductID,
                            Image = imageName
                        };

                        _context.ProductGalleries.Add(newGallery);
                    }
                }

                #endregion

                #region Insert ProductSelectedCategories

                var selectedCategory = _context.ProductSelectedCategoies
                    .Where(p => p.ProductID == editProduct.ProductID).ToList();

                //Remove All This Product Categories
                _context.ProductSelectedCategoies.RemoveRange(selectedCategory);

                //Insert Product Categories
                if (editProduct.SelectedCategories.Any() && editProduct.SelectedCategories != null)
                {
                    foreach (var selctedCategory in editProduct.SelectedCategories)
                    {
                        var addSelectedCategories = new ProductSelectedCategory()
                        {
                            ProductID = editProduct.ProductID,
                            ProductCategoryID = selctedCategory
                        };
                        _context.ProductSelectedCategoies.Add(addSelectedCategories);
                    }
                }

                #endregion

                _context.SaveChanges();
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }

            return View(editProduct);
        }

        #endregion

        #region remove Product

        public IActionResult RemoveProduct(int id)
        {
            var product = _context.Product.SingleOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return Json(new
                {
                    status = "NotFound"
                });
            }

            product.IsDeleted = true;
            _context.Product.Update(product);
            _context.SaveChanges();

            return Json(new
            {
                status = "Success"
            });
        }

        #endregion

        #region Remove Galleries

        public void RemoveGallery()
        {
        }

        #endregion
    }
}