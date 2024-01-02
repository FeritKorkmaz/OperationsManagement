using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.MachineOperations.Queries.GetMachine
{
    
    public class GetMachineCompleteQery
    {
        private readonly IOperationsManagmentDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetMachineCompleteQery(IOperationsManagmentDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<MachineCompleteViewModel> Handle()
        {
            var machine = _dbContext.machines.Include(x => x.ProductionStage).Where(x => x.IsDone == true).OrderBy(x => x.Id).ToList();
            List<MachineCompleteViewModel> vm = _mapper.Map<List<MachineCompleteViewModel>>(machine);
            return vm;
        }
    }

    public class MachineCompleteViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? OrderDate { get; set; }
        public string? ProductionStage { get; set; }
    }
}