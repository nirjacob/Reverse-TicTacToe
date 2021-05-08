using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;

namespace Reverse_TicTacToe
{
    public enum ePlayerIdentifier
    {
        Player1Turn = 1,
        Player2Turn = 2
    }
    class gameBoard
    {
        /////Data Members/////
        gameLogic ticTacToeLogic;
        ePlayerIdentifier m_PlayerTurn;
        
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
        public char getSymbol()
        {
            if (PlayerTurn == ePlayerIdentifier.Player1Turn)
            {
                return 'X';
            }
            else
            {
                return 'O';
            }
        }

        /////Class Methods/////
        public void printBoard()
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
                    Console.Write(" {0} |", ticTacToeLogic.getCell(rowNum, colNum));
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
        public void switchPlayers()
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
        public void takeTurn()
        {
            locationOnBoard move;
            if (PlayerTurn == ePlayerIdentifier.Player2Turn && ticTacToeLogic.vsAI())
            {
                move = pcTurn();
            }
            else
            {
                move = humanTurn();
            }
            ticTacToeLogic.updateLogicAfterTurn(move);
            switchPlayers();
        }
        public locationOnBoard humanTurn()
        {
            string input;
            int rowInput = 0, colInput = 0;
            Console.WriteLine("Please enter a move: \n");
            input = Console.ReadLine();
            while (inputValidity(input) == false)
            {
                input = Console.ReadLine();
            }
            obtainMoveValues(input, ref rowInput, ref colInput);
            locationOnBoard move = new locationOnBoard();
            move.row = rowInput - 1;//The matrix is zero based.
            move.col = colInput - 1;//The matrix is zero based.
            move.symbol = getSymbol();
            return move;
        }

