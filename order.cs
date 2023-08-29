namespace Entities {
    public enum Status { Pending, Completed }
    public class Order {
        uint orderNumber;
        string observation;
        Client client;
        Status orderStatus;

        public Order(uint orderNumber, string observation, Status orderStatus, string clientName, string clientAdress, uint clientNumber) {
            this.orderNumber = orderNumber;
            this.observation = observation;
            this.orderStatus = orderStatus;
            client = new Client(clientName, clientAdress, clientNumber);
        }

        public Status OrderStatus { get => orderStatus; set { orderStatus = value; } }
        public uint OrderNumber { get => orderNumber; }
    }
}