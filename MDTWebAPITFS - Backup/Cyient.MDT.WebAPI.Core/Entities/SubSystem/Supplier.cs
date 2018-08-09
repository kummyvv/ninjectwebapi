using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.SubSystem
{
    public class Supplier
    {
        public int SUPPLIER_ID { get; set; }
        public int EQUIPMENT_ID { get; set; }
        public int SYSTEM_ID { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public double COST { get; set; }
        public DateTime VALID_FROM { get; set; }
        public DateTime VALID_TO { get; set; }
    }
}
