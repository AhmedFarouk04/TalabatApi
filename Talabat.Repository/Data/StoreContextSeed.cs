using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregation;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    { 

        public static async Task SeedAsync(StoreContext dbContext)
        {
            if(!dbContext.ProductBrands.Any())//one element inside collection at least 
            {  //call brand ,type first
            var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                        await dbContext.ProductBrands.AddAsync(brand);

                    await dbContext.SaveChangesAsync();

                }
            }


            if (!dbContext.ProductTypes.Any())//one element inside collection at least 
            {  //call brand ,type first
                var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/Types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (Types?.Count > 0)
                {
                    foreach (var type in Types)
                        await dbContext.ProductTypes.AddAsync(type);

                    await dbContext.SaveChangesAsync();

                }
            }


            if (!dbContext.Products.Any())//one element inside collection at least 
            {  
                //call brand ,type first
                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                        await dbContext.Products.AddAsync(product);

                    await dbContext.SaveChangesAsync();

                }
            }



            if (!dbContext.DeliveryMethods.Any())//one element inside collection at least 
            {
                //call brand ,type first
                var MethodData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(MethodData);
                if (deliveryMethods?.Count > 0)
                {
                    foreach (var methods in deliveryMethods)
                        await dbContext.DeliveryMethods.AddAsync(methods);

                    await dbContext.SaveChangesAsync();

                }
            }



        }
    }
}
 