using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

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

        public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ItemType<KindledChestplate>() && legs.type == ItemType<KindledGreaves>();
		

        public override void UpdateAccessory(Player player, bool hideVisual) => player.minionDamage += 0.03f;

        public override void UpdateArmorSet(Player player)
        {
            var modplayer = Main.player[Main.myPlayer].GetModPlayer<Common.Players.TrelamiumPlayer>();
            player.setBonus = "Increases the amount of damage 'On Fire!' deals to enemies by 2";
            //modplayer.kindledSetBonus = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 25);
            recipe.AddIngredient(ItemID.Topaz, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}