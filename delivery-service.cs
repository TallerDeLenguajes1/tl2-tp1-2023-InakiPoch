namespace Entities {
    public class DeliveryService {
        string name;
        uint cellphoneNumber;
        List<Delivery> deliveriesList;
        List<Order> pendingOrders;

        public DeliveryService(string name, uint cellphoneNumber, List<Delivery> deliveriesList) {
            this.name = name;
            this.cellphoneNumber = cellphoneNumber;
            this.deliveriesList = deliveriesList;
            pendingOrders = new List<Order>();
        }

        public void CreateOrder(uint orderNumber, string observation, char orderStatus, string clientName, string clientAdress, uint clientNumber) {
            pendingOrders.Add(new Order(orderNumber, observation, orderStatus, clientName, clientAdress, clientNumber));
        }
        public void AssignOrder() {
            if(!DeliveriesRemain()) {
                return;
            }
            while(true) {
                int randomDelivery = (new Random()).Next(deliveriesList.Count);
                Delivery targetDelivery = deliveriesList[randomDelivery];
                if(!targetDelivery.IsFull() && pendingOrders.Any()) {
                    targetDelivery.RecieveOrder(pendingOrders[0]);
                    pendingOrders.Remove(pendingOrders[0]);
                    return;
                }
            }
        }

        private bool DeliveriesRemain() {
            foreach(Delivery delivery in deliveriesList) {
                if(delivery.IsFull()) return false;
            }
            return true;
        }
    }
}