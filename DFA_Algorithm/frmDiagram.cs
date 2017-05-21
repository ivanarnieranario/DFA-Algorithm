using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;
using Microsoft.Glee.Drawing;


namespace DFA_Algorithm
{

    public partial class frmDiagram : Form
    {
        private Microsoft.Glee.GraphViewerGdi.GViewer viewer;
        private Graph graph;

        public frmTransition transition { set; get; }

        public frmDiagram()
        {
            InitializeComponent();
            viewer = new Microsoft.Glee.GraphViewerGdi.GViewer();
            graph = new Graph("diagram");
        }



        private void frmDiagram_Load(object sender, EventArgs e)
        {
            DataGridView tables = transition.getTables();

            int rowCount = tables.Rows.Count;
            String[] nodes = new String[rowCount];
            for (int x = 0; x < rowCount; x++)
                nodes[x] = "q" + x;

                //connect nodes
                foreach (DataGridViewColumn column in tables.Columns)
                {
                    foreach (DataGridViewRow row in tables.Rows)
                    {
                        if (row.Index == tables.Rows.Count - 1)
                            break;

                        graph.AddEdge("q"+row.Index,column.HeaderText,row.Cells[column.Index].Value.ToString());
                        
                    }
                }


            viewer.Graph = graph;
            viewer.Dock = DockStyle.Fill;

            this.Controls.Add(viewer);

            
        }

       
    }
}
