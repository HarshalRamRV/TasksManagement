using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayar.Services;

namespace TasksManagement.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksRL _TasksRL;

        public TasksController(ITasksRL TasksRL)
        {
            _TasksRL = TasksRL;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecord(CreateRecordRequest request)
        {
            CreateRecordResponse response = new CreateRecordResponse();
            try {
                response = await _TasksRL.CreateRecord(request);
            }
            catch (Exception ex){
                response.IsSuccess = false;
                response.Message= "Exception Occ"+ex.Message;
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> GetRecord(GetRecordRequest request)
        {
            GetRecordResponse response = new GetRecordResponse();
            try
            {
                response = await _TasksRL.GetRecord(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecordById([FromQuery] string Id)
        {
            GetRecordByIdResponse response = new GetRecordByIdResponse();
            try
            {
                response = await _TasksRL.GetRecordById(Id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecord(CreateRecordRequest request)
        {
            UpdateRecordResponse response = new UpdateRecordResponse();
            try
            {
                response = await _TasksRL.UpdateRecord(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecord([FromQuery] string Id)
        {
            DeleteRecordResponse response = new DeleteRecordResponse();
            try
            {
                response = await _TasksRL.DeleteRecord(Id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }
    }
}