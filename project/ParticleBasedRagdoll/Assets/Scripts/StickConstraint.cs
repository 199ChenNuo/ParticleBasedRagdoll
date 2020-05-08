using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickConstraint : Joint
{
    #region Prop
    public enum SatisfyChoice { HalfHalf = 0, SquareRoot = 1, MassInvolve = 2 };
    public Particle m_A;  // bone A
    public Particle m_B;  // bone B
    public double m_length;      // rest length of stick constraint
    public double m_length_sqr; // squard of rest length
    public SatisfyChoice m_choice; // Choice for satisfy strategy.
    
    #endregion

    #region PropFunc
    public void SetBoneA(Particle b_A) { m_A = b_A; }
    public void SetBoneB(Particle b_B) { m_B = b_B; }
    public void SetRestLength(float len) { m_length = len; }
    #endregion

    public StickConstraint()
    {
        m_A = null;
        m_B = null;
        m_length = 0;
        m_length_sqr = 0;
        m_choice = (SatisfyChoice)2;
    }
    public void init(Particle b_A, Particle b_B, int choice = 2)
    {
        /*
        if (b_A != null)
        {
            SetBoneA(b_A);
        }
        else
        {
            Debug.Log("[Error]  particle A needed for stick constraint.., particle A:", b_A);
            return;
        }

        if (b_B != null)
        {
            SetBoneB(b_B);
        }
        else
        {
            Debug.Log("[Error]  Particle B needed for stick constraint..");
            return;
        }
        */

        SetBoneA(b_A);
        SetBoneB(b_B);
        SetRestLength((m_A.position() - m_B.position()).magnitude);
        m_choice = 0;
    }

    public void Satisfy()
    {
        switch (m_choice)
        {
            case SatisfyChoice.HalfHalf:
                SatisfyType0();
                break;
            case SatisfyChoice.SquareRoot:
                SatisfyType1();
                break;
            default:
                SatisfyType2();
                break;
        }
    }

    void SetRestLength(double l)
    {
        if (l < 0)
        {
            Debug.Log("[ERROR] Set negtive length to StickConstraint.");
            return;
        }
        m_length = l;
        m_length_sqr = l * l;
    }

    /// <summary>
    /// Use delta length to satisfy constraint
    /// </summary>
    public void SatisfyType0()
    {
        Vector3 delta = m_A.m_r - m_B.m_r;
        float delta_length = delta.magnitude;
        float diff = 0.5f * (float)((delta_length - m_length) / delta_length);
        m_A.m_r -= diff * delta;
        m_B.m_r += diff * delta;
    }

    /// <summary>
    /// Use the square of delta length to satisfy constraint
    /// </summary>
    public void SatisfyType1()
    {
        Vector3 delta = m_A.m_r - m_B.m_r;
        float delta_sqr = delta.sqrMagnitude;
        float approx = (float)m_length_sqr / (float)(delta_sqr + m_length_sqr) - 0.5f;
        delta *= approx;
        m_A.m_r += delta;
        m_B.m_r -= delta;
    }

    /// <summary>
    /// Use delta length to satisfy constraint, considering bone mass
    /// </summary>
    public void SatisfyType2()
    {
        Vector3 delta = m_A.m_r - m_B.m_r;
        float delta_length = delta.magnitude;
        float diff = (float)(delta_length - m_length) / (float)(delta_length * (m_A.m_inv_mass + m_B.m_inv_mass));
        m_A.m_r -= (float)(diff * m_A.m_inv_mass) * delta;
        m_B.m_r += (float)(diff * m_B.m_inv_mass) * delta;
    }



    
}
