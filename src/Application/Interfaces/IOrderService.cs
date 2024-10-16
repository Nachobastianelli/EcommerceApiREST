﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Order GetById(int id);
        List<Order> GetAll();
        Order Create (string userId , Order order);
        void Delete (int id);
        void Update (int id,Order order);
        void AddToCart(string userId, int productId);
        void RemoveToCart(string userId, int productId);
    }
}
