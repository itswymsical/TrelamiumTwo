using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Buffs
{
	public class SlitheringGrace : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Slitherin Grace");
			Description.SetDefault("Increase movement speed by 15%\nIncrease damage by 3%\nIncrease crit chance by 5%");
		}
		public override void Update(Player player, ref int buffIndex)
		{
			player.moveSpeed += 0.15f;
			player.maxRunSpeed *= 1.15f;
			player.runAcceleration += 0.15f;

			player.allDamageMult += 0.03f;

			player.magicCrit += 5;
			player.meleeCrit += 5;
			player.rangedCrit += 5;
			player.thrownCrit += 5;
		}
	}
}
