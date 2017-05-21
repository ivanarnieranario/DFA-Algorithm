using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFA_Algorithm
{
    public class TransTable
    {
        private String Q;
        private String QValue;

        public TransTable(String Q, String QValue)
        {
            this.Q = Q;
            this.QValue = QValue;
        }

        public String getQ()
        {
            return Q;
        }

        public String getQValue()
        {
            return QValue;
        }


    }



    class Transition
    {

        private static String[] distinct;
        private List<TransTable> tables;

        public Transition()
        {
            tables = new List<TransTable>();
        }

        public static void initialize(String sigma)
        {
            int length = sigma.Length;
            distinct = new String[length];
            for (int x = 0; x < length; x++)
                distinct[x] = sigma[x].ToString();

        }

        public void set(String Q, String QValue)
        {
            tables.Add(new TransTable(Q, QValue));
        }

        public List<TransTable> getTables()
        {
            return tables;
        }

        public static String[] getDisctinct()
        {
            return distinct;
        }



    }
}
