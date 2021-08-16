﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Consumable.Food
{
	public class Apple : ModItem
	{
		public override string Texture => Assets.Items.Food + "Apple";
		public override void SetDefaults()
		{
			Item.width = Item.height = 20;
			Item.maxStack = 30;
			Item.rare = ItemRarityID.White;
			Item.value = Item.buyPrice(copper: 15);

			Item.useTime = Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.EatFood;

			Item.useTurn = true;
			Item.consumable = true;

			Item.healLife = 35;
			Item.potion = true;

			Item.UseSound = SoundID.Item3;
		}
	}
}
