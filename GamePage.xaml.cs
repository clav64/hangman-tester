using System.Diagnostics;
using System.IO;
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

   // private List<string> wordList;

    public GamePage(string gameType)
    {
        //LoadWordList();
        InitializeComponent();
        GameType = gameType;
        BindingContext = this;
        CreateNewChallenge();
    }

/*!
    private void LoadWordList()
    {
        wordList = new List<string>();

        var resourceName = "C:/Users/rober/Source/Repos/hangman-tester/Resources/Raw/wordList.txt";
        var assembly = typeof(GamePage).Assembly;


        Debug.WriteLine("Available Resources:");
        foreach (var res in assembly.GetManifestResourceNames())
        {
            Debug.WriteLine(res);
        }


        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        using (StreamReader reader = new StreamReader(stream))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                wordList.Add(line);
            }
        }
    }
//
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
        switch (gameType)
        {
            case "Easy":
                return wordList.Find(word => word.Length < 7);
            case "Medium":
                return wordList.Find(word => word.Length >= 7 && word.Length < 10);
            case "Hard":
                return wordList.Find(word => word.Length >= 10);
            default:
                return "placeholder";
        }
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