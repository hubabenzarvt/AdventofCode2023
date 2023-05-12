namespace ConsoleApp2.Day1;

public class CalorieCalculator
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day1\\input.txt").ReadToEnd();
    private string[] lines = inputPath.Split(Environment.NewLine);

    public static void Main()
    {
        var calculator = new CalorieCalculator();
        calculator.ReadLinesPart1();
        calculator.ReadLinesPart2();
    }
    
    private void ReadLinesPart1()
    {
        int prevHighest = 0;
        int total = 0;

        foreach (string line in lines)
        {
            if (!String.IsNullOrEmpty(line))
            {
                total += Int32.Parse(line);
            }
            else
            {
                total = 0;
            }
            if (total >= prevHighest)
            {
                prevHighest = total;
            }
        }
        Console.WriteLine($"Part 1: {prevHighest}");
    }

    private void ReadLinesPart2()
    {
        List<int> allOptions = new List<int>();
        int total = 0;
        int desiredHighest = 3;

        foreach (string line in lines)
        {
            if (!String.IsNullOrEmpty(line))
            {
                total += Int32.Parse(line);
            }
            else
            {
                allOptions.Add(total);
                total = 0;
            }
        }

        var highestScores = 0;
        foreach (var item in allOptions.OrderDescending().Take(desiredHighest).Select((value, i) => new { i, value }))
        {
            Console.WriteLine($"Elf {item.i+1}: {item.value}");
            highestScores += item.value;
        }
        Console.WriteLine($"Part 2: {highestScores}");
    }
}