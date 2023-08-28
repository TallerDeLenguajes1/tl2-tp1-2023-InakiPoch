namespace Entities {
    public class Delivery {
        static int MAX_ORDERS = 5;
        uint id;
        string name;
        string address;
        uint cellphoneNumber;
        List<Order> ordersList;

        public Delivery(uint id, string name, string address, uint cellphoneNumber) {
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

        public bool IsFull() => ordersList.Count == MAX_ORDERS;
    }
}