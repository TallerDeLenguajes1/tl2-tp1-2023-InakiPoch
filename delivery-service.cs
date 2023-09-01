namespace Entities {
    public class DeliveryService {
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


        public void CreateOrder(uint orderNumber, string observation, Status orderStatus, string clientName, string clientAdress, uint clientNumber) {
            pendingOrders.Add(new Order(orderNumber, observation, orderStatus, clientName, clientAdress, clientNumber));
            totalOrders.Add(new Order(orderNumber, observation, orderStatus, clientName, clientAdress, clientNumber));
        }
        
        public void AssignOrder() {
            if(!DeliveriesRemain()) {
                Console.WriteLine("\nNo quedan cadetes disponibles\n");
                return;
            }
            while(true) {
                int randomDelivery = new Random().Next(deliveriesList.Count);
                Delivery targetDelivery = deliveriesList[randomDelivery];
                if(!targetDelivery.IsFull() && pendingOrders.Any()) {
                    targetDelivery.RecieveOrder(pendingOrders[0]);
                    pendingOrders.Remove(pendingOrders[0]);
                    return;
                }
            }
        }

        public void ChangeStatus(uint orderId) {
            Order targetOrder = totalOrders.Single(order => order.OrderNumber == orderId);
            Delivery targetDelivery = deliveriesList.Single(delivery => delivery.OrdersList.Any(order => order.OrderNumber == orderId));
            if(targetOrder.OrderStatus == Status.Completed) {
                Console.WriteLine("\nPedido ya completado\n");
                return;
            }
            targetDelivery.ChangeStatus(orderId);
            targetOrder.OrderStatus = Status.Completed;
        }

        public void ReasignOrder(uint orderId, string deliveryId, string previousDeliveryId) {
            Order targetOrder = totalOrders.Single(order => order.OrderNumber == orderId);
            Delivery targetDelivery = deliveriesList.Single(delivery => delivery.Id == deliveryId);
            Delivery previousDelivery = deliveriesList.Single(delivery => delivery.Id == previousDeliveryId);
            if(targetDelivery.IsFull()) {
                targetDelivery.RecieveOrder(targetOrder);
                previousDelivery.DeleteOrder(targetOrder);
                Console.WriteLine("\nPedido reasignado con exito!\n");
                return;
            }
            Console.WriteLine("\nNo se pudo reasignar el pedido\n");
        }

        public void GenerateReport() {
            var completedOrders = from order in totalOrders where order.OrderStatus == Status.Completed select order;
            int totalPayment = 0;
            Console.WriteLine("\n------INFORME------\n\n");
            Console.WriteLine("Pedidos pendientes: " + pendingOrders.Count);
            Console.WriteLine("Pedidos entregados: " + completedOrders.Count()); 
            Console.WriteLine("Pedidos totales: " + totalOrders.Count);
            foreach(Delivery delivery in deliveriesList) {
                totalPayment += delivery.TotalPayment();
            }
            Console.WriteLine("------------------\nTOTAL A PAGAR: " + totalPayment);
        }

        private bool DeliveriesRemain() {
            foreach(Delivery delivery in deliveriesList) {
                if(delivery.IsFull()) return false;
            }
            return true;
        }

        public List<Order> PendingOrders { get => pendingOrders;}
        public List<Order> TotalOrders { get => totalOrders;}
    }
}