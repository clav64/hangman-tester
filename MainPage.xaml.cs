using Hangman.Models;
using System.Diagnostics;

namespace Hangman;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // Call the public method to load and categorize words
        LoadWordsAsync();
    }
    private async void LoadWordsAsync()
    {
        try
        {
            // Load and process the word list from the asset wordList.txt
            await HangmanWords.LoadAndCategorizeWords();

            // Words are now loaded and categorized
            Debug.WriteLine("Words loaded successfully.");

            //Debugging - Listing the words to ensure they have loaded in
            Debug.WriteLine("Easy Words:");
            foreach (var Word in HangmanWords.EasyWords)
            {
                Debug.WriteLine(Word);
            }

            Debug.WriteLine("Medium Words:");
            foreach (var Word in HangmanWords.MediumWords)
            {
                Debug.WriteLine(Word);
            }

            Debug.WriteLine("Hard Words:");
            foreach (var Word in HangmanWords.HardWords)
            {
                Debug.WriteLine(Word);
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during the loading process
            Debug.WriteLine($"Error loading words: {ex.Message}");
        }
    }
    private void OnGameChosen(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        Navigation.PushAsync(new GamePage(button.Text));
    }

    private void OnViewPreviousGames(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        Navigation.PushAsync(new PreviousGames());
    }
}