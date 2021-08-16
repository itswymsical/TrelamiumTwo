using System;
using System.Collections.Generic;
using System.Linq;

using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Core.Abstraction.Interfaces;


namespace TrelamiumTwo
{
    public partial class TrelamiumTwo : Mod
	{
		public const string Abbreviation = "TM";

		public const string AbbreviationPrefix = Abbreviation + ":";

		public const string PlaceholderTexture = "TrelamiumTwo/Assets/PlaceholderTexture";

		public const string InvisibleTexture = "TrelamiumTwo/Assets/InvisibleTexture";

		public static TrelamiumTwo Instance => ModContent.GetInstance<TrelamiumTwo>();

		public override void Load() => LoadCache();

		public override void Unload() => UnloadCache();

		public override void PostSetupContent() => PostLoad();
		private void PostLoad()
		{
			foreach (Type type in Code.GetTypes())
			{
				if (!type.IsAbstract && type.GetInterfaces().Contains(typeof(IPostLoadable)))
				{
					(Activator.CreateInstance(type) as IPostLoadable).Load();
				}
			}
		}

		private void LoadCache()
		{
			loadCache = new List<ILoadable>();

			foreach (Type type in Code.GetTypes())
			{
				if (!type.IsAbstract && type.GetInterfaces().Contains(typeof(ILoadable)))
				{
					loadCache.Add(Activator.CreateInstance(type) as ILoadable);
				}
			}

			loadCache.Sort((x, y) => x.Priority > y.Priority ? 1 : -1);

			for (int i = 0; i < loadCache.Count; ++i)
			{
				if (Main.dedServ && !loadCache[i].LoadOnDedServer)
				{
					continue;
				}

				loadCache[i].Load();
			}
		}

		private void UnloadCache()
		{
			for (int i = 0; i < loadCache.Count; i++)
			{
				loadCache[i].Unload();
			}
			loadCache?.Clear();
		}
	}
}