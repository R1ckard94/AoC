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
        string fileName = "C:\\Users\\Zion\\Dev\\AoC\\Day7\\input.txt"; 
        List<string> fileContent = FileInput(fileName);
        var inputMapping = HandsAndBids(fileContent);
        Part1(inputMapping);
        Part2(inputMapping);


    }

    public static void Part1(Dictionary<string, int> input)
    {   
        //create a diffrent maps to easier sort the hands
        Dictionary<string, int> fiveOfAKind = new();
        Dictionary<string, int> fourOfAKind = new();
        Dictionary<string, int> fullHouse = new();
        Dictionary<string, int> threeOfAKind = new();
        Dictionary<string, int> twoPair = new();
        Dictionary<string, int> onePair = new();
        Dictionary<string, int> highCard = new();
        long finalScoring = 0;
        int multiplyer = input.Count();
        List<Dictionary<string, int>> sortedList = new()
        {
            fiveOfAKind,
            fourOfAKind,
            fullHouse,
            threeOfAKind,
            twoPair,
            onePair,
            highCard
        };


        foreach (var hand in input)
        {
            switch (FilterHand(hand.Key))
            {
                case 7:
                    fiveOfAKind.Add(hand.Key,hand.Value);
                    break;
                case 6:
                    fourOfAKind.Add(hand.Key, hand.Value);
                    break;
                case 5:
                    fullHouse.Add(hand.Key, hand.Value);
                    break;
                case 4:
                    threeOfAKind.Add(hand.Key, hand.Value);
                    break;
                case 3:
                    twoPair.Add(hand.Key, hand.Value);
                    break;
                case 2:
                    onePair.Add(hand.Key, hand.Value);
                    break;
                case 1:
                    highCard.Add(hand.Key, hand.Value);
                    break;
            }
        }
        
        foreach (var list in sortedList)
        {
            var tmpList = SortDictionaryByCustomOrderPart1(list);
            foreach (var pair in tmpList)
            {
                finalScoring += (pair.Value * multiplyer);
                multiplyer--;
            }
        }
        Console.WriteLine(finalScoring);


    }

    public static void Part2(Dictionary<string, int> input)
    {
        //create a diffrent maps to easier sort the hands
        Dictionary<string, int> fiveOfAKind = new();
        Dictionary<string, int> fourOfAKind = new();
        Dictionary<string, int> fullHouse = new();
        Dictionary<string, int> threeOfAKind = new();
        Dictionary<string, int> twoPair = new();
        Dictionary<string, int> onePair = new();
        Dictionary<string, int> highCard = new();
        long finalScoring = 0;
        int multiplyer = input.Count();
        List<Dictionary<string, int>> sortedList = new()
        {
            fiveOfAKind,
            fourOfAKind,
            fullHouse,
            threeOfAKind,
            twoPair,
            onePair,
            highCard
        };


        foreach (var hand in input)
        {
            switch (FilterHandPart2(hand.Key))
            {
                case 7:
                    fiveOfAKind.Add(hand.Key, hand.Value);
                    break;
                case 6:
                    fourOfAKind.Add(hand.Key, hand.Value);
                    break;
                case 5:
                    fullHouse.Add(hand.Key, hand.Value);
                    break;
                case 4:
                    threeOfAKind.Add(hand.Key, hand.Value);
                    break;
                case 3:
                    twoPair.Add(hand.Key, hand.Value);
                    break;
                case 2:
                    onePair.Add(hand.Key, hand.Value);
                    break;
                case 1:
                    highCard.Add(hand.Key, hand.Value);
                    break;
            }
        }

        foreach (var list in sortedList)
        {
            var tmpList = SortDictionaryByCustomOrderPart2(list);
            foreach (var pair in tmpList)
            {
                finalScoring += (pair.Value * multiplyer);
                multiplyer--;
            }
        }
        Console.WriteLine(finalScoring);


    }


    private static int FilterHand(string hand)
    {
        SortedDictionary<char,int> letterCounter = new();
        foreach (char c in hand)
        {
            if (letterCounter.ContainsKey(c))
            {
                letterCounter[c]++;
            }
            else
            {
                letterCounter.Add(c,1);
            }
        }
        var sortedDict = from entry in letterCounter orderby entry.Value descending select entry;
        if(sortedDict.First().Value == 5)
        {
            return 7;
        }else if(sortedDict.First().Value == 4)
        {
            return 6;
        }
        else if (sortedDict.First().Value == 3)
        {
            if(sortedDict.ElementAt(1).Value == 2)
            {
                return 5;
            }
            return 4;
        }
        else if(sortedDict.First().Value == 2)
        {
            if (sortedDict.ElementAt(1).Value == 3)
            {
                return 5;
            }
            else if(sortedDict.ElementAt(1).Value == 2)
            {
                return 3;
            }
            return 2;
        }
        else
        {
            return 1;
        }

    }

    private static int FilterHandPart2(string hand)
    {
        string customOrder = "AKQT98765432J";
        char JACKS = 'J';
        Dictionary<char, int> letterCounter = new();
        foreach (char c in hand)
        {
            if (letterCounter.ContainsKey(c))
            {
                letterCounter[c]++;
            }
            else
            {
                letterCounter.Add(c, 1);
            }
        }

        var sortedDict = letterCounter.OrderByDescending(kv => kv.Value).ThenByDescending(kv => customOrder.IndexOf(kv.Key));
        letterCounter = new (sortedDict);

        if (letterCounter.ContainsKey(JACKS) && letterCounter[JACKS] != 5)
        {

            if (!(letterCounter.First().Key.Equals(JACKS)))
            {
                char pos = letterCounter.First().Key;
                letterCounter[pos] += letterCounter[JACKS];
                letterCounter[JACKS] = 0;
            }
            else {
                char pos = letterCounter.ElementAt(1).Key;
                letterCounter[pos] += letterCounter[JACKS];
                letterCounter[JACKS] = 0;
            }
            
        }
        sortedDict = letterCounter.OrderByDescending(kv => kv.Value);

        if (sortedDict.First().Value == 5)
        {
            return 7;
        }
        else if (sortedDict.First().Value == 4)
        {
            
            return 6;
        }
        else if (sortedDict.First().Value == 3)
        {
            if (sortedDict.ElementAt(1).Value == 2)
            {
                return 5;
            }
            return 4;
        }
        else if (sortedDict.First().Value == 2)
        {
            if (sortedDict.ElementAt(1).Value == 3)
            {
                return 5;
            }
            else if (sortedDict.ElementAt(1).Value == 2)
            {
                return 3;
            }
            return 2;
        }
        else
        {
            return 1;
        }

    }
    static Dictionary<string, int> SortDictionaryByCustomOrderPart2(Dictionary<string, int> dictionary)
    {
        // Define a custom order for strings
        string customOrder = "AKQT98765432J";


        // Sort the dictionary based on the custom order and then by value
        var sortedPairs = dictionary.OrderBy(kv => customOrder.IndexOf(kv.Key.Length > 0 ? kv.Key[0] : ' '))
                                    .ThenBy(kv => kv.Key.Length > 1 ? customOrder.IndexOf(kv.Key[1]) : -1)
                                    .ThenBy(kv => kv.Key.Length > 2 ? customOrder.IndexOf(kv.Key[2]) : -1)
                                    .ThenBy(kv => kv.Key.Length > 3 ? customOrder.IndexOf(kv.Key[3]) : -1)
                                    .ThenBy(kv => kv.Key.Length > 4 ? customOrder.IndexOf(kv.Key[4]) : -1)
                                    .ThenBy(kv => kv.Value);

        return sortedPairs.ToDictionary(kv => kv.Key, kv => kv.Value);
    }
    static Dictionary<string, int> SortDictionaryByCustomOrderPart1(Dictionary<string, int> dictionary)
    {
        // Define a custom order for strings
        string customOrder = "AKQJT98765432";


        // Sort the dictionary based on the custom order and then by value
        var sortedPairs = dictionary.OrderBy(kv => customOrder.IndexOf(kv.Key.Length > 0 ? kv.Key[0] : ' '))
                                    .ThenBy(kv => kv.Key.Length > 1 ? customOrder.IndexOf(kv.Key[1]) : -1)
                                    .ThenBy(kv => kv.Key.Length > 2 ? customOrder.IndexOf(kv.Key[2]) : -1)
                                    .ThenBy(kv => kv.Key.Length > 3 ? customOrder.IndexOf(kv.Key[3]) : -1)
                                    .ThenBy(kv => kv.Key.Length > 4 ? customOrder.IndexOf(kv.Key[4]) : -1)
                                    .ThenBy(kv => kv.Value);

        return sortedPairs.ToDictionary(kv => kv.Key, kv => kv.Value);
    }

    private static Dictionary<string, int> HandsAndBids(List<string> list)
    {
        Dictionary<string, int> handsAndBids = new();
        foreach (string line in list)
        {
            string tmpHand = line.Substring(0, 5);
            string tmpBid = line.Substring(6);
            handsAndBids.Add(tmpHand, int.Parse(tmpBid));

        }
        return handsAndBids;
    }




    private static List<string> FileInput(string name)
    {
        List<string> list = new();

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
