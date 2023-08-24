using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorCheckWEBUI.Core.DTO.Maintenance
{
    public class MaintenanceDTO
    {
        public Guid Guid { get; set; }

        public string LastMaintenaceDate { get; set; }

        public string RemainingDate { get; set; }

        public Guid StructureGUID { get; set; }

        public string StructureName { get; set; }
    }
}
