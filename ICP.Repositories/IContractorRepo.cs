using ICP.Repositories.Dtos;

namespace ICP.Repositories
{
    public interface IContractorRepo
    {
        Contractor Get(int id);
        int Save(Contractor contractor);
    }
}