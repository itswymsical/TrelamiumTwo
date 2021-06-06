#region Using Directives
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
#endregion

namespace TrelamiumTwo.Content.Items.Armor.DesertRogue
{
	[AutoloadEquip(EquipType.Legs)]
	public class RogueBrogues : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Rogue Sandals");
			Tooltip.SetDefault("Grants the ability to move faster during sandstorms" +
				"\nIncreases movement speed by 5%");
		}

		public override void SetDefaults()
		{
			item.value = Item.sellPrice(silver: 1);
			item.rare = ItemRarityID.White;
			item.defense = 2;
		}

		public override void UpdateEquip(Player player)
		{
			if (player.HasBuff(BuffID.WindPushed))
			{
				player.runAcceleration += .6f;
				player.runSlowdown -= .5f;
			}
			player.moveSpeed += .05f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.AridFiber>(), 6);
			recipe.AddIngredient(ItemID.Chain, 3);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}