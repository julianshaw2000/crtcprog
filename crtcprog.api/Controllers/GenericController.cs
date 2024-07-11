using ctrcprog.api.Data;
using Microsoft.AspNetCore.Mvc; 
namespace crtcprog.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class GenericController<TEntity> : ControllerBase where TEntity : class
    {
        private readonly IRepository<TEntity> _repository; 

        protected GenericController(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            await _repository.InsertAsync(entity);
            await _repository.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.GetType().GetProperty("Id")?.GetValue(entity) }, entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] TEntity entity)
        {
            if (entity == null || id != (int)entity.GetType().GetProperty("Id")?.GetValue(entity))
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(entity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}