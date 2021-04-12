#region Using directives

using Terraria;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Players;

#endregion

namespace TrelamiumTwo.Content.Buffs.Minion
{
	public sealed class FloralSpiritBuff : ModBuff
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
			TrelamiumPlayer modPlayer = player.GetModPlayer<TrelamiumPlayer>();

			if (player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Summon.FloralSpiritGuardian>()] > 0)
			{
				modPlayer.floralSpirit = true;
			}

			if (!modPlayer.floralSpirit)
			{
				player.DelBuff(buffIndex--);
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}
