using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

class Program
{

    static void Main()
    {
        string fileName = "C:\\Users\\ricka\\dev\\AoC\\Day5\\input.txt";
        List<string> fileContent = FileInput(fileName);
        Part1(fileContent);

        Part2(fileContent); //dumb bruteforce way to do it, almost 30min runtime
       

    }

    public static void Part1(List<string> fileContent)
    {
        
        List<long> seeds = GetSeeds(fileContent.First());
        SortedList<Tuple<long, long>, long> seedToSoilMap = GetMappings(fileContent.GetRange(3, 27));
        SortedList<Tuple<long, long>, long> soilToFertalizerMap = GetMappings(fileContent.GetRange(32, 20));
        SortedList<Tuple<long, long>, long> fertalizerToWaterMap = GetMappings(fileContent.GetRange(54, 48));
        SortedList<Tuple<long, long>, long> waterToLightMap = GetMappings(fileContent.GetRange(104, 42));
        SortedList<Tuple<long, long>, long> lightToTempMap = GetMappings(fileContent.GetRange(148, 24));
        SortedList<Tuple<long, long>, long> tempToHumidityMap = GetMappings(fileContent.GetRange(174, 25));
        SortedList<Tuple<long, long>, long> humidityToLocationMap = GetMappings(fileContent.GetRange(201, 37));
        List<SortedList<Tuple<long, long>, long>> MapperList = new() { 
                seedToSoilMap,                                  
                soilToFertalizerMap,
                fertalizerToWaterMap, 
                waterToLightMap, 
                lightToTempMap, 
                tempToHumidityMap, 
                humidityToLocationMap 
        };
        long lowestLocation = long.MaxValue;

        foreach (long seed in seeds)
        {
            long curr = seed;


            foreach (var map in MapperList)
            {
                
                foreach (var range in map) {
                    long rangeCountFromPos = curr-range.Key.Item1;
                    
                    if ( range.Key.Item1 <= curr && curr <= (range.Key.Item1 + (range.Value-1)))
                    {

                        curr = range.Key.Item2+rangeCountFromPos;
                        break;
                    }
                }
                
            }
            
            if (curr < lowestLocation)
            {
                lowestLocation = curr;
            }
            
        }
        Console.WriteLine($"PART1 -> lowestLocation: {lowestLocation}");
    }

    public static void Part2(List<string> fileContent)
    {
        List<long> seeds = GetSeeds(fileContent.First());

        SortedList<Tuple<long, long>, long> seedToSoilMap = GetMappings(fileContent.GetRange(3, 27));
        SortedList<Tuple<long, long>, long> soilToFertalizerMap = GetMappings(fileContent.GetRange(32, 20));
        SortedList<Tuple<long, long>, long> fertalizerToWaterMap = GetMappings(fileContent.GetRange(54, 48));
        SortedList<Tuple<long, long>, long> waterToLightMap = GetMappings(fileContent.GetRange(104, 42));
        SortedList<Tuple<long, long>, long> lightToTempMap = GetMappings(fileContent.GetRange(148, 24));
        SortedList<Tuple<long, long>, long> tempToHumidityMap = GetMappings(fileContent.GetRange(174, 25));
        SortedList<Tuple<long, long>, long> humidityToLocationMap = GetMappings(fileContent.GetRange(201, 37));
        List<SortedList<Tuple<long, long>, long>> MapperList = new() {
                seedToSoilMap,
                soilToFertalizerMap,
                fertalizerToWaterMap,
                waterToLightMap,
                lightToTempMap,
                tempToHumidityMap,
                humidityToLocationMap };




        long lowestLocation = long.MaxValue;

        for (int i = 0; i < seeds.Count; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j<seeds[(i + 1)]; j++) {
                    long curr = (seeds[i] + j);
                    foreach (var map in MapperList)
                    {

                        foreach (var range in map)
                        {
                            long rangeCountFromPos = curr - range.Key.Item1;

                            if (range.Key.Item1 <= curr && curr <= (range.Key.Item1 + (range.Value - 1)))
                            {

                                curr = range.Key.Item2 + rangeCountFromPos;
                                break;
                            }
                        }

                    }
                    if (curr < lowestLocation)
                    {
                        lowestLocation = curr;
                        Console.WriteLine(lowestLocation);
                    }
                }   
            }

                

            

        }
        Console.WriteLine($"PART2 -> lowestLocation: {lowestLocation}");
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
    private static SortedList<Tuple<long, long>, long> GetMappings(List<string> mappings)
    {
        SortedList<Tuple<long, long>, long> dict = new();
        string pattern = @"\b\d+\b";
        var destinationRangeStart = 0L;
        var sourceRangeStart = 0L;
        var rangeLength = 0L;
        foreach (var line in mappings)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            //match will hit 3 times 
            destinationRangeStart = long.Parse(matches.ElementAt(0).Value); 
            sourceRangeStart = long.Parse(matches.ElementAt(1).Value);
            rangeLength = long.Parse(matches.ElementAt(2).Value);

            
            dict.Add(new Tuple<long, long>(sourceRangeStart, destinationRangeStart),  rangeLength);


        }

        return dict;
    }
    




    private static List<string> FileInput(string name)
    {
        List<string> list = [];

        try
        {
            // Create a StreamReader to read from the file
            using StreamReader rFile = new(name);
            // Read each line from the file and add it to the list
            while (!rFile.EndOfStream)
            {
                string? data = rFile.ReadLine();
                if (data != null)
                    list.Add(data);
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
