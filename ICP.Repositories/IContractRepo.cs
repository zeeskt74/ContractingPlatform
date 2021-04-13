using ICP.Repositories.Dtos;
using System.Collections.Generic;

namespace ICP.Repositories
{
    public interface IContractRepo
    {
        Contract Get(int id);
        List<Contract> GetByMainContractId(int id);

        int Save(Contract contract);
        int Remove(Contract contract);
    }
}