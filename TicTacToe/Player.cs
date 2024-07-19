using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Player
    {
        public CellState PlayerSymbol { get; set; }
        public bool IsPlayerTurn { get; set; }

        public Player(CellState side) {
            PlayerSymbol = side;
            IsPlayerTurn = side == CellState.Cross;
        }
    }
}
