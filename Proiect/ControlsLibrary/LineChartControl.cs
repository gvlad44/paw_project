using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlsLibrary
{
    public partial class LineChartControl : Control
    {
        private LineChartValue[] data;
        public LineChartValue[] Data {
            get {return data;}
            set { data = value;
                Invalidate();
                }
        }

        public LineChartControl()
        {

            Data = new LineChartValue[]
            {
                new LineChartValue("2020", 1),
                new LineChartValue("2021", 2),
                new LineChartValue("2022", 3)
            };
            

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics graphics = pe.Graphics;
            Rectangle clipRectangle = pe.ClipRectangle;

            var lineHeight = clipRectangle.Height/ Data.Length; //W = W
            var maxValue = Data.Max(x => x.Value);
            var scalingFactor = 0;
            if (maxValue != 0)
            {
                 scalingFactor = clipRectangle.Width / maxValue; // = H
            }

                for (var i = 0; i < Data.Length; i++)
                {
                    var lineWidth = Data[i].Value * scalingFactor; // H
                    var lineY = i * lineHeight; // W
                    var lineX = 0;

                    graphics.FillRectangle(
                        Brushes.RoyalBlue,
                        lineX,
                        lineY,
                        lineWidth,
                        (float)(lineHeight * 0.75));

                    graphics.DrawRectangle(
                        Pens.Black,
                        lineX,
                        lineY,
                        lineWidth,
                        (float)(lineHeight * 0.75));
                }

        }
    }
}
