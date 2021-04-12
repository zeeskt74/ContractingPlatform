using ICP.Repositories;
using ICP.Repositories.Dtos;
using ICP.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ICP.Tests
{
    [TestClass]
    public class ContractRepoTest
    {
        ICPDbContext _db;
        IContractRepo _repo;

        [TestInitialize]
        public void Setup()
        {
            _db = new ICPTestDbContext();

            _db.Contractors.Add(new SQLite.Models.Contractor() { Name = "Test 1", Phone = "123456789", Type = "Carrier", HealthStatus = "Yellow" });
            _db.Contractors.Add(new SQLite.Models.Contractor() { Name = "Test 2", Phone = "123456789", Type = "Carrier", HealthStatus = "Green" });
            _db.Contractors.Add(new SQLite.Models.Contractor() { Name = "Test 3", Phone = "123456789", Type = "Carrier", HealthStatus = "Green" });
            _db.Contracts.Add(new SQLite.Models.Contract() { MainContractorId = 1, RelationContractorId = 2 });
            _db.Contracts.Add(new SQLite.Models.Contract() { MainContractorId = 2, RelationContractorId = 1 });

            _db.SaveChanges();

            _repo = new ContractRepo(_db);
        }



        [TestMethod]
        public void GetContract_returns_data()
        {
            //Arrange
            //Act
            var c = _repo.Get(1);

            //Assert
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void GetContract_returns_contract_and_related_data()
        {
            //Arrange
            //Act
            var c = _repo.Get(1);

            //Assert
            Assert.IsNotNull(c.MainContactor);
            Assert.IsNotNull(c.RelationContactor);
        }

        [TestMethod]
        public void New_contract_saved_successfully()
        {
            //Arrange
            var c = new Contract(3, 1);

            //Act
            var result = _repo.Save(c);

            //Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void New_contract_should_not_save_multiple_times()
        {
            //Arrange
            var c = new Contract(3, 1);
            var result = _repo.Save(c);

            //Act
            result = _repo.Save(c);

            //Assert
            Assert.IsTrue(result == 0);
        }


        [TestMethod]
        public void remove_contract_should_be_successful()
        {
            //Arrange
            var c = new Contract(3, 1);
            var result = _repo.Save(c);

            //Act
            result = _repo.Remove(c);

            //Assert
            Assert.IsTrue(result > 0);
        }


        [TestMethod]
        public void New_contract_should_fail_when_try_to_self_Contract()
        {
            //Arrange
            var c = new Contract(1, 1);
            var expectedError = $"MainContactor: {c.MainContactor.Id}, RelationContactor: {c.RelationContactor.Id}, can not contract itself.";
            try
            {
                //Act
                var result = _repo.Save(c);

                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch(ArgumentException ex)
            {
                //Assert
                Assert.AreEqual(expectedError, ex.Message);
            }
            catch (Exception)
            {
                // not the right kind of exception
                Assert.Fail();
            }
        }

    }
}
