using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.MachineOperations.Queries.GetMachine
{
    
    public class GetMachineQery
    {
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public GetMachineQery(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<MachineViewModel> Handle()
        {
            var machine = _dbContext.machines.Include(x => x.ProductionStage).Where(x => x.IsDone == false).OrderBy(x => x.Id).ToList();
            List<MachineViewModel> vm = _mapper.Map<List<MachineViewModel>>(machine);            
            return vm;
        }
    }

    public class MachineViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? OrderDate { get; set; }
        public string? ProductionStage { get; set; }

    }
}