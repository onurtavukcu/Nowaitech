using NowaitechDomain.ExcelDbContext;
using NowaitechShared.Models.ExcelEntities;
using NowaitechShared.Models;
using System.Reflection;
using NowaitechDomain.Commands.ExcelToFileModelCommands;

namespace NowaitechDomain.Commands.InsertDataCommands
{
    public class InsertAllDataToDbCommand
    {
        private readonly EntityDbContext _dbContext;
        public InsertAllDataToDbCommand(EntityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertDataAsync(CancellationToken cancellationToken)
        {            
            var interfaceImplementingTypes = GetAssemblies.Assembilies();

            var genericExcelReaderType = typeof(ExcelToFileModelCommand<>);

            foreach (Type item in interfaceImplementingTypes)
            {
                var excelReaderType = genericExcelReaderType.MakeGenericType(item);
                var excelReaderInstance = Activator.CreateInstance(excelReaderType);

                if (excelReaderInstance is null)
                    continue;

                var methodInfo = excelReaderType.GetMethod("GetDataFromExcelAsync");

                if (methodInfo is null)
                    continue;

                var invocationTask = methodInfo.Invoke
                (
                    excelReaderInstance,
                    new object[]
                    {
                        EntityBase.GetTableNumber(item),
                        cancellationToken
                    }
                ) as Task;

                if (invocationTask is null)
                    continue;

                await invocationTask;

                var taskType = methodInfo.ReturnType;

                var resultProperty = taskType.GetProperty("Result");

                if (resultProperty is null)
                    continue;

                var invocationResult = resultProperty.GetValue(invocationTask) as IEnumerable<object>;

                await _dbContext.BulkInsertAsync(invocationResult, cancellationToken);
            }

            //var excelReader = new ExcelFileToModelOps<RouterAktuellOrderList>();

            //var result = await excelReader.GetDataFromExcelAsync(1, cancellationToken);

            //await _EntityDbContext.BulkInsertAsync(result, cancellationToken);
        }
    }
}
