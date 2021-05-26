using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class Lightbulb : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lightbulb");
			Tooltip.SetDefault("Grants the player a small lightsource");
		}
		public override void SetDefaults()
		{
			item.value = Item.sellPrice(gold: 5);
			item.rare = ItemRarityID.Blue;
			item.width = 24;
			item.height = 30;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			Lighting.AddLight(player.Center, Color.Orange.ToVector3() * Main.essScale);
		}
	}
}