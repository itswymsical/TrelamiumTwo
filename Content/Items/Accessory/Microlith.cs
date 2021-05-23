using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
namespace TrelamiumTwo.Content.Items.Accessory
{
	public class Microlith : TrelamiumItem
	{
        public float MoveTimer;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Microlith");
			Tooltip.SetDefault("Grants the player the chance to gain more ore from mining");
		}
		public override void SetDefaults()
		{
			item.value = Item.sellPrice(gold: 5);
			item.rare = ItemRarityID.Blue;
			item.width = 24;
			item.height = 30;
			item.accessory = true;
		}
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
			MoveTimer++;
			gravity *= (float)Math.Sin(MoveTimer * 0.002f);
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<Common.Players.TrelamiumPlayer>().microlith = true;
        }
    }
}