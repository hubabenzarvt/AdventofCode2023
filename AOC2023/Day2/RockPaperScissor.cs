using System.Diagnostics;

namespace AOC2023.Day2;

public class RockPaperScissor
{
    private static int OpponentTotalScore { get; set; }
    private static int MyTotalScore { get; set; }
    
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day2\\input.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);
    
    private static List<string> opponentList = new List<string>();
    private static List<string> myList = new List<string>();
    private static List<string> outcomeList = new List<string>();
    
    private static string[] opponentPlays = new [] {"A", "B", "C"};
    private static string[] myPlays = new [] {"X", "Y", "Z"};

    internal void Part1()
    {
        foreach (var line in lines)
        {
            opponentList.Add(opponentPlays.Any(line.Contains) ? line.Split(" ")[0] : "");
            myList.Add(myPlays.Any(line.Contains) ? line.Split(" ")[1] : "");
        }

        if (opponentPlays.Length != myPlays.Length)
        {
            throw new Exception("Opponent and I have different number of plays");
        }
        
        for (int i = 0; i < lines.Length; i++)
        {
            if (Array.IndexOf(opponentPlays, opponentList[i]) == Array.IndexOf(myPlays, myList[i]))
            {
                OpponentTotalScore += 3 + Array.IndexOf(opponentPlays, opponentList[i])+1;
                MyTotalScore += 3 + Array.IndexOf(myPlays, myList[i])+1;
            }

            else if ((opponentList[i] == "C" && myList[i] == "X") ||
                     (opponentList[i] == "B" && myList[i] == "Z") ||
                     (opponentList[i] == "A" && myList[i] == "Y"))
            {
                OpponentTotalScore += 0 + Array.IndexOf(opponentPlays, opponentList[i])+1;
                MyTotalScore += 6 + Array.IndexOf(myPlays, myList[i])+1;
            }
            else
            {
                OpponentTotalScore += 6 + Array.IndexOf(opponentPlays, opponentList[i])+1;
                MyTotalScore += 0 + Array.IndexOf(myPlays, myList[i])+1;
            }
        }
        Console.WriteLine($"Total Score for their is {OpponentTotalScore}");
        Console.WriteLine($"Total Score for me is {MyTotalScore}");
    }
    
    internal void Part2()
    {
        opponentList.Clear();
        myList.Clear();
        opponentList.Clear();
        MyTotalScore = 0;
        OpponentTotalScore = 0;
        
        foreach (var line in lines)
        {
            opponentList.Add(opponentPlays.Any(line.Contains) ? line.Split(" ")[0] : "");
            outcomeList.Add(myPlays.Any(line.Contains) ? line.Split(" ")[1] : "");
        }

        if (opponentPlays.Length != myPlays.Length)
        {
            throw new Exception("Opponent and outcomes have different number of plays");
        }
        
        for (int i = 0; i < lines.Length; i++)
        {
            /*private static string[] opponentPlays = new [] {"A", "B", "C"};
            private static string[] myPlays = new [] {"X", "Y", "Z"};*/
            //X = Lose
            if (outcomeList[i] == "X")
            {
                myList.Add(opponentList[i] == "A" ? "Z" : opponentList[i] == "B" ? "X" : "Y");
            }
            //Y = Draw
            else if (outcomeList[i] == "Y")
            {
                myList.Add(opponentList[i] == "A" ? "X" : opponentList[i] == "B" ? "Y" : "Z");
            }
            //Z = Win
            else if (outcomeList[i] == "Z")
            {
                myList.Add(opponentList[i] == "A" ? "Y" : opponentList[i] == "B" ? "Z" : "X");
            }

            if (Array.IndexOf(opponentPlays, opponentList[i]) == Array.IndexOf(myPlays, myList[i]))
            {
                OpponentTotalScore += 3 + Array.IndexOf(opponentPlays, opponentList[i])+1;
                MyTotalScore += 3 + Array.IndexOf(myPlays, myList[i])+1;
            }

            else if ((opponentList[i] == "C" && myList[i] == "X") ||
                     (opponentList[i] == "B" && myList[i] == "Z") ||
                     (opponentList[i] == "A" && myList[i] == "Y"))
            {
                OpponentTotalScore += 0 + Array.IndexOf(opponentPlays, opponentList[i])+1;
                MyTotalScore += 6 + Array.IndexOf(myPlays, myList[i])+1;
            }
            else
            {
                OpponentTotalScore += 6 + Array.IndexOf(opponentPlays, opponentList[i])+1;
                MyTotalScore += 0 + Array.IndexOf(myPlays, myList[i])+1;
            }
        }
        Console.WriteLine($"PART 2 Total Score for their is {OpponentTotalScore}");
        Console.WriteLine($"PART 2 Total Score for me is {MyTotalScore}");
    }
}