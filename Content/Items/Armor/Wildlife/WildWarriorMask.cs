using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using TrelamiumTwo.Content.Items.Materials;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.WildWarrior
{
	[AutoloadEquip(EquipType.Head)]
	public class WildWarriorMask : ModItem
	{
		public override string Texture => Assets.Armor.Wildlife + "WildWarriorMask";
		public override void SetStaticDefaults() => DisplayName.SetDefault("Wild Warrior Mask");

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 22;

			Item.defense = 1;

			Item.rare = ItemRarityID.White;
			Item.value = Item.sellPrice(silver: 3);
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair) => drawHair = true;

		public override bool IsArmorSet(Item head, Item body, Item legs) => body.type == ModContent.ItemType<WildWarriorGarb>() && legs.type == ModContent.ItemType<WildWarriorGreaves>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+2 defense";
			player.statDefense += 2;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ModContent.ItemType<WildlifeFragment>(), 6).AddTile(TileID.LivingLoom).Register();
		}
	}
}
