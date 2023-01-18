namespace CsvWordFinder;

public class Program
{
    public async static Task Main(string[] args)
    {
        var consoleInputValidator = new ConsoleInputValidator();

        var validatorResult = consoleInputValidator.Validate(args);

        if (!validatorResult.IsValid)
        {
            Console.WriteLine(validatorResult.Message);
        }
        else
        {
            var filePath = args[0];
            var columnNumber = int.Parse(args[1]);
            var targetedWord = args[2];

            try
            {
                var csvFileWordFinder = new CsvFileWordFinder();
                var result = await csvFileWordFinder.FindAsync(filePath, columnNumber, targetedWord);
                
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(StaticMessages.BuildErrorMessage(ex.Message));
            }
        }
    }
}