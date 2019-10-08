using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment1
{
    public class DollarWords
    {
        public static Dictionary<char, int> cents = new Dictionary<char, int>()
    {{'a',1},{'b',2},{'c',3},{'d',4},{'e',5},{'f',6},{'g',7},{'h',8},{'i',9},{'j',10},{'k',11},{'l',12},{'m',13},{'n',14},
     {'o',15},{'p',16},{'q',17},{'r',18},{'s',19},{'t',20},{'u',21},{'v',22},{'w',23},{'x',24},{'y',25},{'z',26}};

        public static void Main()
        {
            string FilePath = @"C:\Users\mr_br\Desktop\words.txt";
            List<string> words = ListWords(FilePath);
            List<string> dollwords = ListDollarWords(words);
            string ExpWord = ExpensiveWord(words);

            Console.WriteLine("Percentage of dollar words: {0:n2}%\n", PercentDollarWords(dollwords, words));
            Console.WriteLine($"The most expensive word with a cost of {WordScore(ExpWord)} was: \n{ExpWord}\n");
            Console.WriteLine("The longest dollar word was: " + LongWord(dollwords));
        }


        //Finds longest word in a list
        public static string LongWord(List<string> words)
        {
            string CurrLongWord = "";
            foreach (string word in words)
            {
                if (word.Length > CurrLongWord.Length)
                {
                    CurrLongWord = word;
                }
            }
            return CurrLongWord;
        }

        //Finds the word with the highest score
        public static string ExpensiveWord(List<string> words)
        {
            string CurrExpWord = "a";
            foreach (string word in words)
            {
                if (word != null & (WordScore(word) > WordScore(CurrExpWord)))
                {
                    CurrExpWord = word;
                }
            }
            return CurrExpWord;
        }

        //Turns words.txt into a list of strings
        public static List<string> ListWords(string FilePath)
        {
            List<string> words = new List<string>();

            using (StreamReader file = new StreamReader(FilePath))
            {
                while (!file.EndOfStream)
                {
                    words.Add(file.ReadLine());
                }
            }
            return words;
        }

        //Iterates over list of words and creates a list of dollar words
        public static List<string> ListDollarWords(List<string> words)
        {
            List<string> DWords = new List<string>();
            foreach (string word in words)
            {
                if (IsDollarWord(word))
                {
                    DWords.Add(word);
                }
            }
            return DWords;
        }

        //checks if a word is a dollar word
        public static bool IsDollarWord(string word)
        {
            if (WordScore(word) == 100)
                return true;
            else
                return false;
        }

        //computes score for word
        public static int WordScore(string word)
        {
            string lower = word.ToLower();
            lower.ToCharArray();
            int total = 0;
            for (int i = 0; i < word.Length; i++)
            {
                if (cents.ContainsKey(lower[i]))
                {
                    total = total + cents[lower[i]];
                }
            }
            return total;
        }

        //calculates the percentage of dollar words
        public static double PercentDollarWords(List<string> dollwords, List<string> words)
        {
            return ((double)dollwords.Count / (double)words.Count) * 100;
        }
    }
}
