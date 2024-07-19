using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Board
    {
        private CellState[,] _cells;

        public Board()
        {
            _cells = new CellState[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _cells[i, j] = CellState.Empty;
                }
            }
        }

        public CellState GetCell(int move)
        {
            int row = (move - 1) / 3;
            int col = (move - 1) % 3;
            return _cells[row, col];
        }

        public void SetCell(int move, CellState value)
        {
            int row = (move - 1) / 3;
            int col = (move - 1) % 3;
            _cells[row, col] = value;
        }

        public CellState GetWinner()
        {
            // Проверить горизонтальные линии
            for (int i = 0; i < 3; i++)
            {
                if (_cells[i, 0] != CellState.Empty && _cells[i, 0] == _cells[i, 1] && _cells[i, 1] == _cells[i, 2])
                {
                    return _cells[i, 0];
                }
            }

            // Проверить вертикальные линии
            for (int i = 0; i < 3; i++)
            {
                if (_cells[0, i] != CellState.Empty && _cells[0, i] == _cells[1, i] && _cells[1, i] == _cells[2, i])
                {
                    return _cells[0, i];
                }
            }

            // Проверить диагонали
            if (_cells[0, 0] != CellState.Empty && _cells[0, 0] == _cells[1, 1] && _cells[1, 1] == _cells[2, 2])
            {
                return _cells[0, 0];
            }
            if (_cells[0, 2] != CellState.Empty && _cells[0, 2] == _cells[1, 1] && _cells[1, 1] == _cells[2, 0])
            {
                return _cells[0, 2];
            }

            // Ничья или игра продолжается
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_cells[i, j] == CellState.Empty)
                    {
                        return CellState.Empty;
                    }
                }
            }

            return CellState.Draw;
        }
        public int EvaluateBoard()
        {
            // Проверить, есть ли у игрока или ИИ выигрышная комбинация
            CellState winner = GetWinner();
            if (winner == CellState.Cross)
            {
                return -1;
            }
            else if (winner == CellState.Zero)
            {
                return 1;
            }
            // В противном случае оценить количество ходов, оставшихся для игрока и ИИ
            else
            {
                int numEmptyCells = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (GetCell(i * 3 + j + 1) == CellState.Empty)
                        {
                            numEmptyCells++;
                        }
                    }
                }
                return numEmptyCells;
            }
        }

        public void PrintWinner(CellState winner)
        {
            if (winner == CellState.Cross)
                Console.WriteLine("Выйграл крестик!");
            else if (winner == CellState.Zero)
                Console.WriteLine("Выиграл нолик!");
            else
                Console.WriteLine("Ничья!");
        }

        public void PrintBoard()
        {
            Console.WriteLine("-------------");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < 3; j++)
                {
                    char symbol = _cells[i, j] switch
                    {
                        CellState.Empty => (char)(i * 3 + j + 1 + '0'), // Преобразовать число в символ
                        CellState.Cross => 'X',
                        CellState.Zero => 'O',
                        _ => throw new InvalidOperationException("Invalid cell state.")
                    };
                    Console.Write($"{symbol} | ");
                }
                Console.WriteLine();
                Console.WriteLine("-------------");
            }
        }


    }
}
