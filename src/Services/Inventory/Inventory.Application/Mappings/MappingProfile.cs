using AutoMapper;
using Inventory.Application.Common.Enums;
using Inventory.Application.Features.Products.Commands.AddProduct;
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
                .ForMember(d => d.Manufacturer, opts => opts.MapFrom(s => s.ManufacturerId != null ? Enum.GetName(typeof(ProductManufacturerEnum), s.ManufacturerId) : string.Empty))
                .ForMember(d => d.Reference, opts => opts.MapFrom(s => s.Reference))
                .ForMember(d => d.ExpirationDate, opts => opts.MapFrom(s => s.ExpirationDate))
                .ForMember(d => d.Supplier, opts => opts.MapFrom(s => s.SupplierId != null ? Enum.GetName(typeof(ProductSupplierEnum), s.SupplierId) : string.Empty))
                .ForMember(d => d.BasePrice, opts => opts.MapFrom(s => s.BasePrice))
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.Name))
                .ForMember(d => d.NumUnits, opts => opts.MapFrom(s => s.NumUnits))
                .ForMember(d => d.Type, opts => opts.MapFrom(s => s.TypeId != null ? Enum.GetName(typeof(ProductTypeEnum), s.TypeId) : string.Empty))
                ;

            CreateMap<AddProductCommand, Product>()
                .ForMember(d => d.ManufacturerId, opts => opts.MapFrom(s => s.Manufacturer != null ? (int)s.Manufacturer : (int?)null))
                .ForMember(d => d.ExpirationDate, opts => opts.MapFrom(s => s.ExpirationDate))
                .ForMember(d => d.Reference, opts => opts.MapFrom(s => s.Reference))
                .ForMember(d => d.UserCreated, opts => opts.MapFrom(s => s.UserCreated))
                .ForMember(d => d.BasePrice, opts => opts.MapFrom(s => s.BasePrice))
                .ForMember(d => d.Description, opts => opts.MapFrom(s => s.Description))
                .ForMember(d => d.MinStock, opts => opts.MapFrom(s => s.MinStock))
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.Name))
                .ForMember(d => d.NumUnits, opts => opts.MapFrom(s => s.NumUnits))
                .ForMember(d => d.ReceiptDate, opts => opts.MapFrom(s => s.ReceiptDate))
                .ForMember(d => d.Reference, opts => opts.MapFrom(s => s.Reference))
                .ForMember(d => d.SupplierId, opts => opts.MapFrom(s => s.Supplier != null ? (int)s.Supplier : (int?)null))
                .ForMember(d => d.TypeId, opts => opts.MapFrom(s => s.Type != null ? (int)s.Type : 0))
                ;

            CreateMap<Product, NewProductDTO>()
                .ForMember(d => d.Type, opts => opts.MapFrom(s => s.TypeId != null ? Enum.GetName(typeof(ProductTypeEnum), s.TypeId) : string.Empty))
                .ForMember(d => d.Reference, opts => opts.MapFrom(s => s.Reference))
                .ForMember(d => d.ExpirationDate, opts => opts.MapFrom(s => s.ExpirationDate))
                .ForMember(d => d.Id, opts => opts.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opts => opts.MapFrom(s => s.Name))
                ;
        }
    }
}
