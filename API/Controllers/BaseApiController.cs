using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repo,
             ISpecification<T> spec, int pageIndex, int pageSize) where T : BaseEntity
        {
            var data = await repo.ListAsync(spec);
            var count = await repo.CountAsync(spec);

            var pagination = new Pagination<T>(pageIndex, pageSize, count, data);
            return Ok(pagination);
        }
    }
}