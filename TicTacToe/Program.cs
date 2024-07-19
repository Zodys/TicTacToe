using System;
using System.Text;

namespace TicTacToe
{
    class Program
    {

        public static void Main(string[] args)
        {
            // Создать новую игру
            Game game = new Game();

            // Запустить игру
            game.Start();

        }
    }
}