using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.SubSystem
{
    public class EquipmentCostHistory
    {
        public int EQUIPMENT_ID { get; set; }
        public double COST { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
