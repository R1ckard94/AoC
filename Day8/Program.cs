using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

class Program
{
    static private string STARTPart1 = "AAA";
    static private string ENDPart1 = "ZZZ";


    static void Main()
    {
        string fileName = "C:\\Users\\ricka\\dev\\AoC\\Day8\\testinput.txt";
        List<string> fileContent = FileInput(fileName);
        List<char> charsNav = new();
        string navigation = fileContent[0];
        foreach (var x in navigation)
        {
            charsNav.Add(x);
        }
        Dictionary<string,Tuple<string,string>> dict = PopulateDictionary(fileContent.GetRange(2,fileContent.Count-2));
        
        //Part1(charsNav, dict);
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
        int steps = 0;

        bool allAtEnd = false;
        List<string> currStartPos = FilterStringsWithA(pattern.Keys);
        List<string> currEndPos = FilterStringsWithZ(pattern.Keys);
        List<string> currentPos = currStartPos;

        foreach (var item in currentPos)
        {
            Console.WriteLine(item);
        } 
        while (!allAtEnd)
        {
            for (int i = 0; i < currentPos.Count; i++)
            {
                bool added = false;
                string currPos = currentPos[i];
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
                        currentPos[i] = currPos;
                        break; 
                    }
                }
                if (!added)
                {
                    currentPos[i] = currPos;
                }
            }
            foreach (var item in currentPos)
            {
                Console.WriteLine(item);
            }
            if (currentPos.TrueForAll(n => n[2].Equals('Z')))
            {
                allAtEnd = true;
            }
            
        }
        Console.WriteLine("PART 2 --> Total Sum: " + steps);
    }



    private static List<string> FilterStringsWithA(Dictionary<string, Tuple<string, string>>.KeyCollection keys)
    {
        var filteredStrings = keys.Where(str => str.Length >= 3 && str[2] == 'A').ToList();

        return filteredStrings;
    }

    private static List<string> FilterStringsWithZ(Dictionary<string, Tuple<string, string>>.KeyCollection keys)
    {
        var filteredStrings = keys.Where(str => str.Length >= 3 && str[2] == 'Z').ToList();

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
        // Use a regular expression to extract the string within parentheses
        var match = Regex.Match(input, @"\(([^)]*)\)");

        // Check if a match was found
        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        // Return an empty string if no match was found
        return string.Empty;
    }


    private static Tuple<string,string> CreateTuple(string card)
    {
        string extractedString = ExtractStringInParentheses(card);
        // Filter out the first 3 characters, the first 3 characters inside parentheses, and the last 3 characters

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