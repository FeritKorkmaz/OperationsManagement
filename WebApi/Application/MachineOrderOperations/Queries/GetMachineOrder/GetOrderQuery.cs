using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.MachineOrderOperations.Queries.GetMachineOrder
{
    
    public class GetOrderQuery
    {
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetOrderQuery(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<MachineOrderViewModel> Handle()
        {
            var machineOrder = _dbContext.machineorders.Include(x => x.Machines).Where(x => x.IsDone == false).OrderBy(x => x.Id).ToList();
            List<MachineOrderViewModel> vm = _mapper.Map<List<MachineOrderViewModel>>(machineOrder);
            return vm;
        }
    }

    public class MachineOrderViewModel
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public int  OrderQuantity { get; set; }
        public string? OrderDate { get; set; }
        public string? MachineName { get; set; }
    }
}