using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Reverse_TicTacToe
{
    class locationOnBoard
    {
        /////Data Members/////
        int m_X;
        int m_Y;
        char m_Symbol;
        
        /////Getter & Setters/////
        public int row
        {
            get
            {
                return m_X;
            }
            set
            {
                m_X = value;
            }
        }
        public int col
        {
            get
            {
                return m_Y;
            }
            set
            {
                m_Y = value;
            }
        }
        public char symbol
        {
            get
            {
                return m_Symbol;
            }
            set
            {
                m_Symbol = value;
            }
        }
    }
}
