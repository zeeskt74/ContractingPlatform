using ICP.Services.Models;
using System.Collections.Generic;

namespace ICP.Services
{
    public interface IContractService
    {
        IEnumerable<Contract> GetContracts(int mainContractorId);
        IEnumerable<Contract> GetAllContracts();
        int AddContract(int mainContractorId, int relationContractorId);

        dynamic GetShortestPath(int mainContractorId, int relationContractorId);
    }
}