# PUNTO 2A

## Pregunta 1

 Por composición se relacionan el Cliente con el Pedido. Por agregación se relacionan el Cadete con la Cadeteria, y el Cadete con el Pedido

## Pregunta 2

Para la clase Cadeteria:

* CrearPedido(Cliente, Cadete): Devuelve un Pedido a partir de un Cliente y un Cadete

* Pagar(Cadete): Deposita el monto adecuado al Cadete que haya entregado su pedido

* ReasignarCadete(Pedido): Reasigna un Pedido actual a un nuevo Cadete

* GenerarInforme(): Genera un informe de la actividad de la Cadeteria

Para la clase cadete:

* CambiarEstado(Pedido): Cambia el estado del Pedido actual

* RecibirPedido(Pedido): Agrega un nuevo Pedido a la lista de pedidos del cadete

* CancelarPedido(Pedido): Elimina el Pedido de la lista de pedidos del Cadete

## Pregunta 3

* Para la clase Cliente: Todos sus atributos deberian ser publicos pues son necesarios para la entrega del pedido

* Para la clase Pedidos: Todos los atributos deberian ser publicos para el Cadete, y el numero de pedido un atributo privado. Todos sus metodos deberian ser publicos

* Para la clase Cadete: Su ID, Direccion, y ListadoDePedidos deberian ser atributos privados, mientras que su Nombre y Telefono atributos publicos. Los metodos de la clase deberian ser privados excepto JornalACobrar para la clase Cadeteria

* Para la clase Cadeteria: Todos sus atributos y metodos deberian ser privados

## Pregunta 4

* Para la clase Cliente: Se inicializan todos sus atributos

* Para la clase Pedidos: Se inicializan todos sus atributos junto con el Cliente

* Para la clase Cadete: Se inicializan todos sus atributos, tal que el ListadoDePedidos sea una lista vacia inicialmente

* Para la clase Cadeteria: Se inicializan todos sus atributos

## Pregunta 5

No se me ocurre otra manera para el diseño de clases