using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SurfaceCharts
{
    public class ChartFunctions
    {
        public ChartFunctions()
        {
        }
        
        public void Peak3D(DataSeries ds, ChartStyle cs)
        {
            int nz = 0;
            int ierr = 0;
            int id = 0;
            int kode = 1;

            double air = 0;
            double aii = 0;

            cs.XMin = -3;
            cs.XMax = 1;
            cs.YMin = -3;
            cs.YMax = 3;
            cs.ZMin = -18;
            cs.ZMax = 18;
            cs.XTick = 1;
            cs.YTick = 1;
            cs.ZTick = 4;

            ds.XDataMin = cs.XMin;
            ds.YDataMin = cs.YMin;

            ds.XSpacing = 0.03f;
            ds.YSpacing = 0.03f;

            ds.XNumber = Convert.ToInt16((cs.XMax - cs.XMin) / ds.XSpacing) + 1;
            ds.YNumber = Convert.ToInt16((cs.YMax - cs.YMin) / ds.YSpacing) + 1;

            MessageBox.Show(ds.XNumber + "\t" + ds.YNumber);

            Point3[,] pts = new Point3[ds.XNumber, ds.YNumber];

            for (int i = 0; i < ds.XNumber; i++)
            {
                for (int j = 0; j < ds.YNumber; j++)
                {
                    float x = ds.XDataMin + i * ds.XSpacing;
                    float y = ds.YDataMin + j * ds.YSpacing;
                    
                    FortranLib.zairy_wrap(x, y, id, kode, ref air, ref aii, ref nz, ref ierr);

                    float z = (float)air;

                    pts[i, j] = new Point3(x, y, z, 1);
                }
            }
            ds.PointArray = pts;
        }
    }
}