using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Response
{
    public class GenericResponse<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Result { get; set; }
        public ErrorResult ErrorResult { get; set; }

        public static GenericResponse<T> SuccessResponse(T result,int statusCode, string message = "Ok")
        {
            return new GenericResponse<T> { Result = result, StatusCode = statusCode, Message = message };
        }

        public static GenericResponse<T> ErrorResponse(ErrorResult error, int statusCode, string message="Fail")
        {
            return new GenericResponse<T> { ErrorResult = error, StatusCode = statusCode, Message = message};
        }
    }
}
