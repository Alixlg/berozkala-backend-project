using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.Common
{
    public class RequestResultDTO<T>
    {
        public required bool IsSuccess { get; set; }
        public required int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Body { get; set; }
        public RequestResultDTO()
        {
            Thread.Sleep(700);
        }
    }
}