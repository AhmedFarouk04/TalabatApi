using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    public class BuggyController:ApiBaseController
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context)
        {
            this.context = context;
        }
        [HttpGet("notfound")]//api/buggy/notfound
        public ActionResult GetNotFoundRequest()
        {
            var product = context.Products.Find(100);
            if(product == null) return NotFound(new ApiErrorResponse(404) );

            return Ok(product);

            
        }

        [HttpGet("servererror")]//api/buggy/error
        public ActionResult GetServerError()
        {
            var product = context.Products.Find(100);
            var producttoReturn=product.ToString(); //will throw Exception
            return Ok(product);


        }

        [HttpGet("badRequest")]//api/buggy/badRequest
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(400));


        }

        [HttpGet("badRequest/{id}")]//api/buggy/badRequest/five
        public ActionResult GetBadRequest(int id)
        {
            return Ok();


        }


    }
}
