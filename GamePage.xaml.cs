using Hangman.Models;
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
	public string Word {  get; set; }

    int remainingAttempts = 7;

	public GamePage(string gameType)
	{
		InitializeComponent();

        GameType = gameType;
		BindingContext = this;

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
        
    }

    /*!
	 * Uses the GameType to select a word from the list by its length:
	 * Easy : length < 7
	 * Medium : 7 <= length < 10
	 * Hard : length >= 10
	 */
    private string SelectWord(string gameType)
    {
		return "placeholder";
    }

	/* Requires testing */
    private void OnAttemptSubmitted(object sender, EventArgs e)
	{
        var answer = AnswerEntry.Text[0];
        var isCorrect = false;

		isCorrect = CheckLetterInWord(Word, answer);
		UpdateDisplay(isCorrect, Word, answer, remainingAttempts);

        remainingAttempts--;
		AnswerEntry.Text = "";
		AnswerEntry.Focus();

        if (remainingAttempts == 0)
		{
			GameOver(Word);
		}
    }

    /*!
	 * Uses the GameType to select a word from the list by its length:
	 * Easy : length < 7
	 * Medium : 7 <= length < 10
	 * Hard : length >= 10
	 */
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
            await DisplayAlert("No", letter.ToString() + " is not in the word", "OK");
        }
    }


    /*!
	 * Resets all game variables and displays the final result
	 * Also displays the options to return to the menu, exit or play again
	 */
    private void GameOver(string word)
	{
        DisplayAlert("Sorry", "The correct answer was " + word, "OK");
		// add function to reset?
    }

    private void OnBackToMenu(object sender, EventArgs e)
	{
        Navigation.PushAsync(new MainPage());
	}
}