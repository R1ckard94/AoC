using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Program
{

    static void Main()
    {
        string fileName = "C:\\Users\\ricka\\source\\repos\\Day4\\Day4\\input.txt";
        List<string> fileContent = FileInput(fileName);

        Part1(fileContent);
        Console.WriteLine("-----------------------");
        Part2(fileContent);

    }
    public static void Part1(List<string> fileContent)
    {
        int totalSum = 0;
  
        foreach (string card in fileContent)
        {
            totalSum += getCardWinningsPart1(card);

            
        }
        Console.WriteLine("PART 1 --> Total Sum: " + totalSum);
    }
    public static void Part2(List<string> fileContent)
    {

        int[] cardCopys = new int[fileContent.Count];


        int totalCardCopys = 219; //since we start with 219 cards
        for (int i = 0; i < fileContent.Count; i++)
        {

            for (int x = 0; x <= cardCopys[i]; x++)
            {
                int amountOfCopys = getCardWinningsPart2(fileContent[i]);
                if (amountOfCopys > 0)
                {
                    int stepsAhead = 1;
                    for (int j = 0; j < amountOfCopys; j++, stepsAhead++)
                    {

                        if ((i + stepsAhead) < cardCopys.Length)
                            cardCopys[i + stepsAhead] += 1;
                    }
                }
            }
        }
        foreach (var num in cardCopys)
        {
            totalCardCopys += num;

        }
        Console.WriteLine("PART 2 --> Total Cards overall: " + totalCardCopys);


    }
    public static int getCardWinningsPart2(string card)
    {
        int sum = 0;
        string pattern = @"(\d+):\s*((\d+\s*)+)\|\s*((\d+\s*)+)";


        // Match the pattern in the input string
        Match match = Regex.Match(card, pattern);

        if (match.Success)
        {
            // Get the first and second parts as strings
            string part1 = match.Groups[2].Value.Trim();
            string part2 = match.Groups[4].Value.Trim();

            // Convert the string parts to int lists
            List<int> list1 = part1.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            List<int> list2 = part2.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            List<int> unionList = list1.Intersect(list2).ToList();
            for (int i = 0; i < unionList.Count(); i++)
            {
                sum += 1;
            }
        }
        else
        {
            Console.WriteLine("No match found.");
        }
        // Console.WriteLine(sum);
        return sum;
    }

    public static int getCardWinningsPart1(string card)
    {
        int sum = 0;
        string pattern = @"(\d+):\s*((\d+\s*)+)\|\s*((\d+\s*)+)";


        // Match the pattern in the input string
        Match match = Regex.Match(card, pattern);

        if (match.Success)
        {
            // Get the first and second parts as strings
            string part1 = match.Groups[2].Value.Trim();
            string part2 = match.Groups[4].Value.Trim();

            // Convert the string parts to int lists
            List<int> list1 = part1.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            List<int> list2 = part2.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            List<int> unionList = list1.Intersect(list2).ToList();
            for(int i = 0; i<unionList.Count(); i++)
            {
                if (i == 0)
                {
                    sum = 1;
                    continue;
                }
                sum *= 2;
            }
        }
        else
        {
            Console.WriteLine("No match found.");
        }
       // Console.WriteLine(sum);
        return sum;
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
