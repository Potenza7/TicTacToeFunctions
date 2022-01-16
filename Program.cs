using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            game.start();

            Console.Read();
        }
    }

    class Game
    {
        public Player PlayerX { get; set; }
        public Player PlayerO { get; set; }
        public Board Board { get; set; }

        private Player CurrentMovePlayer;
        private Player WaitingPlayer;

        public Game()
        {
            this.PlayerX = new Player("X");
            this.PlayerO = new Player("O");
            this.Board = new Board();
        }

        public void start()
        {
            Console.WriteLine("Welcome to tic-tac-toe!");

            string menuChoice = "";
            while (true)
            {
                showMenu();
                Console.Write("> ");
                menuChoice = Console.ReadLine();

                Console.WriteLine();

                if (menuChoice == "1")
                {
                    newGame();
                }
                else if (menuChoice == "2")
                {
                    Console.WriteLine("Author: Eray Kılıç\n");
                }
                else if (menuChoice == "3")
                {
                    Console.WriteLine("Are you sure want to quit? [y/n]");
                    Console.Write("> ");

                    string exitChoice = Console.ReadLine();

                    if (exitChoice == "y")
                        break;

                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Invalid Choice!");
                }
            }
        }

        public void newGame()
        {
            string[] board = this.Board.getEmptyBoard();

            this.switchPlayerTurn();
            bool availableToContinue = true;
            bool isAvailable = true;
            while (availableToContinue)
            {
                if (isAvailable)
                {
                    this.Board.writeBoard(board);

                    //  reverse behance
                    if (isWinner(board, this.WaitingPlayer.Mark))
                    {
                        this.announceWinner(this.WaitingPlayer);
                        Console.WriteLine("[Press Enter to return to main menu...]");
                        Console.ReadLine();
                        Console.WriteLine("\n");
                        break;
                    }

                    if (isGameOver(board))
                    {
                        Console.WriteLine("Game over!");
                        Console.WriteLine("[Press Enter to return to main menu...]");
                        Console.ReadLine();
                        Console.WriteLine("\n");
                        break;
                    }
                }

                Console.Write(this.CurrentMovePlayer.Mark + "'s move > ");

                string moveStr = Console.ReadLine();
                int move = -1;
                try
                {
                    move = Convert.ToInt32(moveStr);
                }
                catch
                {
                    Console.WriteLine("Illegal move! Try again.");
                    continue;
                }

                isAvailable = this.CurrentMovePlayer.isAvailableMove(board, move);
                if (isAvailable)
                {
                    Console.WriteLine();
                    board[move - 1] = this.CurrentMovePlayer.Mark;
                }
                else
                {
                    Console.WriteLine("Illegal move! Try again.");
                    continue;
                }

                this.switchPlayerTurn();
            }
        }

        public void showMenu()
        {
            Console.WriteLine("1. New game");
            Console.WriteLine("2. About the author");
            Console.WriteLine("3. Exit");
        }

        private bool isGameOver(string[] options)
        {
            bool gameOver = true;

            for (int i = 0; i < options.GetLength(0); i++)
            {
                if (options[i] == " ")
                {
                    gameOver = false;
                    break;
                }
            }

            return gameOver;
        }

        private void switchPlayerTurn()
        {
            if (this.CurrentMovePlayer == null)
            {
                this.CurrentMovePlayer = this.PlayerX;
                this.WaitingPlayer = this.PlayerO;
            }
            else if (this.CurrentMovePlayer.Equals(this.PlayerX))
            {
                this.CurrentMovePlayer = this.PlayerO;
                this.WaitingPlayer = this.PlayerX;
            }
            else
            {
                this.CurrentMovePlayer = this.PlayerX;
                this.WaitingPlayer = this.PlayerO;
            }
        }

        private void announceWinner(Player player)
        {
            Console.WriteLine(player.Mark + " won!");
        }

        private bool isHorizontalMatchFounded(string[] options, string playerMark)
        {
            return
                (options[0] == playerMark && options[1] == playerMark && options[2] == playerMark) ||
                (options[3] == playerMark && options[4] == playerMark && options[5] == playerMark) ||
                (options[6] == playerMark && options[7] == playerMark && options[8] == playerMark);
        }

        private bool isVerticalMatchFounded(string[] options, string playerMark)
        {
            return
                (options[0] == playerMark && options[3] == playerMark && options[6] == playerMark) ||
                (options[1] == playerMark && options[4] == playerMark && options[7] == playerMark) ||
                (options[2] == playerMark && options[5] == playerMark && options[8] == playerMark);
        }

        private bool isDiagonalMatchFounded(string[] options, string playerMark)
        {
            return
                (options[0] == playerMark && options[4] == playerMark && options[8] == playerMark) ||
                (options[2] == playerMark && options[4] == playerMark && options[6] == playerMark);
        }

        private bool isWinner(string[] options, string playerMark)
        {
            return isHorizontalMatchFounded(options, playerMark) || isVerticalMatchFounded(options, playerMark) || isDiagonalMatchFounded(options, playerMark);
        }
    }

    class Player
    {
        public string Mark { get; }

        public Player(string Mark)
        {
            this.Mark = Mark;
        }

        public bool isAvailableMove(string[] options, int move)
        {
            if (move <= 0 || move > options.Length)
            {
                return false;
            }
            else
            {
                return options[move - 1] == null || options[move - 1] == " ";
            }
        }
    }

    class Board
    {
        public string[] getEmptyBoard()
        {
            string[] board = new string[9];
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = " ";
            }

            return board;
        }

        public void writeBoard(string[] options)
        {
            Console.WriteLine($" {options[0]} | {options[1]} | {options[2]}");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {options[3]} | {options[4]} | {options[5]}");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {options[6]} | {options[7]} | {options[8]}");

            Console.WriteLine();
        }
    }
}