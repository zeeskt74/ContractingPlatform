using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.Services.Models
{
    public class Contract
    {
        public int Id { get; set; }

        public int MainContractorId { get; set; }
        public string MainContractorName { get; set; }

        public int RelationContractorId { get; set; }
        public string RelationalContractorName { get; set; }
    }
}
