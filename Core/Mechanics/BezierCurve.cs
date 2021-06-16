using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TrelamiumTwo.Core.Mechanics
{
    public class BezierCurve
	{
		public List<Vector2> ControlPoints;

		public BezierCurve(List<Vector2> controlPoints) => ControlPoints = controlPoints;

		public BezierCurve(params Vector2[] controlPoints)
		{
			List<Vector2> points = new List<Vector2>();

			for (int i = 0; i < controlPoints.Length; ++i)
			{
				points.Add(controlPoints[i]);
			}

			ControlPoints = points;
		}

		private Vector2 Evaluate(List<Vector2> points, float t)
		{
			if (points.Count <= 2)
			{
				return Vector2.Lerp(points[0], points[1], t);
			}

			List<Vector2> nextPoints = new List<Vector2>();

			for (int i = 0; i < points.Count - 1; i++)
			{
				nextPoints.Add(Vector2.Lerp(points[i], points[i + 1], t));
			}

			return Evaluate(nextPoints, t);			
		}

		public Vector2 GetPoint(float t)
		{
			t = MathHelper.Clamp(t, 0f, 1f);

			return Evaluate(ControlPoints, t);
		}

		public List<Vector2> GetPoints(int amount)
		{
			float step = 1f / amount;

			List<Vector2> points = new List<Vector2>();

			for (float i = 0f; i <= 1f; i += step)
			{
				Vector2 point = GetPoint(i);
				points.Add(point);
			}

			return points;
		}
	}
}
