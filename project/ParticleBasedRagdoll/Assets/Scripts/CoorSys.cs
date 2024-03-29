﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoorSys
{
    protected Vector3 m_T;
    protected Quaternion m_Q;

    public Vector3 T() { return m_T; }
    // TODO: replace Quaternion with Matrix 3x3
    public Quaternion Q() { return m_Q; }

    public CoorSys()
    {
        m_T = new Vector3(0, 0, 0);
        m_Q = new Quaternion(1, 0, 0, 0);
    }

    public CoorSys(Vector3 T_val, Quaternion Q_val)
    {
        m_T = T_val;
        m_Q = Q_val.normalized;
    }

    public static bool operator ==(CoorSys val1, CoorSys val2)
    {
        if (val1.m_T == val2.m_T && val1.m_Q == val2.m_Q)
        {
            return true;
        }
        return false;
    }

    public static bool operator !=(CoorSys val1, CoorSys val2)
    {
        return !(val1 == val2);
    }

    public override bool Equals(object other)
    {
        return this == (CoorSys)other;
    }


    /**
      * This method assumes that the point is in this coordinate system.
      * In other words this method maps local points into non local
      * points:
      *
      * BF -> WCS
      *
      * Let p be the point and let f designate the function of this
      * method then we have
      *
      * [p]_WCS = f(p)
      *
      */
    public Vector3 xform_point(Vector3 p)
    {
        p = m_T + m_Q * p;
        return p;
    }

    /**
* This method assumes that the vector is in this
* coordinate system. That is it maps the vector
* from BF into WCS.
*/
    public void xform_vector(Vector3 v)
    {
        v = m_Q * v;
    }

    /**
      * Transform Matrix.
      *
      * @param O   A reference to a rotation matrix, which should be transformed.
      */
    public void xform_matrix(float[][] r)
    {
        r = Utils.MultiplyMatrix(Utils.QuaternionToMatrix3x3(m_Q), r);
    }



    public CoorSys inverse()
    {
        Quaternion conj_q = new Quaternion(m_Q.x, m_Q.y, m_Q.z, -m_Q.w);
        Vector3 inverse_t = conj_q * -m_T;
        return new CoorSys(inverse_t, conj_q);
    }

    public override int GetHashCode()
    {
        return m_T.GetHashCode() + m_Q.GetHashCode();
    }

    public override string ToString()
    {
        return m_T.ToString() + m_Q.ToString();
    }
}
