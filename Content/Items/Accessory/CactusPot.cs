using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class CactusPot : ModItem
	{
       public override string Texture => Assets.Items.Accessory + "CactusPot";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactus In a Pot");
			Tooltip.SetDefault("Taking damage returns 5% of taken damage");
		}
		public override void SetDefaults()
		{
			item.value = Item.sellPrice(silver: 5);
			item.rare = ItemRarityID.White;
			item.width = 16;
			item.height = 26;
			item.accessory = true;
		}
        public override void UpdateAccessory(Player player, bool hideVisual) => player.thorns += .05f;
        public override void AddRecipes()
        {
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Cactus, 15);
			recipe.AddIngredient(ItemID.ClayPot);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}