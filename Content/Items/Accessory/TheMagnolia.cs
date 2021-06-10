using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class TheMagnolia : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases movement speed and life regenereation."
				+ "\nIncreases movement speed and life regeneration even further when at 30% health");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(0, 0, 30, 0);
            
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<Common.Players.TrelamiumPlayer>().theMagnolia = true;
            
			player.lifeRegen += 3;
			player.moveSpeed += 0.05f;
			player.maxRunSpeed += 0.05f;
            
			if (player.statLife <= player.statLifeMax2 / 3)
			{
				player.lifeRegen += 6;
				player.moveSpeed += 0.1f;
				player.maxRunSpeed += 0.1f;
			}
		}
	}
}
