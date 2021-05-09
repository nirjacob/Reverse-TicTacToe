using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_TicTacToe
{
    class Program
    {
        public static void Main()
        {
            inputHandler userInput = new inputHandler();
            userInput.readInitialInput();
            gameLogic ticTacToeGame = new gameLogic(userInput.BoardSize, userInput.NumberOfHumanPlayers);
            ticTacToeGame.run();
        }
    }
}
