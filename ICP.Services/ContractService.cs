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
        private readonly IContractorRepo _contractorRepo;
        public ContractService(IContractRepo contractRepo, IContractorRepo contractorRepo)
        {
            _contractRepo = contractRepo;
            _contractorRepo = contractorRepo;
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
            var nodeVisited = GetShortestPathByBFS(mainContractorId, relationContractorId);

            if(nodeVisited == null)
                throw new KeyNotFoundException("no path exists.");


            var path = RemoveInvalidNodes(nodeVisited);

            return path;
        }

        private HashSet<Repositories.Dtos.Contract> RemoveInvalidNodes(HashSet<Repositories.Dtos.Contract> nodeVisited)
        {
            var path = new HashSet<Repositories.Dtos.Contract>();
            nodeVisited.Reverse();
            var nodes = nodeVisited.ToArray();

            path.Add(nodes[0]);

            if (nodes.Count() == 2)
            {
                path.Add(nodes[1]);
                return path;
            }

            for (var i= 1; i < nodeVisited.Count()-1; i++)
            {
                if (nodes[i].MainContactor.Id == nodes[i + 1].RelationContactor.Id)
                {
                    path.Add(nodes[i]);
                }
            }

            return path;
        }

        public HashSet<Repositories.Dtos.Contract> GetShortestPathByBFS(int mainContractorId, int relationContractorId)
        {
            Queue<int> Q = new Queue<int>();
            HashSet<Repositories.Dtos.Contract> path = new HashSet<Repositories.Dtos.Contract>();

            var contaract = _contractRepo.Get(mainContractorId);

            // 1- Check these items have any possible connections
            var clist = _contractRepo.Get(x => x.MainContractorId == mainContractorId);
            if (clist == null)
                throw new KeyNotFoundException("no path exists.");

            var relationallist = _contractRepo.Get(x => x.MainContractorId == relationContractorId);
            if (relationallist == null)
                throw new KeyNotFoundException("no path exists.");


            Q.Enqueue(mainContractorId);
            path.Add(contaract);
            //path.Add(mainContractorId);

            int element = 0;
            while (Q.Count > 0)
            {
                element = Q.Dequeue();

                foreach (var contract in _contractRepo.GetGraphByMainContractId(element))
                {
                    //stop and return when match if found
                    if (relationContractorId == contract.RelationContactor.Id)
                    {
                        path.Add(_contractRepo.Get(contract.RelationContactor.Id));
                        return path;
                    }

                    if (path.FirstOrDefault(x => x.Id == contract.RelationContactor.Id) == null)
                    {
                        Q.Enqueue(contract.RelationContactor.Id);
                        path.Add(_contractRepo.Get(contract.RelationContactor.Id));
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
