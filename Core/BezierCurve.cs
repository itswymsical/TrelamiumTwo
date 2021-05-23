using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TrelamiumTwo.Core
{
    internal class BezierCurve
    {
        public Vector2[] ControlPoints;

        public BezierCurve(params Vector2[] controlPoints) => ControlPoints = controlPoints;

        public Vector2 Evaluate(float t)
        {
            t = t < 0 ? 0 : (t > 1 ? 1 : t);

            return Evaluate(ControlPoints, t);
        }

        private Vector2 Evaluate(Vector2[] controlPoints, float t)
        {
            if (controlPoints.Length <= 2)
                return Vector2.Lerp(controlPoints[0], controlPoints[1], t);

            Vector2[] nextPoints = new Vector2[controlPoints.Length - 1];

            for (int i = 0; i < nextPoints.Length; i++)
                nextPoints[i] = Vector2.Lerp(controlPoints[i], controlPoints[i + 1], t);

            return Evaluate(nextPoints, t); 
        }

        public List<Vector2> GetPoints(int amount)
        {
            float interval = 1f / amount;
            List<Vector2> points = new List<Vector2>();

            for (float i = 0; i <= 1f; i += interval)
                points.Add(Evaluate(i));

            return points;
        }
    }
}
