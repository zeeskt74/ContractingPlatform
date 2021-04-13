using ICP.Repositories.Dtos;
using System.Collections.Generic;

namespace ICP.Repositories
{
    public interface IContractRepo
    {
        Contract Get(int id);
        List<Contract> GetByMainContractId(int id);
        List<Contract> GetGraphByMainContractId(int id);
        List<Contract> GetAll();
        int Save(Contract contract);
        int Remove(Contract contract);
    }
}