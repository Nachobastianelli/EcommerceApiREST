# 📋 Reglas de negocio

### 📦 **Relaciones entre Usuarios y Órdenes**

- Un **usuario** puede tener **ninguna** o **varias órdenes**.
- Una **orden** siempre está relacionada con un **usuario**.
- Un **usuario** puede agregar **uno** o **varios productos** a una **orden**.
- Una **orden** puede tener **uno** o **varios productos** asociados.

### 📝 **Reseñas y Productos**

- Un **usuario** puede dejar **ninguna** o **muchas reseñas** a un mismo **producto**.
- Un **producto** puede tener **ninguna** o **muchas reseñas**, tanto de **un solo** como de **varios usuarios**.

### 🧾 **Facturación**

- Un **usuario** puede tener **ninguna** o **varias facturas**.
- **Muchas facturas** pueden corresponder a un **único usuario**.

### 🔄 **Estados de Órdenes**

- Una **orden** tiene un **estado**, que puede ser uno de los siguientes:
  - **Nueva**
  - **Pendiente**
  - **Cancelada**
  - **Resuelta**
- Un **usuario** puede cambiar el estado de la orden a:
  - **Cancelada**
  - **Resuelta**
- El **sistema** puede cambiar el estado de una orden a **Cancelada** después de un período de inactividad.

---
