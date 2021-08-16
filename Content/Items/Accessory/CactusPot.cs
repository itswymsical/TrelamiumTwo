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
			Item.value = Item.sellPrice(silver: 5);
			Item.rare = ItemRarityID.White;
			Item.width = 16;
			Item.height = 26;
			Item.accessory = true;
		}
        public override void UpdateAccessory(Player player, bool hideVisual) => player.thorns += .05f;
        public override void AddRecipes()
        {
			CreateRecipe(1).AddIngredient(ItemID.Cactus, 15).AddIngredient(ItemID.ClayPot).AddTile(TileID.WorkBenches).Register();
        }
    }
}