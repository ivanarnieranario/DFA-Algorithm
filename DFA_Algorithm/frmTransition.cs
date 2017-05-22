using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DFA_Algorithm
{
    public partial class frmTransition : Form
    {
        public static Boolean isEndWith = false;

        public List<TransTable> tables { set; get; }
        public frmMain main { set; get; }
        public String finalState { set; get; }


        public frmTransition()
        {
            InitializeComponent();
        }

        private String getValue(String temp)
        {
            String val = "";

            if (isEndWith == true)
            {

                for (int x = 0; x < temp.Length; x++)
                {
                    val = checkValue(temp.Substring(x));
                    if (val != null)
                        return val;
                }
                return "q0";
            }
            else
            {
                if (temp.Contains("∑"))
                    val = checkValue(temp.Substring(1));
                else if (temp.Contains("rejected"))
                    val = "qR";
                else
                    val = checkValue(temp);

                if (val != null)
                    return val;

                return "qR";
            }
        }

        private String checkValue(String value)
        {
            foreach (TransTable table in tables)
            {
                if (isEndWith == true)
                {
                    if (table.getQValue().Equals(value))
                    {
                        return table.getQ();
                    }
                }
                else
                {
                    int length = tables[tables.Count - 2].getQValue().Length;

                    if (value.Length > length)
                    {

                        if (table.getQValue().Length < length)
                            continue;

                        if (value.StartsWith(table.getQValue()))
                        {
                            return table.getQ();
                        }
                    }
                    else
                    {
                        if (table.getQValue().Equals(value))
                        {
                            return table.getQ();
                        }
                    }
                }
            }

            return null;
        }

        

        private void TransitionTable_Load(object sender, EventArgs e)
        {
            dgvTransitions.ColumnHeadersDefaultCellStyle.Font = new Font("Lucida Sans", 12F, FontStyle.Bold);
            dgvTransitions.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            lblFinalState.Text = finalState.Insert(1,"inal State");


            foreach (String str in Transition.getDisctinct())
            {
                DataGridViewColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = str;
                dgvTransitions.Columns.Add(column);
            }

            lblQ.Text = string.Empty;
            
            foreach (TransTable table in tables)
            {
                String Q = table.getQ();
                String QValue = table.getQValue();

                lblQ.Text += QValue+" = " + Q + Environment.NewLine;

                String[] distinct = Transition.getDisctinct();
                int length = distinct.Length;
                Object[] val = new object[length];
                for (int x = 0; x < length; x++)
                {
                    String temp = QValue + distinct[x];
                    val[x] = getValue(temp);
                }
                dgvTransitions.Rows.Add(val);
            
            }
            dgvTransitions.ClearSelection();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            main.init();
            this.Close();
            main.Show();
        }

        private void TransitionTable_Paint(object sender, PaintEventArgs e)
        {
            Pen whitePen = new Pen(Color.White, 3);

            float x1 = 180.0F, y1 = 70.0F;
            float x2 = 230.0F, y2 = 100.0F;
            e.Graphics.DrawLine(whitePen, x1, y1, x2, y2);
            x1 = 230.0F; y1 = 100.0F;
            x2 = 230.0F; y2 = 430.0F;
            e.Graphics.DrawLine(whitePen, x1, y1, x2, y2);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            frmDiagram diagram = new frmDiagram();
            diagram.transition = this;
            diagram.ShowDialog();
           
        }

        public DataGridView getTables()
        {
            return dgvTransitions;
        }

      
    }
}
