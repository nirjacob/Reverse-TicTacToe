namespace Reverse_TicTacToe
{
    class Player
    {
        /////Data Members/////
        private int m_Winning;
        private char m_PlayerSymbol;
        private bool v_IsHuman;
        /////Constructor/////
        public Player(int i_playerNumber)
        {
            m_Winning = 0;
            if (i_playerNumber == 1)
            {
                m_PlayerSymbol = 'O';
            }
            if (i_playerNumber == 2)
            {
                m_PlayerSymbol = 'X';
            }
            v_IsHuman = true;
        }
        /////Getter & Setters/////
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
