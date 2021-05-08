using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_TicTacToe
{

    class gameLogic
    {
        /////Data Members/////
        player m_Player1;
        player m_Player2;
        char[,] m_GameMatrix;
        int m_BoardSize;
        int m_NumOfHumanPlayers;

        /////Constructor/////
        public gameLogic(int i_BoardSize, int i_NumOfHumanPlayers)
        {
            m_Player1 = new player(1);
            m_Player2 = new player(2);
            m_BoardSize = i_BoardSize;
            m_NumOfHumanPlayers = i_NumOfHumanPlayers;
            m_GameMatrix = new char[m_BoardSize, m_BoardSize];
            makeEmptyMatrix();
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

        public char[,] GameMatrix
        {
            get
            {
                return m_GameMatrix;
            }
        }
        
        /////Class Methods/////
        public bool vsAI() 
        {
            if(m_Player2.IsHuman == false)
            {
                return true;
            }
            return false;
        }
        public void makeEmptyMatrix()
        {
            for(int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_GameMatrix[i,j]  = ' ';
                }
            }
        }
        public char getCell(int i_Row,int i_Col)
        {
            return m_GameMatrix[i_Row,i_Col];
        }
        public void updateLogicAfterTurn(locationOnBoard i_move)
        {
            m_GameMatrix[i_move.row, i_move.col] = i_move.symbol;
        }
        public void addScoreToPlayer(int i_playerNum)
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
    }


}
