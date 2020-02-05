using System;
using System.Text.RegularExpressions;

namespace GC_Lab_4_Bonus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the English to Pig Latin converter!");
            do
            {
                string userInput = GetStringFromUser("Enter in a phrase to be converted\n> ");


                string pigLatin = PigLatanizeSentance(userInput);

                Console.WriteLine(pigLatin);
                Console.WriteLine();

            } while (PromptForLoop("Convert another phrase? (y/n) \n"));
        }

        private static string PigLatanizeSentance(string sentance)
        {
            // Gracefully handle bad input
            if (string.IsNullOrWhiteSpace(sentance)) return sentance;
            
            string output = string.Empty;
            string[] words = sentance.Split(" ");

            foreach (string word in words)
            {
                string outputWord = PigLatanizeWord(word.ToLower());
                if (IsUpperCase(word))
                {
                    outputWord = outputWord.ToUpper();
                }
                else if (IsTitleCase(word))
                {
                    outputWord = TitleCaseWord(outputWord);
                }
                
                
                output += outputWord + " ";
            }

            return output.Trim();
        }

        private static string TitleCaseWord(string word)
        {
            string output = string.Empty;
            word = word.ToLower();
            output += word[0].ToString().ToUpper();
            foreach (char letter in word)
            {
                output += letter;
            }
            return output;
        }

        private static bool IsTitleCase(string word)
        {
            string upperCasePattern = @"\b[A-Z][a-z']+\b";
            return Regex.IsMatch(word, upperCasePattern);
        }

        private static bool IsUpperCase(string word)
        {
            string upperCasePattern = @"\b[A-Z]+\b";
            return Regex.IsMatch(word, upperCasePattern);
        }

        private static string PigLatanizeWord(string word)
        {
            // Gracefully handle bad input
            if (string.IsNullOrWhiteSpace(word)) return "";

            // Pull out sentance puncuation
            string sentancePuncuationPattern = @"[\.\?\!\,]";
            string lastChar = word[word.Length - 1].ToString();
            string puncuation = string.Empty;
            if (Regex.IsMatch(lastChar, sentancePuncuationPattern))
            {
                puncuation += Regex.Match(lastChar, sentancePuncuationPattern);
            }

            // Don't convert non words (allows contractions)
            string notAWordPattern = @"[^A-Za-z']";
            if (Regex.IsMatch(word, notAWordPattern)) return word;

            // prep for output stirng
            string output = string.Empty;
            string firstChar = word[0].ToString();
            string vowelPattern = @"[AaEeIiOoUu]";

            // start with vowel if first char
            if (Regex.IsMatch(firstChar, vowelPattern))
            {
                output += firstChar;
            }

            // build output string
            for (int i = 1; i < word.Length; i++)
            {
                output += word[i];
            }
            
            // ending depens on if it is a vowel
            if (Regex.IsMatch(firstChar, vowelPattern))
            {
                output += "way";
            }
            else
            {
                output += firstChar + "ay";
            }

            // readd any senance puncuation and return
            return output + puncuation;
        }

        private static string GetStringFromUser(string prompt)
        {
            while (true)
            { 
                Console.Write(prompt);
                string rawInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(rawInput))
                {
                    return rawInput;
                }
                else
                {
                    Console.WriteLine("ERROR: Please enter a word or sentance to be converted.");
                }
            }
        }

        private static bool PromptForLoop(string prompt)
        {
            do
            {
                Console.Write(prompt);
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if ("Yy".Contains(key))
                {
                    return true;
                }
                else if ("Nn".Contains(key))
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("type y for yes or n for no");
                }
            } while (true);
        }
    }
}
