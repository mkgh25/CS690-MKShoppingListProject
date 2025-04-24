public class ConsoleIO : IConsoleIO
{
    public string ReadLine() => Console.ReadLine();
    public void WriteLine(string message) => Console.WriteLine(message);

}