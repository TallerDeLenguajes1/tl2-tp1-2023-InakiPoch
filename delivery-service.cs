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
        
        public void AssignOrder() {
            if(!deliveriesRemain()) {
                Console.WriteLine("\nNo quedan cadetes disponibles\n");
                return;
            }
            while(true) {
                int randomDelivery = new Random().Next(deliveriesList.Count);
                Delivery targetDelivery = deliveriesList[randomDelivery];
                if(canHaveOrders(targetDelivery)) {
                    pendingOrders[0].Delivery = targetDelivery;
                    targetDelivery.IncreaseCurrentOrders();
                    pendingOrders.Remove(pendingOrders[0]);
                    return;
                }
            }
        }

        public void ChangeStatus(uint orderId) {
            Order targetOrder = totalOrders.Single(order => order.OrderNumber == orderId);
            if(targetOrder.OrderStatus == Status.Completed) {
                Console.WriteLine("\nPedido ya completado\n");
                return;

            }
            targetOrder.OrderStatus = Status.Completed;
        }

        public void ReasignOrder(uint orderId, string deliveryId) {
            Order targetOrder = totalOrders.Single(order => order.OrderNumber == orderId);
            Delivery targetDelivery = deliveriesList.Single(delivery => delivery.Id == deliveryId);
            if(!targetDelivery.IsFull()) {
                targetOrder.Delivery = targetDelivery;
                Console.WriteLine("\nPedido reasignado con exito!\n");
                return;
            }
            Console.WriteLine("\nNo se pudo reasignar el pedido\n");
        }

        private int deliveryPayment(string deliveryID) {
            var ordersList = from order in totalOrders where order.Delivery?.Id == deliveryID select order;
            var ordersCompleted = from order in ordersList where order.OrderStatus == Status.Completed select order;
            return ORDER_PRICE * ordersCompleted.Count();
        }

        public void GenerateReport() {
            var completedOrders = from order in totalOrders where order.OrderStatus == Status.Completed select order;
            int totalPayment = 0;
            Console.WriteLine("\n------INFORME------\n");
            Console.WriteLine("Pedidos pendientes: " + pendingOrders.Count);
            Console.WriteLine("Pedidos entregados: " + completedOrders.Count()); 
            Console.WriteLine("Pedidos totales: " + totalOrders.Count);
            Console.WriteLine("\n-------MONTOS A PAGAR-------");
            foreach(Delivery delivery in deliveriesList) {
                Console.WriteLine($"Cadete {delivery.Id}: {deliveryPayment(delivery.Id)}");
                totalPayment += deliveryPayment(delivery.Id);
            }
            Console.WriteLine("\n------------------\nTOTAL A PAGAR: " + totalPayment);
        }

        private bool deliveriesRemain() {
            foreach(Delivery delivery in deliveriesList) {
                if(delivery.IsFull()) return false;
            }
            return true;
        }

        private bool canHaveOrders(Delivery delivery) => !delivery.IsFull() && pendingOrders.Any();

        public List<Order> PendingOrders { get => pendingOrders;}
        public List<Order> TotalOrders { get => totalOrders;}
    }
}