using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IBoxRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IBoxRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Box>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Box>>> GetBoxs()
        {
            var Boxs = await _repository.GetBoxes();
            return Ok(Boxs);
        }

        [HttpGet("{id:length(24)}", Name = "GetBox")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Box), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Box>> GetBoxById(string id)
        {
            var Box = await _repository.GetBox(id);
            if (Box == null)
            {
                _logger.LogError($"Box with id: {id}, not found.");
                return NotFound();
            }
            return Ok(Box);
        }

        [Route("[action]/{category}", Name = "GetBoxByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Box>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Box>>> GetBoxByCategory(string category)
        {
            var Boxs = await _repository.GetBoxByCategory(category);
            return Ok(Boxs);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Box), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Box>> CreateBox([FromBody] Box Box)
        {
            await _repository.CreateBox(Box);

            return CreatedAtRoute("GetBox", new { id = Box.Id }, Box);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Box), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBox([FromBody] Box Box)
        {
            return Ok(await _repository.UpdateBox(Box));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteBox")]
        [ProducesResponseType(typeof(Box), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBoxById(string id)
        {
            return Ok(await _repository.DeleteBox(id));
        }
    }
}