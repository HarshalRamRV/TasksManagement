using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayar.Services
{
    public class TasksRL : ITasksRL
    {
        public readonly IConfiguration _configuration;
        public readonly ILogger<TasksRL> _logger;
        public readonly MySqlConnection _mySqlConnection;

        public TasksRL(IConfiguration configuration, ILogger<TasksRL> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlDBString"]);
        }

        public async Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request)
        {
            CreateRecordResponse response = new CreateRecordResponse();
            response.IsSuccess = true;
            response.Message = "Insert Record Successfully.";

            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"INSERT INTO taskdetails(Id,TaskTitle,TaskDescription,TaskDueDate,TaskStatus,TaskPriority)
                                    VALUES (@Id,@TaskTitle,@TaskDescription, @TaskDueDate, @TaskStatus, @TaskPriority)";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Id", request.Id);
                    sqlCommand.Parameters.AddWithValue("@TaskTitle", request.TaskTitle);
                    sqlCommand.Parameters.AddWithValue("@TaskDescription", request.TaskDescription);
                    sqlCommand.Parameters.AddWithValue("@TaskDueDate", String.IsNullOrEmpty(request.TaskDueDate) ? null : request.TaskDueDate);
                    sqlCommand.Parameters.AddWithValue("@TaskStatus", request.TaskStatus);
                    sqlCommand.Parameters.AddWithValue("@TaskPriority", request.TaskPriority);

                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Query Not Executed";
                        _logger.LogError("Error Occur : Query Not Executed");
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }

        public async Task<GetRecordResponse> GetRecord(GetRecordRequest request)
        {
            GetRecordResponse response = new GetRecordResponse();
            response.IsSuccess = true;
            response.Message = "Fetch Data Successfully.";

            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                string SqlQuery = string.Empty;


                SqlQuery = @" SELECT Id,TaskTitle, TaskDescription, TaskDueDate, TaskStatus, TaskPriority,
                                  (SELECT COUNT(*) FROM taskdetails) AS TotalTasks
                                  From taskdetails";



                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    using (DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            int Count = 0;
                            response.data = new List<GetRecord>();
                            while (await dataReader.ReadAsync())
                            {
                                response.data.Add(
                                    new GetRecord()
                                    {
                                        TaskId = dataReader["Id"] != DBNull.Value ? (Int32)dataReader["Id"] : -1,
                                        TaskTitle = dataReader["TaskTitle"] != DBNull.Value ? (string)dataReader["TaskTitle"] : null,
                                        TaskDescription = dataReader["TaskDescription"] != DBNull.Value ? (string)dataReader["TaskDescription"] : null,
                                        TaskDueDate = dataReader["TaskDueDate"] != DBNull.Value ? Convert.ToDateTime(dataReader["TaskDueDate"]).ToString("dd/MM/yyyy") : null,
                                        TaskStatus = dataReader["TaskStatus"] != DBNull.Value ? (GetRecord._TaskStatus)dataReader["TaskStatus"] : null,
                                        TaskPriority = dataReader["TaskPriority"] != DBNull.Value ? (GetRecord._TaskPriority)dataReader["TaskPriority"] : null,

                                    });
                                if (Count == 0)
                                {
                                    Count++;
                                    response.TotalTasks = dataReader["TotalTasks"] != DBNull.Value ? Convert.ToInt32(dataReader["TotalTasks"]) : -1;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }

        public async Task<GetRecordByIdResponse> GetRecordById(string Id)
        {
            GetRecordByIdResponse response = new GetRecordByIdResponse();
            response.IsSuccess = true;
            response.Message = "Get Record By Id Successfully";

            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * FROM taskdetails WHERE Id=@Id";//Id, CreatedDate, Record, ScheduleDateTime

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Id", Id);
                    using (DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            await dataReader.ReadAsync();
                            response.data = new CreateRecordRequest();
                            response.data.Id = dataReader["Id"] != DBNull.Value ? (Int32)dataReader["Id"] : -1;
                            response.data.TaskTitle = dataReader["TaskTitle"] != DBNull.Value ? (string)dataReader["TaskTitle"] : null;
                            response.data.TaskDescription = dataReader["TaskDescription"] != DBNull.Value ? (string)dataReader["TaskDescription"] : null;
                            response.data.TaskDueDate = dataReader["TaskDueDate"] != DBNull.Value ? Convert.ToString(dataReader["TaskDueDate"]) : null;
                            response.data.TaskStatus = dataReader["TaskStatus"] != DBNull.Value ? (CreateRecordRequest._TaskStatus?)Enum.Parse(typeof(CreateRecordRequest._TaskStatus), (string)dataReader["TaskStatus"]) : null;
                            response.data.TaskPriority = dataReader["TaskPriority"] != DBNull.Value ? (CreateRecordRequest._TaskPriority?)Enum.Parse(typeof(CreateRecordRequest._TaskPriority), (string)dataReader["TaskPriority"]) : null;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }

        public async Task<UpdateRecordResponse> UpdateRecord(CreateRecordRequest request)
        {
            UpdateRecordResponse response = new UpdateRecordResponse();
            response.IsSuccess = true;
            response.Message = "Update Record Successfully.";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"
                                    UPDATE tasks.taskdetails
                                    SET TaskTitle=@TaskTitle,
                                        TaskDescription=@TaskDescription,
                                        TaskDueDate=@TaskDueDate,
                                        TaskStatus=@TaskStatus,
                                        TaskPriority=@TaskPriority
                                    WHERE Id=@Id
                                    ";//Id, CreatedDate, Record, ScheduleDateTime

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Id", request.Id);
                    sqlCommand.Parameters.AddWithValue("@TaskTitle", request.TaskTitle);
                    sqlCommand.Parameters.AddWithValue("@TaskDescription", request.TaskDescription);
                    sqlCommand.Parameters.AddWithValue("@TaskDueDate", request.TaskDueDate);
                    sqlCommand.Parameters.AddWithValue("@TaskStatus", request.TaskStatus);
                    sqlCommand.Parameters.AddWithValue("@TaskPriority", request.TaskPriority);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Query Not Executed";
                        _logger.LogError("Error Occur : Query Not Executed");
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }

        public async Task<DeleteRecordResponse> DeleteRecord(string Id)
        {
            DeleteRecordResponse response = new DeleteRecordResponse();
            response.IsSuccess = true;
            response.Message = "Delete Record Successfully";

            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"DELETE FROM tasks.taskdetails WHERE Id=@Id";//Id, CreatedDate, Record, ScheduleDateTime

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Id", Id);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Query Not Executed";
                        _logger.LogError("Error Occur : Query Not Executed");
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return response;
        }


    }
}

