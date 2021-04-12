using ICP.Repositories.Dtos;

namespace ICP.Repositories
{
    public interface IContractRepo
    {
        Contract Get(int id);
        int Save(Contract contract);
        int Remove(Contract contract);
    }
}