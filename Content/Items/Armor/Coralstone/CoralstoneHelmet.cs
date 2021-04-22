using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Coralstone
{
	[AutoloadEquip(EquipType.Head)]
	public class CoralstoneHelmet : TrelamiumItem
    {
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Coralstone Helmet");
            Tooltip.SetDefault("Decreases movement speed by 3%");
		}

		public override void SetDefaults()
		{
            item.value = Item.sellPrice(silver: 1);
            item.rare = ItemRarityID.Blue;
            item.defense = 3;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<CoralstoneChestplate>() && legs.type == ModContent.ItemType<CoralstoneGreaves>();
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed -= 0.03f;
        }

        public override void UpdateArmorSet(Player player)
        {
            var modplayer = Main.player[Main.myPlayer].GetModPlayer<Common.Players.TrelamiumPlayer>();
            player.setBonus = "Taking damage summons barnacles around the player";
            //modplayer.coralstoneSetBonus = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.Coralstone>(), 10);
            recipe.AddIngredient(ItemID.Seashell, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}