using ICP.Repositories;
using ICP.Repositories.Models;
using ICP.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ICP.Tests
{
    [TestClass]
    public class ContractorRepoTest
    {
        ICPDbContext _db;
        IContractorRepo _repo;

        [TestInitialize]
        public void Setup()
        {
            _db = new ICPTestDbContext();

            _db.Contractors.Add(new SQLite.Models.Contractor() { Name = "Test", Phone = "123456789", Type = "Carrier", HealthStatus = "Yellow" });
            _db.SaveChanges();

            _repo = new ContractorRepo(_db);
        }

        [TestMethod]
        public void GetContractor_Returns_data()
        {
            var c = _repo.Get(1);

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void New_Contractor_Saved_successfully()
        {
            //setup
            var c = new Contractor() { Name = "Test12345", Phone = "00123456789", Type = ContractorType.MGA, HealthStatus = ContractorHealthStatus.Red };

            //Act
            var result = _repo.Save(c);

            //Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void Contractor_updated_successfully()
        {
            //setup
            var c = _repo.Get(1);

            //Act
            c.Name = "name updated";
            var result = _repo.Save(c);
            var updatedContractor = _repo.Get(1);

            //Assert
            Assert.AreEqual(c.Name, updatedContractor.Name);
        }
    }
}
