using Terraria;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Buffs.Minion
{
	public class FloralSpiritBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Floral Spirit");
			Description.SetDefault("The Floral Spirit will protect you");

			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MinionPlayer minionPlayer = player.GetModPlayer<MinionPlayer>();
			minionPlayer.BloomBulb = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Summon.FloralSpiritGuardian>()] > 0;

			if (!minionPlayer.floralSpirit)
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
