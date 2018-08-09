using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.SubSystem
{
    public class EquipmentVariants
    {
        public bool SELECT { get; set; }
        public int PACKAGE_ID { get; set; }
        public int SYSTEM_ID { get; set; }
        public int EQUIPMENT_ID { get; set; }
        public int SUPPLIER_ID { get; set; }
        public int CONFIGURATION_ID { get; set; }
        public int UPDT_USER_ID { get; set; }
        public bool IS_MANDATORY { get; set; }
        public string EQUIPMENT_NAME { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public double EQUIPMENT_COST { get; set; }
        public int EQPMNT_VARIANT_ID { get; set; }
        public string COMMENTS { get; set; }
        public string EQUIPMENT_DESC { get; set; }

        public string EQUIPMENT_TYPE { get; set; }
        public IEnumerable<EquipmentCostHistory> CostHistories { get; set; }
        public IEnumerable<EquipmentDocuments> Documents { get; set; }
        public IEnumerable<EquipmentSAPLink> SAPLinks { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
    }
}
