using System.Numerics;

namespace AOC2023.Day8;

public class TreeHousePlanner
{
    private static string inputPath = File.OpenText("C:\\Users\\hbenzar\\Documents\\GitHub\\AdventofCode2023\\AOC2023\\Day8\\example.txt").ReadToEnd();
    private static string[] lines = inputPath.Split(Environment.NewLine);
    private List<List<Tree>> forest = new List<List<Tree>>();

    private class Tree
    {
        public int Height { get; init; }
        public int NorthTotal { get; set; }
        public int EastTotal { get; set; }
        public int SouthTotal { get; set; }
        public int WestTotal { get; set; }
        public bool NorthBool { get; set; } = true;
        public bool SouthBool { get; set; } = true;
        public bool EastBool { get; set; } = true;
        public bool WestBool { get; set; } = true;
        
        public int SpecularScore => NorthTotal * EastTotal * SouthTotal * WestTotal;
    }
    
    private void CreateForestMap()
    {
        forest.Clear();
        for (var index = 0; index < lines.Length; index++)
        {
            forest.Add(new List<Tree>());
            foreach (var treeHeight in lines[index].Where(treeHeight => treeHeight is >= '0' and <= '9'))
            {
                forest[index].Add(new Tree {Height = int.Parse(treeHeight.ToString())});
            }
        }
    }
    
    private void CheckNorthFromTree(int row, int column)
    {
        forest[row][column].NorthTotal = 0;
        for (var i = row -1; i >= 0; i--)
        {
            forest[row][column].NorthTotal++;
            var tree = forest[i][column];

            if (forest[row][column].Height <= tree.Height)
            {
                forest[row][column].NorthBool = false;
                return;
            }
        }
    }
    
    private void CheckEastFromTree(int row, int column)
    {
        forest[row][column].EastTotal = 0;
        for (var i = column +1; i < forest[row].Count; i++)
        {
            forest[row][column].EastTotal++;
            var tree = forest[row][i];

            if (forest[row][column].Height <= tree.Height)
            {
                forest[row][column].EastBool = false;
                return;
            }
        }
    }
    
    private void CheckSouthFromTree(int row, int column)
    {
        forest[row][column].SouthTotal = 0;
        for (var i = row +1; i < forest.Count; i++)
        {
            forest[row][column].SouthTotal++;
            var tree = forest[i][column];

            if (forest[row][column].Height <= tree.Height)
            {
                forest[row][column].SouthBool = false;
                return;
            }
        }
    }
    
    private void CheckWestFromTree(int row, int column)
    {
        forest[row][column].WestTotal = 0;
        for (var i = column -1; i >= 0; i--)
        {
            forest[row][column].WestTotal++;
            var tree = forest[row][i];

            if (forest[row][column].Height <= tree.Height)
            {
                forest[row][column].WestBool = false;
                return;
            }
        }
    }

    public void Part1Part2()
    {
        int spectacularRating = 0;
        Vector2 position = new Vector2();
        CreateForestMap();

        for (var row = 0; row < forest.Count; row++)
        {
            for (var column = 0; column < forest[row].Count; column++)
            {
                CheckNorthFromTree(row, column);
                CheckEastFromTree(row, column);
                CheckSouthFromTree(row, column);
                CheckWestFromTree(row, column);
                
                if (forest[row][column].SpecularScore > spectacularRating)
                {
                    spectacularRating = forest[row][column].SpecularScore;
                    position = new Vector2(row, column);
                }
            }
        }
        var total = forest.Sum(treeRow => treeRow.Count(tree => tree.NorthBool || tree.EastBool || tree.SouthBool || tree.WestBool));
        Console.WriteLine($"Part 1 is {total}");
        Console.WriteLine($"Part 2 is {spectacularRating} at position ({position.X}, {position.Y}), which is {forest[(int) position.Y][(int) position.X].Height} high");
    }
}