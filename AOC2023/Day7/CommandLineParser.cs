namespace AOC2023.Day7;

public class CommandLineParser
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day7\\input.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);
    
    private Dictionary<string, List<Tuple<int, string>>> pathDictionary = new Dictionary<string, List<Tuple<int, string>>>();
    private string currentPath = "";
        
    private string lineStart = "";
    private string lineEnd = "";
    private int charIndex = 0;
    
    private void ParseCommandLine(int startIndex = 0)
    {
        for (var i = startIndex; i < lines.Length; i++)
        {
            charIndex = lines[i].LastIndexOf(' ') + 1;
            lineStart = lines[i][..charIndex];
            lineEnd = lines[i][charIndex..];

            if (lines[i].StartsWith("$ cd"))
            {
                HandleCommandRequest();
            }
            else if (lines[i].StartsWith("$ ls"))
            {
                if (AddValuesToDictionary(out startIndex, i)) return;
            }
        }
    }

    private void HandleCommandRequest()
    {
        switch (lineEnd)
        {
            case "..":
                if (currentPath.Contains('/') && currentPath != "/")
                {
                    var endingIndex = currentPath.Substring(0, currentPath.LastIndexOf('/')).LastIndexOf('/');
                    currentPath = currentPath.Substring(0, endingIndex + 1);
                }
                else
                    currentPath = "/";

                break;
            case "/":
                if (!pathDictionary.ContainsKey("/")) 
                    pathDictionary.Add("/", new List<Tuple<int, string>>());
                currentPath = "/";
                break;
            default:
                currentPath = $"{currentPath}{lineEnd}/";
                break;
        }
    }
    
    private bool AddValuesToDictionary(out int startIndex, int i)
    {
        var key = "";
        var previousLoopIndex = i + 1;
        for (int j = previousLoopIndex; j < lines.Length; j++)
        {
            charIndex = lines[j].LastIndexOf(' ') + 1;
            lineStart = lines[j][..charIndex];
            lineEnd = lines[j][charIndex..];

            if (lines[j].StartsWith("dir"))
            {
                key = $"{currentPath}{lineEnd}/";
                if (!pathDictionary.ContainsKey(key)) pathDictionary.Add(key, new List<Tuple<int, string>>());
            }
            else if (lines[j].Split(" ")[0].All(char.IsDigit))
            {
                if (!string.IsNullOrWhiteSpace(currentPath))
                {
                    if (!pathDictionary.ContainsKey(currentPath))
                        pathDictionary.Add(currentPath, new List<Tuple<int, string>>());

                    pathDictionary[currentPath].Add(new Tuple<int, string>(int.Parse(lineStart), lineEnd));
                }
            }
            else
            {
                ParseCommandLine(j);
                startIndex = 0;
                return true;
            }
        }

        startIndex = lines.Length;
        return false;
    }

    private int CalculateFolderValues(int minimumSizeCounter = 0)
    {
        var internalSizeCounter = 0;
        foreach (var parentPath in pathDictionary)
        {
            var sizeCounter = 0;
            foreach (var parentContainingPath in pathDictionary.Keys.Where(x => x.StartsWith(parentPath.Key)).ToList())
            {
                foreach (var childFolder in pathDictionary[parentContainingPath])
                {
                    if (childFolder.Item1 > 0 && !string.IsNullOrWhiteSpace(childFolder.Item2))
                        sizeCounter += childFolder.Item1;
                }

                if (sizeCounter > minimumSizeCounter)
                {
                    sizeCounter = 0;
                    break;
                }
            }

            internalSizeCounter += sizeCounter;
        }

        return internalSizeCounter;
    }

    private int CalculateMinimalDeletion(int driveSize, int spaceRequired)
    {
        //Look through all of the values in the dictionary and determine which folder structures are above deletedAmount
        //Return the value of the folder structure that is closest to deletedAmount
        var folderValues = new List<int>();

        var currentlyFreeSpace = driveSize - SpaceUsed();
        foreach (var parentPath in pathDictionary)
        {
            var sizeCounter = 0;
            foreach (var parentContainingPath in pathDictionary.Keys.Where(x => x.StartsWith(parentPath.Key)).ToList())
            {
                foreach (var childFolder in pathDictionary[parentContainingPath])
                {
                    if (childFolder.Item1 > 0 && !string.IsNullOrWhiteSpace(childFolder.Item2))
                    {
                        sizeCounter += childFolder.Item1;
                    }
                }
            }
            
            if (currentlyFreeSpace + sizeCounter >= spaceRequired) //if already free space + size counter is greater than delete amount
            {
                 folderValues.Add(sizeCounter);
            }
        }

        if (folderValues.Count > 0)
        {
            var orderedFolderValues = folderValues.OrderBy(x => x).ToList();
            return orderedFolderValues[0];
        }

        return 0;
    }

    private int SpaceUsed()
    {
        var usedSpace = 0;
        foreach (var parentContainingPath in pathDictionary.Keys)
        {
            foreach (var childFolder in pathDictionary[parentContainingPath])
            {
                if (childFolder.Item1 > 0 && !string.IsNullOrWhiteSpace(childFolder.Item2))
                    usedSpace += childFolder.Item1;
            }
        }

        return usedSpace;
    }

    public void Part1()
    {
        pathDictionary = new Dictionary<string, List<Tuple<int, string>>>();
        currentPath = "";
        ParseCommandLine();
        Console.WriteLine($"Part 1 - The total deletable is: {CalculateFolderValues(100000)}");
    }
    
    public void Part2()
    {
        pathDictionary = new Dictionary<string, List<Tuple<int, string>>>();
        currentPath = "";
        ParseCommandLine();
        Console.WriteLine($"Part 2 - Minimum folder size to delete is: {CalculateMinimalDeletion(70000000, 30000000)}");
    }
    
    // e = 584
    // a = 94853
    // d = 24933642
    // / = 48381165
}