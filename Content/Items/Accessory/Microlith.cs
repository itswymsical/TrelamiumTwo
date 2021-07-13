using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TrelamiumTwo.Core;

namespace TrelamiumTwo.Content.Items.Accessory
{
	public class Microlith : ModItem
	{
		public override string Texture => Assets.Items.Accessory + "Microlith";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Microlith");
			Tooltip.SetDefault("Grants the player the chance to gain more ore from mining");
		}
		public override void SetDefaults()
		{
			item.value = Item.sellPrice(gold: 2);
			item.rare = ItemRarityID.Blue;
			item.width = 24;
			item.height = 30;
			item.accessory = true;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<Common.Players.TrelamiumPlayer>().microlith = true;
        }
    }
}