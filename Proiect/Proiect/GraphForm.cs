using ControlsLibrary;
using Proiect.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect
{
    public partial class GraphForm : Form
    {

        int noR;
        int noW;
        int noE;

        public GraphForm(int noR, int noW, int noE)
        {
            InitializeComponent();
            this.noR = noR;
            this.noW = noW;
            this.noE = noE;
        }

        private void GraphForm_Load(object sender, EventArgs e)
        {

            var values = new LineChartValue[]
            {
                    new LineChartValue("Read", this.noR),
                    new LineChartValue("Write", this.noW),
                    new LineChartValue("Execute", this.noE)
            };

            lineChartControl.Data = values;
        }
    }
}
