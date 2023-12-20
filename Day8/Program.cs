using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

class Program
{
    static private string STARTPart1 = "AAA";
    static private string ENDPart1 = "ZZZ";


    static void Main()
    {
        string fileName = "C:\\Users\\ricka\\dev\\AoC\\Day8\\input.txt";
        List<string> fileContent = FileInput(fileName);
        List<char> charsNav = new();
        string navigation = fileContent[0];
        foreach (var x in navigation)
        {
            charsNav.Add(x);
        }
        Dictionary<string,Tuple<string,string>> dict = PopulateDictionary(fileContent.GetRange(2,fileContent.Count-2));
        
        Part1(charsNav, dict);
        Console.WriteLine("-----------------------");
        Part2(charsNav, dict);

    }
    public static void Part1(List<char> nav, Dictionary<string, Tuple<string, string>> pattern)
    {
        int steps = 0;

        string currPos = STARTPart1;

        while (!currPos.Equals(ENDPart1))
        {
            foreach (var item in nav)
            {
                steps++;
                if (item.Equals('L'))
                {
                    currPos = pattern[currPos].Item1;
                }else
                {
                    currPos = pattern[currPos].Item2;
                }

                if (currPos.Equals(ENDPart1))
                {
                    break;
                }
            }
        }
        Console.WriteLine("PART 1 --> Total Sum: " + steps);
    }

    public static void Part2(List<char> nav, Dictionary<string, Tuple<string, string>> pattern)
    {
        List<string> currStartPos = FilterStringsWithLastChar(pattern.Keys, 'A');
        List<string> currEndPos = FilterStringsWithLastChar(pattern.Keys, 'Z');
        List<int> AtoZCount = new();



        foreach (var start in currStartPos)
        {
            bool added = false;
            int steps = 0;
            string currPos = start;
            while (!added) {
                foreach (var item in nav)
                {
                    steps++;
                    if (item.Equals('L'))
                    {
                        currPos = pattern[currPos].Item1;
                    }
                    else
                    {
                        currPos = pattern[currPos].Item2;
                    }

                    if (currEndPos.Contains(currPos))
                    {
                        added = true;
                        AtoZCount.Add(steps);
                        break;
                    }
                }
            }
            
        }
        BigInteger lcm = AtoZCount.Last();
        foreach (var num in AtoZCount)
        {
            lcm = ((lcm * num) / BigInteger.GreatestCommonDivisor(lcm, num));
            
        }

        Console.WriteLine("PART 2 --> Total Sum: " + lcm.ToString());
    }


    private static List<string> FilterStringsWithLastChar(Dictionary<string, Tuple<string, string>>.KeyCollection keys, char letter)
    {
        var filteredStrings = keys.Where(str => str.Length >= 3 && str[2] == letter).ToList();

        return filteredStrings;
    }



    private static Dictionary<string, Tuple<string, string>> PopulateDictionary(List<string> fileContent)
    {
        Dictionary<string, Tuple<string, string>> dict = new();
        foreach (string card in fileContent)
        {
            string name = card.Substring(0, Math.Min(3, card.Length));

            dict.Add(name, CreateTuple(card));
        }

        return dict;
    }

   
    public static string ExtractStringInParentheses(string input)
    {

        var match = Regex.Match(input, @"\(([^)]*)\)");

        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        return string.Empty;
    }


    private static Tuple<string,string> CreateTuple(string card)
    {
        string extractedString = ExtractStringInParentheses(card);
        string left = extractedString.Substring(0, Math.Min(3, extractedString.Length));
        string right = extractedString.Substring(extractedString.Length - Math.Min(3, extractedString.Length));
        
        return new Tuple<string,string>(left,right);        
        
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