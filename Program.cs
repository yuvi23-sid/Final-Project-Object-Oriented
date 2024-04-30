using System;

public class ConnectFourGame
{
    private const int Rows = 6;
    private const int Cols = 7;

    private char[,] board;
    private bool isPlayerOneTurn;

    public ConnectFourGame()
    {
        board = new char[Rows, Cols];
        InitializeBoard();
        isPlayerOneTurn = true;
    }

    private void InitializeBoard()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                board[row, col] = ' ';
            }
        }
    }

    public void PlayGame()
    {
        Console.Clear();
        DisplayTitleScreen();

        while (true)
        {
            int choice = GetMainMenuChoice();

            switch (choice)
            {
                case 1:
                    StartNewGame();
                    break;
                case 2:
                    Console.WriteLine("Thanks for playing! Goodbye!");
                    return;
            }
        }
    }

    private void DisplayTitleScreen()
    {
        Console.Clear();

        string title = @"
 CONNECT FOUR
                                            
";
        Console.WriteLine(title);
        Console.WriteLine();

        Console.WriteLine("1. Start New Game");
        Console.WriteLine("2. Exit");
    }



    private int GetMainMenuChoice()
    {
        int choice;
        while (true)
        {
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 2)
            {
                return choice;
            }
            else
            {
                Console.WriteLine("Invalid choice! Please enter 1 or 2.");
            }
        }
    }

    private void StartNewGame()
    {
        Console.Clear();
        Console.WriteLine("Welcome to Connect Four!");
        Console.WriteLine("Player 1: X, Player 2: O");

        bool gameover = false;
        int moves = 0;

        while (!gameover)
        {
            DrawBoard();

            int column = GetPlayerMove();

            if (IsValidMove(column))
            {
                MakeMove(column);
                moves++;
                if (CheckForWin())
                {
                    DrawBoard();
                    string winner = isPlayerOneTurn ? "Player 1" : "Player 2";
                    Console.WriteLine($"Congratulations {winner}! You won!");
                    gameover = true;
                }
                else if (moves == Rows * Cols)
                {
                    DrawBoard();
                    Console.WriteLine("It's a draw!");
                    gameover = true;
                }
                else
                {
                    isPlayerOneTurn = !isPlayerOneTurn;
                }
            }
            else
            {
                Console.WriteLine("Invalid move! Please try again.");
            }
        }

        Console.WriteLine("Do you want to go back to the main menu? (y/n)");
        string response = Console.ReadLine().Trim().ToLower();
        if (response == "y")
        {
            DisplayTitleScreen();
        }
        else
        {
            Console.WriteLine("Thanks for playing! Goodbye!");
            Environment.Exit(0);
        }
    }

    private void DrawBoard()
    {
        Console.WriteLine();
        for (int row = Rows - 1; row >= 0; row--)
        {
            Console.Write("|");
            for (int col = 0; col < Cols; col++)
            {
                Console.Write(board[row, col].ToString().PadLeft(3));
                Console.Write("|");
            }
            Console.WriteLine();
        }

        Console.WriteLine("  1   2   3   4   5   6   7");
        Console.WriteLine();
    }

    private int GetPlayerMove()
    {
        int column;
        while (true)
        {
            Console.Write($"Player {(isPlayerOneTurn ? "1" : "2")}, enter column (1-7): ");
            if (int.TryParse(Console.ReadLine(), out column) && column >= 1 && column <= 7)
            {
                return column - 1;
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a number between 1 and 7.");
            }
        }
    }

    private bool IsValidMove(int column)
    {
        return column >= 0 && column < Cols && board[Rows - 1, column] == ' ';
    }

    private void MakeMove(int column)
    {
        char symbol = isPlayerOneTurn ? 'X' : 'O';
        for (int row = 0; row < Rows; row++)
        {
            if (board[row, column] == ' ')
            {
                board[row, column] = symbol;
                break;
            }
        }
    }

    private bool CheckForWin()
    {
        char symbol = isPlayerOneTurn ? 'X' : 'O';

        // Check rows
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col <= Cols - 4; col++)
            {
                if (board[row, col] == symbol && board[row, col + 1] == symbol && board[row, col + 2] == symbol && board[row, col + 3] == symbol)
                {
                    return true;
                }
            }
        }

        // Check columns
        for (int row = 0; row <= Rows - 4; row++)
        {
            for (int col = 0; col < Cols; col++)
            {
                if (board[row, col] == symbol && board[row + 1, col] == symbol && board[row + 2, col] == symbol && board[row + 3, col] == symbol)
                {
                    return true;
                }
            }
        }

        // Check diagonals
        for (int row = 0; row <= Rows - 4; row++)
        {
            for (int col = 0; col <= Cols - 4; col++)
            {
                if (board[row, col] == symbol && board[row + 1, col + 1] == symbol && board[row + 2, col + 2] == symbol && board[row + 3, col + 3] == symbol)
                {
                    return true;
                }
                if (board[row + 3, col] == symbol && board[row + 2, col + 1] == symbol && board[row + 1, col + 2] == symbol && board[row, col + 3] == symbol)
                {
                    return true;
                }
            }
        }

        return false;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        ConnectFourGame game = new ConnectFourGame();
        game.PlayGame();
    }
}
