using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Contracts.ViewModels;
using TodoList.Domain.Services;
using TodoList.Domain.UnitOfWork;

namespace TodoList.Application.Services
{
    public class PriorityService : IPriorityService
    {
        private readonly ITodoManager _manager;

        public PriorityService(ITodoManager manager)
        {
            _manager = manager;
            _manager.DataConfiguration();
        }

        public async Task<IEnumerable<PriorityVM>> GetPriorities()
        {
            try
            {
                var result = _manager.PriorityRepository
                                 .GetQueryable(x => !x.IsDeleted)
                                 .Select(x => new PriorityVM()
                                 {
                                     PriorityId = x.PriorityId,
                                     Name = x.Name
                                 })
                                 .ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<PriorityVM> GetPriorityById(int priorityId)
        {
            try
            {
                var result = _manager.PriorityRepository
                                    .GetQueryable(x => !x.IsDeleted && 
                                                        x.PriorityId == priorityId)
                                    .Select(x => new PriorityVM()
                                    {
                                        PriorityId = x.PriorityId,
                                        Name = x.Name
                                    })
                                    .FirstOrDefault();

                if(result == null)
                {
                    return new PriorityVM();
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
