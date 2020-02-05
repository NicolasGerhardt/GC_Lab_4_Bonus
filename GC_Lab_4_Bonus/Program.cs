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
                string userInput = GetStringFromUser("\nEnter in a phrase to be converted\n> ");
                string pigLatin = PigLatanizeSentance(userInput);

                var backgroundColor = Console.BackgroundColor;
                var foregroundColor = Console.ForegroundColor;
                
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine();
                Console.WriteLine(pigLatin);
                Console.WriteLine();
                Console.BackgroundColor = backgroundColor;
                Console.ForegroundColor = foregroundColor;
                

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

                if (IsTitleCase(word))
                {
                    outputWord = TitleCaseWord(outputWord);
                }
                
                if (IsUpperCase(word))
                {
                    outputWord = outputWord.ToUpper();
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
            string upperCasePattern = @"(\b[A-Z]+\'[A-Z]+\b|\b[A-Z][A-Z]+\b)";
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
                word = word.Substring(0, word.Length - 1);
            }

            // Don't convert non words (allows contractions)
            string notAWordPattern = @"[^A-Za-z']";
            if (Regex.IsMatch(word, notAWordPattern)) return word;

            //string firstChar = word[0].ToString();
            string vowelPattern = @"[AaEeIiOoUu]";
            
            int count = 0;
            bool StartsWithVowel = Regex.IsMatch(word[0].ToString(), vowelPattern);
            while (!Regex.IsMatch(word[0].ToString(), vowelPattern) && Regex.IsMatch(word, vowelPattern))
            {
                count++;
                word = word.Substring(1, word.Length - 1) + word[0];
            }

            
            if (count == 0 && StartsWithVowel)
            {
                word += "way";
            }
            else
            {
                word += "ay";
            }
            


            return word + puncuation;
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
