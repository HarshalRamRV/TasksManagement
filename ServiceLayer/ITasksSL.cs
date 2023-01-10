using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface ITasksSL 
    {
        public Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request);

    }
}
