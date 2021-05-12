using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlsLibrary
{
    public class LineChartValue
    {
        public string Label { get; set; }
        public int Value { get; set; }

        public LineChartValue(string label, int value)
        {
            Label = label;
            Value = value;
        }


    }
}
