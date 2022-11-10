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
        return View();
    }
}