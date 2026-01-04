using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiBaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
       
        public ProductsController( IMapper mapper 
          ,
            IUnitOfWork unitOfWork)
        {
           
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]//in file IdentifyServicesExtensions 
        //[Authorize]//simple  
        [HttpGet]
        //not write retruen IActionResult because in mvc maybe  return  type error  in view not respond ,bad request ,,,,in api return in end point there is nothing else  
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productSpec)
        {
            var spec = new ProductSpecification(productSpec);
            var Products = await unitOfWork.Repository<Product>().GetAllWitSpecAsync(spec);
          
            var data  = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);

            var CountSpec = new ProductWithFilterationForCountSpecification(productSpec);
            //var Products = await productRepo.GetAllAsync();
            var Count=await unitOfWork.Repository<Product>().GetCountWithSpecAsync(CountSpec);
            return Ok(new Pagination<ProductToReturnDto>(productSpec.PageIndex,productSpec.PageSize,Count,data)); 
        }

        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductSpecification(id);
            var Product = await unitOfWork.Repository<Product>().GetByIdWitSpecAsync(spec);
            if (Product is null) return NotFound(new ApiErrorResponse(404));
            var MappedProduct = mapper.Map<Product, ProductToReturnDto>(Product);

            return Ok(MappedProduct);
        }
       
        
        [HttpGet("brands")]//fregment(brand to distinguishes between  it and all product 
        //api/products/brands
        public  async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var Brands = await unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(Brands);
        }
        
        
        [HttpGet("Types")]//fregment(brand to distinguishes between  it and all product 
        //api/products/types
        public async Task<ActionResult<IReadOnlyList<ProductType>>>GetAllTypes()
        {
            var Types = await unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(Types);
        }

    }
}
