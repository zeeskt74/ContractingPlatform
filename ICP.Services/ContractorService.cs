using ICP.Repositories;
using ICP.Repositories.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.Services
{
    public class ContractorService: IContractorService
    {
        private readonly IContractorRepo _contractorRepo;
        public ContractorService(IContractorRepo contractorRepo)
        {
            _contractorRepo = contractorRepo;
        }

        public Contractor GetContractorById(int id)
        {
            return _contractorRepo.Get(id);
        }

        public int AddContractor(Contractor contractor)
        {

            return _contractorRepo.Save(contractor);
        }

        public int UpdateContractor(Contractor contractor)
        {
            return _contractorRepo.Save(contractor);
        }



        private string RandomlyPickStatus()
        {
            Random r = new Random();
            string[] statuses = new string[] { "60%", "20%", "m20%" };
            int randomIndex = r.Next(statuses.Length);
            return statuses[randomIndex];
        }

        private ContractorHealthStatus GetStatus(string value)
        {
            ContractorHealthStatus status = ContractorHealthStatus.Unknown;
            switch (value)
            {
                case "60%":
                    status = ContractorHealthStatus.Green;
                    break;
                case "20%":
                    status = ContractorHealthStatus.Yellow;
                    break;
                case "m20%":
                    status = ContractorHealthStatus.Red;
                    break;
            }
            return status;
        }
    }
}
