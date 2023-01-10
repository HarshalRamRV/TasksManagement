using CommonLayer.Model;
using RepositoryLayar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class TasksSL : ITasksSL
    {
        private readonly ITasksRL _TasksRL;

        public TasksSL(ITasksRL TasksRL)
        {
            _TasksRL = TasksRL;
        }
        public async Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request)
        {
            return await _TasksRL.CreateRecord(request);
        }
    }
}
