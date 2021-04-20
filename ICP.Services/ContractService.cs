using ICP.Repositories;
using ICP.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepo _contractRepo;
        public ContractService(IContractRepo contractRepo)
        {
            _contractRepo = contractRepo;
        }

        public IEnumerable<Contract> GetContracts(int mainContractorId)
        {
            return _contractRepo.GetByMainContractId(mainContractorId)
                                .Select(MapeDto);
        }

        public IEnumerable<Contract> GetAllContracts()
        {
            return _contractRepo.GetAll()
                                .Select(MapeDto);
        }

        public int AddContract(int mainContractorId, int relationContractorId)
        {
            return _contractRepo.Save(new Repositories.Dtos.Contract(mainContractorId, relationContractorId));
        }

        //public dynamic GetShortestPath(int mainContractorId, int relationContractorId)
        //{
        //    //Queue<int> Q = new Queue<int>();
        //    List<int> path = new List<int>();
        //    //Q.Enqueue(relationContractorId);

        //    // 1- Check these items have any connections
        //    var clist = _contractRepo.Get(x => x.MainContractorId == mainContractorId);
        //    if (clist == null)
        //        throw new KeyNotFoundException("no path exists.");

        //    var relationallist = _contractRepo.Get(x => x.MainContractorId == relationContractorId);
        //    if (relationallist == null)
        //        throw new KeyNotFoundException("no path exists.");


        //    path.Add(mainContractorId);

        //    // Check if it has a direct connection
        //    if(IsExists(clist, relationContractorId))
        //    {
        //        path.Add(relationContractorId);
        //        return path;
        //    }

        //    //Check the list of first level childs
        //    Queue<int> Q = new Queue<int>();
        //    foreach (var c in clist)
        //    {
        //        var items = _contractRepo.Get(x => x.MainContractorId == c.RelationContactor.Id);

        //        if(IsExists(items, relationContractorId))
        //        {
        //            path.Add(c.MainContactor.Id);
        //            path.Add(relationContractorId);
        //            return path;
        //        }

        //        if (HasChild(items))
        //        {

        //        }
        //    }
        //    return null;
        //}

        //public bool IsExists(List<Repositories.Dtos.Contract> list, int id)
        //{
        //    return list.Exists(x => x.RelationContactor?.Id == id) ? true : false;            
        //}

        //public List<int> GetPath(List<Repositories.Dtos.Contract> list, int id)
        //{
        //    if (IsExists(items, relationContractorId))
        //    {
        //        path.Add(c.MainContactor.Id);
        //        path.Add(relationContractorId);
        //        return path;
        //    }
        //}

        //public bool HasChild(List<Repositories.Dtos.Contract> list)
        //{
        //    return list.Exists(x => x.RelationContactor != null) ? true : false;
        //}



        public dynamic GetShortestPath(int mainContractorId, int relationContractorId)
        {
            Queue<int> Q = new Queue<int>();
            HashSet<int> S = new HashSet<int>();
            HashSet<int> path = new HashSet<int>();

            // 1- Check these items have any connections
            var clist = _contractRepo.Get(x => x.MainContractorId == mainContractorId);
            if (clist == null)
                throw new KeyNotFoundException("no path exists.");

            var relationallist = _contractRepo.Get(x => x.MainContractorId == relationContractorId);
            if (relationallist == null)
                throw new KeyNotFoundException("no path exists.");


            Q.Enqueue(mainContractorId);
            S.Add(mainContractorId);
            path.Add(mainContractorId);

            int element = 0;
            while (Q.Count > 0)
            {
                if (element != mainContractorId)
                    path.Remove(element);

                element = Q.Dequeue();
                path.Add(element); //Add current

                foreach (var contract in _contractRepo.GetGraphByMainContractId(element))
                {
                    //stop and return when match if found
                    if (relationContractorId == contract.RelationContactor.Id)
                    {
                        path.Add(relationContractorId);
                        return path;
                    }

                    if (!S.Contains(contract.RelationContactor.Id))
                    {
                        Q.Enqueue(contract.RelationContactor.Id);
                        S.Add(contract.RelationContactor.Id);
                    }
                }
            }

            return null;
        }

        private Contract MapeDto(Repositories.Dtos.Contract contract)
        {
            return new Contract()
            {
                Id = contract.Id,
                MainContractorId = contract.MainContactor.Id,
                MainContractorName = contract.MainContactor.Name,

                RelationContractorId = contract.RelationContactor.Id,
                RelationalContractorName = contract.RelationContactor.Name
            };
        }
    }
}
