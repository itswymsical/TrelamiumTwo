using TrelamiumTwo.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Buffs.Minion
{
	public class BloomBulbBuff : ModBuff
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			return mod.Properties.Autoload;
		}

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Bloom Bulb");
			Description.SetDefault("The Bloom Bulb will fight for you");

			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MinionPlayer minionPlayer = player.GetModPlayer<MinionPlayer>();
			minionPlayer.BloomBulb = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Summon.BloomBulb>()] > 0;

			if (!minionPlayer.BloomBulb)
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