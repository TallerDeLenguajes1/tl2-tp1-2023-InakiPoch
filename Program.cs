using Entities;

internal class Program {
    private static void Main(string[] args) {
        DeliveryService service;
        List<Delivery> workers = new List<Delivery>();
        using(var reader = new StreamReader("delivery-data.csv")) {
            while(!reader.EndOfStream) {
                string? line = reader.ReadLine();
                if(line != null) {
                    var splits = line.Split(';');
                    workers.Add(new Delivery(splits[0], splits[1], splits[2], splits[3]));
                }
            }
        }
        using(var reader = new StreamReader("delivery-service-data.csv")) {
            string? line = reader.ReadLine();
            if(line != null) {
                var splits = line.Split(';');
                service = new DeliveryService(splits[0], splits[1], workers);
                Interface(service);
            }
        }
    }

    private static void Interface(DeliveryService service) {
        int option;
        uint orderNumber = 1;
        do {
            Console.WriteLine("------MENU------\n");
            Console.WriteLine("Pedidos pendientes: " + service.PendingOrders.Count);
            Console.WriteLine("Pedidos totales: " + service.TotalOrders.Count);
            Console.WriteLine("\n1-Crear Pedido\n2-Asignar a cadete\n3-Cambiar estado de pedido\n4-Reasignar pedido\n5-Salir\n");
            var key = Console.ReadLine();
            while(!int.TryParse(key, out option)) {
                Console.WriteLine("\nIngresar una opcion valida\n");
                key = Console.ReadLine();
            }
            switch(option) {
                case 1: 
                    uint clientCellphone;
                    Console.WriteLine("\n-----CREACION DE PEDIDO-----\n");
                    Console.WriteLine("Descripcion del pedido: ");
                    var observation = Console.ReadLine();
                    while(observation == null) {
                        Console.WriteLine("\nIngresar una descripcion valida: ");
                        observation = Console.ReadLine();
                    }
                    Console.WriteLine("Nombre del cliente: ");
                    var clientName = Console.ReadLine();
                    while(clientName == null) {
                        Console.WriteLine("\nIngresar un nombre valido: ");
                        clientName = Console.ReadLine();
                    }
                    Console.WriteLine("Direccion del cliente: ");
                    var clientAdress = Console.ReadLine();
                    while(clientAdress == null) {
                        Console.WriteLine("\nIngresar una direccion valida: ");
                        clientAdress = Console.ReadLine();
                    }
                    Console.WriteLine("Numero del cliente: ");
                    var input = Console.ReadLine();
                    while(!uint.TryParse(input, out clientCellphone)) {
                        Console.WriteLine("\nIngresar un numero valido: ");
                        input = Console.ReadLine();
                    }
                    Console.WriteLine("\nPedido creado con exito\n");
                    service.CreateOrder(orderNumber, observation, clientName, clientAdress, clientCellphone);
                    orderNumber++;
                    break;
                case 2:
                    Console.WriteLine("\nPedido asignado a cadete!\n");
                    service.AssignOrder();
                    break;
                case 3:
                    uint id;
                    Console.WriteLine("\n-----CAMBIO DE ESTADO DE PEDIDO-----\n");
                    Console.WriteLine("Ingresar la ID del pedido: ");
                    var idInput = Console.ReadLine();
                    while(!uint.TryParse(idInput, out id)) {
                        Console.WriteLine("\nIngresar un numero valido: ");
                        idInput = Console.ReadLine();
                    }
                    service.ChangeStatus(id);
                    break;
                case 4:
                    Console.WriteLine("\n-----REASIGNADO DE DE PEDIDO-----\n");
                    Console.WriteLine("Ingresar la ID del pedido: ");
                    idInput = Console.ReadLine();
                    while(!uint.TryParse(idInput, out id)) {
                        Console.WriteLine("\nIngresar un numero valido: ");
                        idInput = Console.ReadLine();
                    }
                    Console.WriteLine("Ingresar la ID del cadete a reasignar el pedido: ");
                    var deliveryId = Console.ReadLine();
                    while(deliveryId == null) {
                        Console.WriteLine("\nIngresar una direccion valida: ");
                        deliveryId = Console.ReadLine();
                    }
                    service.ReasignOrder(id, deliveryId);
                    break;
                case 5: break;
                default:
                    Console.WriteLine("\nIngresar una opcion valida\n");
                    break;
            }
        } while(option != 5);
        service.GenerateReport();
    }
}