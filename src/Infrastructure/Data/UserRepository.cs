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
            return _context.Set<User>()
                .Include(v =>v.Valorations)
                .FirstOrDefault(u => u.Email == email);
        }

        
    }
}
