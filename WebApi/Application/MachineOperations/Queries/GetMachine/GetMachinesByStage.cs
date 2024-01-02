using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.MachineOperations.Queries.GetMachine
{
    
    public class GetMachinesByStage
    {
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public GetMachinesByStage(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<MachineViewModel> Handle(int stage_id)
        {
            var beforeStage = _dbContext.productionstages.FirstOrDefault(x => x.Id > stage_id);
            var machine = _dbContext.machines
            .Include(x => x.ProductionStage)
            .Where(x => x.ProductionStageId == stage_id || x.ProductionStageId == (beforeStage != null ? beforeStage.Id : 0))
            .OrderBy(x => x.ProductionStageId)
            .ThenBy(x => x.OrderDate)
            .ToList();
            List<MachineViewModel> vm = _mapper.Map<List<MachineViewModel>>(machine);            
            return vm;
        }
    }

    public class GetMachinesByStageViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? OrderDate { get; set; }
        public string? ProductionStage { get; set; }

    }
}