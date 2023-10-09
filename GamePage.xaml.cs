using Hangman.Models;
using Windows.Data.Text;
using Windows.Media.AppBroadcasting;

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
			GameOver();
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
        throw new NotImplementedException();
    }


    /*!
	 * Changes the image shown on the page and
	 * Updates the visibility of the labels representing the letters in the word
	 */
    private void UpdateDisplay(bool isCorrect, string word, char letter, int remainingAttempts)
    {
        throw new NotImplementedException();
    }


    /*!
	 * Resets all game variables and displays the final result
	 * Also displays the options to return to the menu, exit or play again
	 */
    private void GameOver()
	{
        throw new NotImplementedException();
    }

    private void OnBackToMenu(object sender, EventArgs e)
	{
        Navigation.PushAsync(new MainPage());
	}
}