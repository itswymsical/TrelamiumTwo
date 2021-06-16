using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class AmethystRing : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases magic damage by 5%" +
				"\nWearing with an [i/s1:1282] modifies the [i/s1:379].");
		}
		public override void SetDefaults()
		{
			item.width = item.height = 22;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 2);
			item.accessory = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicDamage += .5f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Amethyst, 6);
			recipe.AddIngredient(ItemID.CopperBar, 3);
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
