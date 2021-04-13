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
    public class ContractorController : ControllerBase
    {
        private readonly IContractorService _service;

        public ContractorController(IContractorService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult AddContractor([FromBody]ContractorVM contractor)
        {
            if (contractor == null) return new BadRequestObjectResult($"{nameof(contractor)} can not be null.");

            var result = _service.AddContractor(contractor);

            return new OkObjectResult(result);
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAllContractors();

            return new OkObjectResult(result);
        }

    }
}
