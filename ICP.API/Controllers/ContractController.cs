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
        public IActionResult AddContract([FromForm]int mainContractorId, [FromForm] int relationContractorId)
        {
            try
            {
                var result = _service.AddContract(mainContractorId, relationContractorId);

                return new OkObjectResult(result);
            }
            catch(ArgumentException ex)
            {
                return new OkObjectResult(ex.Message);
            }
            catch
            {
                return new BadRequestObjectResult("unable to process your request");
            }
        }


        [HttpGet]
        public IActionResult GetContracts(int mainContractorId)
        {
            var result = _service.GetContracts(mainContractorId);

            return new OkObjectResult(result);
        }

        [HttpGet("all")]
        public IActionResult GetAllContracts()
        {
            var result = _service.GetAllContracts();

            return new OkObjectResult(result);
        }


        [HttpGet("getshortestpath")]
        public IActionResult GetShortPath(int mainContractorId, int relationContractorId)
        {
            if (mainContractorId == relationContractorId)
                return new BadRequestObjectResult("no path exists to self.");

            try
            {
                var result = _service.GetShortestPath(mainContractorId, relationContractorId);

                return new OkObjectResult(result);
            }
            catch(KeyNotFoundException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}
