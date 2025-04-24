using System.Collections.Generic;
using System.Linq;

public class TestConsoleIO : IConsoleIO
{
    private readonly Queue<string> _inputs;
    public List<string> Outputs {get;} = new();

    public TestConsoleIO(IEnumerable<string> inputs)
    {
        _inputs = new Queue<string>(inputs);
    }

    public string ReadLine()
    {
        return _inputs.Count > 0 ? _inputs.Dequeue() : "";
    }

    public void WriteLine(string message)
    {
        Outputs.Add(message);
    } 
}