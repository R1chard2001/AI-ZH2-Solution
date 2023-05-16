using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_ZH_II
{
    public abstract class ASolver
    {
        public List<Operator> Operators = new List<Operator>();
        public ASolver()
        {
            generateOperators();
        }
        private void generateOperators()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Operators.Add(new Operator(i,j));
                }
            }
        }
        public abstract State NextMove(State currentState);
    }
}
