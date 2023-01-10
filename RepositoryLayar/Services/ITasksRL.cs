using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayar.Services
{
    public  interface ITasksRL
    {
        public Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request);
        public Task<GetRecordResponse> GetRecord(GetRecordRequest request);
        public Task<GetRecordByIdResponse> GetRecordById(string Id);
        public Task<UpdateRecordResponse> UpdateRecord(CreateRecordRequest request);
        public Task<DeleteRecordResponse> DeleteRecord(string Id);
    }
}
