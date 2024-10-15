using System;  // Importing the System namespace for console operations
using TicTacToeRendererLib;  // Importing the TicTacToe renderer library
using TicTacToeRendererLib.Enums;  // Importing the Enums used in the renderer
using TicTacToeRendererLib.Renderer;  // Importing the renderer class for TicTacToe

namespace TicTacToeSubmissionConole  // Defining the namespace for the TicTacToe game
{
    public class TicTacToe  // Main class for the TicTacToe game
    {
        private TicTacToeConsoleRenderer _renderer;  // Renderer object to display the game board
        private Player[,] _gameBoard;  // 2D array representing the game board
        private Player _activePlayer;  // Variable to track the active player

        public enum Player  // Enum to represent different players and empty spots
        {
            Empty,  // Unoccupied space
            X,      // Player X
            O       // Player O
        }

        public TicTacToe()  // Constructor to initialize the game board and set up the game
        {
            _renderer = new TicTacToeConsoleRenderer(10, 6);  // Initialize renderer with specific board size
            _gameBoard = new Player[3, 3];  // Create a 3x3 game board
            _activePlayer = Player.X;  // Set the first active player to Player X

            // Initialize the game board with empty spaces
            for (int i = 0; i < 3; i++)  // Loop through rows
                for (int j = 0; j < 3; j++)  // Loop through columns
                    _gameBoard[i, j] = Player.Empty;  // Set each cell to empty
        }

        public void Run()  // Main function to start the game loop
        {
            bool isGameActive = true;  // Flag to track if the game is ongoing

            while (isGameActive)  // Loop until the game ends
            {
                _renderer.Render();  // Render the game board

                Console.SetCursorPosition(2, 19);  // Set cursor to display player prompt
                Console.Write($"Player {_activePlayer}, make your move!");  // Prompt current player to make a move

                int row = -1, col = -1;  // Variables to store user input for row and column
                bool validMove = false;  // Flag to track if the move is valid

                // Validate input for row and column
                while (!validMove)  // Keep prompting until a valid move is entered
                {
                    try
                    {
                        // Get row input
                        Console.SetCursorPosition(0, 20);  // Set cursor for row input
                        ClearLine();  // Clear any previous input
                        Console.SetCursorPosition(2, 20);  // Set position for row prompt
                        Console.Write("Enter Row (0, 1, 2): ");  // Prompt for row
                        row = int.Parse(Console.ReadLine());  // Read row input from user

                        // Get column input
                        Console.SetCursorPosition(0, 22);  // Set cursor for column input
                        ClearLine();  // Clear any previous input
                        Console.SetCursorPosition(2, 22);  // Set position for column prompt
                        Console.Write("Enter Column (0, 1, 2): ");  // Prompt for column
                        col = int.Parse(Console.ReadLine());  // Read column input from user

                        // Validate row and column range and check if the spot is available
                        if (row >= 0 && row < 3 && col >= 0 && col < 3)  // Check if the input is within range
                        {
                            if (_gameBoard[row, col] == Player.Empty)  // Check if the selected spot is empty
                            {
                                _gameBoard[row, col] = _activePlayer;  // Place the player's marker on the board
                                validMove = true;  // Mark the move as valid
                            }
                            else
                            {
                                DisplayError("Spot already taken.");  // Show error if spot is already taken
                            }
                        }
                        else
                        {
                            DisplayError("Invalid range. Please enter values between 0 and 2.");  // Show error if input is out of range
                        }
                    }
                    catch
                    {
                        DisplayError("Invalid input. Please enter a valid number.");  // Show error if input is not a valid number
                    }
                }

                // Map current player to the TicTacToeRendererLib player enum
                TicTacToeRendererLib.Enums.PlayerEnum mappedPlayer = _activePlayer == Player.X
                    ? TicTacToeRendererLib.Enums.PlayerEnum.X  // Map Player X to PlayerEnum.X
                    : TicTacToeRendererLib.Enums.PlayerEnum.O;  // Map Player O to PlayerEnum.O

                _renderer.AddMove(row, col, mappedPlayer, true);  // Add the player's move to the renderer and update the board

                // Check for a win or draw after the move
                if (IsWinningMove(row, col))  // Check if the current move wins the game
                {
                    Console.SetCursorPosition(2, 24);  // Set cursor to display the result
                    Console.WriteLine($"Player {_activePlayer} wins!");  // Announce the winning player
                    isGameActive = false;  // End the game
                }
                else if (IsDraw())  // Check if the game is a draw
                {
                    Console.SetCursorPosition(2, 24);  // Set cursor to display the result
                    Console.WriteLine("The game is a draw!");  // Announce the draw
                    isGameActive = false;  // End the game
                }
                else
                {
                    // Switch player
                    _activePlayer = _activePlayer == Player.X ? Player.O : Player.X;  // Switch the active player after each valid move
                }
            }
        }

        private bool IsWinningMove(int row, int col)  // Function to check if the last move is a winning move
        {
            // Check row for win
            if (_gameBoard[row, 0] == _activePlayer && _gameBoard[row, 1] == _activePlayer && _gameBoard[row, 2] == _activePlayer)
                return true;  // Return true if the entire row is occupied by the current player

            // Check column for win
            if (_gameBoard[0, col] == _activePlayer && _gameBoard[1, col] == _activePlayer && _gameBoard[2, col] == _activePlayer)
                return true;  // Return true if the entire column is occupied by the current player

            // Check diagonals for win
            if (_gameBoard[0, 0] == _activePlayer && _gameBoard[1, 1] == _activePlayer && _gameBoard[2, 2] == _activePlayer)
                return true;  // Return true if the main diagonal is occupied by the current player

            if (_gameBoard[0, 2] == _activePlayer && _gameBoard[1, 1] == _activePlayer && _gameBoard[2, 0] == _activePlayer)
                return true;  // Return true if the anti-diagonal is occupied by the current player

            return false;  // Return false if no winning condition is met
        }

        private bool IsDraw()  // Function to check if the game is a draw
        {
            foreach (var cell in _gameBoard)  // Loop through each cell in the game board
            {
                if (cell == Player.Empty)  // If any cell is empty, the game is not a draw
                    return false;  // Return false if any empty cell is found
            }
            return true;  // Return true if no empty cells are found (the board is full)
        }

        private void ClearLine()  // Function to clear a line in the console
        {
            Console.Write(new string(' ', Console.WindowWidth));  // Overwrite the current line with spaces to clear it
        }

        private void DisplayError(string message)  // Function to display error messages
        {
            Console.SetCursorPosition(2, 23);  // Set cursor to the error display line
            Console.WriteLine(message);  // Output the error message to the console
        }
    }
}
