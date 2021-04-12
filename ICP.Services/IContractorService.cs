using ICP.Repositories.Dtos;

namespace ICP.Services
{
    public interface IContractorService
    {
        Contractor GetContractorById(int id);

        int AddContractor(Contractor contractor);

        int UpdateContractor(Contractor contractor);
    }
}