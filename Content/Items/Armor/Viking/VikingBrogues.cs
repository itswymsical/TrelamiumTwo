using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.Viking
{
	[AutoloadEquip(EquipType.Legs)]
	public class VikingBrogues : ModItem
    {
        public override string Texture => Assets.Items.VikingArmor + "VikingBrogues";
        public override void SetStaticDefaults() => Tooltip.SetDefault("Increases melee speed by 4%");	

		public override void SetDefaults()
		{
            item.value = Item.sellPrice(silver: 2);
            item.rare = ItemRarityID.Blue;
            item.defense = 2;
		}

        public override void UpdateAccessory(Player player, bool hideVisual) => player.meleeSpeed += 0.04f;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Leather, 8);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddIngredient(ItemID.IceBlock, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}