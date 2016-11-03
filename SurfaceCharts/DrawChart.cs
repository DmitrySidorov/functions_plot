using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceCharts
{
    public class DrawChart
    {
        private SurfaceChartForm form1;
        private ChartTypeEnum chartType;
        private int[,] cmap;
        private bool isColorMap = true;
        private bool isHiddenLine = false;
        private bool isInterp = false;
        private int numberInterp = 2;
        private int numberContours = 10;
        private SliceEnum xyzSlice = SliceEnum.XSlice;
        private float sliceLocation = 0;
        private bool isBarSingleColor = true;
        public DrawChart(SurfaceChartForm fm1)
        {
            form1 = fm1;
        }
        public float SliceLocation
        {
            get { return sliceLocation; }
            set { sliceLocation = value; }
        }
        public bool IsBarSingleColor
        {
            get { return isBarSingleColor; }
            set { isBarSingleColor = value; }
        }
        public int NumberContours
        {
            get { return numberContours; }
            set { numberContours = value; }
        }
        public int NumberInterp
        {
            get { return numberInterp; }
            set { numberInterp = value; }
        }
        public bool IsInterp
        {
            get { return isInterp; }
            set { isInterp = value; }
        }
        public bool IsColorMap
        {
            get { return isColorMap; }
            set { isColorMap = value; }
        }
        public bool IsHiddenLine
        {
            get { return isHiddenLine; }
            set { isHiddenLine = value; }
        }
        public int[,] CMap
        {
            get { return cmap; }
            set { cmap = value; }
        }
        public ChartTypeEnum ChartType
        {
            get { return chartType; }
            set { chartType = value; }
        }
        public SliceEnum XYZSlice
        {
            get { return xyzSlice; }
            set { xyzSlice = value; }
        }
        public enum SliceEnum
        {
            XSlice,
            YSlice,
            ZSlice
        }
        public enum ChartTypeEnum
        {
            Mesh,
            Surface,
            Contour
        }
        public void AddChart(Graphics g, DataSeries ds, ChartStyle cs, ChartStyle2D cs2d)
        {
            switch (ChartType)
            {
                case ChartTypeEnum.Mesh:
                    AddMesh(g, ds, cs);
                    AddColorBar(g, ds, cs, cs2d);
                    break;
                //case ChartTypeEnum.Surface:
                //    AddSurface(g, ds, cs, cs2d);
                //    AddColorBar(g, ds, cs, cs2d);
                //    break;
                //case ChartTypeEnum.Contour:
                //    AddContour(g, ds, cs, cs2d);
                //    break;
                //case ChartTypeEnum.FillContour:
            }
        }
        public void AddColorBar(Graphics g, DataSeries ds, ChartStyle cs, ChartStyle2D cs2d)
        {
            if (cs.IsColorBar && IsColorMap)
            {
                Pen aPen = new Pen(Color.Black, 1);
                SolidBrush aBrush = new SolidBrush(cs.TickColor);
                StringFormat sFormat = new StringFormat();
                sFormat.Alignment = StringAlignment.Near;
                SizeF size = g.MeasureString("A", cs.TickFont);

                int x, y, width, height;

                Point3[] pts = new Point3[64];
                PointF[] pta = new PointF[4];

                float zmin, zmax;

                zmin = ds.ZDataMin();
                zmax = ds.ZDataMax();

                float dz = (zmax - zmin) / 63;
                if (ChartType == ChartTypeEnum.Contour )
                {
                    x = 5 * cs2d.ChartArea.Width / 6;
                    y = form1.PlotPanel.Top;
                    width = cs2d.ChartArea.Width / 25;
                    height = form1.PlotPanel.Height;
                    // Add color bar:
                    for (int i = 0; i < 64; i++)
                    {
                        pts[i] = new Point3(x, y, zmin + i * dz, 1);
                    }
                    for (int i = 0; i < 63; i++)
                    {
                        Color color = AddColor(cs, pts[i],
                        zmin, zmax);
                        aBrush = new SolidBrush(color);
                        float y1 = y + height - (pts[i].Z - zmin) *
                        height / (zmax - zmin);
                        float y2 = y + height - (pts[i + 1].Z - zmin) * height / (zmax - zmin);
                        pta[0] = new PointF(x, y2);
                        pta[1] = new PointF(x + width, y2);
                        pta[2] = new PointF(x + width, y1);
                        pta[3] = new PointF(x, y1);
                        g.FillPolygon(aBrush, pta);
                    }
                    g.DrawRectangle(aPen, x, y, width, height);
                    // Add ticks and labels to the color bar:
                    float ticklength = 0.1f * width;
                    for (float z = zmin; z <= zmax; z = z +
                    (zmax - zmin) / 6)
                    {
                        float yy = y + height - (z - zmin) *
                        height / (zmax - zmin);
                        g.DrawLine(aPen, x, yy, x + ticklength, yy);
                        g.DrawLine(aPen, x + width, yy, x +
                        width - ticklength, yy);
                        g.DrawString((Math.Round(z, 2)).ToString(),
                        cs.TickFont, aBrush, new PointF(x + width
                        + 5, yy - size.Height / 2), sFormat);
                    }
                }
                else
                {
                    x = 5 * form1.PlotPanel.Width / 6;
                    y = form1.PlotPanel.Height / 10;
                    width = form1.PlotPanel.Width / 25;
                    height = 8 * form1.PlotPanel.Height / 10;
                    // Add color bar:
                    for (int i = 0; i < 64; i++)
                    {
                        pts[i] = new Point3(x, y, zmin + i * dz, 1);
                    }
                    for (int i = 0; i < 63; i++)
                    {
                        Color color = AddColor(cs, pts[i],
                        zmin, zmax);
                        aBrush = new SolidBrush(color);
                        float y1 = y + height - (pts[i].Z - zmin) *
                        height / (zmax - zmin);
                        float y2 = y + height - (pts[i + 1].Z - zmin) * height / (zmax - zmin);
                        pta[0] = new PointF(x, y2);
                        pta[1] = new PointF(x + width, y2);
                        pta[2] = new PointF(x + width, y1);
                        pta[3] = new PointF(x, y1);
                        g.FillPolygon(aBrush, pta);
                    }
                    g.DrawRectangle(aPen, x, y, width, height);
                    // Add ticks and labels to the color bar:
                    float ticklength = 0.1f * width;
                    for (float z = zmin; z <= zmax; z = z +
                    (zmax - zmin) / 6)
                    {
                        float yy = y + height - (z - zmin) *
                        height / (zmax - zmin);
                        g.DrawLine(aPen, x, yy, x + ticklength, yy);
                        g.DrawLine(aPen, x + width, yy, x + width - ticklength, yy);
                        g.DrawString((Math.Round(z, 2)).ToString(),
                        cs.TickFont, aBrush, new PointF(x + width + 5, yy - size.Height / 2),
                        sFormat);
                    }
                }
            }
        }

        private Color AddColor(ChartStyle cs, Point3 pt, float zmin, float zmax)
        {
            int colorLength = CMap.GetLength(0);
            int cindex = (int)Math.Round((colorLength *
            (pt.Z - zmin) + (zmax - pt.Z)) /
            (zmax - zmin));
            if (cindex < 1)
                cindex = 1;
            if (cindex > colorLength)
                cindex = colorLength;
            Color color = Color.FromArgb(CMap[cindex - 1, 0],
            CMap[cindex - 1, 1], CMap[cindex - 1, 2],
            CMap[cindex - 1, 3]);
            return color;
        }

        private void AddMesh(Graphics g, DataSeries ds, ChartStyle cs)
        {
            Pen aPen = new Pen(ds.LineStyle.LineColor,
            ds.LineStyle.Thickness);
            aPen.DashStyle = ds.LineStyle.Pattern;
            SolidBrush aBrush = new SolidBrush(Color.White);
            Matrix3 m = Matrix3.AzimuthElevation(cs.Elevation, cs.Azimuth);
            PointF[] pta = new PointF[4];
            Point3[,] pts = ds.PointArray;
            // Find the minumum and maximum z values:
            float zmin = ds.ZDataMin();
            float zmax = ds.ZDataMax();
            // Perform transformations on points:
            for (int i = 0; i < pts.GetLength(0); i++)
            {

                for (int j = 0; j < pts.GetLength(1); j++)
                {
                    pts[i, j].Transform(m, form1, cs);
                }
            }
            // Draw mesh:
            for (int i = 0; i < pts.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < pts.GetLength(1) - 1; j++)
                {
                    int ii = i;
                    if (cs.Azimuth >= -180 && cs.Azimuth < 0)
                    {
                        ii = pts.GetLength(0) - 2 - i;
                    }
                    pta[0] = new PointF(pts[ii, j].X, pts[ii, j].Y);
                    pta[1] = new PointF(pts[ii, j + 1].X, pts[ii, j + 1].Y);
                    pta[2] = new PointF(pts[ii + 1, j + 1].X, pts[ii + 1,
                    j + 1].Y);
                    pta[3] = new PointF(pts[ii + 1, j].X, pts[ii + 1, j].Y);
                    if (!IsHiddenLine)
                    {
                        g.FillPolygon(aBrush, pta);
                    }
                    if (IsColorMap)
                    {
                        Color color = AddColor(cs, pts[ii, j], zmin, zmax);
                        aPen = new Pen(color, ds.LineStyle.Thickness);
                        aPen.DashStyle = ds.LineStyle.Pattern;
                    }
                    g.DrawPolygon(aPen, pta);
                }
            }
            aPen.Dispose();
            aBrush.Dispose();
        }
    }
}