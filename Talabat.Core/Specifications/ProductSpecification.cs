using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public  class ProductSpecification:BaseSpecification<Product>
    {
        //where(p => p.productBrandId == brandId && P.ProductTypeId == typeId) two items have values
        //where(p => true && true)// two value are null 
        //where(p => p.productBrandId == brandId && true) brand only 
        //where(P => true && P.ProductTypeId == typeId) type only
        public ProductSpecification(ProductSpecParams productSpec) //Get
       : base(P=>
       (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search))&&
       (!productSpec.BrandId.HasValue|| P.ProductBrandId== productSpec.BrandId) &&
       (!productSpec.TypeId.HasValue || P.ProductTypeId == productSpec.TypeId))
       //|| short cicrit
       {

            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
            if (!string.IsNullOrEmpty(productSpec.Sort))
            {

                switch (productSpec.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }


            }

            //Total Product=100;
            //PageSize=10;
            //pageIndex=3
            ApplyPaginatiion(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);
        }
        public ProductSpecification(int id ):base(P=>P.Id==id) 

        { 

            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }

    }
}
