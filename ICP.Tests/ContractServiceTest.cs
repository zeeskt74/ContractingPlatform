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
    public class ContractServiceTest
    {
        IContractService _service;
        Mock<IContractRepo> _repo;

        [TestInitialize]
        public void Setup()
        {
            _repo = new Mock<IContractRepo>();
            _service = new ContractService(_repo.Object);
        }



        [TestMethod]
        public void GetByMainContractId_Returns_data()
        {
            //Arrange
            var c1 = new Repositories.Dtos.Contract(1, 3);
            _repo.Setup(r => r.GetByMainContractId(It.IsAny<int>())).Returns(new List<Repositories.Dtos.Contract>() { c1 });
            
            //Act
            var c = _service.GetContracts(1);

            //Assert
            Assert.AreEqual(1, c.Count());
        }


        [TestMethod]
        public void GetShortestPath_Returns_data()
        {
            //Arrange
            var c1 = new Repositories.Dtos.Contract(1, 2);
            var c2 = new Repositories.Dtos.Contract(1, 3);
            var c3 = new Repositories.Dtos.Contract(2, 3);
            _repo.Setup(r => r.GetGraphByMainContractId(1)).Returns(new List<Repositories.Dtos.Contract>() { c1, c3 });
            _repo.Setup(r => r.GetGraphByMainContractId(2)).Returns(new List<Repositories.Dtos.Contract>() { c2 });

            var service = new ContractService(_repo.Object);


            //Act
            var c = service.GetShortestPath(1, 3);

            //Assert
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void GetShortestPath_Returns_second_layer_data()
        {
            //Arrange
            var c1 = new Repositories.Dtos.Contract(1, 2);
            var c2 = new Repositories.Dtos.Contract(2, 3);
            //var c3 = new Repositories.Dtos.Contract(2, 3);
            _repo.Setup(r => r.GetGraphByMainContractId(1)).Returns(new List<Repositories.Dtos.Contract>() { c1 });
            _repo.Setup(r => r.GetGraphByMainContractId(2)).Returns(new List<Repositories.Dtos.Contract>() { c2 });

            var service = new ContractService(_repo.Object);


            //Act
            var c = service.GetShortestPath(1, 3);

            //Assert
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void GetShortestPath_Returns_second_layer_2_data()
        {
            //Arrange
            var c1 = new Repositories.Dtos.Contract(1, 2);
            var c2 = new Repositories.Dtos.Contract(2, 3);
            var c3 = new Repositories.Dtos.Contract(2, 4);
            
            _repo.Setup(r => r.GetGraphByMainContractId(1)).Returns(new List<Repositories.Dtos.Contract>() { c1 });
            _repo.Setup(r => r.GetGraphByMainContractId(2)).Returns(new List<Repositories.Dtos.Contract>() { c2, c3 });

            var service = new ContractService(_repo.Object);


            //Act
            var c = service.GetShortestPath(1, 4);

            //Assert
            Assert.IsNotNull(c);
        }


        [TestMethod]
        public void GetShortestPath_Returns_third_layer_data()
        {
            //Arrange
            var c1 = new Repositories.Dtos.Contract(1, 2);
            var c2 = new Repositories.Dtos.Contract(2, 3);
            var c3 = new Repositories.Dtos.Contract(3, 4);

            _repo.Setup(r => r.GetGraphByMainContractId(1)).Returns(new List<Repositories.Dtos.Contract>() { c1 });
            _repo.Setup(r => r.GetGraphByMainContractId(2)).Returns(new List<Repositories.Dtos.Contract>() { c2 });
            _repo.Setup(r => r.GetGraphByMainContractId(3)).Returns(new List<Repositories.Dtos.Contract>() { c3 });

            var service = new ContractService(_repo.Object);


            //Act
            var c = service.GetShortestPath(1, 4);

            //Assert
            Assert.IsNotNull(c);
        }
    }
}
