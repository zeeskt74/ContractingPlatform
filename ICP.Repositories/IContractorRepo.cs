using ICP.Repositories.Dtos;
using System.Collections.Generic;

namespace ICP.Repositories
{
    public interface IContractorRepo
    {
        Contractor Get(int id);
        int Save(Contractor contractor);
        List<Contractor> GetAll();
    }
}