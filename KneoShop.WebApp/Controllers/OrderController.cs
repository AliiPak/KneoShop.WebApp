using KenoShop.WebApp.HttpExtensions;
using KenoShop.WebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KenoShop.WebApp.Controllers.Products;

public class OrderController : Controller
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("add-to-basket")]
    public IActionResult AddProductToOrder(int productID, int count)
    {
        if (!User.Identity.IsAuthenticated) return Json(new {status = "NotAuthenticated" });

        var result = _orderRepository.AddProductToOrder(User.GetCurrentUserId(), productID, count);

        if (result)
        {
            return Json(new {status = "Added"});
        }

        return Json(new {status = "NotAdded"});
    }
}