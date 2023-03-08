using UnityEngine;

namespace Bezier
{
    public class BezierCalculator
    {
        /// <summary>
        /// p0 - start point, p1 - inflection point, p2 - end point, t - percent value between them
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 GetPointQuadratic(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            Vector3 point = oneMinusT * oneMinusT * p0 +
                2f * oneMinusT * t * p1 +
                t * t * p2;
            return point;


        }
    }
}