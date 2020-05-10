using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Notice: this collision detection is inspired by OpenTissue collision_box_box_improved.
/**
* Improved Box Box Collision Test.
*
* @param p_a       Center of box A in WCS.
* @param p_b       Center of box B in WCS
* @param R_a       Box A's orientation in WCS
* @param R_b       Box B's orientation in WCS
* @param ext_a     Extents of box A, i.e. half edge sizes.
* @param ext_b     Extents of box B, i.e. half edge sizes.
* @param envelope  The size of the collision envelope. If cloest point are separted by more than this distance then there is no contact.
* @param p         Pointer to array of contact points, must have room for at least eight vectors.
* @param n         Upon return this argument holds the contact normal pointing from box A towards box B.
* @param distance  Pointer to array of separation (or penetration) distances. Must have room for at least eight values.
*
* @return          If contacts exist then the return value indicates the number of contacts, if no contacts exist the return valeu is zero.
*/
public class CollisionDetection : MonoBehaviour
{
    public int get_box_box_collision(Vector3 p_a, float[][] R_a, Vector3 ext_a,
        Vector3 p_b, float[][] R_b, Vector3 ext_b, float envelope, Vector3 p, Vector3 n, float distance)
    {
        // sign lookup table
        Vector3[] sign = new Vector3[8];
        for (uint mask = 0; mask < 8; ++mask)
        {
            sign[mask].x = (mask & 0x0001)!=0 ? 1 : -1;
            sign[mask].y = ((mask >> 1) & 0x0001) != 0 ? 1 : -1;
            sign[mask].z = ((mask >> 2) & 0x0001) != 0 ? 1 : -1;
        }

        // extract axis of boxes
        Vector3[] A = new Vector3[3];
        return 1;
    }
}
