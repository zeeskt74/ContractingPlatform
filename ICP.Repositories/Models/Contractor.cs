namespace ICP.Repositories.Models
{
    public class Contractor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public ContractorType Type { get; set; }
        public ContractorHealthStatus HealthStatus { get; set; }
    }
}
