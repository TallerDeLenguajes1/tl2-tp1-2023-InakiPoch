using Entities;
using Data;

internal class Program {
    private static void Main(string[] args) {
        List<DeliveryService>? servicesList = new List<DeliveryService>();
        List<Delivery>? workers = new List<Delivery>();
        Interface(servicesList, workers);
    }

    private static void Interface(List<DeliveryService>? servicesList, List<Delivery>? workers) {
        int option;
        uint orderNumber = 1;
        int fileOption;
        DataAdress data;
        Console.WriteLine("-----SERVICIO DE CADETERIA-----");
        Console.WriteLine("Elegir el tipo de acceso\n1-Archivo JSON\n2-Archivo CSV\n");
        var fileInput = Console.ReadLine();
        while(!int.TryParse(fileInput, out fileOption)) {
            Console.WriteLine("\nIngresar una opcion valida\n");
            fileInput = Console.ReadLine();
        }
        switch(fileOption) {
            case 1:
                data = new JSONData();
                data.CreateJSONFile("delivery-service-data.csv");
                data.CreateJSONFile("delivery-data.csv");
                workers = data.GetDeliveries();
                servicesList = data.GetService();
                break;
            case 2:
                data = new CSVData();
                workers = data.GetDeliveries();
                servicesList = data.GetService();
                break;
            default:
                Console.WriteLine("\nOpcion no encontrada\n");
                Environment.Exit(1);
                break;
        }
        if(servicesList == null) {
            Console.WriteLine("\nSe produjo un error al leer los datos de la cadeteria\n");
            return;
        }
        DeliveryService? service = servicesList[0];
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
        var report = service.GenerateReport();
        Console.WriteLine("\n------INFORME------\n");
        Console.WriteLine("Ordenes sin entregar: " + report.GetPendingOrdersCount());
        Console.WriteLine("Ordenes entregadas: " + report.GetCompletedOrdersCount());
        Console.WriteLine("Ordenes totales: " + report.GetTotalOrdersCount());
        Console.WriteLine("\n-------MONTOS A PAGAR-------");
        foreach(Delivery delivery in service.DeliveriesList) {
                Console.WriteLine($"Cadete {delivery.Id}: {service.DeliveryPayment(delivery.Id)}");
        }
        Console.WriteLine("----------------\nTOTAL A PAGAR: " + report.TotalPayment);
    }
}