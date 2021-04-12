using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Woodland
{
	[AutoloadEquip(EquipType.Legs)]
	public class WoodlandWarriorGreaves : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Woodland Warrior Greaves");
			Tooltip.SetDefault("Increases summon damage by 2%");
		}

		public override void SetDefaults()
		{
			item.value = Item.sellPrice(silver: 1);
			item.rare = ItemRarityID.White;
			item.defense = 1;
		}
        public override void UpdateEquip(Player player) => player.minionDamage += 0.02f;
        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}