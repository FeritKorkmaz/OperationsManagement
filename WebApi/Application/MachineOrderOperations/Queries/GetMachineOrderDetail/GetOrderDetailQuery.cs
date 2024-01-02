using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.MachineOrderOperations.Queries.GetMachineOrderDetail
{
    
    public class GetOrderDetailQuery
    {
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;
        public int OrderId { get; set; }
        public GetOrderDetailQuery(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<MachineOrderDetailViewModel> Handle()
        {
            var machineOrderDetail = _dbContext.machines.Include(x => x.ProductionStage).Where(x => x.MachineOrderId == OrderId).OrderBy(x => x.Id).ToList();
            List<MachineOrderDetailViewModel> vm = _mapper.Map<List<MachineOrderDetailViewModel>>(machineOrderDetail);
            return vm;
        }
    }

    public class MachineOrderDetailViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? OrderDate { get; set; }
        public string? ProductionStage { get; set; }

    }
}