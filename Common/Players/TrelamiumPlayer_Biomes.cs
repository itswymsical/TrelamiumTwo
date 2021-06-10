using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Worlds;
using System.IO;

namespace TrelamiumTwo.Common.Players
{
    public partial class TrelamiumPlayer : ModPlayer
    {
        public bool ZoneDruidsGarden;

        public bool ZoneDustifiedCaverns;
        public override bool CustomBiomesMatch(Player other)
        {
            bool result = true;
            TrelamiumPlayer epOther = other.GetModPlayer<TrelamiumPlayer>();

            result &= ZoneDustifiedCaverns == epOther.ZoneDustifiedCaverns;

            return (result);
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            TrelamiumPlayer modOther = other.GetModPlayer<TrelamiumPlayer>();
            modOther.ZoneDustifiedCaverns = ZoneDustifiedCaverns;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = ZoneDustifiedCaverns;
            writer.Write(flags);
        }
        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            ZoneDustifiedCaverns = flags[0];
        }
        public override void UpdateBiomes() 
        {
            ZoneDruidsGarden = TrelamiumWorld.DruidsGardenTiles > 150;
            ZoneDustifiedCaverns = TrelamiumWorld.DustifiedCavernTiles > 150;
        }

        public override void UpdateBiomeVisuals() 
            => player.ManageSpecialBiomeVisuals("Blizzard", 
            NPC.AnyNPCs(ModContent.NPCType<Content.NPCs.Bosses.Glacier.Glacier>()), player.Center);

    }
}