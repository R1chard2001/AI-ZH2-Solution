using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_II
{
    public class State : ICloneable
    {
        public static char PLAYER1 = 'O';
        public static char PLAYER2 = 'X';
        public static char BLANK = ' ';
        public static char DRAW = 'D';
        public char CurrentPlayer = PLAYER1;
        public char[,] Board;
        public State()
        {
            Board = new char[6, 6];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Board[i, j] = BLANK;
                }
            }
        }
        public void ChangePlayer()
        {
            if (CurrentPlayer == PLAYER1)
            {
                CurrentPlayer = PLAYER2;
            }
            else
            {
                CurrentPlayer = PLAYER1;
            }
        }

        public object Clone()
        {
            State newState = new State();
            newState.Board = (char[,])Board.Clone();
            newState.CurrentPlayer = CurrentPlayer;
            return newState;
        }

        public override bool Equals(object obj)
        {
            State other = obj as State;
            if (other.CurrentPlayer != this.CurrentPlayer)
            {
                return false;
            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (other.Board[i,j] != this.Board[i,j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public char GetStatus()
        {
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    if (Board[i,j] == PLAYER1 
                        && Board[i - 1, j] == PLAYER1  // felette
                        && Board[i + 1, j] == PLAYER1  // alatta
                        && Board[i, j + 1] == PLAYER1  // jobbra
                        && Board[i, j - 1] == PLAYER1) // balra
                    {
                        return PLAYER2;
                    }
                    if (Board[i, j] == PLAYER2
                        && Board[i - 1, j] == PLAYER2  // felette
                        && Board[i + 1, j] == PLAYER2  // alatta
                        && Board[i, j + 1] == PLAYER2  // jobbra
                        && Board[i, j - 1] == PLAYER2) // balra
                    {
                        return PLAYER1;
                    }
                }
            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (Board[i,j] == BLANK)
                    {
                        return BLANK;
                    }
                }
            }
            return DRAW;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("    1   2   3   4   5   6");
            sb.AppendLine("  +---+---+---+---+---+---+");
            for (int i = 0; i < 6; i++)
            {
                sb.AppendFormat($"{i + 1} |");
                for (int j = 0; j < 6; j++)
                {
                    sb.AppendFormat($" {Board[i,j]} |");
                }
                sb.AppendLine("\n  +---+---+---+---+---+---+");
            }
            sb.AppendFormat($"Current player: {CurrentPlayer}");
            return sb.ToString();
        }

        private static int WIN = 100;
        private static int LOSE = -100;
        private static int DRAW_SCORE = 50;
        private static int POSSIBLE_LOSE = -1;
        private static int POSSIBLE_WIN = 2;
        
        public int GetHeuristics(char player)
        {
            char otherPlayer = player == PLAYER1 ? PLAYER2 : PLAYER1;
            char status = GetStatus();
            if (status == player)
            {
                return WIN;
            }
            if (status == otherPlayer)
            {
                return LOSE;
            }
            if (status == DRAW)
            {
                return DRAW_SCORE;
            }
            int result = 0;
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    result += CalculateHeuristics(i, j, player, otherPlayer);
                }
            }
            return result;
        }
        private int CalculateHeuristics(int row, int col, char player, char other)
        {
            int result = 0;
            int playerCount = 0;
            int otherCount = 0;
            int[] dy = new int[] { 0, 0,  0, 1, -1 };
            int[] dx = new int[] { 0, 1, -1, 0,  0 };
            for (int i = 0; i < 5; i++)
            {
                if (Board[row + dy[i], col + dx[i]] == player)
                {
                    playerCount++;
                }
                else if (Board[row + dy[i], col + dx[i]] == other)
                {
                    otherCount++;
                }
            }

            if (playerCount > 0 && otherCount == 0)
            {
                result += POSSIBLE_LOSE * playerCount;
            }
            if (otherCount > 0 && playerCount == 0)
            {
                result += POSSIBLE_WIN * otherCount;
            }
            return result;
        }
    }
}
