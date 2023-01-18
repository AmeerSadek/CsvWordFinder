using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using MissingFieldException = CsvHelper.MissingFieldException;

namespace CsvWordFinder;

public class CsvFileWordFinder
{
    public async Task<string> FindAsync(string filePath, int columnNumber, string targetedWord)
    {
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            AllowComments = true,
            Comment = '#',
            Delimiter = ",",
        };

        var result = string.Empty;

        var isFound = false;

        try
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, csvConfig);

            while (await csv.ReadAsync())
            {
                var columnValue = csv.GetField(columnNumber - 1);

                if (columnValue == targetedWord)
                {
                    result = csv.Context.Parser.RawRecord;

                    isFound = true;

                    break;
                }
            }
        }
        catch (FileNotFoundException)
        {
            return StaticMessages.FileNotFound;
        }
        catch (MissingFieldException)
        {
            return StaticMessages.FieldNotFound;
        }

        if (!isFound)
        {
            result = StaticMessages.NoResultsFound;
        }

        return result;
    }
}
