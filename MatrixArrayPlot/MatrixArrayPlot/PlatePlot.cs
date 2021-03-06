﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MatrixArrayPlot
{
    public partial class PlatePlot : Form
    {
        public PlatePlot()
        {
            InitializeComponent();
        }
        public void SetValuesDynamic(dynamic values)
        {
            plateHeatMap.SetValuesDynamic(values);
        }
        public void SwitchToRainbow()
        {
            this.plateHeatMap.SwitchToRainbow();
        }
        public void SaveImage(string filename)
        {
            this.plateHeatMap.SaveImage(filename);
        }
        public void SetTextFormat(string format)
        {
            plateHeatMap.SetLabeFormat(format);
        }
        public void ShowInNewThread()
        {
            Thread t = new Thread(ShowMe);
            t.IsBackground = true;
            t.Start();
        }
        private void ShowMe()
        {
            Application.Run(this);
        }
    }
}
