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

        public void ChangeStatus(uint orderId) {
            Order targetOrder = ordersList.Single(order => order.OrderNumber == orderId);
            targetOrder.OrderStatus = Status.Completed;
        }

        public void DeleteOrder(Order order) => ordersList.Remove(order);
        public bool IsFull() => ordersList.Count == MAX_ORDERS;
        public int TotalPayment() => 500 * ordersList.Count(order => order.OrderStatus == Status.Completed);
        public string Id { get => id; }
        public List<Order> OrdersList { get => ordersList; }
    }
}