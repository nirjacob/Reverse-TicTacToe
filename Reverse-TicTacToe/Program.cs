using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//                        board
//ממשק משתמש -> מדבר עם המשתמש ->מדפיס למשתמש
//                      board uses gameLogic
//ממשק משתמש -> קורא ומשתמש בלוגיקה ->הלוגיקה לא מכירה את ממשק המשתמש
// gameLogic has inner data matrix

// gameLogic has isWin etc
//ממשק המשתמש->שואל את הלוגיקה על מצב הלוח וכו.
// Player 1 = O
// Player 2 = X
namespace Reverse_TicTacToe
{
    class Program
    {
        public static void Main()
        {
            gameBoard ticTacToeGame = new gameBoard();
            ticTacToeGame.run();
        }
    }
}
