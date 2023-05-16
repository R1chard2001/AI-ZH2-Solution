using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_II
{
    public class Operator
    {
        int row, col;
        public Operator(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
        public bool IsApplicable(State state)
        {
            return state.Board[row, col] == State.BLANK;
        }
        public State Apply(State state)
        {
            State newState = (State)state.Clone();
            newState.Board[row, col] = newState.CurrentPlayer;
            newState.ChangePlayer();
            return newState;
        }
    }
}
