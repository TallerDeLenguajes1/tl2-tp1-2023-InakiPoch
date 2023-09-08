using ReportManager;

namespace Entities {
    public class DeliveryService {
        const int ORDER_PRICE = 500;
        string name;
        string cellphoneNumber;
        List<Delivery> deliveriesList;
        List<Order> pendingOrders;
        List<Order> totalOrders;

        public DeliveryService(string name, string cellphoneNumber, List<Delivery> deliveriesList) {
            this.name = name;
            this.cellphoneNumber = cellphoneNumber;
            this.deliveriesList = deliveriesList;
            pendingOrders = new List<Order>();
            totalOrders = new List<Order>();
        }

        public void CreateOrder(uint orderNumber, string observation, string clientName, string clientAdress, uint clientNumber) {
            var newOrder = new Order(orderNumber, observation, clientName, clientAdress, clientNumber);
            pendingOrders.Add(newOrder);
            totalOrders.Add(newOrder);
        }
        
        public bool AssignOrder() {
            if(!deliveriesRemain())
                return false;
            while(true) {
                int randomDelivery = new Random().Next(deliveriesList.Count);
                Delivery targetDelivery = deliveriesList[randomDelivery];
                if(canHaveOrders(targetDelivery)) {
                    pendingOrders[0].Delivery = targetDelivery;
                    targetDelivery.IncreaseCurrentOrders();
                    pendingOrders.Remove(pendingOrders[0]);
                }
            }
        }

        public bool ChangeStatus(uint orderId) {
            Order targetOrder = totalOrders.Single(order => order.OrderNumber == orderId);
            if(targetOrder.OrderStatus == Status.Completed) {
                return false;
            }
            targetOrder.OrderStatus = Status.Completed;
            return true;
        }

        public bool? ReasignOrder(uint orderId, string deliveryId) {
            Order targetOrder = totalOrders.Single(order => order.OrderNumber == orderId);
            Delivery targetDelivery = deliveriesList.Single(delivery => delivery.Id == deliveryId);
            if(!targetDelivery.IsFull()) {
                targetOrder.Delivery = targetDelivery;
                return true;
            }
            return false;
        }

        public int DeliveryPayment(string deliveryID) {
            var ordersList = from order in totalOrders where order.Delivery?.Id == deliveryID select order;
            var ordersCompleted = from order in ordersList where order.OrderStatus == Status.Completed select order;
            return ORDER_PRICE * ordersCompleted.Count();
        }

        public Report GenerateReport() {
            var completedOrders = from order in totalOrders where order.OrderStatus == Status.Completed select order;
            int totalPayment = 0;
            foreach(Delivery delivery in deliveriesList) {
                totalPayment += DeliveryPayment(delivery.Id);
            }
            return new Report(completedOrders, deliveriesList, pendingOrders, totalOrders, totalPayment);
        }

        private bool deliveriesRemain() {
            foreach(Delivery delivery in deliveriesList) {
                if(delivery.IsFull()) return false;
            }
            return true;
        }

        private bool canHaveOrders(Delivery delivery) => !delivery.IsFull() && pendingOrders.Any();

        public List<Order> PendingOrders { get => pendingOrders; }
        public List<Order> TotalOrders { get => totalOrders; }
        public List<Delivery> DeliveriesList { get => deliveriesList; }
        public string Name { get => name; }
        public string CellphoneNumber { get => cellphoneNumber; }
    }
}