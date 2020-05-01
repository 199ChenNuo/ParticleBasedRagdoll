using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 module(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.y * v2.z - v2.y * v1.z, v2.x * v1.z - v1.x * v2.z, v1.x * v2.y - v2.x * v1.y);
    }


    private static float[][] identityMatrix =
    {
     new []{1.0f, 0.0f, 0.0f},
     new []{0.0f, 1.0f, 0.0f},
     new []{0.0f, 0.0f, 1.0f}
    };

    // TODO: or use Math.NET -> Matrix3x3
    public static float[][] QuaternionTo3x3(this Quaternion value)
    {
        float[][] matrix3x3 =
        {
         new float[3],
         new float[3],
         new float[3],
        };

        float[][] symetricalMatrix =
        {
         new float[3] {(-(value.y * value.y) - (value.z * value.z)), value.x * value.y,                                       value.x * value.z},
         new float[3] {value.x * value.y, (-(value.x * value.x) - (value.z * value.z)), value.y * value.z},
         new float[3] {value.x * value.z, value.y * value.z, (-(value.x * value.x) - (value.y * value.y))}
        };

        float[][] antiSymetricalMatrix =
        {
         new[] {0.0f, -value.z, value.y},
         new []{value.z, 0.0f, -value.x},
         new []{-value.y, value.x, 0.0f}
        };

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                matrix3x3[i][j] = identityMatrix[i][j] +
                          (2.0f * symetricalMatrix[i][j]) +
                          (2.0f * value.w * antiSymetricalMatrix[i][j]);
            }
        }

        return matrix3x3;
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

    // TODO: math_matrix3x3.h @ operator=
    public static float[][] QuaternionToMatrix3x3(Quaternion q)
    {
        float[][] matrix =  new float[3][];
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
}
