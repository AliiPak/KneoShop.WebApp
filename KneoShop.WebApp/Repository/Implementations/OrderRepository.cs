using KenoShop.WebApp.Context;
using KenoShop.WebApp.Entities.Account;
using KenoShop.WebApp.Entities.Order;
using KenoShop.WebApp.Repository.Interfaces;
using KenoShop.WebApp.ViewModels.Order;

namespace KenoShop.WebApp.Repository.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly KenoShopDbContext _context;
    private IOrderRepository _orderRepositoryImplementation;

    public OrderRepository(KenoShopDbContext context)
    {
        _context = context;
    }

    public List<Order> GetOrders()
    {
        var orders = _context.Orders.ToList();
        return orders;
    }

    public List<User> GetUsersList()
    {
        var User = _context.Users.Where(u => u.IsActive == true && u.IsDeleted == false).ToList();
        return User;
    }
    public bool CreateOrder(OrderListViewModel orderList)
    {
        Order order = new Order()
        {
            UserID = orderList.UserID,
            RefCode = orderList.RefCode,
            CreateDate = DateTime.Now,
            Description = orderList.Description,
            PaymentDate = orderList.PaymentDate
        };

        _context.Orders.Add(order);
        _context.SaveChanges();

        return true;
    }

    public bool EditOrder(OrderListViewModel orderList)
    {
        throw new NotImplementedException();
    }


    public bool CreateOrderDetail(OrderDetailListViewModel orderDetailList)
    {
        var order = _context.Orders.SingleOrDefault(o => o.OrderID == orderDetailList.OrderID);

        if (order != null)
        {
            OrderDetail orderDetail = new OrderDetail()
            {
                OrderID = orderDetailList.OrderID,
                Count = orderDetailList.Count,
                Price = orderDetailList.Price,
                ProductID = orderDetailList.ProductID
            };

            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();

            return true;
        }
        else
        {
            return false;
        }
    }

    public List<OrderDetail> GetOrderDetails(int orderID)
    {
        var ordersDetails = _context.OrderDetails.Where(od => od.OrderID == orderID).ToList();
        return ordersDetails;
    }

    public bool EditOrderDetail(OrderDetailListViewModel orderDetailList)
    {
        var orderExists = _context.Orders.Any(o => o.OrderID == orderDetailList.OrderID);

        var orderDetail =
            _context.OrderDetails.SingleOrDefault(od => od.OrderDetailID == orderDetailList.OrderDetailID);

        if (orderExists && orderDetail != null)
        {
            orderDetail.Price = orderDetailList.Price;
            orderDetail.Count = orderDetailList.Count;
            orderDetail.ProductID = orderDetailList.ProductID;

            _context.OrderDetails.Update(orderDetail);
            _context.SaveChanges();

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool RemoveOrder(int orderID)
    {
        return _orderRepositoryImplementation.RemoveOrder(orderID);
    }

    public Order GetUserOpenOrder(int userId)
    {
        var openOrder = _context.Orders.SingleOrDefault(o => o.UserID == userId && o.PaymentDate == null);

        if (openOrder == null)
        {
            openOrder = new Order()
            {
                UserID = userId,
                CreateDate = DateTime.Now
            };

            _context.Orders.Add(openOrder);
            _context.SaveChanges();
        }

        return openOrder;
    }

    public bool AddProductToOrder(int userId, int productId, int count)
    {
        var userOpenOrder = GetUserOpenOrder(userId);
        var details = _context.OrderDetails.Where(od => od.OrderID == userOpenOrder.OrderID).ToList();
        var orderDetail = details.SingleOrDefault(od => od.ProductID == productId);
        if (orderDetail != null)
        {
            orderDetail.Count += count;
            _context.OrderDetails.Update(orderDetail);
            _context.SaveChanges();
        }
        else
        {
            var newDetail = new OrderDetail
            {
                OrderID = userOpenOrder.OrderID,
                ProductID = productId,
                Count = count
            };

            _context.OrderDetails.Add(newDetail);
            _context.SaveChanges();
        }

        return true;
    }
}