using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModeBureauDB.Models
{
    public class IncomingTask
    {
        [Key]
        public int IncomingTaskId { get; set; }
        public string Costumer { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfDays { get; set; }
        public string Location { get; set; }
        public int NumberOfModels { get; set; }
        public string Comments { get; set; }
        public ICollection<ModelIncomingTask> ModelIncomingTasks { get; set; }
    }
}
