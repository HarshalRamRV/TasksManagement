using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class GetRecordByIdResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public CreateRecordRequest? data { get; set; }
    }
}
