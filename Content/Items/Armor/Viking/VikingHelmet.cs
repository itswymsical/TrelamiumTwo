using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.Viking
{
	[AutoloadEquip(EquipType.Head)]
	public class VikingHelmet : ModItem
    {
        public override string Texture => Assets.Armors.Viking + "VikingHelmet";
        public override void SetStaticDefaults() => Tooltip.SetDefault("Increases melee critical strike chance by 5%");

		public override void SetDefaults()
		{
            item.value = Item.sellPrice(silver: 2);
            item.rare = ItemRarityID.Blue;
            item.defense = 2;
		} 

        public override bool IsArmorSet(Item head, Item body, Item legs)
            => body.type == ModContent.ItemType<VikingLamellar>() && legs.type == ModContent.ItemType<VikingBrogues>();

        public override void UpdateAccessory(Player player, bool hideVisual) => player.meleeCrit += 5;

        public override void UpdateArmorSet(Player player)
        {
            ArmorSetPlayer armorSetPlayer = player.GetModPlayer<ArmorSetPlayer>();
            player.setBonus = "Melee weapons are imbued with 'Frostburn'" +
                "\nWhile in the tundra you have more resistance to cold damage";
            armorSetPlayer.vikingSet = true;
            player.resistCold = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Leather, 6);
            recipe.AddIngredient(ItemID.Chain, 4);
            recipe.AddIngredient(ItemID.IceBlock, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}