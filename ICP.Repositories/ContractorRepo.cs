using ICP.Repositories.Dtos;
using ICP.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Repositories
{
    public class ContractorRepo : IContractorRepo
    {
        ICPDbContext _db;
        public ContractorRepo(ICPDbContext db)
        {
            _db = db;
        }

        public Contractor Get(int id)
        {
            var c = _db.Contractors.Find(id);

            return ConvertToRepoModel(c);
        }

        public int Save(Contractor contractor)
        {
            if (contractor.Id > 0)
            {
                return Update(contractor);
            }
            else
            {
                return Insert(contractor);
            }
        }

        public List<Contractor> GetAll()
        {
            return _db.Contractors.Select(ConvertToRepoModel).ToList();
        }


        private int Update(Contractor contractor)
        {
            var dbc = UpdateDbModel(_db.Contractors.Find(contractor.Id), contractor);

            dbc.Id = contractor.Id;
            dbc.Name = contractor.Name;
            dbc.Phone = contractor.Phone;
            dbc.Address = contractor.Address;
            dbc.Type = contractor.Type.ToString();
            dbc.HealthStatus = contractor.HealthStatus.ToString();

            return _db.SaveChanges();
        }


        private int Insert(Contractor contractor)
        {
            var dbc = UpdateDbModel(new SQLite.Models.Contractor(), contractor);
            
            _db.Contractors.Add(dbc);
            return _db.SaveChanges();
        }


        private SQLite.Models.Contractor UpdateDbModel(SQLite.Models.Contractor dbc, Contractor dto)
        {
            dbc.Id = dto.Id;
            dbc.Name = dto.Name;
            dbc.Phone = dto.Phone;
            dbc.Address = dto.Address;
            dbc.Type = dto.Type.ToString();
            dbc.HealthStatus = dto.HealthStatus.ToString();

            return dbc;
        }

        private Contractor ConvertToRepoModel(SQLite.Models.Contractor dbContractor)
        {
            ContractorType type;
            ContractorHealthStatus status;

            Enum.TryParse(dbContractor.Type, out type);
            Enum.TryParse(dbContractor.HealthStatus, out status);

            return new Contractor()
            {
                Id = dbContractor.Id,
                Name = dbContractor.Name,
                Address = dbContractor.Address,
                Phone = dbContractor.Phone,
                Type = type,
                HealthStatus = status
            };
        }

    }
}
