using ICP.Repositories.Dtos;
using System;
using System.Collections.Generic;

namespace ICP.Repositories
{
    public interface IContractRepo
    {
        List<Contract> Get(Func<SQLite.Models.Contract, bool> predicate);

        Contract Get(int id);
        List<Contract> GetByMainContractId(int id);
        List<Contract> GetGraphByMainContractId(int id);
        List<Contract> GetAll();
        int Save(Contract contract);
        int Remove(Contract contract);

        Dictionary<int, HashSet<int>> GetTree();
    }
}