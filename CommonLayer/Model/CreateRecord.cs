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
            Started,
            InProgress,
            Completed

        }

        public _TaskStatus? TaskStatus { get; set; }
        public enum _TaskPriority
        {
            Low,
            Medium,
            High
        }
        public _TaskPriority? TaskPriority { get; set; }

    }

    public class CreateRecordResponse { 
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }

}
