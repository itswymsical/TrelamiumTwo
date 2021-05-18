using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Content.UI;
using Terraria.UI;
using Microsoft.Xna.Framework;
using TrelamiumTwo.Core.Abstracts.Interface;
using TrelamiumTwo.Core.Loaders;
using TrelamiumTwo.Content.UI.ArchaeologistUI;
using System.Threading;
using Microsoft.Xna.Framework.Audio;
using MonoMod.Cil;

namespace TrelamiumTwo
{
    public class TrelamiumTwo : Mod
    {
        public const bool DEBUG = true;
        private List<ILoadable> loadCache;
        public static int GeneralTimer;
        private bool stopTitleMusic = false;
        private ManualResetEvent titleMusicStopped = new ManualResetEvent(false);
        private int customTitleMusicSlot;

        #region Texture Loading Variables
        public const string Abbreviation = "TrelamiumTwo";
        public const string AbbreviationPrefix = Abbreviation + ":";

        public const string ProjectileAssets = "TrelamiumTwo/Assets/Projectiles/";
        public const string HeaviesAssets = "TrelamiumTwo/Assets/Heavies/";
        public const string UIAssets = "TrelamiumTwo/Assets/UI";

        internal static string PLACEHOLDER_TEXTURE = "TrelamiumTwo/Assets/PLACEHOLDER_TEXTURE";
        internal static string PSLOGO = "TrelamiumTwo/Assets/PSLogo";
        #endregion

        public static ModHotKey shieldHotkey;
        internal MovementTrackerUI MovementTracker;
        private UserInterface movementTrackerUI;
        internal ArchaeologistsWorkshopUI ArcheologistsWorkshop;
        private UserInterface archaeologistsWorkshopUI;
        internal static TrelamiumTwo Instance { get; set; }
        public TrelamiumTwo()
        {
            Instance = this;

            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadSounds = true,
                AutoloadGores = true,
                AutoloadBackgrounds = true
            };
        }

        public override void Load()
        {
            LoadCache();
            shieldHotkey = RegisterHotKey("Shield Ability", "C");

            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> darkScreenRef = new Ref<Effect>(GetEffect("Effects/darkSky"));
                Filters.Scene["TrelamiumTwo:DarkenVisuals"] = new Filter(new ScreenShaderData(darkScreenRef, "SkyTint").UseIntensity(0.8f), EffectPriority.Medium);

                MovementTracker = new MovementTrackerUI();
                movementTrackerUI = new UserInterface();
                movementTrackerUI.SetState(MovementTracker);

                ArcheologistsWorkshop = new ArchaeologistsWorkshopUI();
                archaeologistsWorkshopUI = new UserInterface();
                archaeologistsWorkshopUI.SetState(ArcheologistsWorkshop);
            }
        }

        public override void Unload()
        {
            UnloadCache();
            if (!Main.dedServ)
                Instance = null;
        }
        private void LoadCache()
        {
            loadCache = new List<ILoadable>();

            foreach (Type type in Code.GetTypes())
                if (!type.IsAbstract && type.GetInterfaces().Contains(typeof(ILoadable)))
                    loadCache.Add(Activator.CreateInstance(type) as ILoadable);

            loadCache.Sort((x, y) => x.Priority.CompareTo(y.Priority));

            for (int i = 0; i < loadCache.Count; i++)
            {
                if (Main.dedServ && !loadCache[i].LoadOnDedServer)
                    continue;

                loadCache[i].Load();
            }
        }

        private void UnloadCache()
        {
            foreach (var loadable in loadCache)
                loadable.Unload();

            loadCache = null;
        }
        public static void Update() => GeneralTimer++;
        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "Fungore", 1f, (Func<bool>)(() => Common.Worlds.TrelamiumWorld.downedFungore), "Use a [i:" + ModContent.ItemType<Content.Items.Fungore.Fungocybin>() + "] Anywhere during the day.");
                bossChecklist.Call("AddBossWithInfo", "Glacier", 10f, (Func<bool>)(() => Common.Worlds.TrelamiumWorld.downedGlacier), "Use a [i:" + ModContent.ItemType<Content.Items.Fungore.Fungocybin>() + "] in the Tundra during night.");
            }
            customTitleMusicSlot = GetSoundSlot(SoundType.Music, "Sounds/Music/IlluminantInkiness");
            IL.Terraria.Main.UpdateAudio += il => {
                var c = new ILCursor(il);
                c.GotoNext(MoveType.After, i => i.MatchLdfld<Main>("newMusic"));
                c.EmitDelegate<Func<int, int>>(newMusic => newMusic == MusicID.Title ? customTitleMusicSlot : newMusic);
            };
        }
        public override void Close()
        {
            // Close isn't called on the main thread. Who doesn't love a bit of thread safety
            // Close may be called even if we didn't reach PostSetupContent, so don't try and stop a music track which hasn't been loaded or played
            if (customTitleMusicSlot > 0)
            {
                stopTitleMusic = true;
                titleMusicStopped.WaitOne();
            }
            base.Close();
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (stopTitleMusic)
            {
                // prevent our IL hook trying to play the track anymore
                // we could just remove our IL hook, but then we'd have to save it in a variable. tML removes it for us anyway
                customTitleMusicSlot = MusicID.Title;

                // stop the music if it's playing (which it probably is)
                var m = GetMusic("Sounds/Music/IlluminantInkiness");
                if (m.IsPlaying)
                    m.Stop(AudioStopOptions.Immediate);

                titleMusicStopped.Set();
                stopTitleMusic = false;
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            int DeathTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Death Text"));

            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer("TrelamiumTwo: Movement Speed Bar", delegate
                {
                    movementTrackerUI.Draw(Main.spriteBatch, new GameTime());
                    return true;
                },
                InterfaceScaleType.UI));
            }
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer("TrelamiumTwo: Archaeologist's Workshop", delegate
                {
                    movementTrackerUI.Draw(Main.spriteBatch, new GameTime());
                    return true;
                },
                InterfaceScaleType.UI));
            }
            for (int i = 0; i < UILoader.UIStates.Count; i++)
            {
                var state = UILoader.UIStates[i];
                UILoader.AddLayer(layers, UILoader.UserInterfaces[i], state, state.InsertionIndex(layers), state.Visible, state.Scale);
            }
        }
        /*public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                if (CutsceneLoader.GetCutscene<Credits>().Visible)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/IlluminantInkiness");
                    priority = MusicPriority.BossHigh;
                }
            }
        }*/
    }
}