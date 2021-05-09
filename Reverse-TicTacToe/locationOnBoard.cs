namespace Reverse_TicTacToe
{
    class LocationOnBoard
    {
        /////Data Members/////
        private int m_X;
        private int m_Y;
        private char m_Symbol;
        
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
