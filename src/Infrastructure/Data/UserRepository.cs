using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context) {}

        public User GetByEmail(string email)
        {
            var query = _context.Set<User>()
                .Include(v => v.Valorations)
                .Include(o => o.Orders)
                    .ThenInclude(o => o.OrderLines)
                .Include(o => o.Orders)
                    .ThenInclude(o => o.Address)
                .Include(o => o.Orders)
                    .ThenInclude(o => o.Invoice)
                .FirstOrDefault(u => u.Email == email);

            return query;
        }

        public override List<User> GetAll()
        {
            var query = _context.Set<User>()
                .Include(v => v.Valorations)
                .Include(o => o.Orders)
                    .ThenInclude(o => o.OrderLines)
                .Include(o => o.Orders)
                    .ThenInclude(o => o.Address)
                .Include(o => o.Orders)
                    .ThenInclude(o => o.Invoice)
                .ToList();

            return query;
        }

        public override User? GetById(int id)
        {
            var query = _context.Set<User>()
                .Include(v => v.Valorations)
                .Include(o => o.Orders)
                    .ThenInclude(o => o.OrderLines)
                .Include(o => o.Orders)
                    .ThenInclude(o => o.Address)
                .Include(o => o.Orders)
                    .ThenInclude(o => o.Invoice)
                .FirstOrDefault(u => u.Id == id);

            return query;


        }


    }
}
