using KenoShop.WebApp.HttpExtensions;
using KenoShop.WebApp.Repository.Interfaces;
using KenoShop.WebApp.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KenoShop.WebApp.Areas.Admin.Controllers;

public class OrderController : AdminBaseController
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("orders-list")]
    public IActionResult Index()
    {
        var orders = _orderRepository.GetOrders();
        return View(orders);
    }

    [HttpGet("order-details-list")]
    public IActionResult OrderDetailsList(int orderID)
    {
        var orderDetails = _orderRepository.GetOrderDetails(orderID);
        return View(orderDetails);
    }
    
    [HttpGet("add-to-basket")]
    public IActionResult AddProductToOrder(int productId, int count)
    {
        if (!User.Identity.IsAuthenticated) return Json(new { status = "NotAuthenticated" });

        var result = _orderRepository.AddProductToOrder(User.GetCurrentUserId(), productId, count);
        if (result)
        {
            return Json(new { status = "Added" });
        }

        return Json(new { status = "NotAdded" });
    }

    [HttpGet("create-order")]
    public IActionResult CreateOrder()
    {
        var usersList = (from user in _orderRepository.GetUsersList()
            select new SelectListItem()
            {
                Text = user.UserName,
                Value = user.UserID.ToString(),
            }).ToList();

        usersList.Insert(0, new SelectListItem()
        {
            Text = "لطفا کاربر محصول را انتخاب کنید",
            Value = string.Empty
        });
        
        ViewBag.ListOfUsers = usersList;
        
        return View();
    }

    [HttpPost("create-order-")]
    public IActionResult CreateOrder(OrderListViewModel orderList)
    {
        if (ModelState.IsValid)
        {
            var result = _orderRepository.CreateOrder(orderList);
            if (result)
            {
                return View();
            }

            return Json(new { status = "NotAdded" });
        }
        else
        {
            return View();
        }
    }

    [HttpGet("create-orderdetail")]
    public IActionResult CreateOrderDetail()
    {
        return View();
    }

    [HttpPost("create-orderdetail")]
    public IActionResult CreateOrderDetail(OrderDetailListViewModel orderDetailList)
    {
        if (ModelState.IsValid)
        {
            var result = _orderRepository.CreateOrderDetail(orderDetailList);
            if (result)
            {
                return Json(new { status = "Added" });
            }

            return Json(new { status = "NotAdded" });
        }
        else
        {
            return View();
        }
    }
}