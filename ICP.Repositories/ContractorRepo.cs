using ICP.Repositories.Dtos;
using ICP.SQLite;
using System;
using System.Collections.Generic;
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



        
        private int Update(Contractor contractor)
        {
            var dbc = _db.Contractors.Find(contractor.Id);

            dbc.Id = contractor.Id;
            dbc.Name = contractor.Name;
            dbc.Phone = contractor.Phone;
            dbc.Type = contractor.Type.ToString();
            dbc.HealthStatus = contractor.HealthStatus.ToString();

            return _db.SaveChanges();
        }


        private int Insert(Contractor contractor)
        {
            var dbc = new SQLite.Models.Contractor()
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Phone = contractor.Phone,
                Type = contractor.Type.ToString(),
                HealthStatus = contractor.HealthStatus.ToString()
            };

            _db.Contractors.Add(dbc);
            return _db.SaveChanges();
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
                Phone = dbContractor.Phone,
                Type = type,
                HealthStatus = status
            };
        }

    }
}
