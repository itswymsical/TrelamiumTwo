using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using TrelamiumTwo.Helpers;

namespace TrelamiumTwo.Core.Mechanics.Trails
{
    public partial class Trail
	{
		protected Vector2 CurveNormal(List<Vector2> points, int index)
		{
			if (points.Count == 1)
            {
				return points[0];
			}

			if (index == 0)
			{
				return Vector2.Normalize(points[1] - points[0]).TurnRight();
			}

			return index == points.Count - 1 ? Vector2.Normalize(points[index] - points[index - 1]) : Vector2.Normalize(points[index + 1] - points[index - 1]).TurnRight();
		}

		protected void AddVertex(Vector2 position, Vector2 texCoords, Color? color = null)
		{
			if (CurrentIndex < Vertices.Length)
			{
				Color vertexColor = (color ?? Color.White) * Alpha;
				var vertex = new VertexPositionColorTexture(position.ToVector3(), vertexColor, texCoords);

				Vertices[CurrentIndex++] = vertex;
			}
		}

		protected void SetBasicShader()
		{
			Effect = Effect ?? Filters.Scene["TM:Primitives"].GetShader().Shader;

            Effect.Parameters["WorldViewProjection"].SetValue(Helpers.Helper.DefaultEffectMatrix);
			Effect.Parameters["uScreenPos"].SetValue(Main.screenPosition);
		}
	}
}
