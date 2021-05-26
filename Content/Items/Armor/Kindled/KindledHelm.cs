using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

namespace TrelamiumTwo.Content.Items.Armor.Kindled
{
	[AutoloadEquip(EquipType.Head)]
	public class KindledHelm : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Kindled Helm");
            Tooltip.SetDefault("Increases minion damage by 3%");
		}

		public override void SetDefaults()
		{
            item.value = Item.buyPrice(silver: 1);
            item.rare = ItemRarityID.White;
            item.defense = 1;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs) 
            => body.type == ModContent.ItemType<KindledChestplate>() && legs.type == ModContent.ItemType<KindledGreaves>();
        public override void UpdateAccessory(Player player, bool hideVisual) 
            => player.minionDamage += 0.03f;
        public override void UpdateArmorSet(Player player)
        {
            ArmorSetPlayer armorSetPlayer = Main.player[Main.myPlayer].GetModPlayer<ArmorSetPlayer>();
            player.setBonus = "Increases the amount of damage 'On Fire!' deals to enemies by 2";
            armorSetPlayer.kindledSet = true;
        }
        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 25);
            recipe.AddIngredient(ItemID.Topaz, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}