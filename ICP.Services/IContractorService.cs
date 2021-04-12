using ICP.Repositories.Dtos;
using ICP.Services.Models;
using System.Collections.Generic;

namespace ICP.Services
{
    public interface IContractorService
    {
        Contractor GetContractorById(int id);

        int AddContractor(ContractorVM contractor);

        IEnumerable<ContractorListVM> GetAllContractors();
        IEnumerable<ContractorListVM> GetAllOthers(int contractorId);
    }
}