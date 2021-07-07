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
			item.value = Item.sellPrice(silver: 68);
			item.rare = ItemRarityID.White;
			item.width = item.height = 30;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual) => Lighting.AddLight(player.Center, Color.Orange.ToVector3() * Main.essScale);
        public override void AddRecipes()
        {
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bottle);
			recipe.AddIngredient(ItemID.FallenStar, 2);
			recipe.AddIngredient(ItemID.Chain);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}