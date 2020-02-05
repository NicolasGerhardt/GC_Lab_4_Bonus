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
                string userInput = GetLowerCaseStringFromUser("Enter in a word: ");

                string pigLatin = PigLatanizeWord(userInput);

                Console.WriteLine(pigLatin);
                Console.WriteLine();

            } while (PromptForLoop("Convert another word? (y/n) \n"));
        }

        private static string PigLatanizeWord(string word)
        {
            // Gracefully handle bad input
            if (string.IsNullOrWhiteSpace(word)) return "";
            
            // Don't convert non words
            string notAWordPattern = @"[^A-Za-z']";
            if (Regex.IsMatch(word, notAWordPattern)) return word;

            string output = string.Empty;
            string firstChar = word[0].ToString();

            if ("AaEeIiOoUu".Contains(firstChar))
            {
                output += firstChar;
            }

            for (int i = 1; i < word.Length; i++)
            {
                output += word[i];
            }

            if ("AaEeIiOoUu".Contains(firstChar))
            {
                output += "way";
            }
            else
            {
                output += firstChar + "ay";
            }


            return output;
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
