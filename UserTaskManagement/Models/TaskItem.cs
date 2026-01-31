using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Models
{
    public class TaskItem
    {
        [JsonIgnore]     
        public int Id { get; set; }
        //validate property using data annotation also possoible but implemented in controller so commented
        //[Required(ErrorMessage = "Title is required")]
        //[StringLength(50,ErrorMessage = "Title shoud not exceed 50 characters")]
        public string Title { get; set; }
        public string Description { get; set; } 
        [EnumDataType(typeof(StatusTypes))]
        public string? Status { get; set; }
        [EnumDataType(typeof(PriorityTypes))]
        public string? Priority { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime CreatedAt { get; set; }
    }

    public enum StatusTypes
    {
        Pending,
        InProgress,
        Completed
    }

    public enum PriorityTypes
    {
        Low,
        Medium,
        High
    }
}
