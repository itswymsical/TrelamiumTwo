using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.Viking
{
	[AutoloadEquip(EquipType.Legs)]
	public class VikingBrogues : ModItem
    {
        public override string Texture => Assets.Armor.Viking + "VikingBrogues";
        public override void SetStaticDefaults() => Tooltip.SetDefault("Increases melee speed by 4%");	

		public override void SetDefaults()
		{
            Item.value = Item.sellPrice(silver: 2);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
		}

        public override void UpdateAccessory(Player player, bool hideVisual) => player.meleeSpeed += 0.04f;

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Leather, 8).AddIngredient(ItemID.Chain, 2).AddIngredient(ItemID.IceBlock, 5).AddTile(TileID.Anvils).Register();
        }
    }
}