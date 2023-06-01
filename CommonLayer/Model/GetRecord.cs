using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class GetRecordRequest
    {
        [Required]
        public string? SortBy { get; set; } // ASC, DESC
    }

    public class GetRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public decimal TotalTasks { get; set; }

        public List<GetRecord> data { get; set; }

    }

    public class GetRecord
    {
        public int TaskId { get; set; }
        public string? TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public string? TaskDueDate { get; set; }

        public enum _TaskStatus
        {
            Started = 1,
            InProgress = 2,
            Completed = 3

        }

        public _TaskStatus? TaskStatus { get; set; }
        public enum _TaskPriority
        {
            Low = 1,
            Medium = 2,
            High = 3
        }
        public _TaskPriority? TaskPriority { get; set; }
    }
}
