using Terraria;
using Terraria.ID;

using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Content.Items.BloodlightShaman
{
	public class TheLacerator : TrelamiumItem
	{
		public override void SetDefaults()
		{
			item.rare = ItemRarityID.LightRed;
			item.value = Item.sellPrice(silver: 25);

			item.crit = 2;
			item.damage = 38;
			item.knockBack = 8f;

			item.useTime = item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.noUseGraphic = 
				item.melee = 
				item.noMelee = 
				item.channel = true;

			item.autoReuse = false;

			item.shootSpeed = 6f;
			item.UseSound = SoundID.DD2_MonkStaffSwing;
		}
	}
}
