using Hangman.Models;
using System.ComponentModel.Design;
using Windows.Data.Text;
using Windows.Media.AppBroadcasting;
using System.Linq;
using Windows.UI.Notifications;
using Windows.ApplicationModel.Chat;
using Windows.Media.Capture;

namespace Hangman;

public partial class GamePage : ContentPage
{
    public string GameType { get; set; }
    List<char> LettersTried { get; set; }
    char CurrentLetterGuess { get; set; }
    public string Word { get; set; }

    int remainingAttempts = 7;

    public GamePage(string gameType)
    {
        InitializeComponent();

        GameType = gameType;
        BindingContext = this;

        //Added by Robert, Loads word list before game starts
        InitializeGame();
    }

    private async void InitializeGame()
    {
        await HangmanWords.WordsLoaded;
        CreateNewChallenge();
    }

    /* Requires testing */
    private void CreateNewChallenge()
    {
        Word = SelectWord(GameType);
        ResetDisplay(Word);
    }

    /*!
	 * Resets the display to the initial image and
	 * the appropriate number of visible labels
	 */
    private void ResetDisplay(string word)
    {

        // Reset hangman image and update UI for hidden word and remaining attempts.

        for (int i = 0; i < 12; i++)
        {
            if (i < word.Length)
            {
                char letter = word[i];
                Label letterLabel = this.FindByName<Label>("Letter" + (i + 1));
                letterLabel.Text = char.IsWhiteSpace(letter) ? " " : letter.ToString();
            }
            else
            {
                Label letterLabel = this.FindByName<Label>("Letter" + (i + 1));
                letterLabel.Text = " ";
            }
        }

        RemainingAttemptsLabel.Text = $"Remaining Attempts: {remainingAttempts}";
    }


    /*!
	 * @param gameType
	 * @brief returns a random word based on difficulty
	 * 
	 * Based on the users selected difficulty, find a word suitable
	 * and return it
	 */
    private string SelectWord(string gameType)
    {
        Random random = new Random();

        switch (gameType)
        {

            case "Easy":

                String wordChosen = HangmanWords.EasyWords[random.Next(HangmanWords.EasyWords.Count)];
                DisplayAlert("Alert", wordChosen, "OK");
                return wordChosen;

            case "Medium":

                return HangmanWords.MediumWords[random.Next(HangmanWords.MediumWords.Count)];

            case "Hard":

                return HangmanWords.HardWords[random.Next(HangmanWords.HardWords.Count)];

        }

        return "Unknown";
    }


    /* Requires testing */
    private void OnAttemptSubmitted(object sender, EventArgs e)
    {
        var answer = AnswerEntry.Text[0];
        var isCorrect = false;

        isCorrect = CheckLetterInWord(Word, answer);

        if (isCorrect == false)
        {
            remainingAttempts--;
        }
        UpdateDisplay(isCorrect, Word, answer, remainingAttempts);

        AnswerEntry.Text = "";
        AnswerEntry.Focus();

        if (remainingAttempts == 0)
        {
            GameOver(Word);
        }

    }


    private bool CheckLetterInWord(string word, char answer)
    {
        if (word.ToLower().Contains(answer))
        {
            return true;
        }
        return false;
    }


    /*!
	 * Changes the image shown on the page and
	 * Updates the visibility of the labels representing the letters in the word
	 */
    private async void UpdateDisplay(bool isCorrect, string word, char letter, int remainingAttempts)
    {

        // stub to show the program is using the correct letter against the word returned in SelectWord function
        if (remainingAttempts > 0 && isCorrect)
        {
            await DisplayAlert("Good", letter.ToString() + " is in word", "OK");

            /// this needs to update the display
        }

        else
        {
            RemainingAttemptsLabel.Text = $"Remaining Attempts: {remainingAttempts}";
            await DisplayAlert("No", letter.ToString() + " is not in the word", "OK");
        }
    }


    /*!
	 * Resets all game variables and displays the final result
	 * Also displays the options to return to the menu, exit or play again
	 */

    private void GameOver(string word)
    {
        //DisplayAlert("Sorry", "The correct answer was " + word, "OK");
        CreateNewChallenge();

    }

    private void OnBackToMenu(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
}