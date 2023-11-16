using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeConsole
{
    public class Game
    {
        public int[,] board = new int[3, 3];
        public int playerTurn = 1;
        public bool gameIsOver = false;
        public int gameWinner = 0;

        public string ErrorInputNotANumber { get { return "Sorry, the input should be a number"; } }
        public string ErrorCellOutOfBounds { get { return "Please choose a position between 1 and 9"; } }
        public string ErrorGameAlreadyOver { get { return "Can't play because the game is over"; } }
        public string ErrorCellAlreadyFull { get { return "Can't play position @cell because it is already full"; } }
        public string MsgPlayerWon { get { return "Player @playerTurn won!"; } }
        public string MsgPlayersTied { get { return "It's a draw!"; } }

        public Game()
        {
            for(int r = 0; r < board.GetLength(0); r++)
                for(int c = 0; c < board.GetLength(1); c++)
                {
                    board[r,c] = 0;
                }
        }

        public void Run()
        {
            // introduce
            Console.WriteLine("Welcome to Tic Tac Toe! The board has 9 positions as shown below. Player 1 is X, Player 2 is O.");

            while (!gameIsOver)
            {
                Console.WriteLine(this.ToString());

                // continue to ask for input until game is over
                Console.WriteLine($"Please choose a position for player {playerTurn}");
                string? str = Console.ReadLine();

                int num;
                if (!int.TryParse(str, out num))
                {
                    Console.WriteLine(ErrorInputNotANumber);
                    continue;
                }

                string response = Play(num);

                if (response.Length > 0)
                    Console.WriteLine(response);
            }
        }

        public string Play(int cell)
        {
            int cellIndex = cell - 1;
            int row = cellIndex / 3;
            int col = cellIndex % 3;

            if (cell < 1 || cell > 9)
            {
                return ErrorCellOutOfBounds;
            }
            if (gameIsOver)
            {
                return ErrorGameAlreadyOver;
            }
            if (board[row, col] != 0)
            {
                return ErrorCellAlreadyFull.Replace("@cell", cell.ToString());
            }
            // make the move
            board[row, col] = playerTurn;

            if (CheckGameOver(row, col))
            {
                if (gameWinner == playerTurn)
                    return MsgPlayerWon.Replace("@playerTurn", playerTurn.ToString());
                else
                    return MsgPlayersTied;
            }

            // next player's turn
            playerTurn = playerTurn == 1 ? 2 : 1;
            return "";
        }
        public bool CheckGameOver(int col, int row)
        {
            if (gameIsOver)
            {
                return true;
            }

            // check col
            int count = 0;
            for (int r = 0; r < board.GetLength(0); r++)
            {
                if (board[r, col] == playerTurn)
                    count++;
                else
                    break; // not all same
            }
            if (count == 3)
            {
                PlayerWon();
                return true;
            }

            // check row
            count = 0;
            for (int c = 0; c < board.GetLength(1); c++)
            {
                if (board[row, c] == playerTurn)
                    count++;
                else
                    break; // not all same
            }
            if (count == 3)
            {
                PlayerWon();
                return true;
            }

            // check diagonals (regardless if the play was in a diagonal)
            count = 0;
            for (int r = 0; r < board.GetLength(0); r++)
            {
                if (board[r, r] == playerTurn)
                    count++;
                else
                    break; // not all same
            }
            if (count == 3)
            {
                PlayerWon();
                return true;
            }

            // opposite slope
            count = 0;
            for (int r = 0; r < board.GetLength(0); r++)
            {
                if (board[r, board.GetLength(0) - r - 1] == playerTurn)
                    count++;
                else
                    break; // not all same
            }
            if (count == 3)
            {
                PlayerWon();
                return true;
            }


            // check for a draw
            count = 0;
            for (int r = 0; r < board.GetLength(0); r++)
                for (int c = 0; c < board.GetLength(1); c++)
                {
                    if (board[r, c] == 0)
                    {
                        break; // not full
                    }
                    else
                    {
                        count++;
                    }
                }

            if (count == 9)
            {
                PlayersTied();
                return true;
            }

            return false;
        }

        public void PlayerWon()
        {
            gameIsOver = true;
            gameWinner = playerTurn;
        }

        public void PlayersTied()
        {
            gameIsOver = true;
            gameWinner = 0;
        }

        //write board
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (col > 0)
                        sb.Append(" ");

                    if (board[row, col] > 0)
                        sb.Append(board[row, col] == 1 ? "X" : "O");
                    else
                        sb.Append("-");
                }
                sb.AppendLine("");
            }

            return sb.ToString();
        }
    }
}
