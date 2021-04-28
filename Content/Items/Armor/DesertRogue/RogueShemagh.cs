#region Using Directives
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
#endregion

namespace TrelamiumTwo.Content.Items.Armor.DesertRogue
{
	[AutoloadEquip(EquipType.Head)]
	public class RogueShemagh : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Desert Raider Shemagh");
            Tooltip.SetDefault("Decreases enemy detection by 5%");
		}

		public override void SetDefaults()
		{
            item.height = 24;
            item.width = 32;
            item.value = Item.sellPrice(silver: 1);
            item.rare = ItemRarityID.White;
            item.defense = 2;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs) 
            => body.type == ModContent.ItemType<RogueTunic>() && legs.type == ModContent.ItemType<RogueBrogues>();

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.stealth += 0.05f;
        }

        public override void UpdateArmorSet(Player player)
        {
            var tp = Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>();
            player.setBonus = "Movement speed is Moderately increased"
            + "\nAttacks ";
            player.moveSpeed += 0.15f;
            tp.desertKB = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Leather, 3);           
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}