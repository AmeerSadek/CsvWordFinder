namespace CsvWordFinder;

public static class StaticMessages
{
    public static readonly string InvalidNumberOfArguments = "Invalid number of arguments.";

    public static readonly string InvalidFileType = "Invalid file type.";

    public static readonly string ImposeCsvFileType = "File must be a CSV file.";

    public static readonly string InvalidColumnNumber = "Invalid column number.";

    public static readonly string NoResultsFound = "No results found.";
    
    public static readonly string FileNotFound = "File is not found. Check the provided path.";
    
    public static readonly string FieldNotFound = "Field not found at the provided index.";
    
    public static string BuildErrorMessage(string errorMessage) => $"Something wrong happend\nError: {errorMessage}";
}
