using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Armor.Woodland
{
	[AutoloadEquip(EquipType.Head)]
	public sealed class WoodlandWarriorHelmet : TrelamiumItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Woodland Warrior Helmet");
			Tooltip.SetDefault("Increases summon damage by 5%");
		}

		public override void SetDefaults()
		{
			item.value = Item.sellPrice(silver: 1);
			item.rare = ItemRarityID.White;
			item.defense = 1;
		}
        public override bool IsArmorSet(Item head, Item body, Item legs) 
			=> body.type == ModContent.ItemType<WoodlandWarriorGarb>() && legs.type == ModContent.ItemType<WoodlandWarriorGreaves>();
        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Summons a Floral Spirit to protect you";
			Main.LocalPlayer.GetModPlayer<Common.Players.TrelamiumPlayer>().floralSpirit = true;
        }
        public override void UpdateEquip(Player player) => player.minionDamage += 0.05f;
        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}