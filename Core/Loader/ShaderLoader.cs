using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using TrelamiumTwo.Core.Abstracts.Interface;

namespace TrelamiumTwo.Core.Loaders
{
	internal sealed class ShaderLoader : ILoadable
	{
		public float Priority => 1f;

		public bool LoadOnDedServer => false;

		public void Load()
		{
			MethodInfo info = typeof(Mod).GetProperty("File", BindingFlags.NonPublic | BindingFlags.Instance).GetGetMethod(true);
			var file = (TmodFile)info.Invoke(TrelamiumTwo.Instance, null);

			var shaders = file.Where(x => x.Name.StartsWith("Effects/") && x.Name.EndsWith(".xnb"));

			foreach (var entry in shaders)
			{
				var shaderPath = entry.Name.Replace(".xnb", string.Empty);
				var shaderName = Path.GetFileName(shaderPath);

				LoadShader(TrelamiumTwo.AbbreviationPrefix + shaderName, shaderPath);
			}
		}

		public void Unload() { }

		internal static void LoadShader(string shaderName, string shaderPath)
		{
			var shaderRef = new Ref<Effect>(TrelamiumTwo.Instance.GetEffect(shaderPath));

			if (ModContent.GetInstance<TrelamiumConfig>().Debug)
				TrelamiumTwo.Instance.Logger.Debug($"Loading shader: <{shaderName}> @ <{shaderPath}>");

			(Filters.Scene[shaderName] = new Filter(new ScreenShaderData(shaderRef, shaderName + "Pass"), EffectPriority.High)).Load();
		}
	}
}
