# pryInventarioApp
GESTIÓN DE INVENTARIO - LABORATORIO DE PROGRAMACIÓN III

Autores: [PEREZ FEDERICO]

Materia: Laboratorio III - IES Siglo 21

Docente:  Juan Carlos Casale / Erica Bongiovanni

---------------------------------------------------
DESCRIPCIÓN
---------------------------------------------------

Aplicación desarrollada en C# con Windows Forms (.NET Framework 4.7) conectada a base de datos Access.

Permite la gestión de productos en un inventario pequeño, aplicando lo aprendido en las primeras semanas del TID.

---------------------------------------------------
FUNCIONALIDADES
---------------------------------------------------

Ver productos: muestra todos los productos registrados con su categoría.
Agregar productos: se pueden registrar nuevos productos cargando su nombre, descripción, precio, stock y categoría.
Modificar productos: permite actualizar los datos de un producto seleccionado.
Eliminar productos: elimina un producto del inventario con confirmación.
Reporte de inventario: genera un archivo "ReporteInventario.txt" automáticamente en la carpeta /bin/Debug.

---------------------------------------------------
BASE DE DATOS
---------------------------------------------------

Archivo: Inventario.mdb

Tablas:
- Categorias (IdCategoria, Nombre)
- Productos (Codigo, Nombre, Descripcion, Precio, Stock, IdCategoria)

Relación: Productos.IdCategoria → Categorias.IdCategoria

---------------------------------------------------
INSTRUCCIONES DE USO
---------------------------------------------------

1. Abrir el proyecto con Visual Studio 2022.
2. Verificar que el archivo "Inventario.mdb" esté en la ruta definida en el código.
3. Ejecutar la aplicación.
4. Utilizar los botones para agregar, modificar o eliminar productos.
5. Presionar "Generar Reporte" para guardar automáticamente el archivo de inventario en /bin/Debug.

---------------------------------------------------
NOTAS
---------------------------------------------------

- Esta aplicación utiliza Application.StartupPath para automatizar la ubicación de los archivos de reporte.
- Se manejan errores con try-catch y se valida la conexión a la base de datos con OleDb.
- Las categorías deben estar precargadas en la base para poder agregar productos.


     
