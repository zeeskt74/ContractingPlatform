using ICP.Repositories;
using ICP.Repositories.Dtos;
using ICP.Services.Models;
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

        public int AddContractor(ContractorVM contractor)
        {
            var c = new Contractor()
            {
                Name = contractor.Name,
                Phone = contractor.Phone,
                Type = ContractorType.Carrier, // Info is not provided which one should we select on save.
                HealthStatus = RandomlyPickStatus()
            };

            return _contractorRepo.Save(c);
        }

        //public int UpdateContractor(ContractorVM contractor)
        //{
        //    var c = new Contractor()
        //    {
        //        Id = contractor.Id,
        //        Name = contractor.Name,
        //        Phone = contractor.Phone,
        //        Type = ContractorType.Carrier, // Info is not provided which one should we select on save.
        //        HealthStatus = RandomlyPickStatus()
        //    };

        //    return _contractorRepo.Save(c);
        //}


        private ContractorHealthStatus RandomlyPickStatus()
        {
            Random r = new Random();            
            int perCent = r.Next(0, 100);
            return GetStatus(perCent);
        }

        /* This one is impelemented base on below assumptions
         * 
         *   Green = 60%  => if it's greater than or equal to 60% 
         *   Yellow = 20% => if it's greater than or equal to 20% and less than 60%
         *   Red = m20% => it's less than 20%             
         *
         */
        private ContractorHealthStatus GetStatus(int perCent)
        {
            ContractorHealthStatus status = ContractorHealthStatus.Unknown;
            if (perCent >= 60) 
            {
                status = ContractorHealthStatus.Green;
            }
            else if(perCent >= 20 && perCent < 60)
            {
                status = ContractorHealthStatus.Yellow;
            }
            else if(perCent < 20)
            {
                status = ContractorHealthStatus.Red;
            }

            return status;
        }
    }
}
