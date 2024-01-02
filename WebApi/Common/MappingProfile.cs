using AutoMapper;
using WebApi.Application.MachineOperations.Commands.UpdateMachine;
using WebApi.Application.MachineOperations.Queries.GetMachine;
using WebApi.Application.MachineOrderOperations.Commands.CreateMachineOrder;
using WebApi.Application.MachineOrderOperations.Commands.UpdateMachineOrder;
using WebApi.Application.MachineOrderOperations.Queries.GetMachineOrder;
using WebApi.Application.MachineOrderOperations.Queries.GetMachineOrderDetail;
using WebApi.Application.ProductionStageOperations.Commands.CreateProductionStage;
using WebApi.Application.ProductionStageOperations.Queries.GetProductionStage;
using WebApi.Entities;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Machine, MachineViewModel>()
                .ForMember(dest => dest.ProductionStage, opt => opt.MapFrom(src => src.ProductionStage != null ? src.ProductionStage.Name : string.Empty));
            CreateMap<Machine, MachineOrderDetailViewModel>()
                .ForMember(dest => dest.ProductionStage, opt => opt.MapFrom(src => src.ProductionStage != null ? src.ProductionStage.Name : string.Empty));
                




            CreateMap<UpdateMachineModel, Machine>();
            CreateMap<UpdateMachineStageModel, Machine>();

                

            CreateMap<CreateMachineOrderModel, MachineOrder>();
            
               
                

            CreateMap<CreateMachineOrderModel, Machine>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom (src => $"{src.CompanyName} {src.MachineType}"))
                .ForMember(dest => dest.ProductionStageId, opt => opt.MapFrom (src => 1));
            CreateMap<UpdateMachineOrderModel, Machine>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom (src => $"{src.CompanyName} {src.MachineType}"));   
            CreateMap<UpdateMachineOrderModel, MachineOrder>();
            CreateMap<MachineOrder, MachineOrderViewModel>()
                .ForMember(dest => dest.MachineName, opt => opt.MapFrom(src => src.Machines != null && src.Machines.Any() ? src.Machines.First().Name : string.Empty));

            CreateMap<CreateProductionStageModel, ProductionStage>();
            CreateMap<UpdateProductionStageModel, ProductionStage>();
            CreateMap<ProductionStage, ProductionStageViewModel>();


              
               
         
       
        }
    }
}