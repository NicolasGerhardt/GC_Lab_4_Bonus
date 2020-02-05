using System;
using System.Text.RegularExpressions;

namespace GC_Lab_4_Bonus
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                string userInput = GetLowerCaseStringFromUser("Enter in a Sentace: ");


                string pigLatin = PigLatanizeSentance(userInput);

                Console.WriteLine(pigLatin);
                Console.WriteLine();

            } while (PromptForLoop("Convert another word? (y/n) \n"));
        }

        private static string PigLatanizeSentance(string sentance)
        {
            // Gracefully handle bad input
            if (string.IsNullOrWhiteSpace(sentance)) return sentance;
            
            string output = string.Empty;
            string[] words = sentance.Split(" ");

            foreach (string word in words)
            {
                output += PigLatanizeWord(word);
                output += " ";
            }

            return output.Trim();
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

        private static string GetLowerCaseStringFromUser(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine().ToLower();
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
