using KenoShop.WebApp.Context;
using KenoShop.WebApp.Entities.Order;
using KenoShop.WebApp.Repository.Interfaces;
using KenoShop.WebApp.ViewModels.Order;

namespace KenoShop.WebApp.Repository.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly KenoShopDbContext _context;

    public ProductRepository(KenoShopDbContext context)
    {
        _context = context;
    }
}