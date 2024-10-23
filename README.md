# 🛒 E-Commerce API

## 📝 Descripción

Esta es una API-Rest para un sistema de e-commerce, diseñada con Clean Architecture para manejar la lógica de backend, la gestión de productos, usuarios y órdenes. Está construida con **ASP.NET Core**, y provee todos los servicios necesarios para que un frontend o aplicación móvil puedan interactuar con el sistema de manera eficiente.

### 🌟 Funcionalidades Principales

- **Autenticación y Autorización**: Gestión de usuarios con **ASP.NET Core Identity**.
- **Gestión de Productos**: Endpoints para agregar, editar, eliminar y listar productos.
- **Gestión de Órdenes**: Creación, modificación y consulta de órdenes de compra.
- **Facturación**: Generación de facturas una vez completada una orden.
- **Roles y Permisos**: Control de acceso según roles de usuario (clientes, editores, administradores).

## 🚀 Tecnologías Utilizadas

- **ASP.NET Core** con Identity
- **Base de datos**: SQLite (configurable a SQL Server)
- **Entity Framework Core**: Para la gestión de la base de datos.
- **JWT**: Autenticación basada en JSON Web Tokens.

## 📋 Endpoints 

### Authentication

| Método | Ruta                                     | Descripción                         |
|--------|------------------------------------------|-------------------------------------|
| POST   | /api/Authentication/authenticate         | Autentica a un usuario              |

### Order

| Método | Ruta                                             | Descripción                                             |
|--------|--------------------------------------------------|---------------------------------------------------------|
| GET    | /api/Order/{orderId}                             | Obtiene una orden por ID                                |
| GET    | /api/Order                                        | Obtiene todas las órdenes                               |
| DELETE | /api/Order                                        | Elimina todas las lineas de producto de una orden                               |
| GET    | /api/Order/GetAllOrdersForOneUser                 | Obtiene todas las órdenes de un usuario específico       |
| POST   | /api/Order/{productId}                            | Crea/actualiza una linea de producto para una orden específica         |
| DELETE | /api/Order/{productId}                            | Elimina un producto de una orden específica              |
| PUT    | /api/Order/UpdateOrdetToStatePending              | Actualiza una orden al estado pendiente                  |
| PUT    | /api/Order/ConfirmOrder/{orderId}                 | Confirma una orden por ID                                |
| PUT    | /api/Order/CancelOrder/{orderId}                  | Cancela una orden por ID                                 |

### Product

| Método | Ruta                                             | Descripción                                             |
|--------|--------------------------------------------------|---------------------------------------------------------|
| GET    | /api/Product/{id}                                | Obtiene un producto por ID                              |
| PUT    | /api/Product/{id}                                | Actualiza un producto por ID                            |
| DELETE | /api/Product/{id}                                | Elimina un producto por ID                              |
| POST   | /api/Product                                     | Crea un nuevo producto                                  |
| GET    | /api/Product/GetAll                              | Obtiene todos los productos                             |
| GET    | /api/Product/GetAvailable                        | Obtiene los productos disponibles                       |
| GET    | /api/Product/GetMoreCheaper                      | Obtiene el producto más barato                          |
| GET    | /api/Product/GetMostExpansive                    | Obtiene el producto más caro                            |
| GET    | /api/Product/GetByName/{name}                    | Obtiene un listado de productos por nombre                          |
| GET    | /api/Product/GetLittleQuantity                   | Obtiene productos con poca cantidad                     |
| PUT    | /api/Product/{id}/{quantity}                     | Actualiza la cantidad de un producto (+ / -)                   |
| GET    | /api/Product/GetW/Valorations/{id}               | Obtiene un producto con sus valoraciones por ID         |

### User

| Método | Ruta                                             | Descripción                                             |
|--------|--------------------------------------------------|---------------------------------------------------------|
| GET    | /api/User/{id}                                   | Obtiene un usuario por ID                               |
| DELETE | /api/User/{id}                                   | Elimina un usuario por ID                               |
| PUT    | /api/User/{id}                                   | Actualiza un usuario por ID                             |
| GET    | /api/User                                        | Obtiene todos los usuarios                              |
| POST   | /api/User                                        | Crea un nuevo usuario                                   |
| GET    | /api/User/profile                                | Obtiene el perfil del usuario autenticado               |
| GET    | /api/User/GetUserWEmail/{email}                  | Obtiene un usuario por correo electrónico               |
| PUT    | /api/User/role/{userId}                          | Actualiza el rol de un usuario por ID                   |

### Valoration

| Método | Ruta                                             | Descripción                                             |
|--------|--------------------------------------------------|---------------------------------------------------------|
| POST   | /api/Valoration/{productId}                      | Crea una nueva valoración para un producto por ID       |
| GET    | /api/Valoration                                  | Obtiene todas las valoraciones                          |
| GET    | /api/Valoration/{id}                             | Obtiene una valoración por ID                           |
| PUT    | /api/Valoration/{id}                             | Actualiza una valoración por ID                         |
| DELETE | /api/Valoration/{id}                             | Elimina una valoración por ID                           |

## 🛠️ Instalación y Ejecución

```bash
1. git clone https://github.com/nachobastianelli/EcommerceApiRest
2. cd EcommerceApiRest
3. dotnet restore
4. dotnet build
5. dotnet run
```

## 🔗 Link to Domain Diagram

<a href="https://excalidraw.com/#json=KKI9_XdSfjNk3ukuqBdVD,0PC_I-Adj7Wv4WIdirgZeA">✏Excalidraw📏</a>

###

<br/>
<br/>

<hr/>

<div align="center">
  
  📚 ***Más información del proyecto en la carpeta [DOCS](/docs)***
  
</div>




