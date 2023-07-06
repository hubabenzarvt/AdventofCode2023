namespace AOC2023.Day5;

public class CratePredictor
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day5\\input.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);

    public void Part1()
    {
        var combinations = new List<string>();
        var instructions = new List<string>();
        var found = false;

        //get all elements that are before the row with only digits in followed by an empty space
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Replace(" ", "").All(char.IsDigit) && found == false)
            {
                if (string.IsNullOrWhiteSpace(lines[i + 1]))
                {
                    combinations = lines.Take(i).ToList();
                    found = true;
                }
            }

            if (found && lines[i].Contains("move"))
            {
                instructions.Add(lines[i].Replace(" ", ""));
            }
        }
        Console.WriteLine($"Found {combinations.Count} combinations");
        Console.WriteLine($"Found {instructions.Count} instructions");

        
        //Create a list of list of characters
        //Iterate through combinations array each line
        //if the current character in the combination index is 1 we want to add that character to the new list
        //Create an entry in the list of character in position 1 of the current index of combinations
        //Second time around get the last known position and add 4 to it and add that to the existing entry
        var crateStacks = new List<List<char>>();
        var currentColumn = 0;
        for (var i = combinations.Count-1; i >= 0; i--)
        {
            for (var j = 1; j < combinations[i].Length; j += 4)
            {
                if (i == combinations.Count-1)
                {
                    crateStacks.Add(new List<char>());
                }

                var currentChar = combinations[i][j];
                if ((currentChar >= 'A'  && currentChar <= 'Z')/* || (currentChar >= 'a'  && currentChar <= 'z')*/)
                {
                    crateStacks[currentColumn].Add(currentChar);
                }
                currentColumn++;
            }
            currentColumn = 0;
        }
        
        Console.WriteLine($"Found {crateStacks} combinations");

        //Move crates
        //Characters that isn't empty between move and from is the amount to move
        //Characters that aren't empty between from and to is where we move from
        //characters after to are the target of where to move the characters
        var moveLength = "move".Length;
        var fromLength = "from".Length;
        var toLength = "to".Length;
        
        foreach (var instruction in instructions)
        {
            var moveIndex = instruction.IndexOf("move", StringComparison.Ordinal);
            var fromIndex = instruction.IndexOf("from", StringComparison.Ordinal);
            var toIndex = instruction.IndexOf("to", StringComparison.Ordinal);
            
            var move = Int32.Parse(instruction.Substring(moveIndex + moveLength, fromIndex - moveIndex - moveLength));
            var from = Int32.Parse(instruction.Substring(fromIndex + fromLength, toIndex - fromIndex - fromLength)) -1;
            var to = Int32.Parse(instruction.Substring(toIndex + toLength)) -1;

            var cratesToMove = crateStacks[from].TakeLast(move).Reverse().ToList();
            crateStacks[from].RemoveRange(crateStacks[from].Count - move, move);
            crateStacks[to].AddRange(cratesToMove);
        }


        //Get last element of each index in crateStacks and assign it to output
        var output = crateStacks.Aggregate("", (current, stack) => current + stack.Last());
        Console.WriteLine($"Part 1 combination is: {output}");
    }
    
    //A method that gets the length of a string and returns the length of the string
    //If the string is empty it returns 0
    private static int GetLength(string s)
    {
        return string.IsNullOrEmpty(s) ? 0 : s.Length;
    }
}