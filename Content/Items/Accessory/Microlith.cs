using System;
using Terraria;
using Terraria.ID;
namespace TrelamiumTwo.Content.Items.Accessories
{
	public class Microlith : TrelamiumItem
	{
        public float MoveTimer;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Microlith");
			Tooltip.SetDefault("Grants the player the chance to gain more ore from mining");
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.value = Item.buyPrice(0, 10, 0, 0);
			item.rare = ItemRarityID.Blue;
			item.width = 24;
			item.height = 30;
			item.accessory = true;
		}
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
			if (++MoveTimer >= 200f)
				MoveTimer = 0f;
			item.velocity.Y += (float)Math.Sin(MoveTimer * 0.05f);
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<Common.Players.TrelamiumPlayer>().microlith = true;
        }
    }
}