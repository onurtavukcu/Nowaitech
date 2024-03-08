using Microsoft.EntityFrameworkCore;
using NowaitechDomain.Commands.ExcelToFileModelCommands;
using NowaitechShared.Models.ExcelEntities;
using NowaitechShared.Models;
using System.Collections;
using LanguageExt.ClassInstances;

namespace NowaitechDomain.Commands.Runtime
{
    public class GetDataOnRuntime
    {
        public IEnumerable<object> getResult { get; private set; }
        public IEnumerable<object> RuntimeOps(CancellationToken cancellationToken)
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

                var taskType = methodInfo.ReturnType;

                var resultProperty = taskType.GetProperty("Result");
                if (resultProperty is null)
                    continue;

                getResult = resultProperty.GetValue(invocationTask) as IEnumerable<object>;
            }
            return getResult;
        }
    }
}
