using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.Viking
{
	[AutoloadEquip(EquipType.Body)]
	public class VikingLamellar : ModItem
    {
        public override string Texture => Assets.Armor.Viking + "VikingLamellar";
        public override void SetStaticDefaults() => Tooltip.SetDefault("Increases melee damage by 5%");	

		public override void SetDefaults()
		{
            item.value = Item.sellPrice(silver: 2);
            item.rare = ItemRarityID.Blue;
            item.defense = 3;
		}

        public override void UpdateAccessory(Player player, bool hideVisual) => player.meleeDamage += 0.05f;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Leather, 14);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.IceBlock, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}