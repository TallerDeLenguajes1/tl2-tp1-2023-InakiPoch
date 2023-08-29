using System.Dynamic;

namespace Entities {
    public class Delivery {
        static int MAX_ORDERS = 5;
        string id;
        string name;
        string address;
        string cellphoneNumber;
        List<Order> ordersList;

        public Delivery(string id, string name, string address, string cellphoneNumber) {
            this.id = id;
            this.name = name;
            this.address = address;
            this.cellphoneNumber = cellphoneNumber;
            ordersList = new List<Order>();
        }

        public void RecieveOrder(Order order) {
            if(!IsFull()) {
                ordersList.Add(order);
            }
        } 

        public int TotalPayment() {
            var completedOrders = from order in ordersList where order.OrderStatus == Status.Completed select order;
            return 500 * completedOrders.Count();
        }

        public void DeleteOrder(Order order) => ordersList.Remove(order);
        public bool IsFull() => ordersList.Count == MAX_ORDERS;
        public string Id { get => id; }
    }
}