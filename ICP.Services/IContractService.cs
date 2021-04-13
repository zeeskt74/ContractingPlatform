using ICP.Services.Models;
using System.Collections.Generic;

namespace ICP.Services
{
    public interface IContractService
    {
        IEnumerable<Contract> GetContracts(int mainContractId);        

        int AddContract(int mainContractId, int relationContractId);

        dynamic GetShortestPath(int mainContractId, int relationContractId);
    }
}