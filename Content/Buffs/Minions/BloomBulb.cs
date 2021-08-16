using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Core;

using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Buffs.Minions
{
	public class BloomBulb : ModBuff
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = Assets.Buffs.Minions + "BloomBulb";

			return Mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
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