﻿using TrelamiumTwo.Common.Players;
using TrelamiumTwo.Content.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Armor.Everbloom
{
	[AutoloadEquip(EquipType.Head)]
	public class EverbloomCrown : ModItem
	{
		public override string Texture => Assets.Items.Everbloom + "EverbloomCrown";
		public override void SetDefaults()
		{
			item.defense = 2;

			item.width = 30;
			item.height = 22;

			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 1, copper: 20);
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair) => drawHair = true;

		public override bool IsArmorSet(Item head, Item body, Item legs) 
			=> body.type == ModContent.ItemType<EverbloomTunic>() && legs.type == ModContent.ItemType<EverbloomLeggings>();

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increases max minions by 1";
			player.maxMinions++;
			player.GetModPlayer<ArmorSetPlayer>().EverbloomSet = true;
		}

		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Materials.BloomRose>(), 2);
			recipe.AddIngredient(ModContent.ItemType<Materials.Leaf>(), 6);
			recipe.AddIngredient(ItemID.Wood, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}