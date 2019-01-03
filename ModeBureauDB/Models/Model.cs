using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModeBureauDB.Models
{
    public class Model
    {
        [Key]
        public int modelId { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string HairColour { get; set; }
        public string Comment { get; set; }
        public ICollection<ModelIncomingTask> ModelIncomingTasks { get; set; }
    }
}
