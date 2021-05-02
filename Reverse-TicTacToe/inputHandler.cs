using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_TicTacToe
{
    class inputHandler
    {
        int m_BoardSize;
        int m_numOfHumanPlayers;
        public int NumberOfHumanPlayers
        {
            get
            {
                return m_numOfHumanPlayers;
            }
            set
            {
                m_numOfHumanPlayers = value;
            }
        }
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
        public void readInitialInput()
        {
            Console.WriteLine("Please enter the board size: ");
            string input = Console.ReadLine();
            while (validMatrixSizeInput(input) != true)
            {
                input = Console.ReadLine();
            }
            BoardSize = int.Parse(Console.ReadLine());

            Console.WriteLine("Please enter the number of human players: ");
            input = Console.ReadLine();
            while (validNumOfPlayersInput(input) != true)
            {
                input = Console.ReadLine();
            }
            NumberOfHumanPlayers = int.Parse(Console.ReadLine());
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
    }
}
