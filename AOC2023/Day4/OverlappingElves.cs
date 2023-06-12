namespace AOC2023.Day4;

public class OverlappingElves
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day4\\input.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);
    
    public void Part1()
    {
        var totalScore = 0;

        foreach (var line in lines)
        {
            var head = line.Split(',')[0].Split('-');
            var tail = line.Split(',')[1].Split('-');
            
            var headFirst = int.Parse(head[0]); //2
            var headSecond = int.Parse(head[1]); //4
            var tailFirst = int.Parse(tail[0]); //6
            var tailSecond = int.Parse(tail[1]); //8

            if ((headFirst >= tailFirst && headSecond <= tailSecond) || (tailFirst >= headFirst && tailSecond <= headSecond))
            {
                totalScore++;
            }
            /* Wish I got that working
            var headNumbers = Enumerable.Range(int.Parse(head[0]), int.Parse(head[1])- int.Parse(head[0])+1).ToList();
            var tailNumbers = Enumerable.Range(int.Parse(tail[0]), int.Parse(tail[1])- int.Parse(tail[0])+1).ToList();

            if (headNumbers.Intersect(tailNumbers))
            {
                totalScore++;
            }*/
        }
        Console.WriteLine("Overlapping elves found pt1: " + totalScore);
    }

    public void Part2()
    {
        var totalScore = 0;

        foreach (var line in lines)
        {
            var head = line.Split(',')[0].Split('-');
            var tail = line.Split(',')[1].Split('-');
            
            var headNumbers = Enumerable.Range(int.Parse(head[0]), int.Parse(head[1])- int.Parse(head[0])+1).ToList();
            var tailNumbers = Enumerable.Range(int.Parse(tail[0]), int.Parse(tail[1])- int.Parse(tail[0])+1).ToList();

            if (headNumbers.Intersect(tailNumbers).Any())
            {
                totalScore++;
            }
        }
        Console.WriteLine("Overlapping elves found pt2: " + totalScore);
    }
}