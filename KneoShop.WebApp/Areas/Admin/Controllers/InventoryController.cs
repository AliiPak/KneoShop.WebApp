using KenoShop.WebApp.Context;
using KenoShop.WebApp.Entities.Products;
using KenoShop.WebApp.ViewModels.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KenoShop.WebApp.Areas.Admin.Controllers;

public class InventoryController : AdminBaseController
{
    #region ctor

    private readonly KenoShopDbContext _context;

    public InventoryController(KenoShopDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Inventory List

    [HttpGet("inventory-list")]
    public IActionResult Index(int productID)
    {
        var query = (from inventory in _context.Inventories
            join product in _context.Product on inventory.ProductID equals product.ProductID
            where (inventory.IsDeleted == false) && (inventory.ProductID == productID)
            
            select new CreateInveotryViewModel()
            {
                ProductID = inventory.InventoryID, ColorName = inventory.ColorName, ColorID = inventory.ColorID,
                SizeName = inventory.SizeName, SizeID = inventory.SizeID,
                InventoryAmount = inventory.InventoryAmount, ProductName = product.ProductName,
                InventoryID = inventory.InventoryID
            }).ToList();

        ViewBag.productID = productID;
        
        return View(query);
    }

    #endregion

    #region Create Inventory

    [HttpGet("create-inventory/{productID}")]
    public IActionResult CreateInventory(int productID)
    {
        #region Colors ComboBox

        var ColorsList = (from color in _context.ProductColors
            select new SelectListItem()
            {
                Text = color.ColorName,
                Value = color.ColorID.ToString(),
            }).ToList();

        ColorsList.Insert(0, new SelectListItem()
        {
            Text = "لطفا رنگ محصول را انتخاب کنید",
            Value = string.Empty
        });
            
        ViewBag.ListofColors = ColorsList;

        #endregion

        #region Sizes ComboBox

        var SizesList = (from size in _context.ProductSizes
            select new SelectListItem()
            {
                Text = size.SizeTitle,
                Value = size.SizeID.ToString(),
            }).ToList();

        SizesList.Insert(0, new SelectListItem()
        {
            Text = "لطفا سایز محصول را انتخاب کنید",
            Value = string.Empty
        });
            
        ViewBag.ListofSizes = SizesList;

        #endregion
        
        return View();
    }

    [HttpPost("create-inventory/{productID}")]
    public IActionResult CreateInventory(int productID, CreateInveotryViewModel inventoryModel)
    {
        if (ModelState.IsValid)
        {
            Inventory invent = new Inventory()
            {
                ProductID = productID,
                InventoryAmount = inventoryModel.InventoryAmount,
                SizeID = inventoryModel.SizeID,
                SizeName = _context.ProductSizes.SingleOrDefault(s=>s.SizeID==inventoryModel.SizeID).SizeTitle,
                ColorID = inventoryModel.ColorID,
                ColorName = _context.ProductColors.SingleOrDefault(c=>c.ColorID==inventoryModel.ColorID).ColorName,
                IsDeleted = false
            };

            var invExists = _context.Inventories.Any(i =>
                i.ProductID == invent.ProductID && i.ColorID == invent.ColorID && i.SizeID == invent.SizeID);

            if (!invExists)
            {
                _context.Inventories.Add(invent);
                _context.SaveChanges();
                return RedirectToAction("Index",new { productID = productID });
            }
            else
            {
                return View();
                ModelState.AddModelError("", "کاربری با مشخصات وارد شده قبلا ثبت نام کرده است");
            }
        }
        else
        {
            return View();
        }
    }

    #endregion

    #region Create Inventory Secondary

    [HttpGet("create-inventoryS")]
    public IActionResult CreateInventoryS()
    {
        #region Products ComboBox

        var ProductsList = (from product in _context.Product
            select new SelectListItem()
            {
                Text = product.ProductName,
                Value = product.ProductID.ToString(),
            }).ToList();

        ProductsList.Insert(0, new SelectListItem()
        {
            Text = "لطفا محصول را انتخاب کنید",
            Value = string.Empty
        });
            
        ViewBag.ListofProducts = ProductsList;

        #endregion
        
        #region Colors ComboBox

        var ColorsList = (from color in _context.ProductColors
            select new SelectListItem()
            {
                Text = color.ColorName,
                Value = color.ColorID.ToString(),
            }).ToList();

        ColorsList.Insert(0, new SelectListItem()
        {
            Text = "لطفا رنگ محصول را انتخاب کنید",
            Value = string.Empty
        });
            
        ViewBag.ListofColors = ColorsList;

        #endregion

        #region Sizes ComboBox

        var SizesList = (from size in _context.ProductSizes
            select new SelectListItem()
            {
                Text = size.SizeTitle,
                Value = size.SizeID.ToString(),
            }).ToList();

        SizesList.Insert(0, new SelectListItem()
        {
            Text = "لطفا سایز محصول را انتخاب کنید",
            Value = string.Empty
        });
            
        ViewBag.ListofSizes = SizesList;

        #endregion
        
        return View();
    }

    /*[HttpPost("create-inventoryS}")]
    public IActionResult CreateInventoryS(CreateInveotrySecondaryViewModel inventoryModel)
    {
        if (ModelState.IsValid)
        {
            Inventory invent = new Inventory()
            {
                ProductID = inventoryModel.ProductID,
                InventoryAmount = inventoryModel.InventoryAmount,
                SizeID = inventoryModel.SizeID,
                SizeName = _context.ProductSizes.SingleOrDefault(s=>s.SizeID==inventoryModel.SizeID).SizeTitle,
                ColorID = inventoryModel.ColorID,
                ColorName = _context.ProductColors.SingleOrDefault(c=>c.ColorID==inventoryModel.ColorID).ColorName,
                IsDeleted = false
            };

            var invExists = _context.Inventories.Any(i =>
                i.ProductID == invent.ProductID && i.ColorID == invent.ColorID && i.SizeID == invent.SizeID);

            if (!invExists)
            {
                _context.Inventories.Add(invent);
                _context.SaveChanges();
                return RedirectToAction("Index",new { productID = invent.ProductID });
            }
            else
            {
                return View();
                ModelState.AddModelError("", "کاربری با مشخصات وارد شده قبلا ثبت نام کرده است");
            }
        }
        else
        {
            return View();
        }
    }*/

    #endregion
    
    #region Edit Inventory

    [HttpGet("edit-inventory/{inventoryID}")]
    public IActionResult EditInventory(int inventoryID)
    {
        var inventryExits = _context.Inventories.Any(i => i.InventoryID == inventoryID && i.IsDeleted == false);

        if (inventryExits)
        {
            var invent = _context.Inventories.SingleOrDefault(i => i.InventoryID == inventoryID);
            EditInveotryViewModel inventModel = new EditInveotryViewModel()
            {
                InventoryID = inventoryID,
                SizeID = invent.SizeID,
                SizeName = invent.SizeName,
                ProductID = invent.ProductID,
                ColorID = invent.ColorID,
                ColorName = invent.ColorName,
                InventoryAmount = invent.InventoryAmount,
                IsDeleted = invent.IsDeleted
            };
            
            #region Colors ComboBox

            var ColorsList = (from color in _context.ProductColors
                select new SelectListItem()
                {
                    Text = color.ColorName,
                    Value = color.ColorID.ToString(),
                }).ToList();

            ColorsList.Insert(0, new SelectListItem()
            {
                Text = "لطفا رنگ محصول را انتخاب کنید",
                Value = string.Empty
            });
            
            ViewBag.ListofColors = ColorsList;

            #endregion

            #region Sizes ComboBox

            var SizesList = (from size in _context.ProductSizes
                select new SelectListItem()
                {
                    Text = size.SizeTitle,
                    Value = size.SizeID.ToString(),
                }).ToList();

            SizesList.Insert(0, new SelectListItem()
            {
                Text = "لطفا سایز محصول را انتخاب کنید",
                Value = string.Empty
            });
            
            ViewBag.ListofSizes = SizesList;

            #endregion

            return View(inventModel);
        }
        else
        {
            return View();
        }
    }


    [HttpPost("edit-inventory/{inventoryID}")]
    public IActionResult EditInventory(int inventoryID, EditInveotryViewModel inventModel)
    {
        if (ModelState.IsValid)
        {
            var invent = _context.Inventories.SingleOrDefault(i => i.InventoryID == inventoryID);

            invent.InventoryAmount = inventModel.InventoryAmount;
            invent.SizeID = inventModel.SizeID;
            invent.SizeName = _context.ProductSizes.SingleOrDefault(s => s.SizeID == inventModel.SizeID).SizeTitle;
            invent.ColorID = inventModel.ColorID;
            invent.ColorName = _context.ProductColors.SingleOrDefault(c => c.ColorID == inventModel.ColorID).ColorName;
            
            _context.Inventories.Update(invent);
            _context.SaveChanges();

            return RedirectToAction("Index",new { ProductID = inventModel.ProductID });
        }
        else
        {
            return View();
        }
    }

    #endregion

    #region Remove Inventory

    public IActionResult RemoveInventory(int id)
    {
        var invent = _context.Inventories.SingleOrDefault(i => i.InventoryID == id && i.IsDeleted == false);

        if (invent == null)
        {
            return Json(new
            {
                status = "NotFound"
            });
        }
        else
        {
            invent.IsDeleted = true;
            _context.Inventories.Update(invent);
            _context.SaveChanges();
            return Json(new
            {
                status = "Success"
            });
        }
    }

    #endregion
}