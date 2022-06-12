using AutoMapper;
using Inventory.Application.Common.Enums;
using Inventory.Application.Features.Products.Queries.GetAllProducts;
using Inventory.Domain.Entities;

namespace Inventory.Application.Mappings
{
    /// <summary>
    /// Class to manage object mapping
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(d => d.Manufacturer, opts => opts.MapFrom(s => Enum.GetName(typeof(ProductManufacturerEnum), s.ManufacturerId)))
                .ForMember(d => d.Reference, opts => opts.MapFrom(s => s.Reference))
                .ForMember(d => d.ExpirationDate, opts => opts.MapFrom(s => s.ExpirationDate))
                .ForMember(d => d.Supplier, opts => opts.MapFrom(s => Enum.GetName(typeof(ProductSupplierEnum), s.SupplierId)))
                .ForMember(d => d.BasePrice, opts => opts.MapFrom(s => s.BasePrice))
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.Name))
                .ForMember(d => d.NumUnits, opts => opts.MapFrom(s => s.NumUnits))
                .ForMember(d => d.Type, opts => opts.MapFrom(s => Enum.GetName(typeof(ProductTypeEnum), s.TypeId)))
                ;
        }
    }
}
