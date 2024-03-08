using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NowaitechDomain.Commands.InsertDataCommands;
using NowaitechDomain.Commands.Runtime;
using NowaitechDomain.ExcelDbContext;

namespace NowaiTechAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkInsertDataToDb : ControllerBase
    {
        private readonly EntityDbContext _dbContext;
        public BulkInsertDataToDb(EntityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("InsertAllDataToDb")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var insertInstance = new InsertAllDataToDbCommand(_dbContext);

            await insertInstance.InsertDataAsync(cancellationToken);

            return await Task.FromResult<IActionResult>(Ok());
        }

        [HttpGet]
        [Route("RuntimeOps")]
        public IActionResult RunTime(CancellationToken cancellationToken)
        {
            var runtimeInstance = new GetDataOnRuntime();

            var result = runtimeInstance.RuntimeOps(cancellationToken);

            return Ok(result);
        }
    }
}
