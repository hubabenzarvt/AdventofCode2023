namespace AOC2023.Day6;

public class SignalCatcher
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day6\\input.txt").ReadToEnd();
    
    public string SignalDecoder(int packetStart)
    {
        for (var i = 0; i < inputPath.Length; i++)
        {
            if (i == inputPath.Length - packetStart)
                return "Ran out of string";
            
            if (!inputPath.Substring(i, packetStart).GroupBy(x => x).Any(g => g.Count() > 1))
                return $"The outcome is at position : {i + packetStart}";
        }
        return "No outcome found";
    }
    
    public void Part1()
    {
        Console.WriteLine($"Part 1 {SignalDecoder(4)}");
    }
    
    public void Part2()
    {
        Console.WriteLine($"Part 2 {SignalDecoder(14)}");
    }
}