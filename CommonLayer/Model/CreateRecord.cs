using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//password: @root123#123
namespace CommonLayer.Model
{
    public class CreateRecordRequest
    {
        public int Id { get; set; }
        [Required]
        public string? TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public string? TaskDueDate { get; set; }

        public enum _TaskStatus
        {
            Started = 1, InProgress = 2, Completed = 3

        }

        public _TaskStatus? TaskStatus { get; set; }
        public enum _TaskPriority
        {
            Low = 1, Medium = 2, High = 3
        }
        public _TaskPriority? TaskPriority { get; set; }

    }

    public class CreateRecordResponse { 
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }

}
