namespace AOC2023.Day10;

public class CathodeRayTube
{
    /// <summary>
    /// Create a list of all executions
    /// Create a list of all commands WAITING to be executed
    /// create a list of commands to BEING executed
    /// create a list of commands that HAVE executed
    /// 
    /// StartExecution()
    /// Iterate through the commands
    /// if it starts with noop then don't add it to the execute list
    /// else
    /// Add to execute list
    ///
    /// Executed()
    /// Get the final item from the executed list and modify the X value based on the amount
    /// If the executed amount is 20 then increment of 40 multiply x by the cycle times.
    /// Remove that executed command from the executed list
    /// Add the executed command to the executed list
    ///
    /// Controller()
    /// foreach command in the waiting to execute list
    /// StartExecution()
    /// Executed()
    ///
    ///
    /// </summary>
    
    
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day10\\input.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);
    
    private List<Tuple<string, int>> executeCommands = new List<Tuple<string, int>>();
    private List<Tuple<string, int>> executingCommands = new List<Tuple<string, int>>();
    private List<Tuple<string, int>> executedCommands = new List<Tuple<string, int>>();
    
    private int currentX = 0;
    private int cycles = 0;
    private int width = 39;
    private int height = 0;
    
    private void ParseCommands()
    {
        foreach (var line in lines)
        {
            var split = line.Split(" ");
            
            var command = split[0];
            if (split.Length == 1)
            {
                executeCommands.Add(new Tuple<string, int>(command, 0));
                continue;
            }
            var amount = Int32.Parse(line.Split(" ")[1]);
            if (command == "noop")
                amount = 0;

            executeCommands.Add(new Tuple<string, int>(command, amount));
        }
    }

    private void CycleChecker(int tempX)
    {
        cycles++;
        if (cycles == 20 || (cycles - 20) % 40 == 0)
        {
            currentX += tempX * cycles;
        }
    }

    private void StartExecution()
    {
        int tempX = 1;

        for (var i = 0; i < executeCommands.Count; i++)
        {
            CycleChecker(tempX);

            if (executeCommands[i].Item1 != "noop")
            {
                executingCommands.Add(executeCommands[i]);
                for (var j = 0; j < executingCommands.Count; j++)
                {
                    CycleChecker(tempX);
                    tempX += executingCommands[j].Item2;
                    executingCommands.RemoveAt(j);
                }
            }
            executedCommands.Add(executeCommands[i]);
        }
    }

    private void PlottingPoints()
    {
        
    }
    
    public void Part1()
    {
        ParseCommands();
        StartExecution();
        
        Console.WriteLine($"X Value {currentX}");
        Console.WriteLine($"Cycles {cycles}");
        Console.WriteLine($"Execute Commands {executeCommands.Count}");
        Console.WriteLine($"Executing Commands {executingCommands.Count}");
        Console.WriteLine($"Executed Commands {executedCommands.Count}");
    }
    
    public void Part2()
    {
        Console.WriteLine($"Part 2 {currentX}");
    }
}