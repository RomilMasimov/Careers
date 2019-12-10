using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.Models.Enums;

namespace Careers.Services
{
    public class OrderSercice : IOrderService
    {
        private readonly CareersDbContext context;

        public OrderSercice(CareersDbContext context)
        {
            this.context = context;
        }

        public Task<Order> ChangeOrderStatus(Order order, OrderStateTypeEnum orderState)
        {
            order.State = orderState;
            return UpdateAsync(order);
        }

        public async Task<bool> DeleteAsync(Order order)
        {
            context.Orders.Remove(order);
        
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>> FindAllByClientAsync(int clientId)
        {
            return await context.Orders.Where(m => m.ClientId == clientId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> FindAllBySpecialistAsync(int specialistId)
        {
            return await context.Orders.Where(m => m.SpecialistId == specialistId).ToListAsync();
        }

        public async Task<Order> FindAsync(int id)
        {
            return await context.Orders.FindAsync(id);
        }

        public async Task<Order> InsertAsync(Order order)
        {
            order.Id = 0;
            var res = await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            var res = context.Orders.Update(order);
            await context.SaveChangesAsync();
            return res.Entity;
        }
    }
}
