using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;


namespace Hangman.Models
{
    internal class HangmanWords
    {
        public static List<string> EasyWords { get; private set; }
        public static List<string> MediumWords { get; private set; }
        public static List<string> HardWords { get; private set; }

        //Debug boolean task
        private static TaskCompletionSource<bool> loadingTaskCompletionSource = new TaskCompletionSource<bool>();

        public static Task WordsLoaded => loadingTaskCompletionSource.Task;

        static HangmanWords()
        {
            EasyWords = new List<string>();
            MediumWords = new List<string>();
            HardWords = new List<string>();
        }

        // Expose a public method for loading and categorizing words
        public static async Task LoadAndCategorizeWords()
        {
            await CategorizeWordsFromMauiAsset();
        }



 /*!
 * Uses the GameType to select a word from the list by its length:
 * Easy : length < 7
 * Medium : 7 <= length < 10
 * Hard : length >= 10
 */

        //Catagorises the 3 lists while reading from the text file.

        protected static async Task CategorizeWordsFromMauiAsset()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("Resources/Raw/wordList.txt");
                using var reader = new StreamReader(stream);

                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var Word = line.Trim();  // Directly trim and categorize the word
                    
                    
                    if (Word.Length < 7)
                    {
                        EasyWords.Add(Word);
                    }
                    else if (Word.Length >= 7 && Word.Length < 10)
                    {
                        MediumWords.Add(Word);
                    }
                    else
                    {
                        HardWords.Add(Word);
                    }
                }
                Console.WriteLine("Words categorized successfully.");
                loadingTaskCompletionSource.SetResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the asset: {ex.Message}");
            }
        }

    }
}
