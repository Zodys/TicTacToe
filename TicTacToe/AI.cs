using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class AI
    {
        public CellState AISymbol { get; set; }

        private Player _player;
        public AI(Player player)
        {
            _player = player;
            AISymbol = (_player.PlayerSymbol == CellState.Cross) ? CellState.Zero : CellState.Cross;
        }
        public int GetBestMove(Board board)
        {
            // Использовать алгоритм Мини-Макс с альфа-бета отсечением для определения оптимального хода
            int bestScore = int.MinValue;
            int bestMove = -1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Проверить, пуста ли клетка
                    if (board.GetCell(i * 3 + j + 1) == CellState.Empty)
                    {
                        // Сделать ход в эту клетку
                        board.SetCell(i * 3 + j + 1, AISymbol);

                        // Получить оценку для этого хода
                        int score = MiniMax(board, false);

                        // Отменить ход
                        board.SetCell(i * 3 + j + 1, CellState.Empty);

                        // Обновить лучший ход и оценку
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = i * 3 + j + 1;
                        }
                    }
                }
            }

            return bestMove;
        }

        private int MiniMax(Board board, bool isMaximizing)
        {
            // Проверить выигрыш
            CellState winner = board.GetWinner();
            if (winner != CellState.Empty)
            {
                if (winner == AISymbol)
                {
                    return 1;
                }
                else if (winner == _player.PlayerSymbol)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }

            //найти максимальную оценку
            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Проверить, пуста ли клетка
                        if (board.GetCell(i * 3 + j + 1) == CellState.Empty)
                        {
                            // Сделать ход в эту клетку
                            board.SetCell(i * 3 + j + 1, AISymbol);

                            // Получить оценку для этого хода
                            int score = MiniMax(board, false);

                            // Отменить ход
                            board.SetCell(i * 3 + j + 1, CellState.Empty);

                            // Обновить лучшую оценку
                            bestScore = Math.Max(bestScore, score);
                        }
                    }
                }
                return bestScore;
            }
            //найти минимальную оценку
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Проверить, пуста ли клетка
                        if (board.GetCell(i * 3 + j + 1) == CellState.Empty)
                        {
                            // Сделать ход в эту клетку
                            board.SetCell(i * 3 + j + 1, _player.PlayerSymbol);

                            // Получить оценку для этого хода
                            int score = MiniMax(board, true);

                            // Отменить ход
                            board.SetCell(i * 3 + j + 1, CellState.Empty);

                            // Обновить лучшую оценку
                            bestScore = Math.Min(bestScore, score);
                        }
                    }
                }
                return bestScore;
            }
        }

    }
}
