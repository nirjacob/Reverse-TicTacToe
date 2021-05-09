using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_TicTacToe
{
    class player
    {
        int m_Winning;
        char m_PlayerSymbol;
        bool v_IsHuman = true;
        public int Winning
        {
            get
            {
                return m_Winning;
            }
            set
            {
                m_Winning = value;
            }
        }

        public char PlayerSymbol
        {
            get
            {
                return m_PlayerSymbol;
            }
            set
            {
                m_PlayerSymbol = value;
            }
        }

        public bool IsHuman
        {
            get
            {
                return v_IsHuman;
            }
            set
            {
                v_IsHuman = value;
            }
        }
    }

}
