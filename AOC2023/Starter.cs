using AOC2023.Day1;
using AOC2023.Day2;
using AOC2023.Day3;

namespace AOC2023;

public class Starter
{
    public static void Main()
    {
        Console.WriteLine("Day1");
        var calculator = new CalorieCalculator();
        calculator.ReadLinesPart1();
        calculator.ReadLinesPart2();
        
        Console.WriteLine("\nDay2");
        var rockPaperScissor = new RockPaperScissor();
        rockPaperScissor.Part1();
        rockPaperScissor.Part2();
        
        Console.WriteLine("\nDay3");
        var rucksackReorganizer = new RucksackReorganizer();
        rucksackReorganizer.Part1();
        rucksackReorganizer.Part2();
    }
}