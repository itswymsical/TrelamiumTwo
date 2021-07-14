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
			item.width = 24;
			item.height = 22;

			item.defense = 1;

			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(silver: 3);
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
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<WildlifeFragment>(), 6);
			recipe.AddTile(TileID.LivingLoom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
