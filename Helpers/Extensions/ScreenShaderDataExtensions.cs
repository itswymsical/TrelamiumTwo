using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria.Graphics.Shaders;

namespace TrelamiumTwo.Helpers.Extensions
{
	internal static class ShaderExtensions
	{
		public static ScreenShaderData ApplyTime(this ScreenShaderData shader, float time)
		{
			if (shader.Shader.HasParameter("uTime"))
			{
				shader.Shader.Parameters["uTime"].SetValue(time);
			}

			return shader;
		}
		public static ScreenShaderData ApplyOpacity(this ScreenShaderData shader, float opacity)
		{
			if (shader.Shader.HasParameter("uOpacity"))
			{
				shader.Shader.Parameters["uOpacity"].SetValue(opacity);
			}

			return shader;
		}
		public static ScreenShaderData ApplyIntensity(this ScreenShaderData shader, float intensity)
		{
			if (shader.Shader.HasParameter("uIntensity"))
			{
				shader.Shader.Parameters["uIntensity"].SetValue(intensity);
			}

			return shader;
		}
		public static ScreenShaderData ApplyColor(this ScreenShaderData shader, Vector3 color)
		{
			if (shader.Shader.HasParameter("uColor"))
			{
				shader.Shader.Parameters["uColor"].SetValue(color);
			}

			return shader;
		}
		public static bool HasParameter(this Effect effect, string name)
		{
			foreach (EffectParameter parameter in effect.Parameters)
			{
				if (parameter.Name == name)
				{
					return true;
				}
			}

			return false;
		}
	}
}
