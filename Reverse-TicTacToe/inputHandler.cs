//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Reverse_TicTacToe
//{
//    class inputHandler
//    {
//        public void readInitialInput()
//        {
//            Console.WriteLine("Please enter the board size: ");
//            string input = Console.ReadLine();
//            while (validMatrixSizeInput(input) != true)
//            {
//                input = Console.ReadLine();
//            }
//            BoardSize = int.Parse(input);

//            Console.WriteLine("Please enter the number of human players: ");
//            input = Console.ReadLine();
//            while (validNumOfPlayersInput(input) != true)
//            {
//                input = Console.ReadLine();
//            }
//            NumberOfHumanPlayers = int.Parse(input);
//        }
//        public bool validMatrixSizeInput(string i_Input)
//        {
//            if (i_Input.All(char.IsDigit) == true)
//            {
//                if (int.Parse(i_Input) < 3 || int.Parse(i_Input) > 9)
//               {
//                    Console.WriteLine("Invalid input, please enter a number between 3-9");
//                    return false;
//                }
//                else
//                {
//                    return true;
//                }
//            }
//            else
//            {
//                Console.WriteLine("Invalid input, please enter a number");
//                return false;
//            }
//        }
//        public bool validNumOfPlayersInput(string i_Input)
//        {
//            if (i_Input.All(char.IsDigit) == true)
//            {
//                if (int.Parse(i_Input) > 2 || int.Parse(i_Input) < 1)
//                {
//                    Console.WriteLine("Invalid input, please enter a number");
//                    return false;
//                }
//                else
//                {
//                    return true;
//                }
//            }
//            else
//            {
//                Console.WriteLine("Invalid input, please enter a number");
//                return false;
//            }
//        }
//    }
//}
