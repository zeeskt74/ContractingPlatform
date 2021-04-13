using ICP.Repositories;
using ICP.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Services
{
    public class ContractService: IContractService
    {
        private readonly IContractRepo _contractRepo;
        public ContractService(IContractRepo contractRepo)
        {
            _contractRepo = contractRepo;
        }

        public IEnumerable<Contract> GetContracts(int mainContractId)
        {
            return _contractRepo.GetByMainContractId(mainContractId)
                                .Select(MapeDto);
        }

        public int AddContract(int mainContractId, int relationContractId)
        {
            return _contractRepo.Save(new Repositories.Dtos.Contract(mainContractId, relationContractId));
        }


        private Contract MapeDto(Repositories.Dtos.Contract contract)
        {
            return new Contract()
            {
                Id = contract.Id,
                MainContractorName = contract.MainContactor.Name,
                RelationalContactName = contract.RelationContactor.Name
            };
        }

    }
}
