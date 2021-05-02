#region Using Directives
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Core.Abstracts;
using TrelamiumTwo.Content.UI;
using Terraria.UI;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using TrelamiumTwo.Core.Loaders;
using TrelamiumTwo.Common.Cutscenes;
using TrelamiumTwo.Common.Players;
#endregion

namespace TrelamiumTwo
{
    public class TrelamiumTwo : Mod
    {
        public const bool DEBUG = true;
        private List<ILoadable> _loadCache;

        #region Texture Loading Variables
        public const string Abbreviation = "TrelamiumTwo";
        public const string AbbreviationPrefix = Abbreviation + ":";
        public const string DustiliteAssets = "TrelamiumTwo/Assets/Dustilite/";
        public const string ProjectileAssets = "TrelamiumTwo/Assets/Projectiles/";
        public const string UIAssets = "TrelamiumTwo/Assets/UI";

        public const string YABHB_AzolinthAssets = "TrelamiumTwo/Assets/YABHB/AzolinthBar";

        internal static string PLACEHOLDER_TEXTURE = "TrelamiumTwo/Assets/PLACEHOLDER_TEXTURE";
        internal static string Invisible_Texture = "TrelamiumTwo/Assets/InvisibleTexture";
        #endregion

        public static ModHotKey shieldHotkey;
        internal MovementTrackerUI MovementTracker;
        private UserInterface movementTrackerUI;
        public static SpriteFont exampleFont;
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
            #region List<T> & loadCache Stuff
            _loadCache = new List<ILoadable>();

            foreach (Type type in Code.GetTypes())
                if (!type.IsAbstract && type.GetInterfaces().Contains(typeof(ILoadable)))
                    _loadCache.Add(Activator.CreateInstance(type) as ILoadable);

            _loadCache.Sort((x, y) => x.Priority.CompareTo(y.Priority));

            for (int i = 0; i < _loadCache.Count; i++)
            {
                if (Main.dedServ && !_loadCache[i].LoadOnDedServer)
                    continue;

                _loadCache[i].Load();
            }
            #endregion

            shieldHotkey = RegisterHotKey("Shield Ability", "C");
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {
                yabhb.Call("hbStart");

                yabhb.Call("hbSetTexture",
                    GetTexture("TrelamiumTwo/Assets/YABHB/AzolinthBarLeft"),
                    GetTexture("TrelamiumTwo/Assets/YABHB/AzolinthBarMid"),
                    GetTexture("TrelamiumTwo/Assets/YABHB/AzolinthBarRight"),
                    GetTexture("TrelamiumTwo/Assets/YABHB/AzolinthBarFill"));

                yabhb.Call("hbFinishMultiple",
                    NPCType("AzolinthHead"),
                    NPCType("AzolinthBody"),
                    NPCType("AzolinthTail"));
            }
            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> darkScreenRef = new Ref<Effect>(GetEffect("Effects/darkSky"));
                Filters.Scene["TrelamiumTwo:DarkenVisuals"] = new Filter(new ScreenShaderData(darkScreenRef, "SkyTint").UseIntensity(0.8f), EffectPriority.Medium);

                MovementTracker = new MovementTrackerUI();
                movementTrackerUI = new UserInterface();
                movementTrackerUI.SetState(MovementTracker);
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
                Instance = null;
        }

        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {

                bossChecklist.Call("AddBossWithInfo", "Fungore", 1f, (Func<bool>)(() => Common.Worlds.TrelamiumWorld.downedFungore), "Use a [i:" + ModContent.ItemType<Content.Items.Fungore.Fungocybin>() + "] Anywhere during the day.");
            }
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            int DeathTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Death Text"));

            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer("PrimordialSands: Absorption Bar", delegate
                {
                    movementTrackerUI.Draw(Main.spriteBatch, new GameTime());
                    return true;
                },
                InterfaceScaleType.UI));
            }
            for (int i = 0; i < CutsceneLoader.Cutscenes.Count; i++)
            {
                var cutscene = CutsceneLoader.Cutscenes[i];
                CutsceneLoader.AddCutsceneLayer(layers, cutscene, cutscene.InsertionIndex(layers), cutscene.Visible);
            }

            if (CutsceneLoader.GetCutscene<Credits>().Visible)
                foreach (var layer in layers.Where(l => !l.Name.Equals("TrelamiumTwo:Credits")))
                    layer.Active = false;
            #region Text Drawing
            /*if (DeathTextIndex != -1)
            {
                layers.Insert(DeathTextIndex, new LegacyGameInterfaceLayer("PrimordialSands: Boss Text", delegate
                {
                    string header = "-- Fungore --";
                    Main.spriteBatch.DrawString(Main.fontDeathText, header, new Vector2((float)(Main.screenWidth / 2 + Main.rand.NextFloat(0f, 2f)) - Main.fontDeathText.MeasureString(header).X / 2f, (float)(Main.screenHeight / 10f + Main.rand.NextFloat(0f, 2f))), new Color(Main.rand.Next(100, 255), Main.rand.Next(100, 255), Main.rand.Next(100, 255), 255), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    string subheader = "-- The mutated fungus symbiote --";
                    Main.spriteBatch.DrawString(Main.fontMouseText, subheader, new Vector2((float)(Main.screenWidth / 2 + Main.rand.NextFloat(0f, 2f)) - Main.fontMouseText.MeasureString(subheader).X / 2f, (float)(Main.screenHeight / 10f + Main.rand.NextFloat(58f, 60f))), new Color(Main.rand.Next(100, 255), Main.rand.Next(100, 255), Main.rand.Next(100, 255), 255), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    return true;
                },
                    InterfaceScaleType.UI));
            }*/
            #endregion
        }
        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                if (CutsceneLoader.GetCutscene<Credits>().Visible)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/IlluminantInkiness");
                    priority = MusicPriority.BossHigh;
                }
            }
        }
    }
}