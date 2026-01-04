using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.APIs.Helper
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(PD => PD.ProductBrand, O => O.MapFrom(P => P.ProductBrand.Name))
                .ForMember(PD => PD.ProductType, O => O.MapFrom(P => P.ProductType.Name))
                //i dont use reverse because I won't receive any data, I'll just switch from one product to ProducToReturntDto
          
                .ForMember(PD => PD.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());


            CreateMap<CustomerBsketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>(); CreateMap<BasketItemDto, BasketItem>();
            CreateMap<Talabat.Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto,Talabat.Core.Entities.Order_Aggregation.Address>();
            
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(D => D.DeliveryMethod, O => O.MapFrom(M => M.DeliveryMethod.ShortName))
                .ForMember(D => D.DeliveryMethodCost, O => O.MapFrom(M => M.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId,
                    o => o.MapFrom(s => s.ItemOrdered.ProductId))
                .ForMember(d => d.ProductName,
                    o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl,
                    o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl,
                    o => o.MapFrom<OrderPictureUrlResolver>());
                
                


        }

    }
}


// by convention he convert the 