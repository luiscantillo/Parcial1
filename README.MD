# MARKET-FACT: Sistema Gestor de Ventas e Inventario

**Desarrollado por:** Luis Miguel Cantillo Sarmiento

## Descripción del Proyecto
MARKET-FACT es una aplicación de consola desarrollada en C# diseñada para gestionar el inventario y las ventas diarias de un negocio. El sistema almacena los datos en memoria mediante el uso de listas paralelas e implementa un control robusto de excepciones para evitar cierres inesperados (crasheos) ante entradas inválidas del usuario. 

Las características principales incluyen:
* **Gestión de Inventario:** Registro de nuevos productos con validación de duplicados y consulta de stock disponible (incluye alertas automáticas para productos con menos de 5 unidades).
* **Procesamiento de Ventas:** Cálculo automático de subtotales, aplicación de descuentos opcionales para clientes frecuentes (10%), cálculo de IVA (19%) y actualización en tiempo real del stock.
* **Reportes y Estadísticas:** Generación de un cuadre de caja que detalla el total de ventas, ingresos totales, promedio por venta y el artículo más vendido del día.

## Instrucciones de Instalación y Ejecución

Para ejecutar este proyecto de forma local, asegúrate de tener instalado el [SDK de .NET](https://dotnet.microsoft.com/download).

1. **Clonar el repositorio:**
   Abre tu terminal o línea de comandos y ejecuta:
   ```bash
   git clone https://github.com/luiscantillo/Parcial1.git

2. **Navegar al directorio del proyecto:**

   ```bash
   cd Parcial1/1ERPARCIAL


3. **Ejecutar la aplicación.**
Inicia el sistema utilizando la herramienta de línea de comandos de .NET:

   ```bash
   dotnet run

4. ** Ejemplos de la ejecución del código. A continuación, se muestran algunas de las pantallas principales con las que interactúa el usuario al ejecutar el programa:

## Menú Principal

   ```bash
   ====================================================
   MARKET-FACT. TU SISTEMA GESTOR DE VENTAS E INVENTARIO
   ====================================================
   1. Registrar nuevo producto en inventario
   2. Consultar inventario completo
   3. Registrar una venta
   4. Ver reporte de caja y estadísticas diarias
   5. Salir
   ====================================================
   Ingresa la opción que desea ejecutar:

## Consulta de Inventario (con alerta de bajo stock)

   ```bash
   ====================================================
   INVENTARIO COMPLETO
   ====================================================
   1. Teclado Mecánico | Precio unitario (SIN IVA): $ 150.000,00 | Stock: 12
   2. Mouse Inalámbrico | Precio unitario (SIN IVA): $ 45.000,00 | Stock: 3 [ALERTA: ESTE ARTICULO TIENE BAJO STOCK]

   Presione cualquier tecla para volver al menú...


## Generación de Ticket de Venta


   ```bash
   ====================================================
   TICKET DE VENTA
   ====================================================
   Producto:        Teclado Mecánico (x2)
   Subtotal:        $ 300.000,00
   Descuento Cliente Frecuente (10%):-$ 30.000,00
   Valor IVA (19%):       +$ 51.300,00
   ---------------------------------------------------
   TOTAL A PAGAR:   $ 321.300,00

   Venta efectuada exitosamente. Stock actualizado: 10 unidades.

   Presione cualquier tecla para volver al menú...


## Reporte de Caja


   ```bash
   ====================================================
   REPORTE DE CAJA Y ESTADÍSTICAS
   ====================================================
   Total de ventas realizadas:  1
   Total ingresado a caja:      $ 321.300,00
   Promedio por venta:          $ 321.300,00
   Producto más vendido:        Teclado Mecánico (2 unidades)

   Presione cualquier tecla para volver al menú...