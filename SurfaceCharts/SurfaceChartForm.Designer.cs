namespace SurfaceCharts
{
    partial class SurfaceChartForm
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
            this.PlotPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PlotPanel
            // 
            this.PlotPanel.BackColor = System.Drawing.Color.White;
            this.PlotPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlotPanel.Location = new System.Drawing.Point(0, 0);
            this.PlotPanel.Name = "PlotPanel";
            this.PlotPanel.Size = new System.Drawing.Size(765, 675);
            this.PlotPanel.TabIndex = 0;
            this.PlotPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PlotPanel_MouseDown);
            this.PlotPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PlotPanel_MouseMove);
            // 
            // SurfaceChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 675);
            this.Controls.Add(this.PlotPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SurfaceChartForm";
            this.Text = "Surface Chart";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel PlotPanel;
    }
}

