using ICP.Repositories;
using ICP.Repositories.Dtos;
using ICP.Services;
using ICP.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
            var c = new Contractor() { Name = "Test12345", Phone = "00123456789", Type = ContractorType.MGA, HealthStatus = ContractorHealthStatus.Red };
            _repo.Setup(r => r.Save(It.IsAny<Contractor>())).Returns(1);

            //Act
            var result = _service.AddContractor(c);

            //Assert
            Assert.IsTrue(result > 0);
        }



        //[TestMethod]
        //public void Contractor_updated_successfully()
        //{
        //    //Arrange
        //    var c = new Contractor() { Name = "Test12345", Phone = "00123456789", Type = ContractorType.MGA, HealthStatus = ContractorHealthStatus.Red };
        //    _repo.Setup(r => r.Save(It.IsAny<Contractor>())).Returns(1);

        //    //Act
        //    c.Name = "name updated";
        //    var result = _repo.Save(c);
        //    var updatedContractor = _repo.Get(id);

        //    //Assert
        //    Assert.AreEqual(c.Name, updatedContractor.Name);
        //}
    }
}
