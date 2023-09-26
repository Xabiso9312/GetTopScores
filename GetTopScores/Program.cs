using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CsvReaderHelper
{
    public static void FindTopScorers(string filePath)
    {
        Dictionary<string, List<int>> scores = new Dictionary<string, List<int>>();

        foreach (var line in File.ReadLines(filePath).Skip(1))
        {
            var values = line.Split(',');
            if (values.Length >= 2 && int.TryParse(values[2], out int score))
            {
                string name = values[0].Trim() + " " + values[1].Trim();
                if (!string.IsNullOrEmpty(name))
                {
                    if (scores.ContainsKey(name))
                    {
                        scores[name].Add(score);
                    }
                    else
                    {
                        scores[name] = new List<int> { score };
                    }
                }
            }
        }

        if (scores.Count == 0)
        {
            Console.WriteLine("No valid data found in the CSV file.");
            return;
        }

        int highestScore = scores.Max(kv => kv.Value.Max());

        var topScorers = scores
            .Where(kv => kv.Value.Max() == highestScore)
            .OrderBy(kv => kv.Key)
            .Select(kv => kv.Key);

        foreach (var scorer in topScorers)
        {
            Console.WriteLine($"{scorer}");
        }
        Console.WriteLine($"Score: {highestScore}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter directory to test data file and press enter");
        string filePath = Console.ReadLine();
        //filePath = @"C:\Users\X475148\Downloads\TestData.csv"; // Replace with CSV file path
        CsvReaderHelper.FindTopScorers(filePath);
    }
}