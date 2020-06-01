using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 VecMulVec(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }

    public static float VecMulVecF(Vector3 v1, Vector3 v2)
    {
        return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
    }

    public static Vector3 module(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.y * v2.z - v2.y * v1.z, v2.x * v1.z - v1.x * v2.z, v1.x * v2.y - v2.x * v1.y);
    }

    public static Quaternion Matrix3x3ToQuaternion(float[][] matrix)
    {
        float m_s; // real part, the w in Quaternion
        Vector3 m_v;// the (x, y, z) in Quaternion

        float M00 = matrix[0][0];
        float M01 = matrix[0][1];
        float M02 = matrix[0][2];
        float M10 = matrix[1][0];
        float M11 = matrix[1][1];
        float M12 = matrix[1][2];
        float M20 = matrix[2][0];
        float M21 = matrix[2][1];
        float M22 = matrix[2][2];

        float tr = M00 + M11 + M22;
        float r;

        const float half = 0.5f;

        if (tr >= 0)
        {
            r = Mathf.Sqrt(tr + 1.0f);
            m_s = half * r;
            r = half / r;
            m_v.x = (M21 - M12) * r;
            m_v.y = (M02 - M20) * r;
            m_v.z = (M10 - M01) * r;
        }
        else
        {
            int i = 0;
            if (M11 > M00)
            {
                i = 1;
            }
            if (M22 > matrix[i][i])
            {
                i = 2;
            }
            switch (i)
            {
                case 0:
                    {
                        r = Mathf.Sqrt((M00 - (M11 + M22)) + 1);
                        m_v.x = half * r;
                        r = half / r;
                        m_v.y = (M01 + M10) * r;
                        m_v.z = (M20 + M02) * r;
                        m_s = (M21 - M12) * r;
                        break;
                    }
                case 1:
                    {
                        r = Mathf.Sqrt((M11 - (M22 + M00)) + 1);
                        m_v.y = half * r;
                        r = half / r;
                        m_v.z = (M12 + M21) * r;
                        m_v.x = (M01 + M10) * r;
                        m_s = (M02 - M20) * r;
                        break;
                    }
                case 2:
                default:
                    {
                        r = Mathf.Sqrt((M22 - (M00 + M11)) + 1);
                        m_v.z = half * r;
                        r = half / r;
                        m_v.x = (M20 + M02) * r;
                        m_v.y = (M12 + M21) * r;
                        m_s = (M10 - M01) * r;
                        break;
                    }
            }
        }

        return new Quaternion(m_v.x, m_v.y, m_v.z, m_s);
    }

    public static float[][] QuaternionToMatrix3x3(Quaternion q)
    {
        float[][] matrix =  new float[3][];

        matrix[0] = new float[3];
        matrix[1] = new float[3];
        matrix[2] = new float[3];
        matrix[0][0] = 1 - 2 * ((q.y * q.y) + (q.z * q.z));
        matrix[1][1] = 1 - 2 * ((q.x * q.x) + (q.z * q.z));
        matrix[2][2] = 1 - 2 * ((q.y * q.y) + (q.x * q.x));
        matrix[1][0] =      2 * ((q.x * q.y) + (q.w * q.z));
        matrix[0][1] =      2 * ((q.x * q.y) - (q.w * q.z));
        matrix[2][0] =      2 * (-(q.w * q.y) + (q.x * q.z));
        matrix[0][2] =      2 * ((q.w * q.y) + (q.x * q.z));
        matrix[2][1] =      2 * ((q.z * q.y) + (q.w * q.x));
        matrix[1][2] =      2 * ((q.z * q.y) - (q.w * q.x));
        
        return matrix;
    }

    public static float[][] MultiplyMatrix(float[][] a, float[][] b)
    {
        float[][] c = new float[3][];
        for(int i=0; i < 3; ++i)
        {
            c[i] = new float[3];
        }

        for(int col = 0; col < 3; ++col)
        {
            for(int row = 0; row < 3; ++row)
            {
                c[row][col] = 0;
                for (int i = 0; i < 3; ++i)
                {
                    c[row][col] += a[row][i] * b[i][col];
                }
            }
        }

        return c;
    }


    public static float[][] Ru(float radians, Vector3 axis)
    {
        float[][] T = new float[3][];
        T[0] = new float[3];
        T[1] = new float[3];
        T[2] = new float[3];

        float cosinus = Mathf.Cos(radians);
        float sinus = Mathf.Sin(radians);
        Vector3 unit = axis.normalized;

        T[0][0] = unit.x * unit.x + cosinus * (1 - unit.x * unit.x);
        T[1][1] = unit.y * unit.y + cosinus * (1 - unit.y * unit.y);
        T[2][2] = unit.z * unit.z + cosinus * (1 - unit.z * unit.z);

        T[0][1] = unit.x * unit.y * (1 - cosinus) - sinus * unit.z;
        T[0][2] = unit.x * unit.z * (1 - cosinus) + sinus * unit.y;

        T[1][0] = unit.x * unit.y * (1 - cosinus) + sinus * unit.z;
        T[1][2] = unit.y * unit.z * (1 - cosinus) - sinus * unit.x;

        T[2][0] = unit.x * unit.z * (1 - cosinus) - sinus * unit.y;
        T[2][1] = unit.y * unit.z * (1 - cosinus) + sinus * unit.x;

        return T;
    }

    public static Vector3 MatrixMulVector(float[][] matrix, Vector3 v)
    {
        Vector3 vec = new Vector3();
        vec.x = matrix[0][0] * v.x + matrix[0][1] * v.y + matrix[0][2] * v.z;
        vec.y = matrix[1][0] * v.x + matrix[1][1] * v.y + matrix[1][2] * v.z;
        vec.z = matrix[2][0] * v.x + matrix[2][1] * v.y + matrix[2][2] * v.z;

        return vec;

    }

    public static float[][] Vector3s2Matrix(Vector3 x, Vector3 y, Vector3 z)
    {
        float[][] T = new float[3][];
        T[0] = new float[3];
        T[1] = new float[3];
        T[2] = new float[3];

        T[0][0] = x.x;
        T[1][0] = x.y;
        T[2][0] = x.z;
        T[0][1] = y.x;
        T[1][1] = y.y;
        T[2][1] = y.z;
        T[0][2] = z.x;
        T[1][2] = z.y;
        T[2][2] = z.z;


        return T;
    }
}
