using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_TicTacToe
{
    class gameLogic
    {//will contain the main loop
        player m_Player1;
        player m_Player2;
        board m_GameBoardView;
        int[,] gameMatrix;
        public gameLogic(int i_BoardSize, int i_NumOfHumanPlayers)
        {
            m_GameBoardView = new board(i_BoardSize);
            m_Player1.PlayerSymbol = 'O';
            m_Player2.PlayerSymbol = 'X';
            if(i_NumOfHumanPlayers == 1)
            {
                m_Player2.IsHuman = false;
            }
            m_GameBoardView.printBoard(gameMatrix);
            //Choose move
        }
        public run()
        {
               
        }
    }


}
