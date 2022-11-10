using KenoShop.WebApp.Entities.Account;
using KenoShop.WebApp.Entities.Order;
using KenoShop.WebApp.ViewModels.Order;

namespace KenoShop.WebApp.Repository.Interfaces;

public interface IOrderRepository
{
    Order GetUserOpenOrder(int userId);
    
    bool AddProductToOrder(int userId, int productId, int count);

    List<Order> GetOrders();

    List<User> GetUsersList();

    bool CreateOrder(OrderListViewModel orderList);

    bool EditOrder(OrderListViewModel orderList);

    bool RemoveOrder(int orderID);

    bool CreateOrderDetail(OrderDetailListViewModel orderDetailList);

    List<OrderDetail> GetOrderDetails(int orderID);
}