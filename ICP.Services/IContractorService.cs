using ICP.Repositories.Dtos;
using ICP.Services.Models;

namespace ICP.Services
{
    public interface IContractorService
    {
        Contractor GetContractorById(int id);

        int AddContractor(ContractorVM contractor);

        //int UpdateContractor(ContractorVM contractor);
    }
}