using ElevatorCheckWEBUI.Core.DTO.Maintenance;
using ElevatorCheckWEBUI.Core.DTO.Structure;
using ElevatorCheckWEBUI.Core.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorCheckWEBUI.Core.Model
{
    public class MaintenanceViewModel
    {
        public List<MaintenanceDTO> Maintenances { get; set; }

        public List<StructureDTO> Structures { get; set; }

        public List<UserDTO> Users { get; set; }

    }
}
