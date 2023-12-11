using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
<<<<<<< HEAD
using System.Text;
=======
>>>>>>> d9163bf (first commit)
using System.Text.RegularExpressions;

class Program
{

    static void Main()
    {
<<<<<<< HEAD
        string fileName = "C:\\Users\\ricka\\dev\\AoC\\Day5\\input.txt";
        List<string> fileContent = FileInput(fileName);
        Part1(fileContent);

        Part2(fileContent); //dumb bruteforce way to do it, almost 30min runtime
       
=======
        string fileName = "C:\\Users\\ricka\\dev\\AoC\\Day5\\ExInput.txt";
        List<string> fileContent = FileInput(fileName);

        Part1(fileContent);
>>>>>>> d9163bf (first commit)

    }

    public static void Part1(List<string> fileContent)
    {
        
        List<long> seeds = GetSeeds(fileContent.First());
<<<<<<< HEAD
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
=======
        SortedList<long,long> mappings = GetMappings(fileContent.GetRange(3,2));



>>>>>>> d9163bf (first commit)
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
<<<<<<< HEAD
    private static SortedList<Tuple<long, long>, long> GetMappings(List<string> mappings)
    {
        SortedList<Tuple<long, long>, long> dict = new();
        string pattern = @"\b\d+\b";
        var destinationRangeStart = 0L;
        var sourceRangeStart = 0L;
        var rangeLength = 0L;
=======
    private static SortedList<long,long> GetMappings(List<string> mappings)
    {
        SortedList<long,long> numbers = new SortedList<long, long>();
        string pattern = @"\b\d+\b";
        var destinationRangeStart = 0l;
        var sourceRangeStart = 0l;
        var rangeLength = 0l;
        var max = 0l;
>>>>>>> d9163bf (first commit)
        foreach (var line in mappings)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            //match will hit 3 times 
            destinationRangeStart = long.Parse(matches.ElementAt(0).Value); 
            sourceRangeStart = long.Parse(matches.ElementAt(1).Value);
            rangeLength = long.Parse(matches.ElementAt(2).Value);

<<<<<<< HEAD
            
            dict.Add(new Tuple<long, long>(sourceRangeStart, destinationRangeStart),  rangeLength);


        }

        return dict;
    }
    




    private static List<string> FileInput(string name)
    {
        List<string> list = [];
=======

        }

        return numbers;
    }
    public static void Part2(List<string> fileContent)
    {


    }
   

    

    private static List<string> FileInput(string name)
    {
        List<string> list = new List<string>();
>>>>>>> d9163bf (first commit)

        try
        {
            // Create a StreamReader to read from the file
<<<<<<< HEAD
            using StreamReader rFile = new(name);
            // Read each line from the file and add it to the list
            while (!rFile.EndOfStream)
            {
                string? data = rFile.ReadLine();
                if (data != null)
                    list.Add(data);
=======
            using (StreamReader rFile = new StreamReader(name))
            {
                // Read each line from the file and add it to the list
                while (!rFile.EndOfStream)
                {
                    string data = rFile.ReadLine();
                    list.Add(data);
                }
>>>>>>> d9163bf (first commit)
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
