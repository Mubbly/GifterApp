using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class ExamplesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ILogger<ExamplesController> _logger;
        private readonly ExampleMapper _mapper = new ExampleMapper();

        public ExamplesController(IAppBLL appBLL, ILogger<ExamplesController> logger)
        {
            _bll = appBLL;
            _logger = logger;
        }
        
        // GET: api/Examples
        /// <summary>
        ///     Get all Example
        /// </summary>
        /// <returns>List of Examples</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ExampleDTO>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ExampleDTO>>> GetExamples()
        {
            return Ok((await _bll.Example.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        // GET: api/Examples/5
        /// <summary>
        ///     Get a single Example
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Example object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ExampleDTO>))]
        public async Task<ActionResult<V1DTO.ExampleDTO>> GetExample(Guid id)
        {
            var example = await _bll.Example.FirstOrDefaultAsync(id);
            if (example == null)
            {
                return NotFound(new V1DTO.MessageDTO($"Example with id {id.ToString()} not found"));
            }
            return Ok(_mapper.Map(example));
        }

        // PUT: api/Examples/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Update Example
        /// </summary>
        /// <param name="id"></param>
        /// <param name="exampleDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutExample(Guid id, V1DTO.ExampleDTO exampleDTO)
        {
            // Don't allow wrong data
            if (id != exampleDTO.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and example.id do not match"));
            }
            var example = await _bll.Example.FirstOrDefaultAsync(exampleDTO.Id, User.UserGuidId());
            if (example == null)
            {
                _logger.LogError($"EDIT. No such example: {exampleDTO.Id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"No Example found for id {id}"));
            }
            // Update existing example
            await _bll.Example.UpdateAsync(_mapper.Map(exampleDTO), User.UserGuidId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Examples
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        ///     Add new Example
        /// </summary>
        /// <param name="exampleDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ExampleDTO))]
        public async Task<ActionResult<V1DTO.ExampleDTO>> PostExample(V1DTO.ExampleDTO exampleDTO)
        {
            // Create example
            var bllEntity = _mapper.Map(exampleDTO);
            _bll.Example.Add(bllEntity);
            await _bll.SaveChangesAsync();

            exampleDTO.Id = bllEntity.Id;
            return CreatedAtAction(
                "GetExample",
                new {id = exampleDTO.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                exampleDTO
                );
        }

        // DELETE: api/Examples/5
        /// <summary>
        ///     Delete Example
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ExampleDTO>))]
        public async Task<ActionResult<V1DTO.ExampleDTO>> DeleteExample(Guid id)
        {
            var example = await _bll.Example.FirstOrDefaultAsync(id, User.UserGuidId());
            if (example == null)
            {
                _logger.LogError($"DELETE. No such example: {id}, user: {User.UserGuidId()}");
                return NotFound(new V1DTO.MessageDTO($"Example with id {id} not found"));
            }
            await _bll.Example.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return Ok(example);
        }
    }
}
