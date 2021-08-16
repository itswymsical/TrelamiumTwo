using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class Lightbulb : ModItem
	{
        public override string Texture => Assets.Items.Accessory + "Lightbulb";
        public override void SetStaticDefaults() => Tooltip.SetDefault("Grants the player a small lightsource");
		
		public override void SetDefaults()
		{
			Item.value = Item.sellPrice(silver: 68);
			Item.rare = ItemRarityID.White;
			Item.width = Item.height = 30;
			Item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual) => Lighting.AddLight(player.Center, Color.Orange.ToVector3() * Main.essScale);
        public override void AddRecipes()
        {
			CreateRecipe(1).AddIngredient(ItemID.Bottle).AddIngredient(ItemID.FallenStar, 2).AddIngredient(ItemID.Chain).AddTile(TileID.Anvils).Register();
        }
    }
}