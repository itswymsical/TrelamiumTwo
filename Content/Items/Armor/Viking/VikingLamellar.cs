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
            Item.value = Item.sellPrice(silver: 2);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 3;
		}

        public override void UpdateAccessory(Player player, bool hideVisual) => player.GetDamage(DamageClass.Melee) += 0.05f;

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.Leather, 14).AddIngredient(ItemID.Chain, 2).AddIngredient(ItemID.IceBlock, 8).AddTile(TileID.Anvils).Register();
        }
    }
}