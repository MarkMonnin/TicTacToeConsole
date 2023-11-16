using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TicTacToeConsole;

namespace TicTacToe.UnitTests
{
    internal class GameTests
    {
        [Test]
        public void Play_OpenFirstSpace_AddsToSpace()
        {
            Game g = new Game();

            Assert.That(g.board[0, 0] == 0);
            g.Play(1);

            Assert.That(g.board[0, 0] == 1);
        }

        [Test]
        public void Play_OpenLastSpace_AddsToSpace()
        {
            Game g = new Game();

            Assert.That(g.board[2, 2] == 0);
            g.Play(9);

            Assert.That(g.board[2, 2] == 1);
        }

        [Test]
        public void Play_FullLastSpace_ReturnsMessage()
        {
            Game g = new Game();

            string response = "";
            response = g.Play(9);
            response = g.Play(9);

            Assert.That(response.StartsWith(g.ErrorCellAlreadyFull.Substring(0, 4)));
            Assert.That(response.EndsWith(g.ErrorCellAlreadyFull.Substring(g.ErrorCellAlreadyFull.Length - 4, 4)));
        }

        [Test]
        public void Play_OutOfBounds_ReturnsMessage()
        {
            Game g = new Game();

            string response = g.Play(10);

            Assert.That(response == g.ErrorCellOutOfBounds);
        }

        [Test]
        public void Play_GameAlreadyOver_ReturnsMessage()
        {
            Game g = new Game();
            g.gameIsOver = true;

            string response = g.Play(1);

            Assert.That(response == g.ErrorGameAlreadyOver);
        }

        [Test]
        public void Play_PlayerWins_ReturnsMessage()
        {
            Game g = new Game();

            string response = "";
            g.board[0, 0] = g.playerTurn;
            g.board[1, 1] = g.playerTurn;
            response = g.Play(9);

            Assert.That(response.StartsWith(g.MsgPlayerWon.Substring(0, 4)));
            Assert.That(response.EndsWith(g.MsgPlayerWon.Substring(g.MsgPlayerWon.Length - 4, 4)));
        }

        [Test]
        public void Play_PlayersTie_ReturnsMessage()
        {
            Game g = new Game();

            string response = "";
            g.board = new int[3, 3] {
               {1, 2, 1} ,
               {1, 2, 1} ,
               {2, 0, 2}
            };
            response = g.Play(8);

            Assert.That(response == g.MsgPlayersTied);
        }

        [Test]
        public void Play_PlayerWins_GameIsOver()
        {
            Game g = new Game();

            string response = "";
            g.board[0, 0] = g.playerTurn;
            g.board[1, 1] = g.playerTurn;
            response = g.Play(9);

            Assert.That(g.gameIsOver);
        }

        [Test]
        public void Play_PlayersTie_GameIsOver()
        {
            Game g = new Game();

            string response = "";
            g.board = new int[3, 3] {
               {1, 2, 1} ,
               {1, 2, 1} ,
               {2, 0, 2}
            };
            response = g.Play(8);

            Assert.That(g.gameIsOver);
        }

        [Test]
        public void CheckGameOver_FirstRowWin_ReturnsTrue()
        {
            Game g = new Game();

            g.board[0, 0] = 1;
            g.board[0, 1] = 1;
            g.board[0, 2] = 1;

            Assert.True(g.CheckGameOver(0, 0));
        }

        [Test]
        public void CheckGameOver_FirstColWin_ReturnsTrue()
        {
            Game g = new Game();

            g.board[0, 0] = 1;
            g.board[1, 0] = 1;
            g.board[2, 0] = 1;

            Assert.True(g.CheckGameOver(0, 0));
        }

        [Test]
        public void CheckGameOver_Diagonal1Win_ReturnsTrue()
        {
            Game g = new Game();

            g.board[0, 0] = 1;
            g.board[1, 1] = 1;
            g.board[2, 2] = 1;

            Assert.True(g.CheckGameOver(0, 0));
        }

        [Test]
        public void CheckGameOver_Diagonal2Win_ReturnsTrue()
        {
            Game g = new Game();

            g.board[0, 2] = 1;
            g.board[1, 1] = 1;
            g.board[2, 0] = 1;

            Assert.True(g.CheckGameOver(0, 2));
        }

        [Test]
        public void ToString_PlayCell7_ReturnsCorrectBoard()
        {
            Game g = new Game();

            Assert.That(g.board[2, 0] == 0);
            g.Play(7);

            Assert.That(g.ToString() ==
@"- - -
- - -
X - -
");
        }

        [Test]
        public void ToString_PlayCells1379_ReturnsCorrectBoard()
        {
            Game g = new Game();

            Assert.That(g.board[2, 0] == 0);
            g.Play(1);
            g.Play(3);
            g.Play(7);
            g.Play(9);

            Assert.That(g.ToString() ==
@"X - O
- - -
X - O
");
        }
    }
}
