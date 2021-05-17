using Terraria.Graphics.Shaders;

namespace TrelamiumTwo.Utilities.Extensions
{
	internal static class ScreenShaderDataExtensions
	{
		public static ScreenShaderData ApplyTime(this ScreenShaderData shader, float time)
		{
			shader.Shader.Parameters["uTime"].SetValue(time);

			return shader;
		}

		public static ScreenShaderData ApplyOpacity(this ScreenShaderData shader, float opacity)
		{
			shader.Shader.Parameters["uOpacity"].SetValue(opacity);

			return shader;
		}

		public static ScreenShaderData ApplyIntensity(this ScreenShaderData shader, float intensity)
		{
			shader.Shader.Parameters["uIntensity"].SetValue(intensity);

			return shader;
		}
	}
}
