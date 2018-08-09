using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyient.MDT.WebAPI.Core.Entities.SubSystem
{
    public class EquipmentDocuments
    {
        public int DOCUMENT_ID { get; set; }
        public int EQUIPMENT_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string DOCUMENT_PATH { get; set; }
    }
}
