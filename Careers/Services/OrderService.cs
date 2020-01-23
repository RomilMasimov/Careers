using Careers.EF;
using Careers.Models;
using Careers.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Careers.Models.Enums;
using BlogWebsite.Extensions;

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

        public async Task<bool> DeleteResponseAsync(OrderResponse response)
        {
            context.OrderResponses.Remove(response);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<Order> ChangeIsActiveOrderAsync(int orderId, bool isActive)
        {
            var order = await context.Orders.FindAsync(orderId);
            order.IsActive = isActive;
            return await UpdateAsync(order);
        }

        public async Task<IEnumerable<Order>> FindAllByClientAsync(int clientId)
        {
            return await context.Orders.Where(m => m.ClientId == clientId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> FindAllBySpecialistAsync(int specialistId)
        {
            var orderIds = await context.UserSpecialistMessages
                .Where(x => x.SpecialistId == specialistId)
                .Select(x => x.Order.Id)
                .ToListAsync();

            return context.Orders
                .Include(x => x.Specialist)
                .Include(x => x.Client)
                .Include(x => x.Service)
                .Where(x => orderIds.Contains(x.Id));
        }

        public async Task<bool> AddMeetingPoints(IEnumerable<OrderMeetingPoint> orderMeetingPoints)
        {
            await context.OrderMeetingPoints.AddRangeAsync(orderMeetingPoints);
            var rows = await context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<IEnumerable<OrderResponse>> FindAllResponseByOrderAsync(int orderId)
        {
            return await context.OrderResponses.Where(m => m.OrderId == orderId)
                .Include(m => m.Order)
                .ThenInclude(m => m.AnswerOrders)
                .Include(m => m.Order.Client)
                .ToListAsync();
        }



        public async Task<Order> FindAsync(int id, bool responses = false)
        {
            if (!responses) return await context.Orders.FindAsync(id);

            return await context.Orders.Include(x => x.OrderResponses)
                .ThenInclude(x => x.Specialist).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order> FindDetailedAsync(int id)
        {
            return await context.Orders
                .Include(x => x.OrderMeetingPoints)
                .ThenInclude(m => m.MeetingPoint)
                .Include(o => o.Measurement)
                .Include(m => m.UserSpecialistMessages)
                .ThenInclude(m => m.Specialist)
                .Include(m => m.AnswerOrders)
                .ThenInclude(m => m.Answer)
                .ThenInclude(m => m.Question)
                .Include(m => m.ClientAnswers)
                .ThenInclude(m => m.Question)
                .Include(m => m.Service)
                .Include(m => m.Specialist)
                .Include(m => m.Client)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Order> InsertAsync(Order order)
        {
            order.Id = 0;
            var res = await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<OrderResponse> InsertResponseAsync(OrderResponse response)
        {
            response.Id = 0;
            var res = await context.OrderResponses.AddAsync(response);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            var res = context.Orders.Update(order);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<OrderResponse> UpdateResponseAsync(OrderResponse response)
        {
            var res = context.OrderResponses.Update(response);
            await context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<IEnumerable<Order>> FindAllAsync()
        {
            return await context.Orders
                .Include(m => m.Service)
                .Include(m => m.Client)
                .Include(m => m.Specialist)
                .ToListAsync();
        }

        public async Task<bool> UpdateAnswerOrdersAsync(IEnumerable<int> answers, int orderId)
        {
            context.UpdateManyToMany(
                   context.AnswerOrders.Where(x => x.OrderId == orderId),
                   answers.Select(x => new AnswerOrder { OrderId = orderId, AnswerId = x }),
                   x => x.AnswerId);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Order>> FindAllForSpecialistAsync(string specialistAppUserId)
        {
            var specialist = await context.Specialists
                                .Include(m => m.SpecialistServices)
                                .Include(m => m.WhereCanGoList)
                                .Include(m => m.WhereCanMeetList)
                                .SingleOrDefaultAsync(m => m.AppUserId == specialistAppUserId);

            var canMeetListIds = specialist.WhereCanMeetList.Select(y => y.WhereCanMeetId).ToList();
            var canGoListIds = specialist.WhereCanGoList.Select(y => y.WhereCanGoId).ToList();

            var query = context.Orders
                .Where(m => m.IsActive &&
                           (m.State != OrderStateTypeEnum.Canceled &&
                            m.State != OrderStateTypeEnum.Finished &&
                            m.State != OrderStateTypeEnum.InProcess));

            //if (canMeetListIds.Any())
            //{
            //    query = query.Where(x => x.OrderMeetingPoints
            //        .Any(o => canMeetListIds
            //            .Any(y => y == o.MeetingPointId)));
            //}

            //if (canGoListIds.Any())
            //{
            //    query = query.Where(x => x.OrderMeetingPoints
            //        .Any(o => canGoListIds
            //            .Any(y => y == o.MeetingPointId)));
            //}

            if (!specialist.SpecialistServices.Any()) return await query.ToListAsync();

            query = query.Where(m => specialist.SpecialistServices
                .Select(x => x.ServiceId)
                .Any(x => x == m.ServiceId));

            return await query
                 .Include(m => m.Service)
                 .Include(m => m.Client)
                 .ToListAsync();
            #region
            //var specialist = await context.Specialists
            //                    .Include(m => m.SpecialistServices)
            //                    .Include(m => m.WhereCanGoList)
            //                    .Include(m => m.WhereCanMeetList)
            //                    .FirstOrDefaultAsync(m => m.AppUserId == specialistAppUserId);

            //var specialistMeetingPointsIds = specialist.WhereCanGoList.Select(y => y.WhereCanGoId).ToList();
            //specialistMeetingPointsIds.AddRange(specialist.WhereCanMeetList.Select(y => y.WhereCanMeetId));

            //var query = context.Orders.Where(m => m.IsActive == true && m.State == OrderStateTypeEnum.InSearchOfSpec && m.OrderMeetingPoints.Any(op => specialistMeetingPointsIds.Any(smp => smp == op.MeetingPointId)));
            //if (specialist.SpecialistServices.Any())
            //{
            //    query = query.Where(m => specialist.SpecialistServices.Select(x => x.ServiceId).Any(x => x == m.ServiceId));
            //    var orders = await query
            //        .Include(m => m.Service)
            //        .Include(m => m.Client)
            //        .ToListAsync();
            //    orders = orders.Where(m => specialist.SpecialistServices.Any(specServ => specServ.MeasurementId == m.MeasurementId &&
            //                specServ.PriceMin >= m.PriceMin && specServ.PriceMax >= m.PriceMax)).ToList();
            //    return orders; 
            //}
            //return new List<Order>();
            #endregion
        }

        public async Task<IEnumerable<Order>> FindAllForSpecialistByClientAsync(int specialistId, int clientId)
        {
            var specialist = await context.Specialists
                                .Include(m => m.SpecialistServices)
                                .SingleOrDefaultAsync(m => m.Id == specialistId);

            var specialistMeetingPointsIds = specialist.WhereCanGoList.Select(y => y.WhereCanGoId).ToList();
            specialistMeetingPointsIds.AddRange(specialist.WhereCanMeetList.Select(y => y.WhereCanMeetId));

            // TODO return m.IsActive == true
            var orderQuery = context.Orders
                .Where(m => m.IsActive &&
                            m.State == OrderStateTypeEnum.InSearchOfSpec &&
                            m.ClientId == clientId);

            if (!specialist.SpecialistServices.Any()) return new List<Order>();

            orderQuery = orderQuery.Where(m => specialist.SpecialistServices
                .Select(x => x.ServiceId)
                .Any(x => x == m.ServiceId));

            var orders = (await orderQuery
                    .Include(m => m.Service).ToListAsync())

                .Where(m => specialist.SpecialistServices
                    .Any(specServ => specServ.ServiceId == m.ServiceId))
                .ToList();

            return orders;

        }
    }
}
