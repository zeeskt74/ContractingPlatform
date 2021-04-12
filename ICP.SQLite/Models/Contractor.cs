using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ICP.SQLite.Models
{
    public class Contractor
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(10)]
        public string Type { get; set; }

        [StringLength(6)]
        public string HealthStatus { get; set; }

        public List<Contract> Contracts { get; set; }
    }
}
