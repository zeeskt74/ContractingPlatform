using ICP.Repositories;
using ICP.Repositories.Dtos;
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

            _db.Contractors.Add(new SQLite.Models.Contractor() { Name = "Test 1", Address = "ABC", Phone = "123456789", Type = "Carrier", HealthStatus = "Yellow" });
            _db.Contractors.Add(new SQLite.Models.Contractor() { Name = "Test 2", Address = "XYZ", Phone = "123456789", Type = "Carrier", HealthStatus = "Green" });
            _db.SaveChanges();

            _repo = new ContractorRepo(_db);
        }



        [TestMethod]
        public void GetContractor_Returns_data()
        {
            //Arrange
            //Act
            var c = _repo.Get(1);

            //Assert
            Assert.IsNotNull(c);
        }



        [TestMethod]
        public void New_Contractor_Saved_successfully()
        {
            //Arrange
            var c = new Contractor() { Name = "Test12345", Phone = "00123456789", Type = ContractorType.MGA, HealthStatus = ContractorHealthStatus.Red };

            //Act
            var result = _repo.Save(c);

            //Assert
            Assert.IsTrue(result > 0);
        }



        [TestMethod]
        public void Contractor_updated_successfully()
        {
            //Arrange
            var id = 1;
            var c = _repo.Get(id);

            //Act
            c.Name = "name updated";
            var result = _repo.Save(c);
            var updatedContractor = _repo.Get(id);

            //Assert
            Assert.AreEqual(c.Name, updatedContractor.Name);
        }
    }
}
