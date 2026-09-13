using System;
using System.Collections.Generic;

class Program
{
    // Declaro las listas donde voy a almacenar los nombres de producto, precios, stocks y 
    // la lista de cual producto tuvo mas ventas
    static List<string> nombresArticulo = new List<string>();
    static List<decimal> preciosArticulo = new List<decimal>();
    static List<int> stocksArticulo = new List<int>();
    static List<int> ventasPorProducto = new List<int>(); // Para saber cuál se vende más

    // Variables para el reporte de caja
    static int totalCantVentas = 0;
    static decimal totalIgresosCaja = 0m; 


//Empezamos con la definición de los metodos a utilizar para nuestras operaciones

// Metodo que lea los enteros y que capture errores si el usuario ingresa una opción invalida
// Se va a usar en las opciones de menú o para el stock

static int LeerEntero(string mensaje, int min, int max)
    {
        int numero;
        while (true)
        {
            Console.Write(mensaje);
            string entrada = Console.ReadLine()!;
            
            // Control contra chasheo. Si es un número válido Y está dentro del rango permitido, lo retorna
            if (int.TryParse(entrada, out numero) && numero >= min && numero <= max)
            {
                return numero;
            }
            Console.WriteLine($"Lo sentimos, esta es una opción invalida. Ingrese un número entre {min} y {max}.\n");
        }
    }

    // Metodo que lee los decimales con captura y control de error.
    //Se usará para los precios

static decimal LeerDecimal(string mensaje, decimal min)
    {
        decimal numero;
        while (true)
        {
            Console.Write(mensaje);
            // Control de errores para que el programa no se crashee por un dato invalido.
            if (decimal.TryParse(Console.ReadLine(), out numero) && numero >= min)
            {
                return numero;
            }
            Console.WriteLine($"Lo sentimos, esta es una opción invalida. Ingrese un valor mayor o igual a {min}.\n");
        }
    }


// Metodo usado para calcular la factura (El subtotal, el iva, el descuento si aplica y el total)

static decimal CalcularFactura(decimal precioArticulo, int cantidadArticulo, bool tieneDescuento, out decimal valorIva, out decimal valorDescuento)
    {
        //Subtotal y calculo de descuento sobre el subtotal
        decimal subtotal = precioArticulo * cantidadArticulo;
        valorDescuento = tieneDescuento ? subtotal * 0.10m : 0m;
        
        // El IVA (19%) se calcula sobre el subtotal menos el descuento
        valorIva = (subtotal - valorDescuento) * 0.19m;

        // Total a pagar
        return subtotal - valorDescuento + valorIva; 
    }
    

// Este metodo es para imprimir encabezados al momento de mostrar el menú o al mostrar la
// factura, de una forma organizada y limpia

static void ImprimirEncabezado(string titulo)
    {
        Console.Clear();
        Console.WriteLine("====================================================");
        Console.WriteLine($" {titulo.ToUpper()}"); //para que siempre esté en mayuscula
        Console.WriteLine("====================================================");
    }

// Metodo para indicar al usuario una instrucción luego de que un metodo de la opciones del menu
//termine de ejecutarse

static void PausarYRegresar()
{
    Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
    Console.ReadKey(); // El programa se congela aquí hasta que se presione una tecla
}

// Inicio del programa principal con el menú y los controles de errores y el llamado de los 
//metodos que haran los calculos de las opciones seleccionadas y que estarán despues del main

static void Main()
    {
        bool salir = false;
        do
        {

            try
            {

                ImprimirEncabezado("MARKET-FACT. TU SISTEMA GESTOR DE VENTAS E INVENTARIO");
                Console.WriteLine("1. Registrar nuevo producto en inventario");
                Console.WriteLine("2. Consultar inventario completo");
                Console.WriteLine("3. Registrar una venta");
                Console.WriteLine("4. Ver reporte de caja y estadísticas diarias");
                Console.WriteLine("5. Salir");
                Console.WriteLine("====================================================");
            
                int opcion = LeerEntero("Ingresa la opción que desea ejecutar: ", 1, 5);

            switch (opcion)
            {
                case 1: RegistrarProducto(); 
                break;

                case 2: ConsultarInventario(); 
                break;

                case 3: RegistrarVenta(); 
                break;

                case 4: //MostrarReporte(); 
                break;

                case 5: salir = true; 
                break;
            }

        }

        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR CRÍTICO] Ocurrió un problema inesperado: {ex.Message}");
            Console.WriteLine("El sistema se recuperó. Presione ENTER para volver al menú...");
            Console.ReadLine();
        }

        } while (!salir);

        Console.WriteLine("¡Gracias por utilizar MARKET-FACT! Hasta pronto.");
    }

// Metodos para toda la logica del sistema de facturación y el inventario

//Metodo que registra los productos

