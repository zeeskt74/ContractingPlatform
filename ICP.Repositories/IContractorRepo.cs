using ICP.Repositories.Models;

namespace ICP.Repositories
{
    public interface IContractorRepo
    {
        Contractor Get(int id);
        int Save(Contractor contractor);
    }
}