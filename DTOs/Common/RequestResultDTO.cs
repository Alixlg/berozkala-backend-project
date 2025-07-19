using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace berozkala_backend.DTOs.Common
{
    public class RequestResultDTO
    {
        public required bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public Object? ResultBody { get; set; }
    }
}