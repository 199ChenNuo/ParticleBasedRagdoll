using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        float[][] a = new float[3][];
        for (int i = 0; i < 3; ++i)
        {
            a[i] = new float[3];
            for(int j=0; j < 3; ++j)
            {
                a[i][j] = i * 10 + j;
            }
        }
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                Debug.Log(a[i][j]);
            }
        }
        */
        float[,] b = new float[3,3];
        for (int i = 0; i < 3; ++i)
        {
            for(int j = 0; j < 3; ++j)
            {
                b[i,j] = i * 10 + j;
            }
        }
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                Debug.Log(b[i, j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