        public locationOnBoard pcTurn()
        {
            //Generate random.
            return new locationOnBoard();
        }
        public bool inputValidity(string i_Input)
        {
            if ((i_Input.Contains('q') || i_Input.Contains('Q')) && i_Input.Length==1)
            {
                Console.WriteLine("Player {0} has quited the game.", PlayerTurn);
                replay();
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
                    return checkBoardValidity(i_CheckRow - 1, i_CheckCol - 1);
                }
            }
            return false;
        }
        public bool checkBoardValidity(int i_CheckRow,int i_CheckCol)
        {
            if (i_CheckRow < ticTacToeLogic.BoardSize && i_CheckCol < ticTacToeLogic.BoardSize && i_CheckCol > 0 && i_CheckRow > 0)
            {
                if(ticTacToeLogic.getCell(i_CheckRow, i_CheckCol) == ' ')
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
        public void obtainMoveValues(string i_Input, ref int io_row, ref int io_col)
        {
            int commaIndex = i_Input.IndexOf(',');
            string rowStr = i_Input.Substring(0, commaIndex);
            string colStr = i_Input.Substring(commaIndex + 1);
            io_row = Int32.Parse(rowStr);
            io_col = Int32.Parse(colStr);
         }
        public void setUpGameLogic()
        {
            int boardSizeUserInput = 0, numOfHumanPlayersUserInput = 0;
            Console.WriteLine("Please enter the board size: ");
            string input = Console.ReadLine();
            while (validMatrixSizeInput(input) != true)
            {
                input = Console.ReadLine();
            }
            boardSizeUserInput = int.Parse(input);
            Console.WriteLine("Please enter the number of human players: ");
            input = Console.ReadLine();
            while (validNumOfPlayersInput(input) != true)
            {
                input = Console.ReadLine();
            }
            numOfHumanPlayersUserInput = int.Parse(input);
            ticTacToeLogic = new gameLogic(boardSizeUserInput, numOfHumanPlayersUserInput);
        }
        public bool validMatrixSizeInput(string i_Input)
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
        public bool validNumOfPlayersInput(string i_Input)
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
        public void run()
        {
            setUpGameLogic();
            printBoard();
            takeTurn();//ask for first move
            bool endGame = false;
            while (endGame == false)
            {
                if (checkWinner())
                {
                    //update score
                    replay(); //offer to play again
                }
                if (checkDraw())
                {
                    Console.WriteLine("Draw!");
                    replay();
                }
                printBoard();
                takeTurn();
            }
        }
        public bool checkWinner()
        {
            int numOfPlayerWon = 0;
            bool verticalWin = checkVertical(ref numOfPlayerWon);
            bool HorizontalWin = checkHorizontal(ref numOfPlayerWon);
            bool DiagonalWin = checkDiagonal(ref numOfPlayerWon);
            if(DiagonalWin == true || HorizontalWin == true|| verticalWin == true)
            {
                ticTacToeLogic.addScoreToPlayer(numOfPlayerWon);
                return true;
            }
            return false;
        }
        public bool checkVertical(ref int io_numOfPlayerWon)
        {
            int playerOneCounter = 0, playerTwoCounter = 0;
            for (int rowIndex = 0; rowIndex < ticTacToeLogic.BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < ticTacToeLogic.BoardSize; colIndex++)
                {
                    if(ticTacToeLogic.GameMatrix[rowIndex, colIndex] == 'O')
                    {
                        playerOneCounter++;
                    }
                    if(ticTacToeLogic.GameMatrix[rowIndex, colIndex] == 'X')
                    {
                        playerTwoCounter++;
                    }
                }
                if(playerOneCounter == ticTacToeLogic.BoardSize || playerTwoCounter == ticTacToeLogic.BoardSize)
                {
                    io_numOfPlayerWon = winnerNumber(playerOneCounter, playerTwoCounter);
                    return true;
                }
            }
            return false;
        }
        public bool checkHorizontal(ref int io_numOfPlayerWon)
        {
            int playerOneCounter = 0, playerTwoCounter = 0;
            for (int rowIndex = 0; rowIndex < ticTacToeLogic.BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < ticTacToeLogic.BoardSize; colIndex++)
                {
                    if (ticTacToeLogic.GameMatrix[colIndex, rowIndex] == 'O')
                    {
                        playerOneCounter++;
                    }
                    if (ticTacToeLogic.GameMatrix[colIndex, rowIndex] == 'X')
                    {
                        playerTwoCounter++;
                    }
                }
                if (playerOneCounter == ticTacToeLogic.BoardSize || playerTwoCounter == ticTacToeLogic.BoardSize)
                {
                    io_numOfPlayerWon = winnerNumber(playerOneCounter, playerTwoCounter);
                    return true;
                }
            }
            return false;
        }
        public bool checkDiagonal(ref int io_numOfPlayerWon)
        {
            int playerOneCounter = 0, playerTwoCounter = 0;
            for (int i = 0; i < ticTacToeLogic.BoardSize; i++)
            {
                if (ticTacToeLogic.GameMatrix[i, i] == 'O')
                {
                    playerOneCounter++;
                }
                if (ticTacToeLogic.GameMatrix[i, i] == 'X')
                {
                    playerTwoCounter++;
                }
                if (playerOneCounter == ticTacToeLogic.BoardSize || playerTwoCounter == ticTacToeLogic.BoardSize)
                {
                    io_numOfPlayerWon = winnerNumber(playerOneCounter, playerTwoCounter);
                    return true;
                }
            }
            playerOneCounter = 0;
            playerTwoCounter = 0;
            int col  = ticTacToeLogic.BoardSize-1;
            for (int i = 0; i < ticTacToeLogic.BoardSize; i++)
            {
                if(ticTacToeLogic.GameMatrix[i, col] == 'O')
                {
                    playerOneCounter++;
                }
                if (ticTacToeLogic.GameMatrix[i, col] == 'Y')
                {
                    playerTwoCounter++;
                }

                col--;
            }
            if (playerOneCounter == ticTacToeLogic.BoardSize || playerTwoCounter == ticTacToeLogic.BoardSize)
            {
                io_numOfPlayerWon = winnerNumber(playerOneCounter, playerTwoCounter);
                return true;
            }
            return false;
        }
        public int winnerNumber(int i_playerOne, int i_playerTwo)
        {
            if(i_playerOne == ticTacToeLogic.BoardSize)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        public bool checkDraw()
        {
            for (int rowIndex = 0; rowIndex < ticTacToeLogic.BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < ticTacToeLogic.BoardSize; colIndex++)
                {
                    if (ticTacToeLogic.GameMatrix[colIndex, rowIndex] != ' ')
                    {
                        return false;
                    }
                   
                }
            }
            return true;
        }
        public void replay()
        {
            Console.WriteLine("Would you like to play another round? yes/no");
            string input = Console.ReadLine();
            input = input.Trim();
            if (String.Compare(input, "yes") == 0)
            {
                ticTacToeLogic.makeEmptyMatrix();
                printBoard();
                PlayerTurn = ePlayerIdentifier.Player1Turn;
            }
            else if(String.Compare(input, "no") == 0)
            {
                Console.WriteLine("Goodbye");
                Environment.Exit(1);
            }
            else
            {
                replay();
            }
        }



    }
}
