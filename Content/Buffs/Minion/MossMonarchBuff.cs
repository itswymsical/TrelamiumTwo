using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Buffs.Minion
{
    public class MossMonarchBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Moss Monarch");
            Description.SetDefault("The Moss Monarch, a hornet with a Vortex Beater!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MinionPlayer minionPlayer = player.GetModPlayer<MinionPlayer>();
            minionPlayer.mossMonarch = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Summon.MossMonarchProjectile>()] > 0;

            if (!minionPlayer.mossMonarch)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}