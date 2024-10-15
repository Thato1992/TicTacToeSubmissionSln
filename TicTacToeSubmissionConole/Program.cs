using System;

namespace TicTacToeSubmissionConole
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleColor oldColor = Console.ForegroundColor;

            Console.SetCursorPosition(10,2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to Tic Tac Toe");

            var ticTacToe = new TicTacToe();

            ticTacToe.Run();


            Console.ForegroundColor = oldColor ;


            Console.SetCursorPosition(20, 25);
            Console.WriteLine("Thank you for playing");
            Console.ReadLine();
        }
    }
}
