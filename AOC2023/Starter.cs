using AOC2023.Day1;
using AOC2023.Day2;
using AOC2023.Day3;
using AOC2023.Day4;
using AOC2023.Day5;
using AOC2023.Day6;
using AOC2023.Day7;
using AOC2023.Day8;
using AOC2023.Day9;

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
        
        Console.WriteLine("\nDay4");
        var overlappingElves = new OverlappingElves();
        overlappingElves.Part1();
        overlappingElves.Part2();
        
        Console.WriteLine("\nDay5");
        var cratePredictor = new CratePredictor();
        cratePredictor.Part1();
        cratePredictor.Part2();
        
        Console.WriteLine("\nDay6");
        var signalCatcher = new SignalCatcher();
        signalCatcher.Part1();
        signalCatcher.Part2();
        
        Console.WriteLine("\nDay7");
        var cmdParser = new CommandLineParser();
        cmdParser.Part1();
        cmdParser.Part2();
        
        Console.WriteLine("\nDay8");
        var housePlanner = new TreeHousePlanner();
        housePlanner.Part1Part2();
        
        Console.WriteLine("\nDay9");
        var ropePhysics = new RopePhysics();
        ropePhysics.Part1();
    }
}