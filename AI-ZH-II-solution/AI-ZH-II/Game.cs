using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_II
{
    internal class Game
    {
        private ASolver solver;
        private static bool IS_PLAYER_STARTING = true;
        private static ConsoleColor P1COLOR = ConsoleColor.Red;
        private static ConsoleColor P2COLOR = ConsoleColor.Blue;
        public Game(ASolver solver)
        {
            this.solver = solver;
        }

        public void Play()
        {
            bool playersTurn = IS_PLAYER_STARTING;
            State currentState = new State();
            while (currentState.GetStatus() == State.BLANK)
            {
                Console.WriteLine(currentState);
                if (playersTurn)
                {
                    currentState = PlayersMove(currentState);
                }
                else
                {
                    currentState = AIsMove(currentState);
                }
                playersTurn = !playersTurn;
            }
            Console.WriteLine(currentState);
            Console.WriteLine($"Winner: {currentState.GetStatus()}");
        }
        public void ColorfulPlay()
        {
            bool playersTurn = IS_PLAYER_STARTING;
            State currentState = new State();
            while (currentState.GetStatus() == State.BLANK)
            {
                ColorfulPrint(currentState);
                if (playersTurn)
                {
                    currentState = PlayersMove(currentState);
                }
                else
                {
                    currentState = AIsMove(currentState);
                }
                playersTurn = !playersTurn;
                Console.Clear();
            }
            ColorfulPrint(currentState);
            char status = currentState.GetStatus();
            if (status == State.DRAW)
            {
                Console.WriteLine("Draw");
            }
            else
            {
                Console.Write("Winner: ");
                if (status == State.PLAYER1)
                {
                    Console.BackgroundColor = P1COLOR;
                }
                else
                {
                    Console.BackgroundColor = P2COLOR;
                }
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }
        private State PlayersMove(State currentState)
        {
            Operator op = null;
            do
            {
                int row, col;
                do
                {
                    Console.Write("Row: ");
                } while (!int.TryParse(Console.ReadLine(), out row) || row < 1 || row > 6);
                do
                {
                    Console.Write("Col: ");
                } while (!int.TryParse(Console.ReadLine(), out col) || col < 1 || col > 6);
                op = new Operator(row - 1, col - 1);
            } while (op == null || !op.IsApplicable(currentState));
            return op.Apply(currentState);
        }

        private void ColorfulPrint(State currentState)
        {
            char[,] Board = currentState.Board;
            Console.WriteLine("    1   2   3   4   5   6");
            Console.WriteLine("  +---+---+---+---+---+---+");
            for (int i = 0; i < 6; i++)
            {
                Console.Write($"{i + 1} |");
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(" ");
                    if (Board[i,j] == State.PLAYER1)
                    {
                        Console.BackgroundColor = P1COLOR;
                    }
                    else if (Board[i, j] == State.PLAYER2)
                    {
                        Console.BackgroundColor = P2COLOR;
                    }
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" |");
                }
                Console.WriteLine("\n  +---+---+---+---+---+---+");
            }
            Console.Write($"Current player: ");
            if (currentState.CurrentPlayer == State.PLAYER1)
            {
                Console.BackgroundColor = P1COLOR;
            }
            else if (currentState.CurrentPlayer == State.PLAYER2)
            {
                Console.BackgroundColor = P2COLOR;
            }
            Console.Write("  ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        private State AIsMove(State currentState)
        {
            State nextState = solver.NextMove(currentState);
            if (nextState == null)
            {
                throw new Exception("The AI cannot select the next move.");
            }
            return nextState;
        }
    }
}