static void RegistrarProducto()
    {
        ImprimirEncabezado("Registrar Nuevo Producto");
        
        Console.Write("Nombre del producto: ");
        string nombre = Console.ReadLine()!;

        // Validar duplicados ignorando mayúsculas/minúsculas
        foreach (string n in nombresArticulo)
        {
            if (n.Equals(nombre, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Ya existe un producto con ese nombre. Intenta nuevamente");
                Console.ReadLine();
                return;
            }
        }

        decimal precio = LeerDecimal("Precio unitario ($): ", 0.01m);
        int stock = LeerEntero("Stock inicial: ", 0, 10000);

        // Como son listas paralelas, agregar en orden asegura que compartan el mismo índice
        nombresArticulo.Add(nombre);
        preciosArticulo.Add(precio);
        stocksArticulo.Add(stock);
        ventasPorProducto.Add(0); // Inicia con 0 ventas

        Console.WriteLine("\nProducto registrado con éxito.");
        PausarYRegresar();
    }

//Metodo para realizar la consulta del inventario

static void ConsultarInventario()
    {
        ImprimirEncabezado("Inventario Completo");

        if (nombresArticulo.Count == 0)
        {
            Console.WriteLine("No hay productos registrados en el inventario.");
        }
        else
        {
            for (int i = 0; i < nombresArticulo.Count; i++)
            {
                string alerta = stocksArticulo[i] < 5 ? " [ALERTA: ESTE ARTICULO TIENE BAJO STOCK]" : "";
                Console.WriteLine($"{i + 1}. {nombresArticulo[i]} | Precio: {preciosArticulo[i]:C} | Stock: {stocksArticulo[i]}{alerta}");
            }
        }

        PausarYRegresar();
    }

// Metodo para registra una venta de un articulo.

static void RegistrarVenta()
    {
        ImprimirEncabezado("Registrar Venta");
        if (nombresArticulo.Count == 0)
        {
            Console.WriteLine("No hay productos actualmente en el inventario para vender.");
            PausarYRegresar();
            return;
        }

        // Mostrar productos y pedir selección
        for (int i = 0; i < nombresArticulo.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {nombresArticulo[i]} | Precio: {preciosArticulo[i]:C} | Stock: {stocksArticulo[i]}");
        }

        int seleccion = LeerEntero($"\nSeleccione el número del producto (1-{nombresArticulo.Count}): ", 1, nombresArticulo.Count);
        int indice = seleccion - 1; // Restamos 1 porque las listas empiezan en 0

        if (stocksArticulo[indice] == 0)
        {
            Console.WriteLine("Lo sentimos, este producto se encuentra agotado.");
            PausarYRegresar();
            return;
        }

        int cantidad = LeerEntero("Ingrese la cantidad a comprar: ", 1, int.MaxValue);

        // Validación de stock
        if (cantidad > stocksArticulo[indice])
        {
            Console.WriteLine($"[ERROR] Stock insuficiente. Solo quedan {stocksArticulo[indice]} unidades.");
            PausarYRegresar();
            return;
        }

        Console.Write("¿Aplica descuento de cliente frecuente (10%)? (S/N): ");
        bool descuento = Console.ReadLine()!.Trim().ToUpper() == "S";

        // Llamada al método con parámetros out
        decimal iva, desc;
        decimal total = CalcularFactura(preciosArticulo[indice], cantidad, descuento, out iva, out desc);

        // Imprimir Ticket
        ImprimirEncabezado("Ticket de Venta");
        Console.WriteLine($" Producto:        {nombresArticulo[indice]} (x{cantidad})");
        Console.WriteLine($" Subtotal:        {(preciosArticulo[indice] * cantidad):C}");
        Console.WriteLine($" Descuento (10%):-{desc:C}");
        Console.WriteLine($" IVA (19%):       +{iva:C}");
        Console.WriteLine(" ---------------------------------------------------");
        Console.WriteLine($" TOTAL A PAGAR:   {total:C}");

        // Actualizar datos
        stocksArticulo[indice] -= cantidad;
        ventasPorProducto[indice] += cantidad;
        totalCantVentas++;
        totalIgresosCaja += total;

        Console.WriteLine($"\nVenta efectuada exitosamente. Stock actualizado: {stocksArticulo[indice]} unidades.");
        PausarYRegresar();
    }


//Metodo que imprime el reporte de ventas general.

static void MostrarReporte()
    {
        ImprimirEncabezado("Reporte de Caja y Estadísticas");

        if (totalCantVentas == 0)
        {
            Console.WriteLine("No se han realizado ventas hoy.");
        }
        else
        {
            decimal promedio = totalIgresosCaja / totalCantVentas;
            
            // Lógica para encontrar el producto más vendido
            int maxVentas = 0;
            string productoTop = "";
            for (int i = 0; i < nombresArticulo.Count; i++)
            {
                if (ventasPorProducto[i] > maxVentas)
                {
                    maxVentas = ventasPorProducto[i];
                    productoTop = nombresArticulo[i];
                }
            }

            Console.WriteLine($"Total de ventas realizadas:  {totalCantVentas}");
            Console.WriteLine($"Total ingresado a caja:      {totalIgresosCaja:C}");
            Console.WriteLine($"Promedio por venta:          {promedio:C}");
            Console.WriteLine($"Producto más vendido:        {productoTop} ({maxVentas} unidades)");
        }
        
        PausarYRegresar();
    }
}