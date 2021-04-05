using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Trelamium2
{
    public class Trelamium2 : Mod
    {

        public Trelamium2()
        {
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

            if (!Main.dedServ)
            {

            }
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