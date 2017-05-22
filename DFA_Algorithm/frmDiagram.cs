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

            int left = 0;
            int right = 1;

            for (int x = 0; x < rowCount; x++)
                nodes[x] = "q" + x;

                //connect nodes
                foreach (DataGridViewColumn column in tables.Columns)
                {
                    foreach (DataGridViewRow row in tables.Rows)
                    {
                        
                        String source = "q"+row.Index;
                        String des = row.Cells[column.Index].Value.ToString();

                        if (frmTransition.isEndWith == false)
                        {

                            if (row.Index == tables.Rows.Count - 1)
                                break;

                            if (source.Equals("qR") || des.Equals("qR"))
                            {
                                graph.AddEdge(source, column.HeaderText, des).EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Red;
                                continue;
                            }

                            graph.AddEdge(source, column.HeaderText, des).EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Blue;
                        }
                        else
                        {

                            int dest = int.Parse(des.Substring(1));

                            if (row.Index < dest)
                            {
                                graph.AddEdge(source, column.HeaderText, des).EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Blue;
                                continue;
                            }

                            graph.AddEdge(source, column.HeaderText, des).EdgeAttr.Color = Microsoft.Glee.Drawing.Color.Red;
                        }

                   

                    }
                }



            if (frmTransition.isEndWith == false)
            {
                try
                {
                    graph.FindNode("qR").Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Red;
                    graph.FindNode("qR").Attr.Fontcolor = Microsoft.Glee.Drawing.Color.White;

                }catch(Exception){
                    MessageBox.Show("No rejected found.");
                }

                graph.FindNode("q0").Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Green;
                graph.FindNode("q0").Attr.Fontcolor = Microsoft.Glee.Drawing.Color.White;

                graph.FindNode(nodes[rowCount - 2]).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Blue;
                graph.FindNode(nodes[rowCount - 2]).Attr.Fontcolor = Microsoft.Glee.Drawing.Color.White;
            }
            else
            {
                graph.FindNode(nodes[rowCount-1]).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Blue;
                graph.FindNode(nodes[rowCount - 1]).Attr.Fontcolor = Microsoft.Glee.Drawing.Color.WhiteSmoke;

                for (int x = 0; x < rowCount - 1; x++)
                {
                    if (x == 0)
                    {
                        graph.FindNode(nodes[x]).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Red;
                        graph.FindNode(nodes[x]).Attr.Fontcolor = Microsoft.Glee.Drawing.Color.WhiteSmoke;
                    }
                    else
                    {
                        graph.FindNode(nodes[x]).Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Red;
                        graph.FindNode(nodes[x]).Attr.Fontcolor = Microsoft.Glee.Drawing.Color.WhiteSmoke;
                    }
                }
            }




            viewer.Graph = graph;
            viewer.Dock = DockStyle.Fill;

            this.Controls.Add(viewer);

            
        }

       
    }
}
