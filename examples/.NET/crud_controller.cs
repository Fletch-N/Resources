namespace API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CrudController<T> : BaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<T>> Create(CreateCommand<T> command)
        {
            T result = await CreateEntityAsync<T>(command);
            return result.Success 
                ? Ok(result) 
                : BadRequest(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<T>>> List(int? limit, int? offset)
        {
            List<T> result = await ListEntityAsync<T>(limit, offset);
            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(string id)
        {
            T result = await GetEntityAsync<T>(id);
            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult<T>> Update(string id, Update<T> update)
        {
            UpdateCommand<T> command = new UpdateCommand<T>(id, update);
            T result = await UpdateEntityAsync<T>(command);
            return result.Success
                ? Ok(result)
                : BadRequest(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            DeleteCommand<T> command = new DeleteCommand<T>(id);
            var result = await DeleteEntityAsync<T>(command);
            return result.Success
                ? Ok()
                : BadRequest(result);
        }
    }
}