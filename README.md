# 🛠️ E-Commerce API

## Descripción

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

## 📋 Endpoints Principales

| Método | Ruta                         | Descripción                                             |
|--------|------------------------------|---------------------------------------------------------|
| POST   | `/api/auth/register`          | Registro de nuevos usuarios                             |
| POST   | `/api/auth/login`             | Inicio de sesión y generación de token JWT              |
| GET    | `/api/products`               | Listado de todos los productos                          |
| POST   | `/api/products`               | Creación de un nuevo producto                           |
| PUT    | `/api/products/{id}`          | Actualización de un producto existente                  |
| DELETE | `/api/products/{id}`          | Eliminación de un producto                              |
| GET    | `/api/orders`                 | Listado de órdenes de compra del usuario autenticado    |
| POST   | `/api/orders`                 | Creación de una nueva orden                             |
| PUT    | `/api/orders/{id}`            | Actualización del estado de una orden                   |
| GET    | `/api/orders/{id}/invoice`    | Generar y obtener la factura de una orden pagada        |
