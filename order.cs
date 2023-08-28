namespace Entities {
    public class Order {
        uint orderNumber;
        string observation;
        Client client;
        char orderStatus;

        public Order(uint orderNumber, string observation, char orderStatus, string clientName, string clientAdress, uint clientNumber) {
            this.orderNumber = orderNumber;
            this.observation = observation;
            this.orderStatus = orderStatus;
            client = new Client(clientName, clientAdress, clientNumber);
        }
    }
}