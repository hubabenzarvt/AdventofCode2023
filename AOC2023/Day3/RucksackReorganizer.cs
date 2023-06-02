namespace AOC2023.Day3;

public class RucksackReorganizer
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day3\\input.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);

    public void Part1()
    {
        int totalScore = 0;
        Console.WriteLine("RucksackReorganizer Part1");
        foreach (var line in lines)
        {
            var leftSide = line.Substring(0, line.Length / 2);
            var rightSide = line.Substring(line.Length / 2);

            var bothContains = leftSide.FirstOrDefault(x => rightSide.Contains(x));
            if (char.IsUpper(bothContains))
            {
                totalScore += bothContains - 'A' + 1 + 26;
            }
            else
            {
                totalScore += bothContains - 'a' + 1;
            }
        }

        Console.WriteLine("Priorities sum is: " + totalScore);
    }
    
    public void Part2()
    {
        int totalScore = 0;
        Console.WriteLine("RucksackReorganizer Part2");

        foreach (var line in lines.Chunk(3)) //Chunk splits the array smaller arrays with the desired number of indexes
        {
            var bothContains = line[0].FirstOrDefault(x => line[1].Contains(x) && line[2].Contains(x));

            if (char.IsUpper(bothContains))
            {
                totalScore += bothContains - 'A' + 1 + 26;
            }
            else
            {
                totalScore += bothContains - 'a' + 1;
            }
        }

        Console.WriteLine("Priorities sum is: " + totalScore);
    }
}