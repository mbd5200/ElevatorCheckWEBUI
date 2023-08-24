using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorCheckWEBUI.Core.DTO.Structure
{
    public class StructureDTO
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
    }
}
