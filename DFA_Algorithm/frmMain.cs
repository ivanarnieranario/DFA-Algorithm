using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DFA_Algorithm
{
    public partial class frmMain : Form
    {
        private Transition transition;
        private List<TransTable> transTable;
        private String finalState;

        public frmMain()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            transition = new Transition();
            transTable = new List<TransTable>();
            txtInput.Focus();
            txtInput.SelectAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(null, "Are you sure you want to close?", "Closing application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
                this.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cbAlgo.SelectedIndex = 1;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(lblOutputs.Text))
            {
                MessageBox.Show(null,"Please input a string to generate","Input string",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtInput.Focus();
            }
            else
            {
                if (cbAlgo.SelectedIndex == 0)
                    frmTransition.isEndWith = true;
                else
                    frmTransition.isEndWith = false;


                transTable = transition.getTables();

                foreach (var str in lblQ.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (str.Contains("∑"))
                    {
                        transition.set("q0", "∑");
                    }
                    else
                    {
                        int index = str.IndexOf("=");
                        transition.set(str.Substring(0, index - 1),str.Substring(index + 2));
                    }
                }

                frmTransition frmTrans = new frmTransition();
                frmTrans.main = this;
                frmTrans.tables = transition.getTables();
                frmTrans.finalState = finalState;
                this.Hide();
                frmTrans.Show();
                
            }
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {

            generate();
        }

        private void generate()
        {
            String input = txtInput.Text;
            if (!input.Contains(" ") && !string.IsNullOrEmpty(input))
            {
                displayMModel();
            }
            else
            {
                lblOutputs.Text = string.Empty;
                lblQ.Text = string.Empty;
            }
        }

        private void displayMModel()
        {
            lblOutputs.Text = string.Empty;
            lblQ.Text = string.Empty;

            string input = txtInput.Text;
            int length = input.Length;

            string qValue = "";

            lblQ.Text += "q0 = initial state ( ∑ )" + Environment.NewLine;
         

            for (int x = 0; x < length; x++)
            {
                qValue += input[x];
                String q = "q" + (x + 1);
                lblQ.Text += q + " = " + qValue;

                lblQ.Text += Environment.NewLine;
            }

            if (cbAlgo.SelectedIndex == 1)
                lblQ.Text += "qR = rejected";

            //sort the string using LinQ
            string sorted = (string)string.Concat(input.OrderBy(c => c));

            //get the distinct in a string
            input = new string(sorted.Distinct().ToArray());

            //initialize the distinct to set it to efsilon
            Transition.initialize(input);

            //add a comma separator of each character of a string
            input = string.Join<char>(",", input);

            lblOutputs.Text += "Qo = q0 " + Environment.NewLine + Environment.NewLine;
            lblOutputs.Text += "∑ = { " + input + " }" + Environment.NewLine;
            lblOutputs.Text += finalState =  "F = { " + "q" + length + " }";
        }

        private void cbAlgo_SelectedIndexChanged(object sender, EventArgs e)
        {
            generate();
        }

     

      







    }
}
