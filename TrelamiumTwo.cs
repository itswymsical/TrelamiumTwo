using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;

using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core.Abstraction.Interfaces;
using TrelamiumTwo.Core.Mechanics.Particles;
using TrelamiumTwo.Core.Mechanics.Trails;
using TrelamiumTwo.Core.Mechanics.Verlet;

namespace TrelamiumTwo
{
    public class TrelamiumTwo : Mod
    {
        public const bool DEBUG = true;
        #region Texture Loading Variables
        public const string Abbreviation = "TrelamiumTwo";
        public const string AbbreviationPrefix = Abbreviation + ":";
        internal static string PLACEHOLDER_TEXTURE = "TrelamiumTwo/Assets/PLACEHOLDER_TEXTURE";
        #endregion

        public static ModHotKey shieldHotkey;

        public static TrelamiumTwo Instance => ModContent.GetInstance<TrelamiumTwo>();

        private List<ILoadable> loadCache;

        public override void Load()
        {
            LoadCache();
            shieldHotkey = RegisterHotKey("Shield Ability", "C");

            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> darkScreenRef = new Ref<Effect>(GetEffect("Effects/darkSky"));
                Filters.Scene["TrelamiumTwo:DarkenVisuals"] = new Filter(new ScreenShaderData(darkScreenRef, "SkyTint").UseIntensity(0.8f), EffectPriority.Medium);
            }
        }

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
            foreach (var loadable in loadCache)
            {
                loadable?.Unload();
            }

            loadCache?.Clear();
        }
    }
}