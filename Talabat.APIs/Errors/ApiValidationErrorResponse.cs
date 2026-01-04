using Talabat.APIs.Errors;

namespace Talabat.APIs.Errors
{
    public class ApiValidationErrorResponse : ApiErrorResponse // Status Code => Message
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiValidationErrorResponse() : base(400)
        {
            Errors = new List<string>();
        }
    }
}
