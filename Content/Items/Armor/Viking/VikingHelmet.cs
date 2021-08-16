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
        public override string Texture => Assets.Armor.Viking + "VikingHelmet";
        public override void SetStaticDefaults() => Tooltip.SetDefault("Increases melee critical strike chance by 5%");

		public override void SetDefaults()
		{
            Item.value = Item.sellPrice(silver: 2);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
		} 

        public override bool IsArmorSet(Item head, Item body, Item legs)
            => body.type == ModContent.ItemType<VikingLamellar>() && legs.type == ModContent.ItemType<VikingBrogues>();

        public override void UpdateAccessory(Player player, bool hideVisual) => player.GetCritChance(DamageClass.Melee) += 5;

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
            CreateRecipe(1).AddIngredient(ItemID.Leather, 6).AddIngredient(ItemID.Chain, 4).AddIngredient(ItemID.IceBlock, 8).AddTile(TileID.Anvils).Register();
        }
    }
}