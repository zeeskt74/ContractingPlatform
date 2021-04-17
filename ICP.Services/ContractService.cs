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

        public IEnumerable<Contract> GetContracts(int mainContractorId)
        {
            return _contractRepo.GetByMainContractId(mainContractorId)
                                .Select(MapeDto);
        }

        public IEnumerable<Contract> GetAllContracts()
        {
            return _contractRepo.GetAll()
                                .Select(MapeDto);
        }

        public int AddContract(int mainContractorId, int relationContractorId)
        {
            return _contractRepo.Save(new Repositories.Dtos.Contract(mainContractorId, relationContractorId));
        }


        public dynamic GetShortestPath(int mainContractorId, int relationContractorId)
        {
            Queue<int> Q = new Queue<int>();
            HashSet<int> S = new HashSet<int>();
            HashSet<int> T = new HashSet<int>();
            Q.Enqueue(mainContractorId);
            S.Add(mainContractorId);
            T.Add(mainContractorId);

            int element = 0;
            while (Q.Count > 0)
            {
                if (element != mainContractorId)
                    T.Remove(element);

                element = Q.Dequeue();
                T.Add(element); //Add current


                foreach (var contract in _contractRepo.GetGraphByMainContractId(element))
                {
                    //stop and return when match if found
                    if (relationContractorId == contract.RelationContactor.Id)
                    {
                        T.Add(relationContractorId);
                        return T;
                    }

                    if (!S.Contains(contract.RelationContactor.Id))
                    {
                        Q.Enqueue(contract.RelationContactor.Id);
                        S.Add(contract.RelationContactor.Id);
                    }
                }
            }

            return null;
        }


        private Contract MapeDto(Repositories.Dtos.Contract contract)
        {
            return new Contract()
            {
                Id = contract.Id,
                MainContractorId = contract.MainContactor.Id,
                MainContractorName = contract.MainContactor.Name,

                RelationContractorId = contract.RelationContactor.Id,
                RelationalContractorName = contract.RelationContactor.Name
            };
        }
    }
}
