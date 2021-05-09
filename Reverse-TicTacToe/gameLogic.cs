using System;
using System.Collections.Generic;
namespace Reverse_TicTacToe
{
    class GameLogic
    {
        /////Data Members/////
        private Player m_Player1;
        private Player m_Player2;
        private char[,] m_GameMatrix;
        private int m_BoardSize;
        private int m_NumOfHumanPlayers;
        /////Constructor/////
        public GameLogic(int i_BoardSize, int i_NumOfHumanPlayers)
        {
            m_Player1 = new Player(1);
            m_Player2 = new Player(2);
            m_BoardSize = i_BoardSize;
            m_NumOfHumanPlayers = i_NumOfHumanPlayers;
            m_GameMatrix = new char[m_BoardSize, m_BoardSize];
            MakeEmptyMatrix();
            if (i_NumOfHumanPlayers == 1)
            {
                m_Player2.IsHuman = false;
            }
        }
        /////Getter & Setters/////
        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
            set
            {
                m_BoardSize = value;
            }
        }
        public int NumOfHumanPlayers
        {
            get
            {
                return m_NumOfHumanPlayers;
            }
            set
            {
                m_NumOfHumanPlayers = value;

            }
        }
        public int GetPlayerScore(int i_NumOfPlayer)
        {
            if(i_NumOfPlayer == 1)
            {
                return m_Player1.Winning;
            }
            else if(i_NumOfPlayer == 2)
            {
                return m_Player2.Winning;
            }
            else
            {
                Console.WriteLine("Please enter valid player number. (1 or 2)");
                return -1;
            }
        }
        public char[,] GameMatrix
        {
            get
            {
                return m_GameMatrix;
            }
        }
        
