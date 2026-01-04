namespace Talabat.APIs.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }


        public ApiErrorResponse(int StatusCode, string? message = null)
        {
            this.StatusCode = StatusCode;
            Message = message ?? GetDefaultMessageOrStatusCode(StatusCode);

        }
        private string? GetDefaultMessageOrStatusCode(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "A bad Request, You have Made",
                401 => "You are not authorized",
                404 => "Resources Not Found",
                500 => "There is a Server Error",
                _ => null

            };
        }

    }
}
