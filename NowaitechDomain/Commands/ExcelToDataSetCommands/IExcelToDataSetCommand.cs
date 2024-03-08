using System.Data;

namespace NowaitechDomain.Commands.ExcelToDataSetCommands
{
    public interface IExcelToDataSetCommand
    {
        Task<DataSet> TakeExcelToDataset(string fileLocation, int rowCount, int tableNumber, CancellationToken cancellationToken);

        Task<DataSet> FirstRowToColumnName(DataSet dataSet, int rowCount, int tablenumber, CancellationToken CancellationToken);
    }
}
