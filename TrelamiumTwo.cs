using System;
using System.Collections.Generic;
using System.Linq;

using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using TrelamiumTwo.Common.Cutscenes;
using TrelamiumTwo.Core.Abstraction.Interfaces;
using TrelamiumTwo.Core.Loaders;
using TrelamiumTwo.Core.Mechanics.Particles;
using TrelamiumTwo.Core.Mechanics.Trails;
using TrelamiumTwo.Core.Mechanics.Verlet;

namespace TrelamiumTwo
{
    public partial class TrelamiumTwo : Mod
	{
		public const string Abbreviation = "TM";

		public const string AbbreviationPrefix = Abbreviation + ":";

		public const string PlaceholderTexture = "TrelamiumTwo/Assets/PlaceholderTexture";

		public const string InvisibleTexture = "TrelamiumTwo/Assets/InvisibleTexture";

		public static TrelamiumTwo Instance => ModContent.GetInstance<TrelamiumTwo>();

		private List<ILoadable> loadCache;

		public override void Load() => LoadCache();

		public override void Unload() => UnloadCache();

		public override void PostSetupContent() => PostLoad();

        public override void PostUpdateEverything()
        {
            if (!Main.dedServ)
            {
				ParticleManager.Instance.UpdateParticles();
				TrailManager.Instance.UpdateTrails();
				VerletManager.Instance.UpdateChains();
            }
        }
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			for (int i = 0; i < CutsceneLoader.Cutscenes.Count; i++)
			{
				var cutscene = CutsceneLoader.Cutscenes[i];
				CutsceneLoader.AddCutsceneLayer(layers, cutscene, cutscene.InsertionIndex(layers), cutscene.Visible);
			}

			if (CutsceneLoader.GetCutscene<Credits>().Visible)
				foreach (var layer in layers.Where(l => !l.Name.Equals("TM:Credits")))
					layer.Active = false;
		}
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