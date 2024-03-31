using TicTacToeMobile.Utility;

namespace TicTacToeMobile.Model
{
	public class Game : Bindable
	{
		public enum PlayerEnum
		{
			PlayerX,
			PlayerY,
			None
		}


		private const int SIZE = 3;


		public char[] Board { get; private set; }
		public PlayerEnum CurrentPlayer { get; private set; }
		private int _playerXScore { get; set; } = 0;
		public int PlayerXScore
		{
			get { return _playerXScore; }
			private set { _playerXScore = value; OnPropertyChanged(); }
		}

		private int _playerYScore { get; set; } = 0;

		public int PlayerYScore
		{
			get { return _playerYScore; }
			private set { _playerYScore = value; OnPropertyChanged(); }
		}

		public Game()
		{
			Board = new char[SIZE * SIZE];

			Restart();
		}


		public void PlacePieceAt(int x, int y)
		{
			int index = y * SIZE + x;
			Board[index] = CurrentPlayer == PlayerEnum.PlayerX ? 'X' : 'O';

			CurrentPlayer = CurrentPlayer == PlayerEnum.PlayerX ? PlayerEnum.PlayerY : PlayerEnum.PlayerX;
		}


		public char GetPieceAt(int x, int y)
		{
			int index = y * SIZE + x;

			if (index >= Board.Length)
			{
				return ' ';
			}

			return Board[index];
		}


		public PlayerEnum WinningPlayer()
		{
			PlayerEnum player = PlayerEnum.None;


			// Overkill to parallelize but would be relevant for larger grids

			Parallel.For(0, SIZE, i =>
			{
				if (player == PlayerEnum.None && RowWinConditionCheck(i * SIZE))
				{
					player = Board[i * SIZE] == 'X' ? PlayerEnum.PlayerX : PlayerEnum.PlayerY;
				}

				if (player == PlayerEnum.None && ColumnWinConditionCheck(i))
				{
					player = Board[i] == 'X' ? PlayerEnum.PlayerX : PlayerEnum.PlayerY;
				}
			});


			if (player == PlayerEnum.None && PrimaryDiagonalWinConditionCheck())
			{
				player = Board[0] == 'X' ? PlayerEnum.PlayerX : PlayerEnum.PlayerY;
			}

			if (player == PlayerEnum.None && SecondaryDiagonalWinConditionCheck())
			{
				player = Board[SIZE - 1] == 'X' ? PlayerEnum.PlayerX : PlayerEnum.PlayerY;
			}

			AddToScoreCounter(player);

			return player;
		}

		public bool IsStalemate()
		{
            foreach (char val in Board)
            {
                if (val == ' ') {  return false; }
            }

			return true;
        }


		private bool RowWinConditionCheck(int rowIndex)
		{
			return Board[rowIndex] != ' ' &&
				Board[rowIndex] == Board[rowIndex + 1] &&
				Board[rowIndex + 1] == Board[rowIndex + 2];
		}

		private bool ColumnWinConditionCheck(int columnIndex)
		{
			return Board[columnIndex] != ' ' &&
				Board[columnIndex] == Board[columnIndex + SIZE] &&
				Board[columnIndex + SIZE] == Board[columnIndex + 2 * SIZE];
		}

		private bool PrimaryDiagonalWinConditionCheck()
		{
			return Board[0] != ' ' &&
				Board[0] == Board[SIZE + 1] &&
				Board[SIZE + 1] == Board[2 * SIZE + 2];
		}

		private bool SecondaryDiagonalWinConditionCheck()
		{
			return Board[SIZE - 1] != ' ' &&
				Board[SIZE - 1] == Board[SIZE + SIZE - 2] &&
				Board[SIZE + SIZE - 2] == Board[SIZE + SIZE];
		}


		private void AddToScoreCounter(PlayerEnum player)
		{
			switch (player)
			{
				case PlayerEnum.PlayerX:
					PlayerXScore += 1;
					break;
				case PlayerEnum.PlayerY:
					PlayerYScore += 1;
					break;
				default:
					return;
			}
		}

		public void Restart()
		{
			for (int i = 0; i < Board.Length; i++)
			{
				Board[i] = ' ';
			}

			CurrentPlayer = PlayerEnum.PlayerX;
		}

		public void NewGame()
		{
			Restart();
			PlayerXScore = 0;
			PlayerYScore = 0;
		}

	}
}
