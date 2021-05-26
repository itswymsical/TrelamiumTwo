using Terraria;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Buffs.Potions
{
	public sealed class GleamingNectar : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Gleaming Nectar");
			Description.SetDefault("Grants rapid life and mana regeneration");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			Main.LocalPlayer.GetModPlayer<Common.Players.BuffPlayer>().gleamingNectar = true;
		}
	}
}