        /////Class Methods/////
        public bool VsAI() 
        {
            if(m_Player2.IsHuman == false)
            {
                return true;
            }
            return false;
        }
        public void MakeEmptyMatrix()
        {
            for(int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_GameMatrix[i,j]  = ' ';
                }
            }
        }
        public char GetCell(int i_Row,int i_Col)
        {
            return m_GameMatrix[i_Row,i_Col];
        }
        public void UpdateLogicAfterTurn(LocationOnBoard i_move)
        {
            m_GameMatrix[i_move.row, i_move.col] = i_move.symbol;
        }
        public void AddScoreToPlayer(int i_playerNum)
        {
            if(i_playerNum == 1)
            {
                m_Player1.Winning++;
            }
            if(i_playerNum == 2)
            {
                m_Player2.Winning++;
            }
        }
        public bool CheckWinner(ref int io_numOfPlayerWon)
        {
            int numOfPlayerWon = 0;
            bool verticalWin = CheckVertical(ref numOfPlayerWon);
            bool HorizontalWin = CheckHorizontal(ref numOfPlayerWon);
            bool DiagonalWin = CheckDiagonal(ref numOfPlayerWon);
            if (DiagonalWin == true || HorizontalWin == true || verticalWin == true)
            {
                AddScoreToPlayer(numOfPlayerWon);
                io_numOfPlayerWon = numOfPlayerWon;
                return true;
            }
            return false;
        }
        public bool CheckVertical(ref int io_numOfPlayerWon)
        {
            int playerOneCounter = 0, playerTwoCounter = 0;
            for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < BoardSize; colIndex++)
                {
                    if (GameMatrix[rowIndex, colIndex] == 'O')
                    {
                        playerOneCounter++;
                    }
                    if (GameMatrix[rowIndex, colIndex] == 'X')
                    {
                        playerTwoCounter++;
                    }
                }
                if (playerOneCounter == BoardSize || playerTwoCounter == BoardSize)
                {
                    io_numOfPlayerWon = WinnerNumber(playerOneCounter);
                    return true;
                }
                playerOneCounter = 0;
                playerTwoCounter = 0;
            }
            return false;
        }
        public bool CheckHorizontal(ref int io_numOfPlayerWon)
        {
            int playerOneCounter = 0, playerTwoCounter = 0;
            for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < BoardSize; colIndex++)
                {
                    if (GameMatrix[colIndex, rowIndex] == 'O')
                    {
                        playerOneCounter++;
                    }
                    if (GameMatrix[colIndex, rowIndex] == 'X')
                    {
                        playerTwoCounter++;
                    }
                }
                if (playerOneCounter == BoardSize || playerTwoCounter == BoardSize)
                {
                    io_numOfPlayerWon = WinnerNumber(playerOneCounter);
                    return true;
                }
                playerOneCounter = 0;
                playerTwoCounter = 0;
            }
            return false;
        }
        public bool CheckDiagonal(ref int io_numOfPlayerWon)
        {
            int playerOneCounter = 0, playerTwoCounter = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                if (GameMatrix[i, i] == 'O')
                {
                    playerOneCounter++;
                }
                if (GameMatrix[i, i] == 'X')
                {
                    playerTwoCounter++;
                }
                if (playerOneCounter == BoardSize || playerTwoCounter == BoardSize)
                {
                    io_numOfPlayerWon = WinnerNumber(playerOneCounter);
                    return true;
                }
            }
            playerOneCounter = 0;
            playerTwoCounter = 0;
            int col = BoardSize - 1;
            for (int i = 0; i < BoardSize; i++)
            {
                if (GameMatrix[i, col] == 'O')
                {
                    playerOneCounter++;
                }
                if (GameMatrix[i, col] == 'Y')
                {
                    playerTwoCounter++;
                }

                col--;
                if (playerOneCounter == BoardSize || playerTwoCounter == BoardSize)
                {
                    io_numOfPlayerWon = WinnerNumber(playerOneCounter);
                    return true;
                }
            }
            return false;
        }
        public int WinnerNumber(int i_playerOneCounter)
        {
            if (i_playerOneCounter == BoardSize)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
        public bool CheckDraw()
        {
            for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < BoardSize; colIndex++)
                {
                    if (GameMatrix[colIndex, rowIndex] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void CalculateAiTurn(ref int io_RowIndex, ref int io_ColIndex)
        {
            int bestTurnValue = Int32.MinValue;
            bool isAiTurn = true;
            for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
            {
                for (int colIndex = 0; colIndex < BoardSize; colIndex++)
                {
                    if(GameMatrix[rowIndex, colIndex] == ' ')
                    {
                        GameMatrix[rowIndex, colIndex] = 'X';
                        int currMoveValue = EvaluateAiMove(0, !isAiTurn);
                        GameMatrix[rowIndex, colIndex] = ' ';
                        if (currMoveValue > bestTurnValue)
                        {
                            bestTurnValue = currMoveValue;
                            io_RowIndex = rowIndex;
                            io_ColIndex = colIndex;
                        }
                    }
                }
            }
        }
        public int EvaluateAiMove(int i_Depth, bool i_IsAiTurn)
        {
            int moveScore = EvaluateBoard();
            if(moveScore == 10)
            {
                return moveScore;
            }
            if(moveScore == -10)
            {
                return moveScore;
            }
            if(CheckDraw() == true)
            {
                return 0;
            }
            if (i_IsAiTurn == true)
            {
                int AIbestMoveValue = Int32.MinValue;

                for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < BoardSize; colIndex++)
                    {
                        if(GameMatrix[rowIndex, colIndex] == ' ' && i_Depth < setMaxDepth())
                        {
                            GameMatrix[rowIndex, colIndex] = 'X';
                            AIbestMoveValue = Math.Max(AIbestMoveValue, EvaluateAiMove(i_Depth + 1, !i_IsAiTurn));
                            GameMatrix[rowIndex, colIndex] = ' ';
                        }
                    }
                }
                return AIbestMoveValue;
            }
            else
            {
                int humanBestMoveValue = Int32.MaxValue;
                for (int rowIndex = 0; rowIndex < BoardSize; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < BoardSize; colIndex++)
                    {
                        if (GameMatrix[rowIndex, colIndex] == ' ' && i_Depth < setMaxDepth())
                        {
                            GameMatrix[rowIndex, colIndex] = 'O';
                            humanBestMoveValue = Math.Min(humanBestMoveValue, EvaluateAiMove(i_Depth + 1, !i_IsAiTurn));
                            GameMatrix[rowIndex, colIndex] = ' ';
                        }
                    }
                }
                return humanBestMoveValue;
            }
        }
        public int setMaxDepth()
        {
            switch (BoardSize)
            {
                case 3:
                    return Int32.MaxValue;
                case 4:
                    return 4;
                default:
                    return 2;
            }
        }
        public int EvaluateBoard()
        {
            int numOfPlayerWon = 0;

            if(CheckHorizontal(ref numOfPlayerWon) == true)
            {
                if(numOfPlayerWon == 1)
                {
                    return -10;
                }
                else if(numOfPlayerWon == 2)
                {
                    return 10;
                }
            }
            else if (CheckVertical(ref numOfPlayerWon) == true)
            {
                if (numOfPlayerWon == 1)
                {
                    return -10;
                }
                else if (numOfPlayerWon == 2)
                {
                    return 10;
                }
            }
            else if (CheckDiagonal(ref numOfPlayerWon) == true)
            {
                if (numOfPlayerWon == 1)
                {
                    return -10;
                }
                else if (numOfPlayerWon == 2)
                {
                    return 10;
                }
            }
            return 0;
        }

    }
}
