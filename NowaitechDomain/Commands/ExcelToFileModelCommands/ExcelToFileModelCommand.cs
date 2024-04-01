﻿using NowaitechShared.Models;
using System.Data;
using System.Reflection;

namespace NowaitechDomain.Commands.ExcelToFileModelCommands
{
    public class ExcelToFileModelCommand<T> : IExcelToFileModalCommand<T> where T : new()
    {
        public async Task<List<T>> GetDataFromExcelAsync(int tableNumber, CancellationToken cancellationToken)
        {
            var thisName = typeof(T).Name;

            var fileLocator = new FileResourcePath();

            var (fileLocation, rowDataCount) = fileLocator.MatchLocation(thisName);

            var datasetOperations = new ExcelToDataSetCommands.ExcelToDataSetCommand();

            var datasets = datasetOperations.TakeExcelToDataset(fileLocation, rowDataCount, tableNumber, cancellationToken);

            var dataList = new List<T>();

            var dataTypes = typeof(T);

            var properties = dataTypes.GetProperties();

            foreach (DataRow row in datasets.Result.Tables[tableNumber].Rows)
            {
                T instance = new();

                foreach (var property in properties)
                {
                    var attribute = property.GetCustomAttribute<ColumnNameAttribute>();

                    var columnName = attribute is null
                        ? property.Name
                        : attribute.Name;

                    if (!datasets.Result.Tables[tableNumber].Columns.Contains(columnName))
                        continue;

                    var data = row[columnName];

                    if (data is null || data == DBNull.Value)
                        continue;

                    Console.WriteLine($"Column Name: {columnName}, Data: {data}, Property Name: {property.Name}");

                    if (property.CanWrite)
                    {
                        try
                        {
                            //    if (property.Name == "id")
                            //    {
                            //        if (!datasets.Result.Tables[tableNumber].Columns.Contains("id"))
                            //            continue;
                            //    }

                            //    if (property.Name == "Projekt_ID"
                            //            || property.Name == "SO_Nr"
                            //            || property.Name == "PID"
                            //            || property.Name == "Objekt_ID"
                            //            || property.Name == "Site_Code")
                            //    {
                            //        if (data is DBNull)
                            //            if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                            //            {
                            //                property.SetValue(instance, null);
                            //            }
                            //            else
                            //            {
                            //                property.SetValue(instance, Activator.CreateInstance(property.PropertyType));
                            //            }
                            //        else
                            //        {
                            //            property.SetValue(instance, Convert.ChangeType(data, property.PropertyType));
                            //        }
                            //    }

                            property.SetValue(instance, Convert.ChangeType(data, data.GetType()));

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error setting value for property {property.Name}. ColumnName: {columnName}, Data: {data}, Error: {ex}");
                        }
                    }
                }

                dataList.Add(instance);
            }
            return await Task.FromResult(dataList);
        }
    }
}
