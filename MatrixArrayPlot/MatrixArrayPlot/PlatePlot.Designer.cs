namespace MatrixArrayPlot
{
    partial class PlatePlot
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.plateHeatMap = new MatrixArrayPlot.PlateHeatMap();
            this.SuspendLayout();
            // 
            // plateHeatMap
            // 
            this.plateHeatMap.BackColor = System.Drawing.SystemColors.Control;
            this.plateHeatMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plateHeatMap.Location = new System.Drawing.Point(0, 0);
            this.plateHeatMap.Name = "plateHeatMap";
            this.plateHeatMap.RoomForText = 25;
            this.plateHeatMap.ShowValues = true;
            this.plateHeatMap.Size = new System.Drawing.Size(725, 555);
            this.plateHeatMap.TabIndex = 0;
            // 
            // PlatePlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 555);
            this.Controls.Add(this.plateHeatMap);
            this.Name = "PlatePlot";
            this.ResumeLayout(false);

        }

        #endregion

        private PlateHeatMap plateHeatMap;

    }
}