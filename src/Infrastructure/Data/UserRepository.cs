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
                .Include(v =>v.Valorations)
                .FirstOrDefault(u => u.Email == email);

            return query;
        }

        public override List<User> GetAll()
        {
            var query = _context.Set<User>()
                .Include(v => v.Valorations)
                .Include(o => o.Orders)
                .Include(i => i.Invoices)
                .ToList();

            return query;
        }

        public override User? GetById(int id)
        {
            var query = _context.Set<User>()
                .Include(v => v.Valorations)
                .Include(o => o.Orders)
                .Include(i => i.Invoices)
                .FirstOrDefault(u => u.Id == id);

            return query;


        }


    }
}
