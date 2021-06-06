using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Common.Players;

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
            ArmorSetPlayer armorSetPlayer = player.GetModPlayer<ArmorSetPlayer>();
            player.setBonus = "[To be conceptualized]";
            armorSetPlayer.desertRogueSet = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Materials.AridFiber>(), 8);
            recipe.AddIngredient(ItemID.Chain, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}