using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Core;
using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Buffs.Minions
{
	public class Tumbleweed : ModBuff
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = Assets.Buffs.Minions + "Tumbleweed";

			return mod.Properties.Autoload;
		}

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Tumbleweed");
			Description.SetDefault("The Tumbleweed will fight for you");

			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			MinionPlayer minionPlayer = player.GetModPlayer<MinionPlayer>();
			minionPlayer.Tumbleweed = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Summon.Tumbleweed>()] > 0;

			if (!minionPlayer.Tumbleweed)
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