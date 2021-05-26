using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Buffs.Potions
{
	public sealed class SolarAuraPotion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Solar Aura");
			Description.SetDefault("Increases the damage of fire related buffs and increases defense while in lava" +
				"\nOn Fire!: 2 damage" +
				"\nCursed Inferno: 3 damage" +
				"\nShadowflame: 6 damage");
			
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			Main.LocalPlayer.GetModPlayer<Common.Players.BuffPlayer>().solarAura = true;
		}
	}
}
