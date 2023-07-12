namespace AOC2023.Day7;

public class CommandLineParser
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day7\\input.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);
    
    private Dictionary<string, List<Tuple<int, string>>> pathDictionary = new Dictionary<string, List<Tuple<int, string>>>();
    private string currentPath = "";
    private int deletableDirSum = 0;
        
    private string lineStart = "";
    private string lineEnd = "";
    private int charIndex = 0;

    public void Part1(int startIndex = 0)
    {
        for (var i = startIndex; i < lines.Length; i++)
        {
            charIndex = lines[i].LastIndexOf(' ') + 1;

            lineStart = lines[i][..charIndex];
            lineEnd = lines[i][charIndex..];

            if (lines[i].StartsWith("$ cd"))
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
                        currentPath = "/";
                        break;
                    default:
                        currentPath = $"{currentPath}{lineEnd}/";
                        break;
                }
            }
            else if (lines[i].StartsWith("$ ls"))
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
                        if (!pathDictionary.ContainsKey(key))
                            pathDictionary.Add(key,
                                new List<Tuple<int, string>>()); //Technically can ignore since it won't have any size
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
                        Part1(j);
                        return;
                    }
                }

                startIndex = lines.Length;
            }
        }

        if (startIndex >= lines.Length)
        {
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

                    if (sizeCounter > 100000)
                    {
                        sizeCounter = 0;
                        break;
                    }
                }

                deletableDirSum += sizeCounter;
            }

            Console.WriteLine($"Part 1 - The total deletable is: {deletableDirSum}");
        }
    }

    /// <summary>
    /// e = 584
    /// a = 94853
    /// d = 24933642
    /// / = 48381165
    /// </summary>

    public void Part2()
    {
        Console.WriteLine("CommandLineParser Part2");
    }
}