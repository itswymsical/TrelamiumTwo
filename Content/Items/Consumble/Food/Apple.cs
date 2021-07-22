using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Consumable.Food
{
	public class Apple : ModItem
	{
		public override string Texture => Assets.Items.Food + "Apple";
		public override void SetDefaults()
		{
			item.width = item.height = 20;
			item.maxStack = 30;
			item.rare = ItemRarityID.White;
			item.value = Item.buyPrice(copper: 15);

			item.useTime = item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.EatingUsing;

			item.useTurn = true;
			item.consumable = true;

			item.healLife = 35;
			item.potion = true;

			item.UseSound = SoundID.Item3;
		}
	}
}
