#region Using directives

using Terraria;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Buffs.Potions
{
	public sealed class BarkskinPotion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Barkskin");
			Description.SetDefault("Increases defense, but lowers mobility");
			
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.jumpSpeedBoost *= 0.75f;
			player.statDefense = (int)(player.statDefense * 1.15f);
		}
	}
}
