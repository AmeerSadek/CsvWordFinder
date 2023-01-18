namespace CsvWordFinder;

public class ConsoleInputValidator
{
    public ValidationResult Validate(string[] args)
    {
        // Check if the number of arguments is correct
        if (args.Length != 3)
        {
            return new ValidationResult
            {
                IsValid = false,
                Message = StaticMessages.InvalidNumberOfArguments
            };
        }

        string filePath = args[0];
        if (!string.Equals(Path.GetExtension(filePath), ".csv", StringComparison.OrdinalIgnoreCase))
        {
            return new ValidationResult
            {
                IsValid = false,
                Message = $"{StaticMessages.InvalidFileType}\n{StaticMessages.ImposeCsvFileType}"
            };
        }

        if (!int.TryParse(args[1], out _))
        {
            return new ValidationResult
            {
                IsValid = false,
                Message = StaticMessages.InvalidColumnNumber
            };
        }

        return new ValidationResult
        {
            IsValid = true,
            Message = null
        };
    }
}
