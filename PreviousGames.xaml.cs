namespace Hangman;

public partial class PreviousGames : ContentPage
{
	public PreviousGames()
	{
		InitializeComponent();
		gamesList.ItemsSource = App.GameRepository.GetAllGames();
	}

	private void OnDelete(object sender, EventArgs e)
	{
		Button button = sender as Button;
		App.GameRepository.Delete((int)button.BindingContext);
		gamesList.ItemsSource = App.GameRepository.GetAllGames();
	}
}