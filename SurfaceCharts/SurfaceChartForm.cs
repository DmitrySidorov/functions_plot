using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SurfaceCharts
{
    public partial class SurfaceChartForm : Form
    {
        ChartStyle cs;
        ChartStyle2D cs2d;
        DataSeries ds;
        DrawChart dc;
        ChartFunctions cf;
        ColorMap cm;

        public SurfaceChartForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint |
            ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            // Subscribing to a paint eventhandler to drawingPanel:
            PlotPanel.Paint += new PaintEventHandler(PlotPanelPaint);
            cs = new ChartStyle(this);
            cs2d = new ChartStyle2D(this);
            ds = new DataSeries();
            dc = new DrawChart(this);
            cf = new ChartFunctions();
            cm = new ColorMap();
            cs.GridStyle.LineColor = Color.LightGray;
            cs.GridStyle.Pattern = DashStyle.Dash;
            cs.Title = "No Title";
            dc.ChartType = DrawChart.ChartTypeEnum.Mesh;
            dc.IsColorMap = true;
            dc.IsHiddenLine = false;
            dc.CMap = cm.Jet();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (dc.ChartType == DrawChart.ChartTypeEnum.Contour)
            {
                Rectangle rect = this.ClientRectangle;
                cs2d.ChartArea = new Rectangle(rect.X, rect.Y, rect.Width, 19 * rect.Height / 30);
                cf.Peak3D(ds, cs);
            }
            cs2d.SetPlotArea(g, cs);
            dc.AddColorBar(g, ds, cs, cs2d);
        }

        private void PlotPanelPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (dc.ChartType == DrawChart.ChartTypeEnum.Contour)
            {
                cs2d.AddChartStyle2D(g, cs);
                dc.AddChart(g, ds, cs, cs2d);
            }
            else
            {
                cs.Elevation = 45;
                cs.Azimuth = 45;
                cf.Peak3D(ds, cs);
                cs.AddChartStyle(g);
                dc.AddChart(g, ds, cs, cs2d);
            }
        }
    }
}
