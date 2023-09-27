namespace AOC2023.Day10;

public class CathodeRayTube
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day10\\input.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);
    
    private List<Tuple<string, int>> executeCommands = new List<Tuple<string, int>>();
    private List<Tuple<string, int>> executingCommands = new List<Tuple<string, int>>();
    private List<Tuple<string, int>> executedCommands = new List<Tuple<string, int>>();
    
    private int currentX = 0;
    private int cycles = 0;
    
    private void ParseCommands()
    {
        foreach (var line in lines)
        {
            var split = line.Split(" ");
            var command = split[0];
            var amount = split.Length > 1 ? line.Split(" ")[1] : "0";
            executeCommands.Add(new Tuple<string, int>(command, int.Parse(amount)));
        }
    }

    private void StartExecution()
    {
        int tempX = 1;
        foreach (var command in executeCommands)
        {
            DrawPixel(tempX);

            if (command.Item1 != "noop")
            {
                executingCommands.Add(command);
                for (var j = 0; j < executingCommands.Count; j++)
                {
                    DrawPixel(tempX);
                    tempX += executingCommands[j].Item2;
                    executingCommands.RemoveAt(j);
                }
            }
            executedCommands.Add(command);
        }
    }

    private void DrawPixel(int tempX)
    {
        if (MathF.Abs(cycles % 40 - tempX) <= 1)
            Console.Write("#");
        
        else
            Console.Write(".");

        if ((cycles+ 1) % 40 == 0 && cycles != 0)
            Console.WriteLine();
        
        cycles++;
        if (cycles == 20 || (cycles - 20) % 40 == 0)
            currentX += tempX * cycles;
    }
    
    public void Part1Part2()
    {
        ParseCommands();
        StartExecution();
        Console.WriteLine($"X Value {currentX}");
    }
}