using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Game
    {
        private Board _board;
        private AI _ai;
        private Player _player;

        public Game()
        {
            _board = new Board();
            GetPlayerSide();


        }
        private void GetPlayerSide()
        {
            Console.WriteLine("Выберите сторону (крестики (X) или нолики (O)):");

            do
            {
                string side = Console.ReadLine().ToUpper();
                // Проверяем, является ли введенный символ X или O, используя оператор switch
                switch (side)
                {
                    case "X":
                        _player = new Player(CellState.Cross);
                        _ai = new AI(_player);
                        break;
                    case "O":
                        _player = new Player(CellState.Zero);
                        _ai = new AI(_player);
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Пожалуйста, введите X или O.");
                        continue; // Переходим к следующей итерации цикла
                }
            } while (_player == null); // Продолжаем цикл, пока игрок не будет создан
        }

        public void Start()
        {

            while (true)
            {
                // Вывести поле игры
                _board.PrintBoard();

                // Получить ход игрока
                if (_player.IsPlayerTurn)
                {
                    CellState playerSide = _player.PlayerSymbol;
                    int move = GetPlayerMove();
                    _board.SetCell(move, playerSide);
                    _player.IsPlayerTurn = false;
                }
                // Получить ход ИИ
                else
                {
                    Console.WriteLine("Ход ИИ:");
                    CellState aiSide = _ai.AISymbol;
                    int move = _ai.GetBestMove(_board);
                    _board.SetCell(move, aiSide);
                    _player.IsPlayerTurn = true;
                }

                // Проверить выигрыш
                CellState winner = _board.GetWinner();
                if (winner != CellState.Empty)
                {
                    _board.PrintBoard();
                    // Вывести победителя
                    _board.PrintWinner(winner);

                    // Спросить игрока, хочет ли он сыграть еще одну партию
                    Console.WriteLine("Хотите сыграть еще одну партию? (y/n)");
                    string answer = Console.ReadLine();
                    if (answer.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        // Сбросить игру
                        _board = new Board();
                        _player.IsPlayerTurn = _player.PlayerSymbol == CellState.Cross;
                    }
                    else if (answer.Equals("n", StringComparison.OrdinalIgnoreCase))
                    {
                        // Выйти из игры
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод. Пожалуйста, введите y или n.");
                    }
                }
            }
        }

        private int GetPlayerMove()
        {
            Console.WriteLine("Введите число (1-9) чтобы сделать ход:");
            int move = int.Parse(Console.ReadLine());
            while (move < 1 || move > 9 || _board.GetCell(move) != CellState.Empty)
            {
                Console.WriteLine("Неверный ход. Пожалуйста, введите число от 1 до 9 для пустой клетки:");
                move = int.Parse(Console.ReadLine());
            }
            return move;
        }
    }
}
