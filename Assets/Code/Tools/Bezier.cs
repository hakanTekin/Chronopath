using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
    /// <summary>
    /// <br>Point calculation for 3 point quadratic bezier curve.</br>
    /// </summary>
    /// <param name="t"><br>Bezier function parameter, t.</br><br>Value should be between 0 and 1</br><br>Value is normalized if not in the given range</br></param>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns>The calculated Vector3 point</returns>
    public static Vector3 CalculatePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2) {
        Vector3 res = new Vector3();
        float one = 1f;
        res = (one - t) * (((one - t) * p0) + (t * p1)) + (t * ( ((1-t)*p1) + (t*p2) ));
        return res;
    }

}
