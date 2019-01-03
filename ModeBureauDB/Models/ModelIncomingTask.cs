using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModeBureauDB.Models
{
    public class ModelIncomingTask
    {
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int IncomingTaskId { get; set; }
        public IncomingTask IncomingTask { get; set; }
    }
}
