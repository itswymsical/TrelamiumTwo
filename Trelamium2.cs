using Terraria;
using Terraria.ModLoader;
using Trelamium2.Core;

namespace Trelamium2
{
    public class Trelamium2 : Mod
    {
        internal static string PLACEHOLDER_TEXTURE = "Trelamium2/Assets/MarioCumming";
            
        internal static Trelamium2 Instance { get; set; }

        public Trelamium2()
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

        public override void Unload()
        {
            if (!Main.dedServ)
                Instance = null;
        }

        public override void PostSetupContent() => WeakReferences.PerformModSupport();

        /* public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                /*if (Main.LocalPlayer.GetModPlayer<TrelamiumModPlayer>().ZoneDruidsGarden)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/DruidsGarden");
                    priority = MusicPriority.BiomeHigh;
                }
            }
        } */
    }
}