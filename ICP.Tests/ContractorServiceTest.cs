using ICP.Repositories;
using ICP.Repositories.Dtos;
using ICP.Services;
using ICP.Services.Models;
using ICP.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ICP.Tests
{
    [TestClass]
    public class ContractorServiceTest
    {
        IContractorService _service;
        Mock<IContractorRepo> _repo;

        [TestInitialize]
        public void Setup()
        {
            _repo = new Mock<IContractorRepo>();
            _service = new ContractorService(_repo.Object);
        }



        [TestMethod]
        public void GetContractor_Returns_data()
        {
            //Arrange
            _repo.Setup(r => r.Get(It.IsAny<int>())).Returns(new Contractor() { Id = 1, Name = "Test12345", Phone = "00123456789", Type = ContractorType.MGA, HealthStatus = ContractorHealthStatus.Red });
            
            //Act
            var c = _service.GetContractorById(1);

            //Assert
            Assert.AreEqual(c.Id, 1);
        }



        [TestMethod]
        public void New_Contractor_Saved_successfully()
        {
            //Arrange
            var c = new ContractorVM() { Name = "Test12345", Phone = "00123456789" };
            _repo.Setup(r => r.Save(It.IsAny<Contractor>())).Returns(1);

            //Act
            var result = _service.AddContractor(c);

            //Assert
            Assert.IsTrue(result > 0);
        }



        [TestMethod]
        public void GetAllContractos_returns_all()
        {
            //Arrange
            var c1 = new Contractor() { Name = "Test 12345", Phone = "00123456789", Type = ContractorType.Advisor, HealthStatus = ContractorHealthStatus.Red };
            var c2 = new Contractor() { Name = "Test 3465", Phone = "00123456789", Type = ContractorType.MGA, HealthStatus = ContractorHealthStatus.Green };

            _repo.Setup(r => r.GetAll()).Returns(new List<Contractor> { c1, c2 });

            //Act
            var result = _service.GetAllContractors();

            //Assert
            Assert.AreEqual(2, result.Count());
        }


        [TestMethod]
        public void GetAllOthers_returns_all_expect_id_is_provided()
        {
            //Arrange
            var c1 = new Contractor() { Id = 1, Name = "Test 12345", Phone = "00123456789", Type = ContractorType.Advisor, HealthStatus = ContractorHealthStatus.Red };
            var c2 = new Contractor() { Id = 2, Name = "Test 3465", Phone = "00123456789", Type = ContractorType.MGA, HealthStatus = ContractorHealthStatus.Green };

            _repo.Setup(r => r.GetAll()).Returns(new List<Contractor> { c1, c2 });

            //Act
            var result = _service.GetAllOthers(1);

            //Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.First().Id == 2);
        }
    }
}
