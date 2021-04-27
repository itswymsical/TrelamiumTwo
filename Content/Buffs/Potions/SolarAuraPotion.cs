#region Using directives

using Terraria;
using Terraria.ModLoader;

#endregion

namespace TrelamiumTwo.Content.Buffs.Potions
{
	public sealed class SolarAuraPotion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Solar Aura");
			Description.SetDefault("Generates a radiance of solar power");
			
			Main.debuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			// TODO: Eldrazi - Implement buff logic.
		}
	}
}
