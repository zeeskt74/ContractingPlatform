using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.Repositories.Dtos
{
    public class Contract
    {
        public Contract()
        {

        }

        public Contract(int mainContactorId, int relationContactorId)
        {
            MainContactor = new ContractDetail(mainContactorId);
            RelationContactor = new ContractDetail(relationContactorId);
        }

        public int Id { get; set; }
        public ContractDetail MainContactor { get; set; }
        public ContractDetail RelationContactor { get; set; }
    }

    public class ContractDetail
    {

        public ContractDetail()
        {
        }

        public ContractDetail(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
