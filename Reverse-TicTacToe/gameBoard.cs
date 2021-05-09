using System;
using System.Linq;
using System.Threading;
using Ex02.ConsoleUtils;

namespace Reverse_TicTacToe
{
    public enum ePlayerIdentifier
    {
        Player1Turn = 1,
        Player2Turn = 2
    }
    class GameBoard
    {
        /////Data Members/////
        private GameLogic ticTacToeLogic;
        private ePlayerIdentifier m_PlayerTurn;
        /////Constructor/////
        public GameBoard()
        {
            PlayerTurn = ePlayerIdentifier.Player1Turn;
        }
        /////Getter & Setters/////
        public ePlayerIdentifier PlayerTurn
        {
            get
            {
                return m_PlayerTurn;
            }
            set
            {
                m_PlayerTurn = value;
            }
        }
        public int GetCurrentPlayer()
        {
            if (PlayerTurn == ePlayerIdentifier.Player1Turn)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        public char GetSymbol()
        {
            if (PlayerTurn == ePlayerIdentifier.Player1Turn)
            {
                return 'O';
            }
            else
            {
                return 'X';
            }
        }
        /////Class Methods/////
        public void PrintBoard()
        {
            Screen.Clear();
            //Print first row of number.
            Console.Write("  ");
            for (int colNum = 0; colNum < ticTacToeLogic.BoardSize; colNum++)
            {
                Console.Write("{0}   ", colNum + 1);
            }
            Console.WriteLine();
            //Print each row.
            for (int rowNum = 0; rowNum < ticTacToeLogic.BoardSize; rowNum++)
            {
                Console.Write("{0}|", rowNum + 1);
                for (int colNum = 0; colNum < ticTacToeLogic.BoardSize; colNum++)
                { 
                    Console.Write(" {0} |", ticTacToeLogic.GetCell(rowNum, colNum));
                }
                Console.WriteLine();
                Console.Write(" ");
                for (int colNum = 0; colNum < ((ticTacToeLogic.BoardSize) * 4) + 1; colNum++)
                {
                    Console.Write("=");
                }
                Console.WriteLine();
            }
        }
        public void SwitchPlayers()
        {
            if(PlayerTurn == ePlayerIdentifier.Player1Turn)
            {
                PlayerTurn = ePlayerIdentifier.Player2Turn;
            }
            else
            {
                PlayerTurn = ePlayerIdentifier.Player1Turn;
            }
        }
        public void TakeTurn()
        {
            LocationOnBoard move;
            if (PlayerTurn == ePlayerIdentifier.Player2Turn && ticTacToeLogic.VsAI())
            {
                move = AiTurn();
            }
            else
            {
                move = HumanTurn();
            }
            ticTacToeLogic.UpdateLogicAfterTurn(move);
            SwitchPlayers();
        }
        public LocationOnBoard HumanTurn()
        {
            string input;
            int rowInput = 0, colInput = 0;
            Console.Write("Player {0} Turn. {2}Please choose row,col to place the symbol '{1}'{2}", GetCurrentPlayer(), GetSymbol(),"\n");
            input = Console.ReadLine();
            while (InputValidity(input) == false)
            {
                input = Console.ReadLine();
            }
            ObtainMoveValues(input, ref rowInput, ref colInput);
            LocationOnBoard move = new LocationOnBoard();
            move.row = rowInput - 1;//The matrix is zero based.
            move.col = colInput - 1;//The matrix is zero based.
            move.symbol = GetSymbol();
            return move;
        }

        public LocationOnBoard AiTurn()
        {
            int rowIndex = -1, colIndex = -1;
            LocationOnBoard move = new LocationOnBoard();
            ticTacToeLogic.CalculateAiTurn(ref rowIndex, ref colIndex);
            if(rowIndex != -1 && colIndex != -1)
            {
                move.row = rowIndex;
                move.col = colIndex;
                move.symbol = 'X';
            }
            return move;
        }
        public bool InputValidity(string i_Input)
        {
            if ((i_Input.Contains('q') || i_Input.Contains('Q')) && i_Input.Length==1)
            {
                Console.WriteLine("Player {0} has quited the game.", (int)PlayerTurn);
                Replay();
                return false;
            }
            if (i_Input.Contains(',') == false)
            {
                Console.WriteLine($"Error - invalid input, please enter a move from the format row,col {Environment.NewLine}");
                return false;
            }
            int i_CommaIndex = i_Input.IndexOf(',');
            string i_FirstNumber = i_Input.Substring(0, i_CommaIndex);
            string i_SecondNumber = i_Input.Substring(i_CommaIndex + 1);
            int i_CheckRow,i_CheckCol;
            if (int.TryParse(i_FirstNumber, out i_CheckRow) == true)
            {
                if (int.TryParse(i_SecondNumber, out i_CheckCol) == true)
                {
                    return CheckBoardValidity(i_CheckRow - 1, i_CheckCol - 1);
                }
            }
            return false;
        }
        public bool CheckBoardValidity(int i_CheckRow,int i_CheckCol)
        {
            if (i_CheckRow < ticTacToeLogic.BoardSize && i_CheckCol < ticTacToeLogic.BoardSize)
            {
                if(ticTacToeLogic.GetCell(i_CheckRow, i_CheckCol) == ' ')
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine($"Error - invalid input, please enter a number within board limits {Environment.NewLine}");
                return false;
            }
            Console.WriteLine($"Error - invalid input, cell is already taken {Environment.NewLine}");
            return false;
        }
        public void ObtainMoveValues(string i_Input, ref int io_row, ref int io_col)
        {
            int commaIndex = i_Input.IndexOf(',');
            string rowStr = i_Input.Substring(0, commaIndex);
            string colStr = i_Input.Substring(commaIndex + 1);
            io_row = Int32.Parse(rowStr);
            io_col = Int32.Parse(colStr);
         }
        public void SetUpGameLogic()
        {
            int boardSizeUserInput = 0, numOfHumanPlayersUserInput = 0;
            Console.WriteLine("Please enter the board size: ");
            string input = Console.ReadLine();
            while (ValidMatrixSizeInput(input) != true)
            {
                input = Console.ReadLine();
            }
            boardSizeUserInput = int.Parse(input);
            Console.WriteLine("Please enter the number of human players: ");
            input = Console.ReadLine();
            while (ValidNumOfPlayersInput(input) != true)
            {
                input = Console.ReadLine();
            }
            numOfHumanPlayersUserInput = int.Parse(input);
            ticTacToeLogic = new GameLogic(boardSizeUserInput, numOfHumanPlayersUserInput);
        }
        public bool ValidMatrixSizeInput(string i_Input)
        {
            if (i_Input.All(char.IsDigit) == true)
            {
                if (int.Parse(i_Input) < 3 || int.Parse(i_Input) > 9)
                {
                    Console.WriteLine("Invalid input, please enter a number between 3-9");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a number");
                return false;
            }
        }
        public bool ValidNumOfPlayersInput(string i_Input)
        {
            if (i_Input.All(char.IsDigit) == true)
            {
                if (int.Parse(i_Input) > 2 || int.Parse(i_Input) < 1)
                {
                    Console.WriteLine("Invalid input, please enter a number");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a number");
                return false;
            }
        }
        public void Replay()
        {
            Console.WriteLine("Score: [Player1] {0}:{1} [Player2]", ticTacToeLogic.GetPlayerScore(1), ticTacToeLogic.GetPlayerScore(2));
            Console.WriteLine("Would you like to play another round? yes/no");
            string input = Console.ReadLine();
            input = input.Trim();
            if (String.Compare(input, "yes") == 0)
            {
                ticTacToeLogic.MakeEmptyMatrix();
                PrintBoard();
                PlayerTurn = ePlayerIdentifier.Player1Turn;
            }
            else if (String.Compare(input, "no") == 0)
            {
                Console.WriteLine("Goodbye");
                Thread.Sleep(1500);
                Environment.Exit(1);
            }
            else
            {
                Screen.Clear();
                Console.WriteLine("Please enter a valid input. (yes/no)");
                Replay();
            }
        }
        public void Run()
        {
            SetUpGameLogic();
            PrintBoard();
            TakeTurn();//ask for first move
            bool gameRunning = true;
            int winningPlayerNum = 0;
            while (gameRunning)
            {
                if (ticTacToeLogic.CheckWinner(ref winningPlayerNum))
                {
                    PrintBoard();
                    Console.WriteLine("Player {0} Won!", winningPlayerNum);
                    Replay(); //offer to play again
                }
                else if (ticTacToeLogic.CheckDraw())
                {
                    PrintBoard();
                    Console.WriteLine("Draw!");
                    Replay();
                }
                PrintBoard();
                TakeTurn();
            }
        }
    }
}
