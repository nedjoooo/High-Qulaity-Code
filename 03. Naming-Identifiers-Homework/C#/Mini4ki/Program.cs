using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mini4ki
{
    public class Minesweeper
    {
        public class Ranking
        {
            private string name;

            private int points;

            public string Player
            {
                get
                {
                    return this.name;
                }
                set
                {
                    this.name = value;
                }
            }

            public int Points
            {
                get
                {
                    return this.points;
                }

                set
                {
                    this.points = value;
                }
            }

            public Ranking()
            {
            }

            public Ranking(string name, int points)
            {
                this.name = name;
                this.points = points;
            }
        }

        private static void Main(string[] arguments)
        {
            string menuNavigation = string.Empty;
            char[,] gameField = CreateGameField();
            char[,] bombs = SetBombs();
            int movesCounter = 0;
            bool mineField = false;
            List<Ranking> championsList = new List<Ranking>(6);
            int fieldRow = 0;
            int fieldColumn = 0;
            bool flagNewGame = true;
            const int MaksimumGameMoves = 35;
            bool flagEndGame = false;

            do
            {
                if (flagNewGame)
                {
                    Console.WriteLine(
                        "Hajde da igraem na “Mini4KI”. Probvaj si kasmeta da otkriesh poleteta bez mini4ki."
                        + " Komanda 'top' pokazva klasiraneto, 'restart' po4va nova igra, 'exit' izliza i hajde 4ao!");
                    FillGameField(gameField);
                    flagNewGame = false;
                }

                Console.Write("Daj red i kolona : ");
                menuNavigation = Console.ReadLine().Trim();
                if (menuNavigation.Length >= 3)
                {
                    if (int.TryParse(menuNavigation[0].ToString(), out fieldRow) && int.TryParse(menuNavigation[2].ToString(), out fieldColumn)
                        && fieldRow <= gameField.GetLength(0) && fieldColumn <= gameField.GetLength(1))
                    {
                        menuNavigation = "turn";
                    }
                }

                switch (menuNavigation)
                {
                    case "top":
                        Rating(championsList);
                        break;
                    case "restart":
                        gameField = CreateGameField();
                        bombs = SetBombs();
                        FillGameField(gameField);
                        mineField = false;
                        flagNewGame = false;
                        break;
                    case "exit":
                        Console.WriteLine("4a0, 4a0, 4a0!");
                        break;
                    case "turn":
                        if (bombs[fieldRow, fieldColumn] != '*')
                        {
                            if (bombs[fieldRow, fieldColumn] == '-')
                            {
                                PlayerMoveChanging(gameField, bombs, fieldRow, fieldColumn);
                                movesCounter++;
                            }

                            if (MaksimumGameMoves == movesCounter)
                            {
                                flagEndGame = true;
                            }
                            else
                            {
                                FillGameField(gameField);
                            }
                        }
                        else
                        {
                            mineField = true;
                        }

                        break;
                    default:
                        Console.WriteLine("\nGreshka! nevalidna Komanda\n");
                        break;
                }

                if (mineField)
                {
                    FillGameField(bombs);
                    Console.Write("\nHrrrrrr! Umria gerojski s {0} to4ki. " + "Daj si niknejm: ", movesCounter);
                    string nickName = Console.ReadLine();
                    Ranking playerRanking = new Ranking(nickName, movesCounter);
                    if (championsList.Count < 5)
                    {
                        championsList.Add(playerRanking);
                    }
                    else
                    {
                        for (int i = 0; i < championsList.Count; i++)
                        {
                            if (championsList[i].Points < playerRanking.Points)
                            {
                                championsList.Insert(i, playerRanking);
                                championsList.RemoveAt(championsList.Count - 1);
                                break;
                            }
                        }
                    }

                    championsList.Sort((Ranking r1, Ranking r2) => r2.Player.CompareTo(r1.Player));
                    championsList.Sort((Ranking r1, Ranking r2) => r2.Points.CompareTo(r1.Points));
                    Rating(championsList);

                    gameField = CreateGameField();
                    bombs = SetBombs();
                    movesCounter = 0;
                    mineField = false;
                    flagNewGame = true;
                }

                if (flagEndGame)
                {
                    Console.WriteLine("\nBRAVOOOS! Otvri 35 kletki bez kapka kryv.");
                    FillGameField(bombs);
                    Console.WriteLine("Daj si imeto, batka: ");
                    string playerName = Console.ReadLine();
                    Ranking to4kii = new Ranking(playerName, movesCounter);
                    championsList.Add(to4kii);
                    Rating(championsList);
                    gameField = CreateGameField();
                    bombs = SetBombs();
                    movesCounter = 0;
                    flagEndGame = false;
                    flagNewGame = true;
                }
            }
            while (menuNavigation != "exit");
            Console.WriteLine("Made in Bulgaria - Uauahahahahaha!");
            Console.WriteLine("AREEEEEEeeeeeee.");
            Console.Read();
        }

        private static void Rating(List<Ranking> points)
        {
            Console.WriteLine("\nTo4KI:");
            if (points.Count > 0)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} kutii", i + 1, points[i].Player, points[i].Points);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("prazna klasaciq!\n");
            }
        }

        private static void PlayerMoveChanging(char[,] filed, char[,] bombs, int fieldRow, int fieldColumn)
        {
            char bombsCount = BombsCount(bombs, fieldRow, fieldColumn);
            bombs[fieldRow, fieldColumn] = bombsCount;
            filed[fieldRow, fieldColumn] = bombsCount;
        }

        private static void FillGameField(char[,] board)
        {
            int fieldRows = board.GetLength(0);
            int filedColumns = board.GetLength(1);
            Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < fieldRows; i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < filedColumns; j++)
                {
                    Console.Write(string.Format("{0} ", board[i, j]));
                }

                Console.Write("|");
                Console.WriteLine();
            }

            Console.WriteLine("   ---------------------\n");
        }

        private static char[,] CreateGameField()
        {
            int boardRows = 5;
            int boardColumns = 10;
            char[,] board = new char[boardRows, boardColumns];
            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    board[i, j] = '?';
                }
            }

            return board;
        }

        private static char[,] SetBombs()
        {
            int rows = 5;
            int columns = 10;
            char[,] gameBoard = new char[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    gameBoard[i, j] = '-';
                }
            }

            List<int> bombsPositionList = new List<int>();
            while (bombsPositionList.Count < 15)
            {
                Random generatedRandomBombPosition = new Random();
                int asfd = generatedRandomBombPosition.Next(50);
                if (!bombsPositionList.Contains(asfd))
                {
                    bombsPositionList.Add(asfd);
                }
            }

            foreach (int bombPosition in bombsPositionList)
            {
                int column = bombPosition / columns;
                int row = bombPosition % columns;
                if (row == 0 && bombPosition != 0)
                {
                    column--;
                    row = columns;
                }
                else
                {
                    row++;
                }

                gameBoard[column, row - 1] = '*';
            }

            return gameBoard;
        }

        private static void Results(char[,] board)
        {
            int column = board.GetLength(0);
            int row = board.GetLength(1);

            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (board[i, j] != '*')
                    {
                        char bombsCount = BombsCount(board, i, j);
                        board[i, j] = bombsCount;
                    }
                }
            }
        }

        private static char BombsCount(char[,] board, int column, int row)
        {
            int count = 0;
            int rows = board.GetLength(0);
            int columns = board.GetLength(1);

            if (column - 1 >= 0)
            {
                if (board[column - 1, row] == '*')
                {
                    count++;
                }
            }

            if (column + 1 < rows)
            {
                if (board[column + 1, row] == '*')
                {
                    count++;
                }
            }

            if (row - 1 >= 0)
            {
                if (board[column, row - 1] == '*')
                {
                    count++;
                }
            }

            if (row + 1 < columns)
            {
                if (board[column, row + 1] == '*')
                {
                    count++;
                }
            }

            if ((column - 1 >= 0) && (row - 1 >= 0))
            {
                if (board[column - 1, row - 1] == '*')
                {
                    count++;
                }
            }

            if ((column - 1 >= 0) && (row + 1 < columns))
            {
                if (board[column - 1, row + 1] == '*')
                {
                    count++;
                }
            }

            if ((column + 1 < rows) && (row - 1 >= 0))
            {
                if (board[column + 1, row - 1] == '*')
                {
                    count++;
                }
            }

            if ((column + 1 < rows) && (row + 1 < columns))
            {
                if (board[column + 1, row + 1] == '*')
                {
                    count++;
                }
            }

            return char.Parse(count.ToString());
        }
    }
}