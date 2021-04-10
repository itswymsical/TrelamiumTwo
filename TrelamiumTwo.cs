using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo
{
    public class TrelamiumTwo : Mod
    {
        internal static string PLACEHOLDER_TEXTURE = "TrelamiumTwo/Assets/PLACEHOLDER_TEXTURE";
        internal static string Invisible_Texture = "TrelamiumTwo/Assets/InvisibleTexture";
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
            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {
                yabhb.Call("hbStart");

                yabhb.Call("hbSetTexture",
                    GetTexture("UI/OmikBarLeft"),
                    GetTexture("UI/OmikBarMid"),
                    GetTexture("UI/OmikBarRight"),
                    GetTexture("UI/OmikBarFill"));

                yabhb.Call("hbFinishMultiple",
                    NPCType("AzolinthHead"),
                    NPCType("AzolinthBody"),
                    NPCType("AzolinthTail"));
            }
            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> darkScreenRef = new Ref<Effect>(GetEffect("Effects/DarkenVisuals"));
                Filters.Scene["TrelamiumTwo:DarkenVisuals"] = new Filter(new ScreenShaderData(darkScreenRef, "SkyTint").UseIntensity(0.8f), EffectPriority.Medium);
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

            }
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            /*if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                /*if (Main.LocalPlayer.GetModPlayer<TrelamiumModPlayer>().ZoneDruidsGarden)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/DruidsGarden");
                    priority = MusicPriority.BiomeHigh;
                }
            }*/
        }
    }
}