using TicTacToeMobile.Model;
using TicTacToeMobile.Utility;

namespace TicTacToeMobile
{
	public partial class MainPage : ContentPage
	{
		public Game Game { get; set; }

		public MainPage()
		{
			InitializeComponent();
			InitializeButtons();

			Game = new Game();

			this.BindingContext = Game;
		}

		public void InitializeButtons()
		{
			const int SIZE = 3;
			Style customStyle = StyleUtil.ButtonStyles;

			for (int i = 0; i < SIZE * SIZE; i++)
			{
				Button button = new Button();
				button.Style = customStyle;
				button.Clicked += OnButtonClicked;

				int row = i / SIZE;
				int col = i % SIZE;

				// button.Text = (row + col) % 2 == 0 ? "X" : "O";

				button.BindingContext = new ButtonPosition { Row = row, Column = col };

				Grid.SetColumn(button, row);
				Grid.SetRow(button, col);
				ButtonGrid.Children.Add(button);
			}
		}


		private async void OnButtonClicked(object? sender, EventArgs e)
		{
			if (sender is Button button)
			{
				Button? currentButton = sender as Button;

				await currentButton.ScaleTo(0.95, 100, Easing.BounceIn);
				await currentButton.ScaleTo(1.00, 100, Easing.BounceOut);

				ButtonPosition? context = button.BindingContext as ButtonPosition;
				int row = context.Row;
				int col = context.Column;

				char piece = Game.GetPieceAt(row, col);

				if (piece != ' ')
				{
					return;
				}

				Game.PlacePieceAt(row, col);

				piece = Game.GetPieceAt(row, col);
				currentButton.Text = piece.ToString();

				if (currentButton.Text == "X")
				{
					currentButton.TextColor = Color.FromArgb("#FF0000");
				}
				else if (currentButton.Text == "O")
				{
					currentButton.TextColor = Color.FromArgb("#FFFFFF");
				}

				if (Game.WinningPlayer() != Game.PlayerEnum.None)
				{
					RestartGame();
				}
				else if (Game.IsStalemate())
				{
					RestartGame();
				}
			}
		}

		private async void OnRestartClicked(object sender, EventArgs e)
		{
			Button? currentButton = sender as Button;

			await currentButton.ScaleTo(0.95, 100, Easing.BounceIn);
			await currentButton.ScaleTo(1.00, 100, Easing.BounceOut);
			Game.NewGame();
		}

		private void RestartGame()
		{
			Game.Restart();

			ButtonGrid.Children.Clear();

			InitializeButtons();
		}

		public class ButtonPosition
		{
			public int Row { get; set; }
			public int Column { get; set; }
		}

	}

}
