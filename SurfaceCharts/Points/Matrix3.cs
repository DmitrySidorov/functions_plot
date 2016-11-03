using System;

namespace SurfaceCharts
{
    public class Matrix3
    {
        public float[,] M = new float[4, 4];
        public Matrix3()
        {
            Identity3();
        }
        public Matrix3(float m00, float m01, float m02, float m03,
        float m10, float m11, float m12, float m13,
        float m20, float m21, float m22, float m23,
        float m30, float m31, float m32, float m33)
        {
            M[0, 0] = m00;
            M[0, 1] = m01;
            M[0, 2] = m02;
            M[0, 3] = m03;
            M[1, 0] = m10;
            M[1, 1] = m11;
            M[1, 2] = m12;
            M[1, 3] = m13;
            M[2, 0] = m20;
            M[2, 1] = m21;
            M[2, 2] = m22;
            M[2, 3] = m23;
            M[3, 0] = m30;
            M[3, 1] = m31;
            M[3, 2] = m32;
            M[3, 3] = m33;
        }
        // Define a Identity matrix:
        public void Identity3()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == j)
                    {
                        M[i, j] = 1;
                    }
                    else
                    {
                        M[i, j] = 0;
                    }
                }
            }
        }
        // Multiply two matrices together:
        public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
        {
            Matrix3 result = new Matrix3();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    float element = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        element += m1.M[i, k] * m2.M[k, j];
                    }
                    result.M[i, j] = element;
                }
            }
            return result;
        }
        // Apply a transformation to a vector (point):
        public float[] VectorMultiply(float[] vector)
        {
            float[] result = new float[4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i] += M[i, j] * vector[j];
                }
            }
            return result;
        }

        // Perspective projection matrix:
        public static Matrix3 Perspective(float d)
        {
            Matrix3 result = new Matrix3();
            result.M[3, 2] = -1 / d;
            return result;
        }

        public static Matrix3 AzimuthElevation(float elevation, float azimuth, float oneOverd)
        {
            Matrix3 result = new Matrix3();
            Matrix3 rotate = new Matrix3();
            // make sure elevation in the range of [-90, 90]:
            if (elevation > 90)
                elevation = 90;
            else if (elevation < -90)
                elevation = -90;
            // Make sure azimuth in the range of [-180, 180]:
            if (azimuth > 180)
                azimuth = 180;
            else if (azimuth < -180)
                azimuth = -180;
            elevation = elevation * (float)Math.PI / 180.0f;
            float sne = (float)Math.Sin(elevation);
            float cne = (float)Math.Cos(elevation);
            azimuth = azimuth * (float)Math.PI / 180.0f;
            float sna = (float)Math.Sin(azimuth);
            float cna = (float)Math.Cos(azimuth);
            rotate.M[0, 0] = cna;
            rotate.M[0, 1] = sna;
            rotate.M[0, 2] = 0;
            rotate.M[1, 0] = -sne * sna;
            rotate.M[1, 1] = sne * cna;
            rotate.M[1, 2] = cne;
            rotate.M[2, 0] = cne * sna;
            rotate.M[2, 1] = -cne * cna;
            rotate.M[2, 2] = sne;
            if (oneOverd <= 0)
                result = rotate;
            else if (oneOverd > 0)
            {
                Matrix3 perspective = Matrix3.Perspective(1 / oneOverd);
                result = perspective * rotate;
            }
            return result;
        }

        public static Matrix3 AzimuthElevation(float elevation, float azimuth)
        {
            Matrix3 result = new Matrix3();
            // make sure elevation in the range of [-90, 90]:
            if (elevation > 90)
                elevation = 90;
            else if (elevation < -90)
                elevation = -90;
            // Make sure azimuth in the range of [-180, 180]:
            if (azimuth > 180)
                azimuth = 180;
            else if (azimuth < -180)
                azimuth = -180;
            elevation = elevation * (float)Math.PI / 180.0f;
            float sne = (float)Math.Sin(elevation);
            float cne = (float)Math.Cos(elevation);
            azimuth = azimuth * (float)Math.PI / 180.0f;
            float sna = (float)Math.Sin(azimuth);
            float cna = (float)Math.Cos(azimuth);
            result.M[0, 0] = cna;
            result.M[0, 1] = sna;
            result.M[0, 2] = 0;
            result.M[1, 0] = -sne * sna;
            result.M[1, 1] = sne * cna;
            result.M[1, 2] = cne;
            result.M[2, 0] = cne * sna;
            result.M[2, 1] = -cne * cna;
            result.M[2, 2] = sne;
            return result;
        }
    }
}