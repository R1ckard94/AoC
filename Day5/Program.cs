using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

class Program
{

    static void Main()
    {
        string fileName = "C:\\Users\\ricka\\dev\\AoC\\Day5\\ExInput.txt";
        List<string> fileContent = FileInput(fileName);

        Part1(fileContent);

    }

    public static void Part1(List<string> fileContent)
    {
        
        List<long> seeds = GetSeeds(fileContent.First());
        SortedList<long,long> mappings = GetMappings(fileContent.GetRange(3,2));



    }

    private static List<long> GetSeeds(string firstLine)
    {
        List<long> numbers = new List<long>();
        string pattern = @"\b\d+\b";
        MatchCollection matches = Regex.Matches(firstLine, pattern);

        foreach (Match match in matches)
        {
            // Convert each match to an integer and add it to the list
            numbers.Add(long.Parse(match.Value));
        }
        return numbers;
    }
    private static SortedList<long,long> GetMappings(List<string> mappings)
    {
        SortedList<long,long> numbers = new SortedList<long, long>();
        string pattern = @"\b\d+\b";
        var destinationRangeStart = 0l;
        var sourceRangeStart = 0l;
        var rangeLength = 0l;
        var max = 0l;
        foreach (var line in mappings)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            //match will hit 3 times 
            destinationRangeStart = long.Parse(matches.ElementAt(0).Value); 
            sourceRangeStart = long.Parse(matches.ElementAt(1).Value);
            rangeLength = long.Parse(matches.ElementAt(2).Value);


        }

        return numbers;
    }
    public static void Part2(List<string> fileContent)
    {


    }
   

    

    private static List<string> FileInput(string name)
    {
        List<string> list = new List<string>();

        try
        {
            // Create a StreamReader to read from the file
            using (StreamReader rFile = new StreamReader(name))
            {
                // Read each line from the file and add it to the list
                while (!rFile.EndOfStream)
                {
                    string data = rFile.ReadLine();
                    list.Add(data);
                }
            }
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("An error occurred.");
            Console.WriteLine(e.ToString());
        }

        return list;
    }


}
