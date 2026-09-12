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
            Console.WriteLine($"o sentimos, esta es una opción invalida. Ingrese un valor mayor o igual a {min}.\n");
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

    

    }