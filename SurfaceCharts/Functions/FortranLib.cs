using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceCharts
{
    static class FortranLib
    {
        [DllImport("libcomplex_bessel.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void zairy_wrap(double zr, double zi, int id, int kode, ref double air, ref double aii, ref int nz, ref int ierr);
    }
}
