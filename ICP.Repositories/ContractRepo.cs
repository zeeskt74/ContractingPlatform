using ICP.Repositories.Dtos;
using ICP.SQLite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Repositories
{
    public class ContractRepo : IContractRepo
    {
        ICPDbContext _db;
        public ContractRepo(ICPDbContext db)
        {
            _db = db;
        }

        public Contract Get(int id)
        {
            var c = _db.Contracts
                        .Include(c => c.MainContractor)
                        .Include(c => c.RelationContractor)
                        .FirstOrDefaultAsync(c => c.Id == id)
                        .GetAwaiter() // we are calling it synchronously other option is to use async await.
                        .GetResult();

            return ConvertToRepoModel(c);
        }

        public List<Contract> GetByMainContractId(int id)
        {
            return _db.Contracts
                        .Include(c => c.MainContractor)
                        .Include(c => c.RelationContractor)
                        .Where(c => c.MainContractorId == id)
                        .Select(ConvertToRepoModel)
                        .ToList();
        }

        public List<Contract> GetGraphByMainContractId(int id)
        {
            return _db.Contracts
                        .Where(c => c.MainContractorId == id)
                        .Select(ConvertToRepoModel)
                        .ToList();
        }

        public List<Contract> GetAll()
        {
            return _db.Contracts
                        .Include(c => c.MainContractor)
                        .Include(c => c.RelationContractor)
                        .Select(ConvertToRepoModel)
                        .ToList();
        }

        public List<Contract> Get(Func<SQLite.Models.Contract, bool> predicate)
        {
            return _db.Contracts
                        .Include(c => c.MainContractor)
                        .Include(c => c.RelationContractor)
                        .Where(predicate)
                        .Select(ConvertToRepoModel)
                        .ToList();
        }

        public Dictionary<int, HashSet<int>> GetTree()
        {
            return _db.Contractors
                                .AsEnumerable()
                                .GroupJoin(_db.Contracts.AsEnumerable(),
                                     contractor => contractor.Id,
                                     contract => contract.MainContractorId,
                                     (contractor, contacts) => new { contractor.Id, contacts })                                
                                .ToDictionary(x => x.Id, v => v.contacts.Select(c => c.RelationContractorId).ToHashSet());
        }

        public int Save(Contract contractDto)
        {

            if (IsExists(contractDto))
                throw new ArgumentException($"Contract already exitst between {contractDto.MainContactor.Id}:{contractDto.RelationContactor.Id}");

            if (!IsValid(contractDto))
                throw new ArgumentException($"contractor(s) does not exists {contractDto.MainContactor.Id}:{contractDto.RelationContactor.Id}");

            if (IsSelfContract(contractDto))
                throw new ArgumentException($"MainContactor: {contractDto.MainContactor.Id}, RelationContactor: {contractDto.RelationContactor.Id}, can not contract itself.");


            return Insert(contractDto);
        }

        public int Remove(Contract contract)
        {
            var c = GetContract(contract.MainContactor.Id, contract.RelationContactor.Id);

            if (c == null) 
                return 0;
            
            var obj = _db.Contracts.Remove(c);
            return (obj.State == EntityState.Deleted) ? 1 : 0;
        }


        private int Insert(Contract contractDto)
        {
            var dbc = new SQLite.Models.Contract()
            {
                MainContractorId = contractDto.MainContactor.Id,
                RelationContractorId = contractDto.RelationContactor.Id
            };

            _db.Contracts.Add(dbc);
            return _db.SaveChanges();
        }

        private Boolean IsSelfContract(Contract contractDto)
        {
            return contractDto.MainContactor.Id == contractDto.RelationContactor.Id;
        }

        private Boolean IsExists(Contract dto)
        {
            return GetContract(dto.MainContactor.Id, dto.RelationContactor.Id) != null;
        }

        private Boolean IsValid(Contract dto)
        {
            return _db.Contractors.Find(dto.MainContactor.Id) != null &&
                   _db.Contractors.Find(dto.RelationContactor.Id) != null;
        }

        private SQLite.Models.Contract GetContract(int mainContractId, int relationContractId)
        {
            return _db.Contracts.FirstOrDefault(c =>
                            c.MainContractorId == mainContractId &&
                            c.RelationContractorId == relationContractId);
        }
 

        private Contract ConvertToRepoModel(SQLite.Models.Contract dbContract)
        {
            
            return new Contract()
            {
                Id = dbContract.Id,
                MainContactor = new ContractDetail()
                {
                    Id = dbContract.MainContractorId,
                    Name = dbContract.MainContractor?.Name
                },
                RelationContactor = new ContractDetail()
                {
                    Id = dbContract.RelationContractorId,
                    Name = dbContract.RelationContractor?.Name
                }
            };
        }
    }
}
