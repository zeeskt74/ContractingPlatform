using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICP.Services;
using ICP.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ICP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _service;

        public ContractController(IContractService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult AddContract(int mainContractId, int relationContractId)
        {
            var result = _service.AddContract(mainContractId, relationContractId);

            return new OkObjectResult(result);
        }


        [HttpGet]
        public IActionResult GetContracts(int mainContractId)
        {
            var result = _service.GetContracts(mainContractId);

            return new OkObjectResult(result);
        }
    }
}
