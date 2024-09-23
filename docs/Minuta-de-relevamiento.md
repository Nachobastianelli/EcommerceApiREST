# 🗝️ Minuta de Relevamiento - Sistema de E-commerce

## 🧭 Contexto:

El sistema se enfoca en la creación de una plataforma de **e-commerce** que permita a los usuarios gestionar productos, realizar compras y administrar pedidos. Existen tres roles de usuario principales:

1. **Cliente:** puede navegar, añadir productos al carrito, realizar órdenes y ver el historial de compras.
2. **Editor:** puede añadir, eliminar y actualizar productos.
3. **Superadmin:** tiene control total sobre la gestión de usuarios y productos.

La plataforma también simula el proceso completo de una compra, desde la selección de productos hasta el estado final de envío.

## 🌱 Proceso Actual:

- **Carrito de Compras como Órdenes:**

  - El "carrito" no es un carrito real, sino una orden en estado **"Nueva"**.
  - Los usuarios pueden agregar/eliminar productos libremente hasta que la orden se confirme al presionar **"go to checkout"**, momento en el que pasa a estado **"Pendiente"**.
  - Después de la confirmación, los productos en la orden quedan bloqueados.
  - La transición de estados en esta fase será simulada hasta que la API soporte toda la funcionalidad.

- **Estados de la Orden:**

  1. **"Nueva":** La orden se crea cuando los productos se agregan al carrito.
  2. **"Pendiente":** Estado una vez que el carrito se confirma, simulando que el pago y los detalles de envío aún están por procesarse.
  3. **"Cancelada":** Puede ocurrir por:
     - Cancelación por parte del usuario.
     - Expiración del tiempo límite (10 minutos).
  4. **"Realizada":** Simulación inicial; en futuras versiones, implicará validación de pago y envío. Se genera una factura (e-invoice) y se descuenta el stock.

- **Generación de Facturas:** Una vez que una orden llega a **"Realizada"**, se genera una factura digital con los detalles del pedido.

## 🕹️ Funcionalidades:

1. **Usuario y Ordenes:**

   - Los usuarios pueden iniciar sesión/registrarse con **JWT**.
   - Las órdenes solo pueden crearse si el usuario está registrado.
   - Los usuarios pueden agregar/eliminar/vaciar productos mientras el estado de la orden esté en **"Nuevo"**.
   - Confirmar el carrito cambia el estado de la orden a **"Pendiente"**.
   - Si el pago se realiza, se genera una factura vinculada al usuario como parte de su historial de compras.

2. **Gestión de Productos:**

   - Solo los **Editores** pueden agregar, eliminar o actualizar productos.
   - Los productos tienen características como **color**, **tamaño**, y **disponibilidad**.

3. **Cancelación y Eliminación de la Orden:**

   - Si la compra es cancelada (por el usuario o por expiración), la orden se elimina y el carrito se vacía.

4. **Gestión de Usuarios:**
   - El **Superadmin** tiene el poder de gestionar y administrar usuarios según sea necesario.
