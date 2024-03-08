namespace NowaitechDomain.Commands.ExcelToFileModelCommands
{
    public interface IExcelToFileModalCommand<T>
    {
        Task<List<T>> GetDataFromExcelAsync(int tableNumber, CancellationToken cancellationToken);
    }
}
