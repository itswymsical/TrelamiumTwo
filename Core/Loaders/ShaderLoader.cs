using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using TrelamiumTwo.Common.Configuration;
using TrelamiumTwo.Core.Abstraction.Interfaces;

namespace TrelamiumTwo.Core.Loaders
{
    public sealed class ShaderLoader : ILoadable
	{
		public float Priority => 1f;

		public bool LoadOnDedServer => false;

		public void Load()
		{
			MethodInfo info = typeof(Mod).GetProperty("File", BindingFlags.NonPublic | BindingFlags.Instance).GetGetMethod(true);
			var file = (TmodFile)info.Invoke(TrelamiumTwo.Instance, null);

			var shaders = file.Where(x => x.Name.Contains("Effects/") && x.Name.EndsWith(".xnb"));

			if (shaders.Any())
            {
				foreach (var entry in shaders)
				{
					var shaderPath = entry.Name.Replace(".xnb", string.Empty);
					var shaderName = Path.GetFileName(shaderPath);

					LoadShader(shaderName, shaderPath);
				}
			}
            else
            {
				if (TrelamiumClientConfig.Instance.Debug)
                {
					TrelamiumTwo.Instance.Logger.Debug("No shader was found!");
                }
            }
		}

		public void Unload() { }

		internal static void LoadShader(string shaderName, string shaderPath)
		{
			var shaderRef = new Ref<Effect>(TrelamiumTwo.Instance.Assets.Request<Effect>(shaderPath).Value);

			if (TrelamiumClientConfig.Instance.Debug)
            {
				TrelamiumTwo.Instance.Logger.Debug($"Loading shader: <{shaderName}> @ <{shaderPath}>");
            }

			(Filters.Scene[TrelamiumTwo.AbbreviationPrefix + shaderName] = new Filter(new ScreenShaderData(shaderRef, shaderName + "Pass"), EffectPriority.High)).Load();
		}
	}
}
