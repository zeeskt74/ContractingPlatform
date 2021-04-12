using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ICP.SQLite.Models
{
    public class Contract
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int MainContractorId { get; set; }

        [Required]
        public int RelationContractorId { get; set; }
    }
}
